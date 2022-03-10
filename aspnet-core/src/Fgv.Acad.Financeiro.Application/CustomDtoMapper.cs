using Fgv.Acad.Financeiro.Applications.Dtos;
using Fgv.Acad.Financeiro.Applications;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using Fgv.Acad.Financeiro.Auditing.Dto;
using Fgv.Acad.Financeiro.Authorization.Accounts.Dto;
using Fgv.Acad.Financeiro.Authorization.Permissions.Dto;
using Fgv.Acad.Financeiro.Authorization.Roles;
using Fgv.Acad.Financeiro.Authorization.Roles.Dto;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.Authorization.Users.Dto;
using Fgv.Acad.Financeiro.Authorization.Users.Profile.Dto;
using Fgv.Acad.Financeiro.Editions;
using Fgv.Acad.Financeiro.Editions.Dto;
using Fgv.Acad.Financeiro.Localization.Dto;
using Fgv.Acad.Financeiro.MultiTenancy;
using Fgv.Acad.Financeiro.MultiTenancy.Dto;
using Fgv.Acad.Financeiro.MultiTenancy.HostDashboard.Dto;
using Fgv.Acad.Financeiro.MultiTenancy.Payments;
using Fgv.Acad.Financeiro.MultiTenancy.Payments.Dto;
using Fgv.Acad.Financeiro.Navigations;
using Fgv.Acad.Financeiro.Notifications.Dto;
using Fgv.Acad.Financeiro.Organizations.Dto;
using Fgv.Acad.Financeiro.Sessions.Dto;
using Fgv.Acad.Financeiro.Eventos;

namespace Fgv.Acad.Financeiro
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
           configuration.CreateMap<CreateOrEditApplicationDto, Application>();
           configuration.CreateMap<Application, ApplicationDto>();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

            configuration.CreateMap<NavigationDto, Navigation>()
                .ForMember(dst => dst.RequiredPermissionName, options => options.MapFrom(x => x.PermissionName))
                .ForMember(dst => dst.ChildrenNavigations, options => options.MapFrom(x => x.Items))
                .ForMember(dst => dst.UrlPath, options => options.MapFrom(x => x.Route))
                .ForMember(dst => dst.LocalizableDisplayName, options => options.MapFrom(x => x.Name))
                .ForMember(dst => dst.Name, options => options.MapFrom(x => x.Name))
                .ForMember(dst => dst.Module, options =>
                {
                    options.Condition((src, dst) => string.IsNullOrEmpty(dst.Module));
                    options.MapFrom(x => FinanceiroConsts.ModuleName);
                });

            configuration.CreateMap<ClienteDto, Cliente>()
               .ForMember(dst => dst.Timestamp, options => options.MapFrom(x => x.Timestamp.Value ))
               .ForMember(dto => dto.Vendas, options => options.Ignore());
        }
    }
}