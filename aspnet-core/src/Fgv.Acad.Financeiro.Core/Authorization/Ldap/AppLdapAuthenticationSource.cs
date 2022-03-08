using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.MultiTenancy;

namespace Fgv.Acad.Financeiro.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}