using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=Financeiro; Trusted_Connection=True;");
            csb["Database"].ShouldBe("Financeiro");
        }
    }
}
