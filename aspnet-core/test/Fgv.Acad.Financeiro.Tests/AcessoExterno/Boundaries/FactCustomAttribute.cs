using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries
{
    public class FactCustomAttribute : FactAttribute
    {
        public override string Skip
        {
            get
            {
                return TestConfig.SkipTest;
            }
            set
            {
            }
        }

        public FactCustomAttribute()
        {
        }
    }
}
