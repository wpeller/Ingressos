using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico.Dto
{
    public class OutputRetornoTurmaDto
    {
        public List<TurmaDto> ItensTurma { get; set; }
        public int TotalDeRegistrosNaConsulta { get; set; }
    }
}
