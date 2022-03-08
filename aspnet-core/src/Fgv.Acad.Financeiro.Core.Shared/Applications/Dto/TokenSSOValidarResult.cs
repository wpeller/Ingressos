namespace Fgv.Acad.Financeiro.Applications.Dto
{
    public class TokenSSOValidarResult
    {
        public TokenSSOValidarData Data { get; set; }
        public bool Sucesso { get { return Erro == null; } }
        public TokenSSOValidarErro Erro { get; set; }
    }
}
