using Abp.Auditing;
using Fgv.Acad.Financeiro.Authorization.Users;

namespace Fgv.Acad.Financeiro.Auditing
{
    /// <summary>
    /// A helper class to store an <see cref="AuditLog"/> and a <see cref="User"/> object.
    /// </summary>
    public class AuditLogAndUser
    {
        public AuditLog AuditLog { get; set; }

        public User User { get; set; }
    }
}