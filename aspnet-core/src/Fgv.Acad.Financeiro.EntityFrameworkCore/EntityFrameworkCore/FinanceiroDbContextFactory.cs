using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.Web;

namespace Fgv.Acad.Financeiro.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FinanceiroDbContextFactory : IDesignTimeDbContextFactory<FinanceiroDbContext>
    {
        public FinanceiroDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FinanceiroDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            FinanceiroDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FinanceiroConsts.ConnectionStringName));

            return new FinanceiroDbContext(builder.Options);
        }
    }
}