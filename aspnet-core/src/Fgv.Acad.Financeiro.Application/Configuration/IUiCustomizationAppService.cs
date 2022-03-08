using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Configuration.Dto;

namespace Fgv.Acad.Financeiro.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<List<ThemeSettingsDto>> GetUiManagementSettings();

        Task UpdateUiManagementSettings(ThemeSettingsDto settings);

        Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

        Task UseSystemDefaultSettings();
    }
}
