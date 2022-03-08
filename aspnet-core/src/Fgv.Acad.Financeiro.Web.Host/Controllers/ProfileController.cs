using Abp.AspNetCore.Mvc.Authorization;
using Fgv.Acad.Financeiro.Storage;

namespace Fgv.Acad.Financeiro.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(ITempFileCacheManager tempFileCacheManager) :
            base(tempFileCacheManager)
        {
        }
    }
}