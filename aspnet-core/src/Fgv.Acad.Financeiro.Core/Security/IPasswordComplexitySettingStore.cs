using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
