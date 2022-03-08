using System.Threading.Tasks;
using Abp.Dependency;

namespace Fgv.Acad.Financeiro.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}