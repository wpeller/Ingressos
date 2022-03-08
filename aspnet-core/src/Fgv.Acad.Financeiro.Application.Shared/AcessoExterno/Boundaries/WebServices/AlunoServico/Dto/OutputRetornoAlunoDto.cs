using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico.Dto
{
    public class OutputRetornoAlunoDto
    {
        public List<AlunoDto> ItensAluno { get; set; }
        public int TotalDeRegistrosNaConsulta { get; set; }
    }    
}
