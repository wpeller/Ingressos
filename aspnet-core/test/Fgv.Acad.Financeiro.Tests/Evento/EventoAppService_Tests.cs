using System.Collections.Generic;
using System.Threading.Tasks;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;
using Fgv.Acad.Financeiro.Eventos;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
	public class EventoAppService_Tests : AppTestBase
	{

		private readonly IEventoServicoAppService _eventoServico;
		public EventoAppService_Tests()
		{
			_eventoServico = Resolve<IEventoServicoAppService>();
		}

		[Fact]
		public async Task devo_obter_todos_eventos_ativos()
		{
			await this.devo_Cadastrar_novo_evento();

			var output = _eventoServico.ObterTodosAtivos();


			output.Item.Count.ShouldBeGreaterThanOrEqualTo(1);
		}

		[Fact]
		public async Task devo_obter_evento_por_id()
		{
			await devo_Cadastrar_novo_evento();

			var output = _eventoServico.ObterPorId (1);


			output.Item.ShouldNotBeNull() ;
		}

		[Fact ]
		public async Task devo_Cadastrar_novo_evento()
		{

			List<TipoIngressoDto> tipoIngresso = new List<TipoIngressoDto>();
			tipoIngresso.Add(new TipoIngressoDto() { Descricao = "Pista premium", Valor = 30 });
			tipoIngresso.Add(new TipoIngressoDto() { Descricao = "Pista comum", Valor = 10 });

			var output =   _eventoServico.SalvarOuAlterar(new EventoDto()
			{
				CnpjEmpresaParceira = "011654554",
				DataHoraEvento = System.DateTime.Now.AddDays(3),
				Descricao = "Descricao Evento",
				InicioOferta = System.DateTime.Now.AddDays(1),
				FimOferta = System.DateTime.Now.AddDays(2),
				NomeEvento ="Nome Evento",
				local = "Endereço ou nome do lugar",
				ListaTipoIngresso = tipoIngresso
			});
			output.ShouldNotBeNull();
		}
		 

	}
}
