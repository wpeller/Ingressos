using Microsoft.Extensions.Configuration;

namespace Fgv.Acad.Financeiro.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
