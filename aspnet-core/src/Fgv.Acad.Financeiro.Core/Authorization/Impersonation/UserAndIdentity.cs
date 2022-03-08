using System.Security.Claims;
using Fgv.Acad.Financeiro.Authorization.Users;

namespace Fgv.Acad.Financeiro.Authorization.Impersonation
{
    public class UserAndIdentity
    {
        public User User { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public UserAndIdentity(User user, ClaimsIdentity identity)
        {
            User = user;
            Identity = identity;
        }
    }
}