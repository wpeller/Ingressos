using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Timing;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico.Dto;
using Fgv.Acad.Financeiro.Auditing;
using Fgv.Acad.Financeiro.Auditing.Dto;
using Fgv.Acad.Financeiro.Authorization.Users;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
	public class UsuarioServicoAppService_Tests : AppTestBase
	{

		private readonly IUsuarioServicoAppService _usuarioServicoAppService;
		public UsuarioServicoAppService_Tests()
		{
			_usuarioServicoAppService = Resolve<IUsuarioServicoAppService>();
		}

		[Fact(Skip = "Pulando o teste por ser execução de mock. será implementando esse teste no futuro.")]
		public async Task devo_autenticar_usuario_com_sucesso()
		{
			var output = await _usuarioServicoAppService.Autenticar(new AutenticarInput()
			{
			   Usuario = "JEANLUC",
			   Senha = "a"
			});

			output.Status.ShouldBe("Autenticado", "Erro ao autenticar o usuário");
		}

		[Fact(Skip = "Pulando o teste por ser execução de mock. será implementando esse teste no futuro.")]
		public async Task devo_obter_dados_do_usuario_por_codigoExterno_com_sucesso()
		{
			var output = await _usuarioServicoAppService.ObterUsuarioPorCodigoExterno("JEANLUC");
			output.ShouldNotBeNull();

		}

        [Fact(Skip = "Pulando o teste por ser execução de mock. será implementando esse teste no futuro.")]
        public async Task devo_obter_acesso_com_sucesso()
        {
            ValidaUsuarioTemPermissaoDto input = new ValidaUsuarioTemPermissaoDto();
            input.Rota = "app/administracao/aviso/cadastro";
            input.PapelId = 799; //S2 - Caps
            input.UsuarioCodigoExterno = "jeanluc";
            var output = await _usuarioServicoAppService.ValidaUsuarioTemPermissao(input);
            output.ShouldNotBeNull();
            output.Sucesso.ShouldNotBeNull();
            output.Sucesso.ShouldBeTrue();
        }
    }
}
