using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.MultiTenancy.Accounting.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
