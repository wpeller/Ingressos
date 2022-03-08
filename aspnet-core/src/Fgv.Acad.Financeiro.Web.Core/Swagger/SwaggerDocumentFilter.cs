using Abp.Authorization;
using Abp.Dependency;
using Abp.Runtime.Session;
using Fgv.Acad.Financeiro.Authorization;
using Fgv.Acad.Financeiro.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fgv.Acad.Financeiro.Web.Swagger
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            if (IocManager.Instance.Resolve<IPermissionChecker>().IsGranted(AppPermissions.Pages_Administration_Swagger))
                return;

            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                pathItem.Delete = null;
                pathItem.Patch = null;
                pathItem.Post = null;
                pathItem.Put = null;
                pathItem.Get = null;
            }

            swaggerDoc.Definitions.Clear();
        }
    }
}