using Abp;
using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Castle.MicroKernel.Registration;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.EntityFrameworkCore.Repositories;
using Fgv.Acad.Financeiro.EntityHistory;
using Fgv.Acad.Financeiro.Migrations.Seed;
using Fgv.Acad.Financeiro.Repositories;

namespace Fgv.Acad.Financeiro.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(FinanceiroCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
        )]
    public class FinanceiroEntityFrameworkCoreModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<FinanceiroDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        FinanceiroDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        FinanceiroDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }

            IocManager.IocContainer.Register(Component.For(typeof(IFinanceiroRepository<,>))
                .ImplementedBy(typeof(FinanceiroRepository<,>)).LifestyleTransient());

            // Uncomment below line to write change logs for the entities below:
            //Configuration.EntityHistory.Selectors.Add("FinanceiroEntities", EntityHistoryHelper.TrackedTypes);
            //Configuration.CustomConfigProviders.Add(new EntityHistoryConfigProvider(Configuration));
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FinanceiroEntityFrameworkCoreModule).GetAssembly());
            if (!SkipDbContextRegistration)
                IocManager.Resolve<AbpZeroDbMigrator>().CreateOrMigrate();
            //var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();

            //using (var scope = IocManager.CreateScope())
            //{
            //    if (!SkipDbSeed && scope.Resolve<DatabaseCheckHelper>().Exist(configurationAccessor.Configuration["ConnectionStrings:Default"]))
            //    {
            //        SeedHelper.SeedHostDb(IocManager);
            //    }
            //}
        }

        public override void PostInitialize()
        {
            
        }
    }
}
