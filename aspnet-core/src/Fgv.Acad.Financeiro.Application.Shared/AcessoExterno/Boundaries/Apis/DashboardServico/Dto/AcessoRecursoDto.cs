using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto
{
	public class AcessoRecursoDto
	{
		public long Id { get; set; }
		public RecursoDto Recurso { get; set; }
		public int Acessos { get; set; }
		public string ModuloAcesso { get; set; }
	}
}
