using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto
{
    public class TurmasAptasParaTrancamentoDisciplinaDto
    {
		public string CodigoTurma { get; set; }
		public string CodigoCurso { get; set; }
		public string Curriculo { get; set; }
		public DateTime? DataInicio { get; set; }
		public DateTime? DataFim { get; set; }
		public string Programa { get; set; }
		public string SituacaoTurma { get; set; }
		public string Pendencia { get; set; }
		public string CodigoUnidade { get; set; }
		public bool IsSelected { get; set; }
	}
}
