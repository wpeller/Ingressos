using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro
{
    public class FinanceiroAppService : IFinanceiroAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private HttpClientConfigurationResolverOutput _httpClientConfigurationResolver;

        public FinanceiroAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _configurationResolver = configurationResolver;
            _httpClientApiRequest = httpClientApiRequest;
            _httpClientConfigurationResolver = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "LyceumAPI");
        }

        public async Task<OutputPlanoComExcessoDeParcelamentoDto> ObterAlunosComExcessoDeParcelamento(FiltroPlanoComExcessoDeParcelamentoDto input)
        {
            var httpConfig = new HttpClientApiRequestInput<FiltroPlanoComExcessoDeParcelamentoDto>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieLyceum",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "/api/services/app/Financeiro/ObterAlunosComExcessoDeParcelamento"
            };

            var output = await _httpClientApiRequest.SendAsync<FiltroPlanoComExcessoDeParcelamentoDto, OutputPlanoComExcessoDeParcelamentoDto>(httpConfig);

            return output;
        }
    }
}
