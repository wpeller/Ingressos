using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}