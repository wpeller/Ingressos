using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class NovaVendaDto
    {
        [Required]
        public long idTipoIngresso { get; set; }

        [Required]
        public string cpf { get; set; }

        [Required]
        public bool EhMeiaEntrada { get; set; }
    }
}