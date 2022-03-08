using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
	public class PapelServicoAppService_Tests : AppTestBase
	{

		private readonly IPapelServicoAppService _papelServicoAppService;
		public PapelServicoAppService_Tests()
		{
			_papelServicoAppService = Resolve<IPapelServicoAppService>();
		}

		[Fact]
		public async Task deve_listar_os_papeis_do_usuario_com_sucesso()
		{
			var output = await _papelServicoAppService.ObterPapeisDoUsuarioParaMenu(new ObterPapeisDoUsuarioParaMenuInput()
			{
			   CodigoExternoUsuario = "JEANLUC"
			});

			output.Count.ShouldBeGreaterThanOrEqualTo(1);
		}
	}
}
