using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico.Dto
{
    public class OutputUnidadeEnsinoDto
    {
        public List<UnidadeEnsinoDto> ListaDeUnidadesDeEnsino { get; set; }
        public int TotalDeRegistrosDaConsulta { get; set; }
    }
}
