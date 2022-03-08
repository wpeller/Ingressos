using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}