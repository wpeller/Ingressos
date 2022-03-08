using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
