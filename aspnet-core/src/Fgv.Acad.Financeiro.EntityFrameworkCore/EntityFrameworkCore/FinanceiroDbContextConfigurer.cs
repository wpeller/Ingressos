using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Fgv.Acad.Financeiro.EntityFrameworkCore
{
    public static class FinanceiroDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FinanceiroDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FinanceiroDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}