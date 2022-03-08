using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Editions.Dto;
using Fgv.Acad.Financeiro.MultiTenancy.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}