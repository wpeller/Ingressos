using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Fgv.Acad.Financeiro.Navigations
{
    public interface INavigationManager : IDomainService
    {
        Task CreateOrUpdateIfNecessary(IList<Navigation> navigations);
        Task<List<Navigation>> GetAllParentsAsync();
        Task RemoveOutOfDates(IList<Navigation> navigations);
    }
}