using Abp.Domain.Services;

namespace Fgv.Acad.Financeiro
{
    public abstract class FinanceiroDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected FinanceiroDomainServiceBase()
        {
            LocalizationSourceName = FinanceiroConsts.LocalizationSourceName;
        }
    }
}
