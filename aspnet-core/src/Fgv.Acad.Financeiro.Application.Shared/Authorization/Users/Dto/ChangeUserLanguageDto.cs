using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
