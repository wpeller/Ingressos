using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
    [Table("Venda")]
    public class Venda : Entity<long>
    {

        public long IdTipoIngresso { get; set; }
        [ForeignKey("IdTipoIngresso")]
        public TipoIngresso TipoIngresso { get; set; }
        public long IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }
        public bool EhMeiaEntrada { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime ?DataCancelamentoVenda { get; set; }
    }
}
