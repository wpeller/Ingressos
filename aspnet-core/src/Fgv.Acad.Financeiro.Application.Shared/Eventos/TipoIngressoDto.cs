using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
   
    public class TipoIngressoDto  
    {        
        public long Id { get; set; }
        public string  Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Timestamp { get; set; }

    }

}
