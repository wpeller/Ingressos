using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico
{
    public class CursoServicoAppService : ICursoServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private readonly IUnidadeServicoAppService _unidadeServicoAppService;

        public CursoServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver, IUnidadeServicoAppService unidadeServicoAppService)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
            _unidadeServicoAppService = unidadeServicoAppService;
        }

        public async Task<OutputCursoDto> ObterCursoPor(InputFiltroCursoDto filtro)
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

            var urlMetodo = "CursoServico.asmx/ObterCursoPor";

            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = urlMetodo
            };

            var output = await _httpClientApiRequest.SendAsync<object, OutputCursoDto>(httpConfig);
            return output;
        }

        public async Task<OutputCurriculoCursoDto> ObterCurriculoCursoPor(InputFiltroCurriculoCursoDto filtro)
        {
            dynamic input = new ExpandoObject();

            input.filtro = filtro;

            var urlMetodo = "CursoServico.asmx/ObterCurriculoCursoPor";

            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = urlMetodo
            };

            var output = await _httpClientApiRequest.SendAsync<object, OutputCurriculoCursoDto>(httpConfig);
            return output;
        }

        public async Task<List<ProgramaCursoDto>> ObterProgramaCurso()
        {
            var urlMetodo = "CursoServico.asmx/ObterProgramaCurso";

            var config = _configurationResolver.Get("Siga2", "URLsWebServices", "Academico");
            var httpConfig = new HttpClientApiRequestInput<object>()
            {
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieS2Academico",
                Url = config.Url,
                ApiService = urlMetodo
            };

            var output = await _httpClientApiRequest.SendAsync<object, List<ProgramaCursoDto>>(httpConfig);
            return output;
        }



    }
}
