using Abp.Application.Services.Dto;
using System;

namespace Fgv.Acad.Financeiro.Applications.Dtos
{
    public class GetAllApplicationsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxSecondsToExpireFilter { get; set; }
		public int? MinSecondsToExpireFilter { get; set; }



    }
}