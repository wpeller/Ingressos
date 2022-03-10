using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fgv.Acad.Financeiro.Eventos
{

    public class ClienteDto  
    {
        public long Id { get; set; }
        public string  Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public List<Venda> Vendas { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
