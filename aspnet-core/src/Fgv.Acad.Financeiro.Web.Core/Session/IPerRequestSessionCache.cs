using System.Threading.Tasks;
using Fgv.Acad.Financeiro.Sessions.Dto;

namespace Fgv.Acad.Financeiro.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
