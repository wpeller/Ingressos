using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica
{
    public class AlunoServicoAppServices : IAlunoServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private HttpClientConfigurationResolverOutput _httpClientConfigurationResolver;

        public AlunoServicoAppServices(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
            _httpClientConfigurationResolver = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "LyceumAPI");
        }

        public async Task<OutputListaAlunosAptosParaTrancamentoDisciplinaDto> ListarAlunosAptosParaTrancamentoDisciplina(InputListarAlunosAptosParaTrancamentoDisciplinaDto input)
        {
            var httpConfig = new HttpClientApiRequestInput<InputListarAlunosAptosParaTrancamentoDisciplinaDto>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieLyceum",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/Aluno/ListarAlunosAptosParaTrancamentoDisciplina"
            };

            var output = await _httpClientApiRequest.SendAsync<InputListarAlunosAptosParaTrancamentoDisciplinaDto, OutputListaAlunosAptosParaTrancamentoDisciplinaDto>(httpConfig);

            return output;
        }
    }
}
