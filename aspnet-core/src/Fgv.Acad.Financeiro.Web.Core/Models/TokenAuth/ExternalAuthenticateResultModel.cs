using Fgv.Acad.Financeiro.Applications;

namespace Fgv.Acad.Financeiro.Web.Models.TokenAuth
{
    public class ExternalAuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool WaitingForActivation { get; set; }

        public string ReturnUrl { get; set; }

        public ApplicationTokenData Data { get; set; }

	}
}