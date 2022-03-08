using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.LogIDE;
using Fgv.Acad.Financeiro.Authorization.Distributed;
using Fgv.Acad.Financeiro.Relatorios;
using Fgv.Acad.Financeiro.Relatorios.Exporting;
using Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces;
using Fgv.Acad.Financeiro.SSO;
using Fgv.Tic.WsConnectorCore;

namespace Fgv.Acad.Financeiro
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(FinanceiroCoreModule)
        )]
    public class FinanceiroApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<DistributedAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FinanceiroApplicationModule).GetAssembly());
            IocManager.Register<IdentityManager.IdentityManager>();
            IocManager.RegisterIfNot<Connector>();
            IocManager.RegisterIfNot<ConfigurationResolver>();
            IocManager.RegisterIfNot<IHttpClientApiRequest, HttpClientApiRequest>();
            IocManager.RegisterIfNot<ITokenSSOManager, TokenSSOManager>();            
            IocManager.RegisterIfNot<IPlanoComExcessoDeParcelamentoAppService, PlanoComExcessoDeParcelamentoAppService>();
            IocManager.RegisterIfNot<ILogApiIDEService, LogApiIDEService>();
        }
    }
}