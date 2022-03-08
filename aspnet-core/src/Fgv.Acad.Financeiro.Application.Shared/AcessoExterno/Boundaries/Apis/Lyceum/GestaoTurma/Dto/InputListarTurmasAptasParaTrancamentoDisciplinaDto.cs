using System;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto
{
    public class InputListarTurmasAptasParaTrancamentoDisciplinaDto
    {
        public List<string> ListaDeUnidades { get; set; }
        public string CodigoCurso { get; set; }
        public string Curriculo { get; set; }
        public DateTime? DataInicioTurma { get; set; }
        public DateTime? DataFimTurma { get; set; }
    }
}
