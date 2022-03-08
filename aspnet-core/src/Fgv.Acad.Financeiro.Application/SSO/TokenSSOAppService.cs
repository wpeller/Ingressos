using Abp.Auditing;
using Fgv.Acad.Financeiro.Applications.Dto;

namespace Fgv.Acad.Financeiro.SSO
{
    public class TokenSSOAppService : ITokenSSOAppService
    {
        #region "Classes Injetadas"
        private readonly ITokenSSOManager _tokenSSOManager;
        #endregion

        public TokenSSOAppService(ITokenSSOManager tokenManager)
        {
            _tokenSSOManager = tokenManager;
        }

        [DisableAuditing]
        public TokenSSOValidarResult ValidarToken(string token)
        {
            return _tokenSSOManager.ValidarToken(token);
        }
    }
}
