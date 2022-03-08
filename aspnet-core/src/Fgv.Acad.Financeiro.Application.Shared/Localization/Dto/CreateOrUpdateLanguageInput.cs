using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}