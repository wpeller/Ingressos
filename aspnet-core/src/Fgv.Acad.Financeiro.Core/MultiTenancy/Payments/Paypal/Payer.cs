using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.MultiTenancy.Payments.Paypal
{
    public class Payer
    {
        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }
    }
}