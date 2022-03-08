using System.Collections.Generic;
using Abp.MultiTenancy;
using Fgv.Acad.Financeiro.Authorization.Distributed;

namespace Fgv.Acad.Financeiro.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;
        private IList<DistributedAuthorization> _permissions;

        public AppAuthorizationProvider()
        {
            _isMultiTenancyEnabled = FinanceiroConsts.MultiTenancyEnabled;
            ChargePermissions();
        }

        private void ChargePermissions()
        {
            _permissions = new List<DistributedAuthorization>();

            var pages = new DistributedAuthorization { Name = AppPermissions.Pages, LocalizableDisplayName = "Pages", LocalizableDescriptionName = "Pages", Module = "Core" };

            var applications = pages.CreateChild(AppPermissions.Pages_Applications,"Applications");
            applications.CreateChild(AppPermissions.Pages_Applications_Create, "CreateNewApplication");
            applications.CreateChild(AppPermissions.Pages_Applications_Edit, "EditApplication");
            applications.CreateChild(AppPermissions.Pages_Applications_Delete, "DeleteApplication");

            pages.CreateChild(AppPermissions.Pages_DemoUiComponents, "DemoUiComponents");

            var administration = pages.CreateChild(AppPermissions.Pages_Administration, "Administration");

            var roles = administration.CreateChild(AppPermissions.Pages_Administration_Roles, "Roles");
            roles.CreateChild(AppPermissions.Pages_Administration_Roles_Create, "CreatingNewRole");
            roles.CreateChild(AppPermissions.Pages_Administration_Roles_Edit, "EditingRole");
            roles.CreateChild(AppPermissions.Pages_Administration_Roles_Delete, "DeletingRole");

            var users = administration.CreateChild(AppPermissions.Pages_Administration_Users, "Users");
            users.CreateChild(AppPermissions.Pages_Administration_Users_Create, "CreatingNewUser");
            users.CreateChild(AppPermissions.Pages_Administration_Users_Edit, "EditingUser");
            users.CreateChild(AppPermissions.Pages_Administration_Users_Delete, "DeletingUser");
            users.CreateChild(AppPermissions.Pages_Administration_Users_ChangePermissions, "ChangingPermissions");
            users.CreateChild(AppPermissions.Pages_Administration_Users_Impersonation, "LoginForUsers");

            var languages = administration.CreateChild(AppPermissions.Pages_Administration_Languages, "Languages");
            languages.CreateChild(AppPermissions.Pages_Administration_Languages_Create, "CreatingNewLanguage");
            languages.CreateChild(AppPermissions.Pages_Administration_Languages_Edit, "EditingLanguage");
            languages.CreateChild(AppPermissions.Pages_Administration_Languages_Delete, "DeletingLanguages");
            languages.CreateChild(AppPermissions.Pages_Administration_Languages_ChangeTexts, "ChangingTexts");

            administration.CreateChild(AppPermissions.Pages_Administration_AuditLogs, "AuditLogs");

            var organizationUnits = administration.CreateChild(AppPermissions.Pages_Administration_OrganizationUnits, "OrganizationUnits");
            organizationUnits.CreateChild(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, "ManagingOrganizationTree");
            organizationUnits.CreateChild(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, "ManagingMembers");

            administration.CreateChild(AppPermissions.Pages_Administration_UiCustomization, "VisualSettings");

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChild(AppPermissions.Pages_Tenant_Dashboard, "Dashboard", multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChild(AppPermissions.Pages_Administration_Tenant_Settings, "Settings", multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChild(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, "Subscription", multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChild(AppPermissions.Pages_Editions, "Editions", multiTenancySides: MultiTenancySides.Host);
            editions.CreateChild(AppPermissions.Pages_Editions_Create, "CreatingNewEdition", multiTenancySides: MultiTenancySides.Host);
            editions.CreateChild(AppPermissions.Pages_Editions_Edit, "EditingEdition", multiTenancySides: MultiTenancySides.Host);
            editions.CreateChild(AppPermissions.Pages_Editions_Delete, "DeletingEdition", multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChild(AppPermissions.Pages_Tenants, "Tenants", multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChild(AppPermissions.Pages_Tenants_Create, "CreatingNewTenant", multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChild(AppPermissions.Pages_Tenants_Edit, "EditingTenant", multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChild(AppPermissions.Pages_Tenants_ChangeFeatures, "ChangingFeatures", multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChild(AppPermissions.Pages_Tenants_Delete, "DeletingTenant", multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChild(AppPermissions.Pages_Tenants_Impersonation, "LoginForTenants", multiTenancySides: MultiTenancySides.Host);

            administration.CreateChild(AppPermissions.Pages_Administration_Host_Settings, "Settings", multiTenancySides: MultiTenancySides.Host);
            administration.CreateChild(AppPermissions.Pages_Administration_Host_Maintenance, "Maintenance", multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChild(AppPermissions.Pages_Administration_HangfireDashboard, "HangfireDashboard", multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChild(AppPermissions.Pages_Administration_Swagger, "Swagger", multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChild(AppPermissions.Pages_Administration_Host_Dashboard, "Dashboard", multiTenancySides: MultiTenancySides.Host);

            _permissions.Add(pages);
        }

        public IList<DistributedAuthorization> GetAllCharged()
        {
            return _permissions;
        }
    }
}
