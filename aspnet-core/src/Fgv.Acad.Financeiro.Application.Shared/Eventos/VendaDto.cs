using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{
   
    public class VendaDto
    {

        public long Id { get; set; }

        public TipoIngressoDto TipoIngresso { get; set; }

        public ClienteDto Cliente { get; set; }

        public bool EhMeiaEntrada { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
