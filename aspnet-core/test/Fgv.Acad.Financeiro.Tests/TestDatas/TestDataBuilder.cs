using Fgv.Acad.Financeiro.EntityFrameworkCore;

namespace Fgv.Acad.Financeiro.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly FinanceiroDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(FinanceiroDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
            new TestEditionsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
