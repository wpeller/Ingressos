using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    [Table("Evento")]
    public class Evento : Entity<long>
    {

        public string CnpjEmpresaParceira { get; set; }
        public string NomeEvento { get; set; }
        public string Descricao { get; set; }
        public string local { get; set; }
        public List<TipoIngresso> ListaTipoIngresso { get; set; }        
        public DateTime InicioOferta { get; set; }
        public DateTime FimOferta { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public DateTime Timestamp { get; set; }

    }
}



