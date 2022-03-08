using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.Sessions.Dto;

namespace Fgv.Acad.Financeiro.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
