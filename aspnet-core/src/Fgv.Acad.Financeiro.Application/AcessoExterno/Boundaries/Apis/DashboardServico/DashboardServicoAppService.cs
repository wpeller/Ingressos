using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto;
using Fgv.Acad.Financeiro.Navigations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico
{
	public class DashboardServicoAppService : IDashboardServicoAppService
	{
		private readonly IHttpClientApiRequest _httpClientApiRequest;
		private readonly ConfigurationResolver _configurationResolver;
		private readonly HttpClientConfigurationResolverOutput _httpClientConfigurationResolver;

		public DashboardServicoAppService(IHttpClientApiRequest httpClientApiRequest, ConfigurationResolver configurationResolver)
		{
			_httpClientApiRequest = httpClientApiRequest;
			_configurationResolver = configurationResolver;
			_httpClientConfigurationResolver =
				_configurationResolver.Get("Siga2", "URLsWebApiBoundaries", "Administracao");
		}
   
		public async Task<List<CategoriaDocumentoSigaDto>> ObterCategoriasDeDocumentos()
		{
			var httpConfig = new HttpClientApiRequestInput<object>()
			{
				InputRequest = new object(),
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = "api/services/app/Dashboard/ObterCategoriasDeDocumentos"
			};

			var output = await _httpClientApiRequest.SendAsync<object, List<CategoriaDocumentoSigaDto>>(httpConfig);
			return output;
		}

		public async Task<List<AvisoDto>> ObterAvisosDoPapel(PapelDto papel)
		{
			var httpConfig = new HttpClientApiRequestInput<PapelDto>()
			{
				InputRequest = papel,
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = "api/services/app/Dashboard/ObterAvisosDoPapel"
			};

			var output = await _httpClientApiRequest.SendAsync<PapelDto, List<AvisoDto>>(httpConfig);
			return output;
		}

		public async Task<List<NavigationDto>> ObterRecursosFavoritos(string codigoExternoUsuario, PapelDto papel)
		{
			var httpConfig = new HttpClientApiRequestInput<PapelDto>()
			{
				InputRequest = papel,
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = $"api/services/app/Dashboard/ObterRecursosFavoritos?codigoExternoUsuario={codigoExternoUsuario}"
			};

			var output = await _httpClientApiRequest.SendAsync<PapelDto, List<NavigationDto>>(httpConfig);
			return output;
		}

		public async Task<List<AcessoRecursoDto>> ObterMaisVisitados(string codigoExternoUsuario, PapelDto papel)
		{
			var httpConfig = new HttpClientApiRequestInput<PapelDto>()
			{
				InputRequest = papel,
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = $"api/services/app/Dashboard/ObterMaisVisitados?codigoExternoUsuario={codigoExternoUsuario}"
			};

			var output = await _httpClientApiRequest.SendAsync<PapelDto, List<AcessoRecursoDto>>(httpConfig);
			return output;
		}

		public async Task<byte[]> DownloadDocumento(DocumentoSigaDto input)
		{
			var httpConfig = new HttpClientApiRequestInput<DocumentoSigaDto>()
			{
				InputRequest = input,
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = "api/services/app/AdmDocumentoSigaServico/Download"
			};

			var output = await _httpClientApiRequest.SendAsync<DocumentoSigaDto, byte[]>(httpConfig);
			return output;
		}

		public async Task<List<DocumentoSigaDto>> ObterListaDeDocumentosPorCategoria(long idCategoria)
		{
			var httpConfig = new HttpClientApiRequestInput<object>()
			{
				InputRequest = new object(),
				Method = new HttpClientApiMethod("POST"),
				ApplicationName = "BoundarieAdmnistracao",
				Url = _httpClientConfigurationResolver.Url,
				ApiUserName = _httpClientConfigurationResolver.UserName,
				ApiUserNamePassword = _httpClientConfigurationResolver.Password,
				ApiService = $"api/services/app/AdmDocumentoSigaServico/ObterListaPorCategoria?categoriaId={idCategoria}"
			};

			var output = await _httpClientApiRequest.SendAsync<object, List<DocumentoSigaDto>>(httpConfig);
			return output;
		}
	}
}
