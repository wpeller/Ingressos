using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
    [Table("Cliente")]
    public class Cliente : Entity<long>
    {
        [Required]
        public string  Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        public string Endereco { get; set; }
        public List<Venda> Vendas { get; set; }
       public DateTime Timestamp { get; set; }

    }
}
