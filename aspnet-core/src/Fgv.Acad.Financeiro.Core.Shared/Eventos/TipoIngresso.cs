using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
    [Table("TipoIngresso")]
    public class TipoIngresso : Entity<long>
    {

        [ForeignKey("IdEvento")]
        public Evento Evento{ get; set; }
        public string  Descricao { get; set; }
        public decimal Valor { get; set; }
        public List<Venda> Vendas { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
