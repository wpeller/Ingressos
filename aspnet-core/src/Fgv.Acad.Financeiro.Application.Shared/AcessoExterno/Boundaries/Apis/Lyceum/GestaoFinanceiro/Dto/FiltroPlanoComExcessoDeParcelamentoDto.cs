using System;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto
{
    public class FiltroPlanoComExcessoDeParcelamentoDto
    {
        public List<string> ListaDeUnidades { get; set; }
        public string CodigoUnidadeEnsino { get; set; }
        public string Programa { get; set; }
        public string CodigoAluno { get; set; }
        public string CodigoTurma { get; set; }
        public DateTime DataInicioPeriodo { get; set; }
        public DateTime DataFimPeriodo { get; set; }
        public int Skip { get; set; }
        public int TotalDeRegistrosPorPagina { get; set; }
        public string Sorting { get; set; }
        public string Mnemonico { get; set; }
        public bool EhOnline { get; set; }
        public bool EhPresencial { get; set; }
    }
}
