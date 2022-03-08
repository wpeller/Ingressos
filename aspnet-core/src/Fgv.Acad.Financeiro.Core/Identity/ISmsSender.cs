using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}