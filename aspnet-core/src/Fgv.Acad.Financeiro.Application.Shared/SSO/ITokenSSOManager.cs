using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;
using Fgv.Acad.Financeiro.Applications.Dto;

namespace Fgv.Acad.Financeiro.SSO
{
    public interface ITokenSSOManager
    {
        string CriarToken(string usuario, string papel, bool tokenIdentityManager);

        string CriarToken(string usuario, string papel, long idItemMenu);

        string CriarToken(string usuario, string papel, ItemMenuDto itemMenu);

        string CriarTokenIM(string usuario, string papel);

        string CriarTokenApp(string usuario, long papel, long idRecurso, string modulo, string rota);

        TokenSSOValidarResult ValidarToken(string token);
    }
}
