using System.Threading.Tasks;
using Abp;
using Fgv.Acad.Financeiro.Configuration.Dto;
using Fgv.Acad.Financeiro.UiCustomization;
using Fgv.Acad.Financeiro.UiCustomization.Dto;

namespace Fgv.Acad.Financeiro.Tests.UiCustomization
{
    public class NullThemeUiCustomizer : IUiCustomizer
    {
        public async Task<UiCustomizationSettingsDto> GetUiSettings()
        {
            await Task.CompletedTask;

            return new UiCustomizationSettingsDto();
        }

        public Task UpdateUserUiManagementSettingsAsync(UserIdentifier user, ThemeSettingsDto settings)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTenantUiManagementSettingsAsync(int tenantId, ThemeSettingsDto settings)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateApplicationUiManagementSettingsAsync(ThemeSettingsDto settings)
        {
            throw new System.NotImplementedException();
        }

        public Task<ThemeSettingsDto> GetHostUiManagementSettings()
        {
            throw new System.NotImplementedException();
        }

        public Task<ThemeSettingsDto> GetTenantUiCustomizationSettings(int tenantId)
        {
            throw new System.NotImplementedException();
        }
    }
}