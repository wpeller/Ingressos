using Abp.UI;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico
{
    public class AlunoServicoAppService : IAlunoServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private readonly IUnidadeServicoAppService _unidadeServicoAppService;

        public AlunoServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver, IUnidadeServicoAppService unidadeServicoAppService)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
            _unidadeServicoAppService = unidadeServicoAppService;
        }

        private HttpClientApiRequestInput<object> ConfiguracaoServico(string _urlMetodo, ExpandoObject _input = null)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = _urlMetodo
            };
            if (_input != null)
            {
                httpConfig.InputRequest = _input;
            }
            return httpConfig;
        }

        private async Task<T> ExecutarServico<T>(string _urlMetodo, ExpandoObject _input = null)
        {
            try
            {
                var httpConfig = ConfiguracaoServico(_urlMetodo, _input);
                var output = await _httpClientApiRequest.SendAsync<object, T>(httpConfig);
                return output;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message, ex);
            }
        }

        public async Task<OutputRetornoAlunoDto> ObterAlunosPor(FiltroAlunoInput filtro)
        {
            dynamic input = new ExpandoObject();
            if (!string.IsNullOrEmpty(filtro.Mnemonico))
            {
                if (filtro.ListaUnidades == null || filtro.ListaUnidades.Count <= 0)
                {
                    var unidades = _unidadeServicoAppService.TrataUnidades(filtro.Mnemonico);
                    filtro.ListaUnidades = unidades.Result;
                }
            }
            input.filtro = filtro;
            //var urlMetodo = "AlunoServico.asmx/ObterAlunosPor";
            //return await ExecutarServico<OutputRetornoAlunoDto>(urlMetodo, input);

            var urlMetodo = "AlunoServico.asmx/ObterAlunosPor";

            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = urlMetodo
            };

            var output = await _httpClientApiRequest.SendAsync<object, OutputRetornoAlunoDto>(httpConfig);
            return output;
        }
    }
}
