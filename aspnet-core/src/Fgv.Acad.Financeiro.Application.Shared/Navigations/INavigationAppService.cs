using System.Threading.Tasks;
using Abp.Application.Services;

namespace Fgv.Acad.Financeiro.Navigations
{
    public interface INavigationAppService : IApplicationService
    {
        Task<NavigationDto> SendAndSynchronize(NavigationDto model);
    }
}