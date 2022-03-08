namespace Fgv.Acad.Financeiro.MultiTenancy.Payments.Cache
{
    public interface IPaymentCache
    {
        PaymentCacheItem GetCacheItemOrNull(SubscriptionPaymentGatewayType gateway, string paymentId);

        void AddCacheItem(PaymentCacheItem item);

        void RemoveCacheItem(SubscriptionPaymentGatewayType gateway, string paymentId);
    }
}
