namespace Fgv.Acad.Financeiro.Applications.Dto
{
    public class TokenSSOValidarErro
    {
        public string Mensagem { get; set; }
        public TokenSSOValidarErroEnum Codigo { get; set; }
    }

    public enum TokenSSOValidarErroEnum
    {
        Desconhecido = 0,
        ApplicationDoesNotExists = 1,
        MD5InvalidToken = 2,
        TokenHasExpired = 3,
        DataNotFound = 4,
        LoginUserNotFound = 5,
    }
}
