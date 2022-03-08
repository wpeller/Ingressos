using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;
using Fgv.Acad.Financeiro.Navigations;
using System.Linq;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LogIDE.Dtos;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.LogIDE;
using Abp.Auditing;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico
{
    public class MenuServicoAppService : IMenuServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        private HttpClientConfigurationResolverOutput _httpClientConfigurationResolver;
        private readonly ILogApiIDEService _logApiIdeService;
        private readonly IClientInfoProvider _clientInfoProvider;

        public MenuServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver, IClientInfoProvider clientInfoProvider, ILogApiIDEService logApiIdeService)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
            _logApiIdeService = logApiIdeService;
            _clientInfoProvider = clientInfoProvider;
            _httpClientConfigurationResolver =
                _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
        }


        public async Task<List<NavigationDto>> ObterItensDeMenuPorUsuarioEPapel(ObterItensDeMenuPorUsuarioEPapelInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<ObterItensDeMenuPorUsuarioEPapelInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/ObterItensDeMenuPorUsuarioEPapel"
            };

            var output =
                await _httpClientApiRequest.SendAsync<ObterItensDeMenuPorUsuarioEPapelInput, List<NavigationDto>>(
                    httpConfig);

            return output;
        }

        public async Task<ItemMenuDto> ObterItemDeMenuPorId(ObterItemDeMenuPorIdInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<ObterItemDeMenuPorIdInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/ObterItemDeMenuPorId"
            };

            var output =
                await _httpClientApiRequest.SendAsync<ObterItemDeMenuPorIdInput, ItemMenuDto>(
                    httpConfig);

            return output;
        }

        public async Task AdicionarFavorito(AdicionarFavoritoInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<AdicionarFavoritoInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/AdicionarFavorito"
            };

            var output =
                await _httpClientApiRequest.SendAsync<AdicionarFavoritoInput>(
                    httpConfig);
        }

        public async Task RegistrarAcessoRecurso(RegistrarAcessoRecursoInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<RegistrarAcessoRecursoInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/RegistrarAcessoRecurso"
            };

            var output =
                await _httpClientApiRequest.SendAsync<RegistrarAcessoRecursoInput>(
                    httpConfig);
        }

        public async Task RemoverFavorito(RemoverFavoritoInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<RemoverFavoritoInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("Delete"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/RemoverFavorito"
            };

            var output =
                await _httpClientApiRequest.SendAsync<RemoverFavoritoInput>(
                    httpConfig);
        }

        public async Task<NavigationDto> ObterItemMenu(ObterItemMenuInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<ObterItemMenuInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/ObterItemMenu"
            };

            var output =
                await _httpClientApiRequest.SendAsync<ObterItemMenuInput, NavigationDto>(
                    httpConfig);
            return output;
        }

        public async Task<List<ItemMenuDto>> ObterItensDeMenu(ObterItensDeMenuPorUsuarioEPapelInput input)
        {
            var httpConfig = new HttpClientApiRequestInput<ObterItensDeMenuPorUsuarioEPapelInput>()
            {
                InputRequest = input,
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = _httpClientConfigurationResolver.Url,
                ApiUserName = _httpClientConfigurationResolver.UserName,
                ApiUserNamePassword = _httpClientConfigurationResolver.Password,
                ApiService = "api/services/app/S2Menu/ObterItensDeMenu"
            };

            var output =
                await _httpClientApiRequest.SendAsync<ObterItensDeMenuPorUsuarioEPapelInput, List<ItemMenuDto>>(
                    httpConfig);

            return output;
        }

        public async Task<List<ItemMenuDto>> ObterItensDeMenuRotas(ObterItensDeMenuPorUsuarioEPapelInput input)
        {
            var menusExternos = new List<ItemMenuDto>();
            var itens = await ObterItensDeMenu(input);
            var filhos = itens.Select(x => x.Filhos).ToList();

            foreach (var filho in filhos)
            {
                if (filho.Count > 0)
                {
                    ListarItemMenuFilhos(filho, ref menusExternos);
                }
            }

            return menusExternos;
        }

        private IEnumerable<ItemMenuDto> ListarItemMenuFilhos(List<ItemMenuDto> itemMenu, ref List<ItemMenuDto> menusExternos)
        {
            foreach (var item in itemMenu)
            {
                if (item.Filhos.Count > 0)
                {
                    ListarItemMenuFilhos(item.Filhos, ref menusExternos);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(item.Url))
                        menusExternos.Add(item);
                }
            }

            return menusExternos;
        }

        public void GravarLogDeAcesso(string usuario, string nomePapel, string descricaoMenu, string url)
        {
            var logAcesso = new InputRegistraLogAcesso()
            {
                dataHoraAcao = DateTime.Now,
                ipAcesso = _clientInfoProvider.ClientIpAddress,
                menu = descricaoMenu,
                nomePapel = nomePapel,
                servidor = Environment.MachineName,
                sistema = "SIGA2",
                url = url,
                usuarioLogado = usuario
            };

            Task.Run(() => _logApiIdeService.RegistraLogAcesso(logAcesso));
        }
    }
}
