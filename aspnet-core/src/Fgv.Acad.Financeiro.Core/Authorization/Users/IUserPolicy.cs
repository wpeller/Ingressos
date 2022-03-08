using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Fgv.Acad.Financeiro.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
