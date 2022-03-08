using Abp.Application.Services.Dto;

namespace Fgv.Acad.Financeiro.Applications.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}