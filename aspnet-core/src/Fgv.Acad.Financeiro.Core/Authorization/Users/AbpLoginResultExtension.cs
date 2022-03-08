using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Abp.Authorization;
using Abp.Authorization.Users;
using Fgv.Acad.Financeiro.Applications;
using Fgv.Acad.Financeiro.MultiTenancy;

namespace Fgv.Acad.Financeiro.Authorization.Users
{
	public class AbpLoginResultExtension : AbpLoginResult<Tenant, User>
	{
		public AbpLoginResultExtension(AbpLoginResultType result, Tenant tenant = null, User user = null) : base(result, tenant, user)
		{
		}

		public AbpLoginResultExtension(Tenant tenant, User user, ClaimsIdentity identity) : base(tenant, user, identity)
		{
		}

		public ApplicationTokenData Data { get; set; }
	}
}
