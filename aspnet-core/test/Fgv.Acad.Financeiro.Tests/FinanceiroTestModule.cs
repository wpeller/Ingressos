using System;
using System.IO;
using Abp;
using Abp.AspNetZeroCore;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Organizations;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Fgv.Acad.Financeiro.Authorization.Roles;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.EntityFrameworkCore;
using Fgv.Acad.Financeiro.Migrations.Seed;
using Fgv.Acad.Financeiro.MultiTenancy;
using Fgv.Acad.Financeiro.Security.Recaptcha;
using Fgv.Acad.Financeiro.Tests.Configuration;
using Fgv.Acad.Financeiro.Tests.DependencyInjection;
using Fgv.Acad.Financeiro.Tests.UiCustomization;
using Fgv.Acad.Financeiro.Tests.Url;
using Fgv.Acad.Financeiro.Tests.Web;
using Fgv.Acad.Financeiro.UiCustomization;
using Fgv.Acad.Financeiro.Url;
using NSubstitute;

namespace Fgv.Acad.Financeiro.Tests
{
    [DependsOn(
        typeof(FinanceiroApplicationModule),
        typeof(FinanceiroEntityFrameworkCoreModule),
        typeof(AbpTestBaseModule))]
    public class FinanceiroTestModule : AbpModule
    {
        public FinanceiroTestModule(FinanceiroEntityFrameworkCoreModule abpZeroTemplateEntityFrameworkCoreModule)
        {
            abpZeroTemplateEntityFrameworkCoreModule.SkipDbContextRegistration = true;
            abpZeroTemplateEntityFrameworkCoreModule.SkipDbSeed = true;
        }

        public override void PreInitialize()
        {
            IocManager.IocContainer.Kernel.AddHandlerSelector(new CastleSelectorHandler());

            var configuration = GetConfiguration();

            Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
            Configuration.UnitOfWork.IsTransactional = false;

            //Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
            Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;

            //Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            

            IocManager.Register<IAppUrlService, FakeAppUrlService>();
            IocManager.Register<IWebUrlService, FakeWebUrlService>();
            IocManager.Register<IRecaptchaValidator, FakeRecaptchaValidator>();

            Configuration.ReplaceService<IAppConfigurationAccessor, TestAppConfigurationAccessor>();
            Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);

            Configuration.ReplaceService<IUiThemeCustomizerFactory, NullUiThemeCustomizerFactory>();

            Configuration.Modules.AspNetZero().LicenseCode = configuration["AbpZeroLicenseCode"];

            //Uncomment below line to write change logs for the entities below:
            Configuration.EntityHistory.IsEnabled = true;
            Configuration.EntityHistory.Selectors.Add("FinanceiroEntities", typeof(User), typeof(Tenant));
        }

        public override void Initialize()
        {
            ServiceCollectionRegistrar.Register(IocManager);
            SeedHelper.SeedHostDb(IocManager);
            //RegisterFakeService<AbpZeroDbMigrator>();
        }

        private void RegisterFakeService<TService>()
            where TService : class
        {
            IocManager.IocContainer.Register(
                Component.For<TService>()
                    .UsingFactoryMethod(() => Substitute.For<TService>())
                    .LifestyleSingleton()
            );
        }

        private static IConfigurationRoot GetConfiguration()
        {
            return AppConfigurations.Get(Directory.GetCurrentDirectory(), addUserSecrets: true);
        }
    }
}
