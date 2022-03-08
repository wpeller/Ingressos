using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto
{
    public class InputPlanoComExcessoDeParcelamentoDto
    {
        public List<PlanoComExcessoDeParcelamentoDto> Itens { get; set; }
        public string NomeArquivo { get; set; }
        public string TituloArquivo { get; set; }
    }
}
