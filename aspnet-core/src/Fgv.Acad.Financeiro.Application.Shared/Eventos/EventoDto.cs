using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class EventoDto
    {
        public long Id { get; set; }
        [Required]
        [StringLength(14)]
        public string CnpjEmpresaParceira { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Nome do evento")]
        public string NomeEvento { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Descrição do evento")]
        public string Descricao { get; set; }
        [Required]
        [StringLength(100)]
        public string local { get; set; }
        public List<TipoIngressoDto> ListaTipoIngresso { get; set; }
        public DateTime InicioOferta { get; set; }
        public DateTime FimOferta { get; set; }
        public DateTime DataHoraEvento { get; set; }
        public DateTime Timestamp { get; set; }
    }
}