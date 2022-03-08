using System.Threading.Tasks;
using Abp.Application.Services;

namespace Fgv.Acad.Financeiro.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
