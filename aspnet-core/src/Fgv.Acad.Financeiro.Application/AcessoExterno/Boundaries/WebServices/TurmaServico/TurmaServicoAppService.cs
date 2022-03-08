using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico
{
    public class TurmaServicoAppService : ITurmaServicoAppService
	{
		private readonly IHttpClientApiRequest _httpClientApiRequest;
		private readonly ConfigurationResolver _configurationResolver;
        private readonly IUnidadeServicoAppService _unidadeServicoAppService;

		public TurmaServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver, IUnidadeServicoAppService unidadeServicoAppService)
		{
			_httpClientApiRequest = httpClientApiRequest;
			_configurationResolver = configurationResolver;
			_unidadeServicoAppService = unidadeServicoAppService;
		}

        public async Task<OutputRetornoTurmaDto> ObterTurmaPor(FiltroTurmaInput filtro)
        {
            dynamic input = new ExpandoObject();

            if (!string.IsNullOrEmpty(filtro.Mnemonico))
            {
                if (filtro.ListaDeUnidades == null || filtro.ListaDeUnidades.Count <= 0)
                {
                    var unidades = _unidadeServicoAppService.TrataUnidades(filtro.Mnemonico);
                    filtro.ListaDeUnidades = unidades.Result;
                }
            }

            input.filtro = filtro;

            var urlMetodo = "TurmaServico.asmx/BuscarTurma";

            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = urlMetodo
            };

            var output = await _httpClientApiRequest.SendAsync<object, OutputRetornoTurmaDto>(httpConfig);
            return output;
        }
    }
}