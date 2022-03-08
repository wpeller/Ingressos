using System.Threading.Tasks;
using Fgv.Acad.Financeiro.Security.Recaptcha;

namespace Fgv.Acad.Financeiro.Tests.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
