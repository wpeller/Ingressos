using Fgv.Acad.Financeiro.Editions.Dto;

namespace Fgv.Acad.Financeiro.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }
    }
}
