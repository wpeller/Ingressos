using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico
{
	public class PapelServicoAppService : IPapelServicoAppService
	{
		private readonly IHttpClientApiRequest _httpClientApiRequest;
		private readonly ConfigurationResolver _configurationResolver;

		public PapelServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
		{
			_httpClientApiRequest = httpClientApiRequest;
			_configurationResolver = configurationResolver;
		}

		public async Task<List<PapelDto>> ObterPapeisDoUsuarioParaMenu(ObterPapeisDoUsuarioParaMenuInput input)
		{
			var config = _configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
			var httpConfig = new HttpClientApiRequestInput<ObterPapeisDoUsuarioParaMenuInput>()
			{
				InputRequest = input,
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = config.Url,
				ApiService = "api/services/app/S2Papel/ObterPapeisDoUsuarioParaMenu"
			};

			var output =
				await _httpClientApiRequest.SendAsync<ObterPapeisDoUsuarioParaMenuInput, List<PapelDto>>(httpConfig);
			return output;
		}


	}
}
