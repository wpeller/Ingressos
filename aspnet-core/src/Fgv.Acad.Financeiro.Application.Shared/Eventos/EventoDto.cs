using System;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class EventoDto
    {
        public long Id { get; set; }
        public string CnpjEmpresaParceira { get; set; }
        public string NomeEvento { get; set; }
        public string Descricao { get; set; }
        public string local { get; set; }
        public List<TipoIngressoDto> ListaTipoIngresso { get; set; }
        public DateTime InicioOferta { get; set; }
        public DateTime FimOferta { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public DateTime Timestamp { get; set; }
    }
}