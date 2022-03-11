using System.Collections.Generic;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;
using Fgv.Acad.Financeiro.Eventos;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
	public class ClienteAppService_Tests : AppTestBase
	{

		private readonly IClienteServicoAppService _clienteServico;
		public ClienteAppService_Tests()
		{
			_clienteServico = Resolve<IClienteServicoAppService>();
		}

		[Fact]
		public async Task devo_obter_todos_eventos_ativos()
		{

			await this.devo_Cadastrar_novo_cliente();

			var output = _clienteServico.ObterTodos();


			output.Item.Count.ShouldBeGreaterThanOrEqualTo(1);
		}

		[Fact]
		public async Task devo_obter_evento_por_id()
		{
			await devo_Cadastrar_novo_cliente();

			var output = _clienteServico.ObterPorCpf  ("065665556");
			 

			output.Item.ShouldNotBeNull() ;
		}

		[Fact ]
		public async Task devo_Cadastrar_novo_cliente()
		{

			List<TipoIngressoDto> tipoIngresso = new List<TipoIngressoDto>();
			tipoIngresso.Add(new TipoIngressoDto() { Descricao = "Pista premium", Valor = 30 });
			tipoIngresso.Add(new TipoIngressoDto() { Descricao = "Pista comum", Valor = 10 });

			var output =   _clienteServico.SalvarOuAlterar(new ClienteDto()
			{
				 CPF = "065665556",
				 Email = "teste@test.com",
				 Nome ="Nome Cliente",
				 Endereco = "Endereço do cliente"
			});
			output.ShouldNotBeNull();
		}
		 

	}
}
