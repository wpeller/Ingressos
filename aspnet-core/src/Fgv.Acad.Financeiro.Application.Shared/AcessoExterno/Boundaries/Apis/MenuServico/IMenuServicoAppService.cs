using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico
{
	public interface IMenuServicoAppService : IApplicationService
	{
		Task<List<Navigations.NavigationDto>> ObterItensDeMenuPorUsuarioEPapel(ObterItensDeMenuPorUsuarioEPapelInput input);
		Task<ItemMenuDto> ObterItemDeMenuPorId(ObterItemDeMenuPorIdInput input);
		Task AdicionarFavorito(AdicionarFavoritoInput input);
		Task RegistrarAcessoRecurso(RegistrarAcessoRecursoInput input);
		Task RemoverFavorito(RemoverFavoritoInput input);
		Task<Navigations.NavigationDto> ObterItemMenu(ObterItemMenuInput input);
		Task<List<ItemMenuDto>> ObterItensDeMenuRotas(ObterItensDeMenuPorUsuarioEPapelInput input);
		Task<List<ItemMenuDto>> ObterItensDeMenu(ObterItensDeMenuPorUsuarioEPapelInput input);
		void GravarLogDeAcesso(string usuario, string nomePapel, string descricaoMenu, string url);
	}
}
