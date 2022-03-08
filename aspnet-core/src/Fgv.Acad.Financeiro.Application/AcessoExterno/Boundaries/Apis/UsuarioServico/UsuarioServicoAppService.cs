using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico
{
    public class UsuarioServicoAppService : IUsuarioServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        public UsuarioServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
        }

        public async Task<RetornoOutputDto> AlterarSenhaUsuarioLogado(TrocaDeSenhaInput input)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
            var httpConfig = new HttpClientApiRequestInput<TrocaDeSenhaInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                ApiUserName = config.UserName,
                ApiUserNamePassword = config.Password,
                ApiService = "api/services/app/AdmUsuarioServico/AlterarSenhaUsuarioLogado"
            };

            var output = await _httpClientApiRequest.SendAsync<TrocaDeSenhaInput, RetornoOutputDto>(httpConfig);
            return output;
        }

        public async Task<AutenticarOutput> Autenticar(AutenticarInput autenticarInput)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
            var httpConfig = new HttpClientApiRequestInput<AutenticarInput>()
            {
                InputRequest = autenticarInput,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                ApiService = "api/services/app/S2Usuario/Autenticar"
            };

            var output = await _httpClientApiRequest.SendAsync<AutenticarInput, AutenticarOutput>(httpConfig);
            return output;
        }

        public async Task<UsuarioSigaDoisDto> ObterUsuarioPorCodigoExterno(string user)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
            var httpConfig = new HttpClientApiRequestInput<string>()
            {
                InputRequest = string.Empty,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                ApiUserName = config.UserName,
                ApiUserNamePassword = config.Password,
                ApiService = $"api/services/app/AdmUsuarioServico/BuscarPorCodigoExterno?_codigoExterno={user}"
            };

            var output = await _httpClientApiRequest.SendAsync<string, UsuarioSigaDoisDto>(httpConfig);
            return output;
        }

        public async Task<RetornoOutputDto> ValidaUsuarioTemPermissao(ValidaUsuarioTemPermissaoDto _validar)
        {
            var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
            var httpConfig = new HttpClientApiRequestInput<ValidaUsuarioTemPermissaoDto>()
            {
                InputRequest = _validar,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                ApiUserName = config.UserName,
                ApiUserNamePassword = config.Password,
                ApiService = $"api/services/app/AdmUsuarioServico/ValidaUsuarioTemPermissao"
            };

            var output = await _httpClientApiRequest.SendAsync<ValidaUsuarioTemPermissaoDto, RetornoOutputDto>(httpConfig);
            return output;
        }
    }
}
