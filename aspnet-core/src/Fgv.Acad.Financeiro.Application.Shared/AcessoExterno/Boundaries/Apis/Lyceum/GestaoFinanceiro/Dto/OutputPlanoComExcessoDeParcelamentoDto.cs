using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto
{
    public class OutputPlanoComExcessoDeParcelamentoDto
    {
        public List<PlanoComExcessoDeParcelamentoDto> Itens { get; set; }
        public int TotalDeRegistrosNaConsulta { get; set; }
    }
}
