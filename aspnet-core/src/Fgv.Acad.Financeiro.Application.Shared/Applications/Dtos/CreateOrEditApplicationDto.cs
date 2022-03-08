
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Fgv.Acad.Financeiro.Applications.Dtos
{
    public class CreateOrEditApplicationDto : EntityDto<Guid?>
    {

		[Required]
		[StringLength(ApplicationConsts.MaxNameLength, MinimumLength = ApplicationConsts.MinNameLength)]
		public string Name { get; set; }
		
		
		[Required]
		[StringLength(ApplicationConsts.MaxUrlPathLength, MinimumLength = ApplicationConsts.MinUrlPathLength)]
		public string UrlPath { get; set; }
		
		
		[Required]
		[StringLength(ApplicationConsts.MaxSecretWordLength, MinimumLength = ApplicationConsts.MinSecretWordLength)]
		public string SecretWord { get; set; }

        [Required]
		public int SecondsToExpire { get; set; }
    }
}