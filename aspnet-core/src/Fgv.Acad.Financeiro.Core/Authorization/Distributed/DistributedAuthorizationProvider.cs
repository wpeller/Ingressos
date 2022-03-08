using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Localization;

namespace Fgv.Acad.Financeiro.Authorization.Distributed
{
    public class DistributedAuthorizationProvider : AuthorizationProvider
    {
        private readonly IRepository<DistributedAuthorization, Guid> _repositorio;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DistributedAuthorizationProvider(IRepository<DistributedAuthorization, Guid> repositorio, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositorio = repositorio;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                CreateAllPermissionIfNotExists();
                RemoveOutOfDates(new AppAuthorizationProvider().GetAllCharged());
                uow.Complete();
            }

            using (var uow = _unitOfWorkManager.Begin())
            {
                var authorizations = _repositorio.GetAllList(x => x.ParentAuthorization == null);
                foreach (var authorization in authorizations)
                {
                    var p = context.GetPermissionOrNull(authorization.Name) ?? context.CreatePermission(authorization.Name);
                    SetChildPermission(p, authorization);
                }

                uow.Complete();
            } 
        }

        private void SetChildPermission(Permission permission, DistributedAuthorization distributedAuthorization)
        {
            var children = _repositorio.GetAllList(x => x.ParentAuthorization.Id == distributedAuthorization.Id);
            foreach (var child in children)
            {
                var p = permission.CreateChildPermission(child.Name, L(child.LocalizableDisplayName ?? child.Name), L(child.LocalizableDescriptionName ?? child.Name), child.MultiTenancySide.GetValueOrDefault());
                SetChildPermission(p, child);
            }
        }

        public void CreateAllPermissionIfNotExists()
        {
            CreateIfNotExists(new AppAuthorizationProvider().GetAllCharged());
        }

        private void CreateIfNotExists(IList<DistributedAuthorization> permissions)
        {
            foreach (var distributedAuthorization in permissions)
            {
                CreateIfNotExists(distributedAuthorization);
            }
        }

        private void RemoveOutOfDates(IList<DistributedAuthorization> navigations)
        {
            var currentNavs = _repositorio.GetAllList(x => x.Module == FinanceiroConsts.ModuleName);

            foreach (var navigation in currentNavs)
            {
                if (!AuthorizationExistsIn(navigation, navigations))
                    _repositorio.Delete(navigation);
            }
        }

        private bool AuthorizationExistsIn(DistributedAuthorization authorization, IList<DistributedAuthorization> authorizations)
        {
            foreach (var auth in authorizations)
            {
                if (auth.Name == authorization.Name)
                    return true;
                if (auth.ChildrenAuthorizations == null || !auth.ChildrenAuthorizations.Any())
                    continue;
                if (AuthorizationExistsIn(authorization, auth.ChildrenAuthorizations))
                    return true;
            }

            return false;
        }

        private void CreateIfNotExists(DistributedAuthorization authorization)
        {
            var auth = _repositorio.FirstOrDefault(x => x.Name == authorization.Name);
            if (auth != null && authorization.ChildrenAuthorizations?.Count > 0)
                foreach (var authorizationChildAuthorization in authorization.ChildrenAuthorizations)
                {
                    authorizationChildAuthorization.ParentAuthorization = auth;
                    CreateIfNotExists(authorizationChildAuthorization);
                }

            if (auth == null)
                _repositorio.Insert(authorization);
            else
                Update(authorization, auth);
        }

        private void Update(DistributedAuthorization authorizationSource, DistributedAuthorization authorizationDestination)
        {
            authorizationDestination.Name = authorizationSource.Name;
            authorizationDestination.LocalizableDescriptionName = authorizationSource.LocalizableDescriptionName;
            authorizationDestination.LocalizableDisplayName = authorizationSource.LocalizableDisplayName;
            authorizationDestination.Module = authorizationSource.Module;
            authorizationDestination.MultiTenancySide = authorizationSource.MultiTenancySide;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, FinanceiroConsts.LocalizationSourceName);
        }
    }
}
