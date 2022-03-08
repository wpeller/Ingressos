using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LogIDE.Dtos;
using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.LogIDE
{
	public class LogApiIDEService : ILogApiIDEService
	{

		private readonly ConfigurationResolver _configurationResolver;

		public LogApiIDEService(ConfigurationResolver configurationResolver)
		{
			_configurationResolver = configurationResolver;
		}

		public Task RegistraLogAcesso(InputRegistraLogAcesso input)
		{
			var method = "api/Logs/api/Logs/registraAcesso";
			ExecutarHTTPClient<InputRegistraLogAcesso>(input, method);
			return Task.CompletedTask;
		}

		public Task RegistraLogLogin(InputRegistraLogLoginDto input)
		{
			var method = "api/Logs/api/Logs/registraLogin";
			ExecutarHTTPClient<InputRegistraLogLoginDto>(input, method);
			return Task.CompletedTask;
		}

		private Task ExecutarHTTPClient<TInput>(TInput input, string metodoAPI)
		{
			System.Net.ServicePointManager.SecurityProtocol =
				SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) =>
			{
				return true;
			};

			var baseURL = _configurationResolver.GetValue("Siga2", "LogAPI", "Url");

			var cliente = new HttpClient();
			cliente.DefaultRequestHeaders.Clear();
			cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			cliente.DefaultRequestHeaders.Add(_configurationResolver.GetValue("Siga2", "LogAPI", "HeaderKey"),
				_configurationResolver.GetValue("Siga2", "LogAPI", "HeaderKeyValue"));

			var message = new HttpRequestMessage()
			{
				Method = HttpMethod.Post,
				Content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json"),
				RequestUri = new Uri($"{baseURL}{metodoAPI}")
			};

			var response = cliente.SendAsync(message).Result;
			return Task.CompletedTask;
		}
	}
}
