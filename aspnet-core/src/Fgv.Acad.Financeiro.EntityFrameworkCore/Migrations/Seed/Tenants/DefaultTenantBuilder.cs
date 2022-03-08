using System.Linq;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Fgv.Acad.Financeiro.Editions;
using Fgv.Acad.Financeiro.EntityFrameworkCore;

namespace Fgv.Acad.Financeiro.Migrations.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly FinanceiroDbContext _context;

        public DefaultTenantBuilder(FinanceiroDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == MultiTenancy.Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new MultiTenancy.Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
        }
    }
}
