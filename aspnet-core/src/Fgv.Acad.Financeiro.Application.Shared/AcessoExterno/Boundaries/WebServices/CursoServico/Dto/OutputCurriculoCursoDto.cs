using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico.Dto
{
    public class OutputCurriculoCursoDto
    {
        public List<CurriculoCursoDto> ItensCurriculoCurso { get; set; }
        public int TotalDeRegistrosNaConsulta { get; set; }
    }
}
