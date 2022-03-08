using Fgv.Acad.Financeiro.Auditing;
using Shouldly;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.Auditing
{
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Fgv.Acad.Financeiro.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Fgv.Acad.Financeiro.Auditing.GenericEntityService`1[[Fgv.Acad.Financeiro.Storage.BinaryObject, Fgv.Acad.Financeiro.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Fgv.Acad.Financeiro.Auditing.XEntityService`1[Fgv.Acad.Financeiro.Auditing.AService`5[[Fgv.Acad.Financeiro.Storage.BinaryObject, Fgv.Acad.Financeiro.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Fgv.Acad.Financeiro.Storage.TestObject, Fgv.Acad.Financeiro.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
