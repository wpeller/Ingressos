using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}