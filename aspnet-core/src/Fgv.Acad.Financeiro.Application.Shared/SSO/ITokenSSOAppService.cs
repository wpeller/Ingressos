using Abp.Application.Services;
using Fgv.Acad.Financeiro.Applications.Dto;

namespace Fgv.Acad.Financeiro.SSO
{
    public interface ITokenSSOAppService : IApplicationService
    {
        TokenSSOValidarResult ValidarToken(string token);
    }
}
