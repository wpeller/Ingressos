using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma
{
    public class TurmaServicoAppService : ITurmaServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private HttpClientConfigurationResolverOutput _httpClientConfigurationResolver;

        public TurmaServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
            _httpClientConfigurationResolver = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "LyceumAPI");
        }

        public async Task<OutputListaTurmasAptasParaTrancamentoDisciplinaDto> ListarTurmasAptasParaTrancamentoDisciplina(InputListarTurmasAptasParaTrancamentoDisciplinaDto input)
        {
            var httpConfig = new HttpClientApiRequestInput<InputListarTurmasAptasParaTrancamentoDisciplinaDto>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieLyceum",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/Turma/ListarTurmasAptasParaTrancamentoDisciplina"
            };

            var output = await _httpClientApiRequest.SendAsync<InputListarTurmasAptasParaTrancamentoDisciplinaDto, OutputListaTurmasAptasParaTrancamentoDisciplinaDto>(httpConfig);

            return output;
        }

        public async Task<string> ValidarSeListaTurmaDisciplinaEhRetroativa(List<string> listaDeTurmas, string codigoDisciplina)
        {

            var msgRetorno = string.Empty;
            var turmas = new List<string>();
            var paginacaoDto = new PaginacaoDto()
            {
                inicio = 0,
                total = 0
            };

            if (listaDeTurmas != null && listaDeTurmas.Count > 0)
            {
                foreach (var turma in listaDeTurmas)
                {
                    var input = new FiltroInfoTurmasDisciplinasDto()
                    {
                        codigoDisciplina = codigoDisciplina,
                        codigoTurma = turma,
                        paginacaoDto = paginacaoDto,
                        estrategiaListarInfoTurmasDisciplinas = EstrategiaListarInfoTurmasDisciplinas.PorTurma

                    };

                    var httpConfig = new HttpClientApiRequestInput<FiltroInfoTurmasDisciplinasDto>()
                    {
                        InputRequest = input,
                        Method = new HttpClientApiMethod("POST"),
                        ApplicationName = "BoundarieLyceum",
                        Url = _httpClientConfigurationResolver.Url,
                        ApiUserName = _httpClientConfigurationResolver.UserName,
                        ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                        ApiService = "api/services/app/Turma/ObterListaDeTurmasDisciplinas"
                    };

                    var retornoTurmas = await _httpClientApiRequest.SendAsync<FiltroInfoTurmasDisciplinasDto, List<InfoTurmaDisciplinaDto>>(httpConfig);

                    if (retornoTurmas.Where(t => t.codigoTurma == turma && t.codigoDisciplina == codigoDisciplina && t.terminoAulas.HasValue && t.terminoAulas.Value < System.DateTime.Today).Any())
                    {
                        turmas.Add(turma);
                    }
                }

                if (turmas.Count > 0)
                {
                    msgRetorno = string.Format("Não é permitido trancar turmas retroativas. A(s) turma(s): {0} precisa(m) ser desmarcada(s) pois a disciplina as ser trancadas ja foi cursada ou finalizada.", string.Join(", ", turmas));
                }
            }
            else
            {
                msgRetorno = $"Não foram informadas turmas para validar.";
            };

            return msgRetorno;
        }
    }
}
