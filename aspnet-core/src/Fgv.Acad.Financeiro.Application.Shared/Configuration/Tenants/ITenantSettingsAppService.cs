using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Configuration.Tenants.Dto;

namespace Fgv.Acad.Financeiro.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
