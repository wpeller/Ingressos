using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico.Dto
{
    public class AutorizacaoInput
    {
        public long papelId { get; set; }
        public long itemMenuId { get; set; }
        public string codigoExterno { get; set; }
    }
}
