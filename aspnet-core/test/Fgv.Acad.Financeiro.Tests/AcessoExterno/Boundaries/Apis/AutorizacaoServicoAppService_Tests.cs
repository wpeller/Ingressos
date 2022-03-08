using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico.Dto;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
    public class AutorizacaoServicoAppService_Tests : AppTestBase
    {
        private readonly IAutorizacaoServicoAppService _autorizacaoServicoAppService;
        public AutorizacaoServicoAppService_Tests()
        {
            _autorizacaoServicoAppService = Resolve<IAutorizacaoServicoAppService>();
        }

       [Fact(Skip = "Pulando o teste por ser execução de mock. será implementando esse teste no futuro.")]
        public async Task devo_obter_lista_de_permissoes_com_sucesso()
        {
            AutorizacaoInput input = new AutorizacaoInput();
            input.itemMenuId = 5214; //Cadastro de aviso
            input.papelId = 799; //S2 - Caps
            input.codigoExterno = "jeanluc";
            var output = await _autorizacaoServicoAppService.ObterPermissoes(input);
            output.ShouldNotBeNull();
            output.Recurso.ShouldNotBeNull();
            output.Permissoes.ShouldNotBeNull();
            output.Permissoes.Count.ShouldBeGreaterThan(0);
        }
    }
}
