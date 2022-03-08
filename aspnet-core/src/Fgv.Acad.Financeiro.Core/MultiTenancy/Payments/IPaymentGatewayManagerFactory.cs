using Abp.Dependency;

namespace Fgv.Acad.Financeiro.MultiTenancy.Payments
{
    public interface IPaymentGatewayManagerFactory
    {
        IDisposableDependencyObjectWrapper<IPaymentGatewayManager> Create(SubscriptionPaymentGatewayType gateway);
    }
}