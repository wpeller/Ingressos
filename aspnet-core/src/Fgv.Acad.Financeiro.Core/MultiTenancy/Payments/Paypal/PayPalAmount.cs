﻿using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.MultiTenancy.Payments.Paypal
{
    public class PayPalAmount
    {
        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
