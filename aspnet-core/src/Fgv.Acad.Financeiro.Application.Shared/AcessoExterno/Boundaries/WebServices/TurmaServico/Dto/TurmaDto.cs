using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico.Dto
{
	public class TurmaDto
	{
		public string CodigoTurma { get; set; }
		public string NomeTurma { get; set; }
		public string CodigoUnidadeResponsavel { get; set; }
		public string CodigoUnidadeFisica { get; set; }
		public string CodigoCurriculo { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
	}
}
