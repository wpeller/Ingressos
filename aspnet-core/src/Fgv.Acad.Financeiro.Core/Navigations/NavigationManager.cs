using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Extensions;

namespace Fgv.Acad.Financeiro.Navigations
{
    public class NavigationManager : FinanceiroDomainServiceBase, INavigationManager
    {
        private readonly IRepository<Navigation, Guid> _repositorio;

        public NavigationManager(IRepository<Navigation, Guid> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Navigation>> GetAllParentsAsync()
        {
            return await _repositorio.GetAllListAsync(x=>x.ParentNavigation == null);
        }

        public async Task CreateOrUpdateIfNecessary(IList<Navigation> navigations)
        {
            var currentExistedNavigations = await _repositorio.GetAllListAsync();
            foreach (var navigation in navigations)
            {
                await CreateOrUpdateIfNecessary(navigation, currentExistedNavigations);
            }
        }

        private async Task CreateOrUpdateIfNecessary(Navigation navigation, List<Navigation> currentExistedNavigations)
        {
            if(navigation.Module.IsNullOrEmpty())
                navigation.Module = FinanceiroConsts.ModuleName;

            var nav = currentExistedNavigations.FirstOrDefault(x => x.Name == navigation.Name && x.UrlPath == navigation.UrlPath && x.Module == navigation.Module);
            if (nav != null && navigation.ChildrenNavigations?.Count > 0)
                foreach (var childNavigation in navigation.ChildrenNavigations)
                {
                    childNavigation.ParentNavigation = nav;
                    await CreateOrUpdateIfNecessary(childNavigation, currentExistedNavigations);
                }

            if (nav == null)
                await _repositorio.InsertAsync(navigation);
            else
                await Update(navigation, nav);
        }

        public async Task RemoveOutOfDates(IList<Navigation> navigations)
        {
            var currentNavs = await _repositorio.GetAllListAsync(x => x.Module == FinanceiroConsts.ModuleName);

            foreach (var navigation in currentNavs)
            {
                if (!await NavigationExistsIn(navigation, navigations))
                    await _repositorio.DeleteAsync(navigation);
            }
        }

        private async Task<bool> NavigationExistsIn(Navigation navigation, IList<Navigation> navigations)
        {
            foreach (var nav in navigations)
            {
                if (nav.Name == navigation.Name && nav.UrlPath == navigation.UrlPath)
                    return true;
                if (nav.ChildrenNavigations == null || !nav.ChildrenNavigations.Any())
                    continue;
                if (await NavigationExistsIn(navigation, nav.ChildrenNavigations))
                    return true;
            }

            return false;
        }

        private Task Update(Navigation navigationSource, Navigation navigationDestination)
        {
            navigationDestination.Name = navigationSource.Name;
            navigationDestination.DisplayNameEnUs = navigationSource.DisplayNameEnUs;
            navigationDestination.DisplayNamePtBr = navigationSource.DisplayNamePtBr;
            navigationDestination.Icon = navigationSource.Icon;
            navigationDestination.LocalizableDisplayName = navigationSource.LocalizableDisplayName;
            navigationDestination.RequiredPermissionName = navigationSource.RequiredPermissionName;
            navigationDestination.Visible = navigationSource.Visible;
            navigationDestination.Module = navigationSource.Module;
            navigationDestination.TemplateUrl = navigationSource.TemplateUrl;
            navigationDestination.UrlPath = navigationSource.UrlPath;

            return Task.CompletedTask;
        }
    }
}