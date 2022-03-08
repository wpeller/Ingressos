using Abp.AspNetZeroCore;
using Abp.Dependency;

namespace Fgv.Acad.Financeiro
{
    public class FgvFinanceiroCustomCoreModule : AbpAspNetZeroCoreModule
    {
        public override void PreInitialize()
        {
            IocManager.RegisterIfNot<AspNetZeroConfiguration>();
        }

        public override void PostInitialize()
        {
      
        }
    }
}