using Abp.UI;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico.Dto;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico
{
    public class UnidadeServicoAppService : IUnidadeServicoAppService
	{
		private readonly IHttpClientApiRequest _httpClientApiRequest;
		private readonly ConfigurationResolver _configurationResolver;

		public UnidadeServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
		{
			_httpClientApiRequest = httpClientApiRequest;
			_configurationResolver = configurationResolver;
		}

        private HttpClientApiRequestInput<object> ConfiguracaoServico(string _urlMetodo, ExpandoObject _input = null)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Unidade");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Unidade",
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

        public async Task<OutputUnidadeEnsinoDto> ObterUnidadesPor(FiltroUnidadeInput filtro)
        {
            dynamic input = new ExpandoObject();
            input.filtro = filtro;
            var urlMetodo = "UnidadeServico.asmx/ObterUnidadesPor";
            return await ExecutarServico<OutputUnidadeEnsinoDto>(urlMetodo, input);
        }

        public async Task<OutputPapelPorTipoDto> BuscarPapelPorTipo(string mnemonico)
        {
            dynamic input = new ExpandoObject();
            input.mnemonico = mnemonico;
            var urlMetodo = "UnidadeServico.asmx/BuscarPapelPorTipo";
            OutputPapelPorTipoDto retorno =  await ExecutarServico<OutputPapelPorTipoDto>(urlMetodo, input);
            if(retorno.CodigoUnidadePapel == "FGVONLINE")
			{
                retorno.CodigoUnidadePapel = string.Empty;
                retorno.NomeUnidadePapel = string.Empty;

            }

            return retorno;
        }

        public async Task<List<string>> TrataUnidades(string mnemonico)
        {
            var retorno = new List<string>();

            var configuracaoPapel = await BuscarPapelPorTipo(mnemonico);

            var unidadesDoPapel = ObterUnidadesPor(new FiltroUnidadeInput()
            {
                PapelEhAcademicoOuFinanceiro = configuracaoPapel.PapelEhAcademicoOuFinanceiro,
                PapelEhAuditoria = configuracaoPapel.PapelEhAuditoria,
                PapelEhControladoriaOuCaps = configuracaoPapel.PapelEhControladoriaOuCaps,
                PapelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline = configuracaoPapel.PapelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline,
                PapelEhSuperintendenciaNucleo = configuracaoPapel.PapelEhSuperintendenciaNucleo,
                PapelEhSuperintendenciaNucleoRJ = configuracaoPapel.PapelEhSuperintendenciaNucleoRJ,
                PapelEhSuperintendenciaNucleoSP = configuracaoPapel.PapelEhSuperintendenciaNucleoSP,
                PapelEhSuperintendenciaNucleoBR = configuracaoPapel.PapelEhSuperintendenciaNucleoBR,
                PapelEhSuperintendenciaNucleoBH = configuracaoPapel.PapelEhSuperintendenciaNucleoBH,
                PapelEhSuperintendenciaPEC = configuracaoPapel.PapelEhSuperintendenciaPEC,
                PapelEhSuperintendenciaRede = configuracaoPapel.PapelEhSuperintendenciaRede,
                PapelEhSuperintendenteRedeMGM = configuracaoPapel.PapelEhSuperintendenteRedeMGM,
                CodigoExterno = configuracaoPapel.CodigoExterno,
                CodigoUnidadePapel = configuracaoPapel.CodigoUnidadePapel,
                NomeUnidade = configuracaoPapel.NomeUnidadePapel,
                Order = "Codigo ASC",
                Skip = 0,
                RegistrosPorPagina = 10000
            }).Result;

            if (unidadesDoPapel != null && unidadesDoPapel.ListaDeUnidadesDeEnsino != null && unidadesDoPapel.ListaDeUnidadesDeEnsino.Count > 0)
            {
                retorno = new List<string>();
                foreach (var item in unidadesDoPapel.ListaDeUnidadesDeEnsino)
                {
                    retorno.Add(item.Codigo);
                }
            }

            return retorno;
        }
    }
}
