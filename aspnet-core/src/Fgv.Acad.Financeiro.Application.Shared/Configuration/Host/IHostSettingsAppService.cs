using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Configuration.Host.Dto;

namespace Fgv.Acad.Financeiro.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
