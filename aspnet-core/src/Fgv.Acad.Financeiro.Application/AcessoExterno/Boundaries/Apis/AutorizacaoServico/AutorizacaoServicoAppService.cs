using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico
{
    public class AutorizacaoServicoAppService : IAutorizacaoServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        public AutorizacaoServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
        }

        public async Task<FuncionalidadesPermissoes> ObterPermissoes(AutorizacaoInput input)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
            var httpConfig = new HttpClientApiRequestInput<AutorizacaoInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                ApiUserName = config.UserName,
                ApiUserNamePassword = config.Password,
                ApiService = "api/services/app/AuthorizationSigaServico/ObterPermissoes"
            };

            var output = await _httpClientApiRequest.SendAsync<AutorizacaoInput, FuncionalidadesPermissoes>(httpConfig);
            return output;
        }
    }
}