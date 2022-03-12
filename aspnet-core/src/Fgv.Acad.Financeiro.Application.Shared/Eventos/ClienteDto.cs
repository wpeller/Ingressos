using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{

    public class ClienteDto  
    {
        public long ?Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do cliente")]
        public string  Nome { get; set; }
        [Required]
        [StringLength(9)]
        [Display(Name = "CPF do cliente")]
        public string CPF { get; set; }
        [EmailAddress]
        public string Email { get; set; }        
        public string Endereco { get; set; }
        public DateTime ?Timestamp { get; set; }

    }
}
