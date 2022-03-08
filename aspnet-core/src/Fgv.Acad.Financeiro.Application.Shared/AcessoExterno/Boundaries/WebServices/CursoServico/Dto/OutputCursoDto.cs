using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico.Dto
{
    public class OutputCursoDto
    {
        public List<CursoDto> ItensCurso { get; set; }
        public int TotalDeRegistrosNaConsulta { get; set; }
    }
}
