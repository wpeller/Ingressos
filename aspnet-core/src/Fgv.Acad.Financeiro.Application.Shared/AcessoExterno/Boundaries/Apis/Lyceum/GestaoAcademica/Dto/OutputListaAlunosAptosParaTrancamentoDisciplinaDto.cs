using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica.Dto
{
    public class OutputListaAlunosAptosParaTrancamentoDisciplinaDto
    {
        public List<AlunosAptosParaTrancamentoDisciplinaDto> ListaAlunosAptosParaTrancamentoDisciplina { get; set; }
        public string Error { get; set; }
    }
}
