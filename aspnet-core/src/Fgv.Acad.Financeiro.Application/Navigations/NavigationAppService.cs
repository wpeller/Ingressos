using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Fgv.Acad.Financeiro.Applications;

namespace Fgv.Acad.Financeiro.Navigations
{
    public class NavigationAppService : FinanceiroAppServiceBase, INavigationAppService
    {
        private readonly INavigationManager _navigationManager;
        private readonly IApplicationManager _applicationManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public NavigationAppService(INavigationManager navigationManager, IApplicationManager applicationManager, IUnitOfWorkManager unitOfWorkManager)
        {
            _navigationManager = navigationManager;
            _applicationManager = applicationManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<NavigationDto> SendAndSynchronize(NavigationDto model)
        {
            var navs = model.Items.MapTo(new List<Navigation>());

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _navigationManager.CreateOrUpdateIfNecessary(navs);
                await _navigationManager.RemoveOutOfDates(navs);

                await _unitOfWorkManager.Current.SaveChangesAsync();
                await uow.CompleteAsync();
            }
                

            var navigations = await _navigationManager.GetAllParentsAsync();
            var retorno = new NavigationDto()
                { Items = new List<NavigationDto>() };

            var applications = await _applicationManager.GetAllAsync();
            foreach (var nav in navigations)
            {
                if (nav.Module == "Core")
                    continue;

                var application = applications.FirstOrDefault(x => x.Name == nav.Module);

                var p = new NavigationDto
                {
                    Name = nav.LocalizableDisplayName, Route = nav.UrlPath, FullUrlPath = nav.GetFullUrlPath(),
                    Icon = nav.Icon, PermissionName = nav.RequiredPermissionName,
                    Visible = true,
                    External = application != null && application.Name != FinanceiroConsts.ModuleName,
                    ModulePath = application?.UrlPath,
                    ModuleId = application?.Id,
                    Items = new List<NavigationDto>(),
                    DisplayNameEnUs = nav.DisplayNameEnUs, DisplayNamePtBr = nav.DisplayNamePtBr
                };
                if (retorno.Items.FirstOrDefault(find => find.Name == p.Name) == null)
                {
                    retorno.Items.Add(p);
                }


                if (nav.ChildrenNavigations?.Count > 0)
                    SetChildMenu(p, nav, application);
            }
            return retorno;
        }

        private void SetChildMenu(NavigationDto dto, Navigation navigation, Application application)
        {
            foreach (var child in navigation.ChildrenNavigations)
            {
                var p = new NavigationDto
                {
                    Name = child.LocalizableDisplayName,
                    Route = child.UrlPath,
                    FullUrlPath = child.GetFullUrlPath(),
                    Icon = child.Icon,
                    PermissionName = child.RequiredPermissionName,
                    Visible = true,
                    External = application != null && application.Name != FinanceiroConsts.ModuleName,
                    ModulePath = application?.UrlPath,
                    ModuleId = application?.Id,
                    Items = new List<NavigationDto>(),
                    DisplayNameEnUs = child.DisplayNameEnUs,
                    DisplayNamePtBr = child.DisplayNamePtBr
                };

                dto.Items.Add(p);

                if (child.ChildrenNavigations?.Count > 0)
                    SetChildMenu(p, child, application);
            }
        }

    }
}