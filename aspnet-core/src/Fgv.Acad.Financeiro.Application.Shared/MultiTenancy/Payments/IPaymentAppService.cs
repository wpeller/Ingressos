using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.MultiTenancy.Dto;
using Fgv.Acad.Financeiro.MultiTenancy.Payments.Dto;
using Abp.Application.Services.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task<PaymentInfoDto> GetPaymentInfo(PaymentInfoInput input);

        Task<CreatePaymentResponse> CreatePayment(CreatePaymentDto input);

        Task CancelPayment(CancelPaymentDto input);

        Task<ExecutePaymentResponse> ExecutePayment(ExecutePaymentDto input);

        Task<PagedResultDto<SubscriptionPaymentListDto>> GetPaymentHistory(GetPaymentHistoryInput input);
    }
}
