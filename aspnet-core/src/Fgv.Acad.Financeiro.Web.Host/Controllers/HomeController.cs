using Abp.Auditing;
using Fgv.Acad.Financeiro.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Fgv.Acad.Financeiro.Web.Controllers
{
    public class HomeController : FinanceiroControllerBase
    {
        private readonly AppConfigurationAccessor _configurationAccessor;

        public HomeController(AppConfigurationAccessor configurationAccessor)
        {
            _configurationAccessor = configurationAccessor;
        }

        [DisableAuditing]
        public IActionResult Index()
        {
            var serverRootAddress = _configurationAccessor.Configuration["App:ServerRootAddress"];

            return Redirect($"{serverRootAddress}swagger");
        }
    }
}
