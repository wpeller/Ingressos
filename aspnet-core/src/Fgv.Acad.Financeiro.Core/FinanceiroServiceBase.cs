using Abp;

namespace Fgv.Acad.Financeiro
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="FinanceiroDomainServiceBase"/>.
    /// For application services inherit FinanceiroAppServiceBase.
    /// </summary>
    public abstract class FinanceiroServiceBase : AbpServiceBase
    {
        protected FinanceiroServiceBase()
        {
            LocalizationSourceName = FinanceiroConsts.LocalizationSourceName;
        }
    }
}