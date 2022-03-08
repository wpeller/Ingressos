using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Localizacao
{
    public class LocalizacaoServicoAppService : ILocalizacaoServicoAppService
    {
        private readonly IHttpClientApiRequest _httpClientApiRequest;
        private readonly ConfigurationResolver _configurationResolver;
        public LocalizacaoServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
        {
            _httpClientApiRequest = httpClientApiRequest;
            _configurationResolver = configurationResolver;
        }

        public async Task<ValidarGlobalizacaoDto> ValidarGlobalizacao(ValidarGlobalizacaoDto _validar)
        {
            var config = _configurationResolver.Get("Servicos", "Administracao");

            var dic = new Dictionary<string, object>();
            dic.Add("_validar", _validar);

            var httpConfig = new HttpClientApiRequestInput<ValidarGlobalizacaoDto>()
            {
                Method = new HttpClientApiMethod("POST"),
                ApplicationName = "BoundarieAdmnistracao",
                Url = config.Url,
                InputRequesDictionary = dic,
                ApiService = $"Webservices/LocalizacaoServico.asmx/ValidarGlobalizacao"
            };

            var output = await _httpClientApiRequest.SendAsync<ValidarGlobalizacaoDto, ValidarGlobalizacaoDto>(httpConfig);
            return output;
        }
    }
}
