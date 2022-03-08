using Abp.Application.Services;
using Fgv.Acad.Financeiro.Dto;
using Fgv.Acad.Financeiro.Logging.Dto;

namespace Fgv.Acad.Financeiro.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
