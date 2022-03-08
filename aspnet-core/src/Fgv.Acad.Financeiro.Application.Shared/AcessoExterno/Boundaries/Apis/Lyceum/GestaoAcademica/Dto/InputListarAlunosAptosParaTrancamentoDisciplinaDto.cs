using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica.Dto
{
    public class InputListarAlunosAptosParaTrancamentoDisciplinaDto
    {
        public List<string> ListaDeTurmas { get; set; }
        public List<string> ListaDeDisciplinas { get; set; }
    }
}
