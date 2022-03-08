using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto
{
    public class OutputListaTurmasAptasParaTrancamentoDisciplinaDto
    {
        public List<TurmasAptasParaTrancamentoDisciplinaDto> ListaTurmasAptasParaTrancamentoDisciplina { get; set; }
        public string Error { get; set; }
    }
}
