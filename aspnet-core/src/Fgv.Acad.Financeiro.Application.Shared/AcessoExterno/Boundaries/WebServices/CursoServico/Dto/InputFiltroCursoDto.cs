using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico.Dto
{
    public class InputFiltroCursoDto
    {
        public string ProgramaCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string NomeCurso { get; set; }
        public List<string> ListaDeUnidades { get; set; }
        public string Order { get; set; }
        public int Skip { get; set; }
        public int RegistrosPorPagina { get; set; }
        public string Mnemonico { get; set; }
    }
}
