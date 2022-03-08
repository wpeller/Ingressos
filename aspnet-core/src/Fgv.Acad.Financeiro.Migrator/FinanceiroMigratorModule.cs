using Abp.AspNetZeroCore;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.EntityFrameworkCore;
using Fgv.Acad.Financeiro.Migrator.DependencyInjection;

namespace Fgv.Acad.Financeiro.Migrator
{
    [DependsOn(typeof(FinanceiroEntityFrameworkCoreModule))]
    public class FinanceiroMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public FinanceiroMigratorModule(FinanceiroEntityFrameworkCoreModule abpZeroTemplateEntityFrameworkCoreModule)
        {
            abpZeroTemplateEntityFrameworkCoreModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(FinanceiroMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            IocManager.IocContainer.Kernel.AddHandlerSelector(new CastleSelectorHandler());

            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                FinanceiroConsts.ConnectionStringName
                );
            Configuration.Modules.AspNetZero().LicenseCode = _appConfiguration["AbpZeroLicenseCode"];

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FinanceiroMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}