using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Tic.ApiConnectorCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis
{
    public class HttpClientApiRequest : IHttpClientApiRequest
    {
        private HttpClient _connector;
        private List<HttpClientApiApplication> _httpClientApiApplications;
        private readonly object lockProperty = new object();
        private readonly IAppConfigurationAccessor _configuration;
        private readonly string DefaultUrlLoginApiCore = "api/TokenAuth/Authenticate";

        public HttpClientApiRequest(IAppConfigurationAccessor configuration)
        {
            _configuration = configuration;
            if (_httpClientApiApplications == null) _httpClientApiApplications = new List<HttpClientApiApplication>();
        }

        public async Task<T> SendAsync<TInput, T>(HttpClientApiRequestInput<TInput> input)
        {
            return await ExecutarAsync<TInput, T>(input);
        }
        public T Send<TInput, T>(HttpClientApiRequestInput<TInput> input)
        {
            return ExecutarAsync<TInput, T>(input).GetAwaiter().GetResult();
        }

        public async Task<JObject> SendAsync<TInput>(HttpClientApiRequestInput<TInput> input)
        {
            return await ExecutarAsync(input);
        }

        public JObject Send<TInput>(HttpClientApiRequestInput<TInput> input)
        {
            return ExecutarAsync(input).GetAwaiter().GetResult();
        }

        private async Task<T> ExecutarAsync<TInput, T>(HttpClientApiRequestInput<TInput> input)
        {
            var application = GetIntanceApplication<TInput, T>(input);

            if (application.DataExpiracaoToken < DateTime.Now.AddMinutes(-30))
            {
                GetToken(input, application);
            }

            var request = await ProcessarRequisicao<TInput, T>(input);

            var result = request.Content.ReadAsStringAsync().Result;
            var parse = JObject.Parse(result);

            if (parse.ContainsKey("success") && !parse["success"].ToObject<bool>())
            {
                if (parse["error"]["message"].ToString().Contains("Current user did not login to the application"))
                {
                    _httpClientApiApplications.Remove(application);
                    return await ExecutarAsync<TInput, T>(input);
                }
                else
                {
                    throw new UserFriendlyException(parse["error"]["message"].ToString());
                }
            }

            if (parse.ContainsKey("d"))
            {
                return parse["d"].ToObject<T>(StartupConfig.JsonSerializer);
            }
            return parse["result"].ToObject<T>(StartupConfig.JsonSerializer);

        }

        private async Task<JObject> ExecutarAsync<TInput>(HttpClientApiRequestInput<TInput> input)
        {
            var application = GetIntanceApplication<TInput, JObject>(input);
            if (application.DataExpiracaoToken < DateTime.Now.AddMinutes(-30))
            {
                GetToken(input, application);
            }
            var request = await ProcessarRequisicao<TInput, JObject>(input);
            var result = request.Content.ReadAsStringAsync().Result;
            var parse = JObject.Parse(result);

            if (parse.ContainsKey("success") && !parse["success"].ToObject<bool>())
            {
                if (parse["error"]["message"].ToString().Contains("Current user did not login to the application"))
                {
                    _httpClientApiApplications.Remove(application);
                    return await ExecutarAsync<TInput>(input);
                }
                else
                {
                    throw new UserFriendlyException(parse["error"]["message"].ToString());
                }
            }

            return parse;
        }

        private async Task<HttpResponseMessage> ProcessarRequisicao<TInput, T>(HttpClientApiRequestInput<TInput> input)
        {
            System.Net.ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };
            if (input.UseSiga2Key)
            {
                _connector.DefaultRequestHeaders.Add("ApiKey", _configuration.Configuration["App.Siga2WebServices.ApiKey"]);
            }

            var json = getStringContent(input);

            var httpMessage = new HttpRequestMessage()
            {
                Method = input.Method,
                RequestUri = getUriTratada(input.Url, input.ApiService, input.Method, json),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage request = await _connector.SendAsync(httpMessage, HttpCompletionOption.ResponseContentRead);
            return request;
        }

        private Uri getUriTratada(string inputUrl, string inputApiService, HttpClientApiMethod inputMethod, string json)
        {
            var url = inputUrl.Trim();

            if (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }

            if (inputApiService.StartsWith("/")) inputApiService = inputApiService.Remove(0, 1);

            var query = string.Empty;
            if (inputMethod == HttpMethod.Delete || inputMethod == HttpMethod.Get)
            {
                query = $"?{json.Replace("{", "").Replace("\"", "").Replace(",", "&").Replace(":", "=").Replace("}", "")}";
            }
            return new Uri($"{url}/{inputApiService.Trim()}{query}");
        }

        private string getStringContent<TInput>(HttpClientApiRequestInput<TInput> input)
        {
            string objJson = string.Empty;

            if (input.InputRequesDictionary != null && input.InputRequesDictionary.Count > 0)
            {
                foreach (var item in input.InputRequesDictionary)
                {
                    var content = new StringBuilder("{");
                    content.Append($"'{item.Key}': {JsonConvert.SerializeObject(item.Value)}");
                    content.Append("}");
                    if (!string.IsNullOrEmpty(objJson)) objJson += ",";
                    objJson += content.ToString();
                }
            }
            else
            {
                if (input.InputRequest != null) objJson = JsonConvert.SerializeObject(input.InputRequest);
            }

            return objJson;
        }

        private HttpClientApiApplication GetIntanceApplication<TInput, T>(HttpClientApiRequestInput<TInput> input)
        {
            var application = _httpClientApiApplications.FirstOrDefault(p => p.Name == input.ApplicationName);
            if (application == null)
            {
                application = new HttpClientApiApplication()
                {
                    Name = input.ApplicationName,
                    UserName = input.ApiUserName,
                    Password = input.ApiUserNamePassword
                };

                _connector = new HttpClient();
                _connector.DefaultRequestHeaders.Accept.Clear();
                GetToken(input, application);
                application.Connector = _connector;
                _httpClientApiApplications.Add(application);
                return application;
            }
            _connector = (HttpClient)application.Connector;

            return application;
        }

        private void GetToken<TInput>(HttpClientApiRequestInput<TInput> input, HttpClientApiApplication application)
        {
            if (string.IsNullOrEmpty(input.ApiUserName) || string.IsNullOrEmpty(input.ApiUserNamePassword)) return;
            dynamic inputLogin = new ExpandoObject();
            inputLogin.userNameOrEmailAddress = input.ApiUserName;
            inputLogin.password = input.ApiUserNamePassword;
            var jsonString = JsonConvert.SerializeObject(inputLogin);
            var result = _connector.SendAsync(
                    new HttpRequestMessage()
                    {
                        Method = HttpMethod.Post,
                        Content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json"),
                        RequestUri = new Uri($"{input.Url}/{DefaultUrlLoginApiCore}")
                    }, HttpCompletionOption.ResponseContentRead)
                .GetAwaiter()
                .GetResult()
                .Content
                .ReadAsStringAsync()
                .Result;

            var parse = JObject.Parse(result);
            if (parse["success"].ToObject<bool>())
            {
                _connector.DefaultRequestHeaders.Clear();
                //_connector.DefaultRequestHeaders.Add("Authorization", $"Bearer {parse["result"]["accessToken"].ToString()}");
                _connector.SetBearerToken(parse["result"]["accessToken"].ToString());
                application.DataExpiracaoToken = DateTime.Now.AddSeconds(parse["result"]["expireInSeconds"].ToObject<long>());
            }
			else
			{
                throw new UserFriendlyException(parse["error"]["message"].ToString() + $", Não foi possivel conectar com o servidor {input.Url}");
            }
        }

    }
}
