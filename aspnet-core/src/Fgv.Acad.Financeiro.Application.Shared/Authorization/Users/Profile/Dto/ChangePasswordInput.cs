using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace Fgv.Acad.Financeiro.Authorization.Users.Profile.Dto
{
    public class ChangePasswordInput
    {
        [Required]
        [DisableAuditing]
        public string CurrentPassword { get; set; }

        [Required]
        [DisableAuditing]
        public string NewPassword { get; set; }
    }
}