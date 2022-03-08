using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Install.Dto;

namespace Fgv.Acad.Financeiro.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}