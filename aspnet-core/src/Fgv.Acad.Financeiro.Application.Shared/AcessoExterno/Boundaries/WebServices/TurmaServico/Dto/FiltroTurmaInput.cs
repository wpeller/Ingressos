using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico.Dto
{
    public class FiltroTurmaInput
    {
        public string CodigoTurma { get; set; }
        public List<string> ListaDeUnidades { get; set; }
        public string Order { get; set; }
        public int Skip { get; set; }
        public int RegistrosPorPagina { get; set; }
		public bool EhGerencialOnline { get; set; }
		public bool EhGerencialRede { get; set; }
		public bool EhGerencialRedeMGM { get; set; }
        public string Mnemonico { get; set; }

	}
}
