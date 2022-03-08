using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using Fgv.Acad.Financeiro.Security;

namespace Fgv.Acad.Financeiro.Applications
{
	[Table("Applications")]
    public class Application : FullAuditedEntity<Guid>, IMustHaveTenant
    {
        
        public virtual int TenantId { get; set; }

        [Required]
		[StringLength(ApplicationConsts.MaxNameLength, MinimumLength = ApplicationConsts.MinNameLength)]
		public virtual string Name { get; set; }
		
		[Required]
		[StringLength(ApplicationConsts.MaxUrlPathLength, MinimumLength = ApplicationConsts.MinUrlPathLength)]
		public virtual string UrlPath { get; set; }
		
		[Required]
		public virtual string SecretWordEncrypted { get; set; }
		
		public virtual int SecondsToExpire { get; set; }

        [Required]
        [NotMapped]
        [MaxLength(ApplicationConsts.MaxSecretWordLength)]
        public virtual string SecretWord
        {
            get
            {
                return ScSimpleStringCypher.Instance.Decrypt(SecretWordEncrypted);
            }

            set
            {
                SecretWordEncrypted = !value.IsNullOrWhiteSpace() ? ScSimpleStringCypher.Instance.Encrypt(value) : value;
            }
        }

    }
}