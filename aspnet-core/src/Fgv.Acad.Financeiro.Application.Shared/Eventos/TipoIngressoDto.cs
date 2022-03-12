using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
   
    public class TipoIngressoDto  
    {        
        public long Id { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Descrição do tipo ingresso")]
        public string  Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        public DateTime Timestamp { get; set; }

    }

}
