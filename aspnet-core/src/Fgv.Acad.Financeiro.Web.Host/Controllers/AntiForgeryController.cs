using Microsoft.AspNetCore.Antiforgery;

namespace Fgv.Acad.Financeiro.Web.Controllers
{
    public class AntiForgeryController : FinanceiroControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
