using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Fgv.Acad.Financeiro.Applications;
using Fgv.Acad.Financeiro.Authorization.Roles;
using Fgv.Acad.Financeiro.Authorization.Users;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.MultiTenancy;
using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.Authorization
{
	public static class LogInManagerExtensions
	{
		public static async Task<AbpLoginResultExtension> GetExternalLoginTokenAsync(
			this AbpLogInManager<Tenant, Role, User> logInManager,
			string token,
			string tenancyName)
		{
			var applicationManager = IocManager.Instance.Resolve<ApplicationManager>();
			var userManager = IocManager.Instance.Resolve<UserManager>();
			var tenantRepository = IocManager.Instance.Resolve<IRepository<Tenant>>();
			var principalFactory = IocManager.Instance.Resolve<UserClaimsPrincipalFactory>();

			try
			{
				var tokenDecrypt = applicationManager.ValidateToken(token);

				var tokenObject = JsonConvert.DeserializeObject<ApplicationTokenSSOOutput>(TrataStringTokenDecryptToJson(tokenDecrypt));

				var userName = tokenObject.user;

				var user = await userManager.FindByNameAsync(userName);

				Debug.Assert(user.TenantId != null, "user.TenantId != null");
				var tenant = await tenantRepository.GetAsync(user.TenantId.Value);

				var identity = (ClaimsIdentity)(await principalFactory.CreateAsync(user)).Identity;
				identity.AddClaim(new Claim(FinanceiroConsts.ClaimSiga2UserName, tokenObject.data.UsuarioLogado));
				identity.AddClaim(new Claim(FinanceiroConsts.ClaimSiga2Name, tokenObject.data.UsuarioLogado));

				var result = new AbpLoginResult<Tenant, User>(
					tenant,
					user,
					identity
				);

				await SaveLoginAttempt(logInManager, result, tenancyName, user.UserName);

				return new AbpLoginResultExtension(tenant, user, identity)
				{
					Data = tokenObject.data
				};
			}
			catch (Exception)
			{
				return new AbpLoginResultExtension(AbpLoginResultType.UnknownExternalLogin);
			}
		}

		private static string TrataStringTokenDecryptToJson(string tokenDecrypt)
		{

			if (string.IsNullOrEmpty(tokenDecrypt)) { throw new Exception("erro ao tratar o retorno da leitura do token"); }

			var array = tokenDecrypt.Split('&');
			var json = "";
			foreach (var s in array)
			{
				var arr = s.Split('=');

				if (!string.IsNullOrEmpty(json))
				{
					json += ",";
				}

				json += "'" + arr[0] + "':";
				if (arr[1].StartsWith('{'))
					json += arr[1];
				else if (arr[1].GetTypeCode() != TypeCode.String
						 && arr[1].GetTypeCode() != TypeCode.Char
						 && arr[1].GetTypeCode() != TypeCode.Object)
				{
					json += arr[1];
				}
				else
				{
					json += "'" + arr[1] + "'";
				}
			}

			return "{" + json + "}";
		}

		public static async Task<AbpLoginResult<Tenant, User>> GetIdentityManagerTokenAsync(
			this AbpLogInManager<Tenant, Role, User> logInManager,
			string tenancyName, string usuarioUserName, string usuarioNome)
		{
			var userManager = IocManager.Instance.Resolve<UserManager>();
			var tenantRepository = IocManager.Instance.Resolve<IRepository<Tenant>>();
			var principalFactory = IocManager.Instance.Resolve<UserClaimsPrincipalFactory>();
			var configurationAccessor = IocManager.Instance.Resolve<IAppConfigurationAccessor>();

			try
			{
				var user = await userManager.FindByNameAsync(usuarioUserName);
				if (user == null)
				{
					user = await userManager.FindByNameAsync(
						configurationAccessor.Configuration["UsuarioPadraoParaImpersonate"]);

					if (user == null)
						throw new UserFriendlyException(
							$"O usuário '{configurationAccessor.Configuration["UsuarioPadraoParaImpersonate"]}' configurado para impersonate não existe no banco de dados.");
				}

				Debug.Assert(user.TenantId != null, "user.TenantId != null");
				var tenant = await tenantRepository.GetAsync(user.TenantId.Value);

				var identity = (ClaimsIdentity)(await principalFactory.CreateAsync(user)).Identity;
				identity.AddClaim(new Claim(FinanceiroConsts.ClaimSiga2UserName, usuarioUserName));
				identity.AddClaim(new Claim(FinanceiroConsts.ClaimSiga2Name, usuarioNome));

				var result = new AbpLoginResult<Tenant, User>(
					tenant,
					user,
					identity
				);

				return result;
			}
			catch (UserFriendlyException ex)
			{
				throw ex;
			}
			catch (Exception)
			{
				return new AbpLoginResult<Tenant, User>(AbpLoginResultType.UnknownExternalLogin);
			}
		}

		private static async Task SaveLoginAttempt(
			AbpLogInManager<Tenant, Role, User> logInManager,
			AbpLoginResult<Tenant, User> loginResult,
			string tenancyName,
			string userNameOrEmailAddress)
		{
			var unitOfWorkManager = IocManager.Instance.Resolve<IUnitOfWorkManager>();
			var userLoginAttemptRepository = IocManager.Instance.Resolve<IRepository<UserLoginAttempt, long>>();

			using (var uow = unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
			{
				var tenantId = loginResult.Tenant?.Id;
				using (unitOfWorkManager.Current.SetTenantId(tenantId))
				{
					var loginAttempt = new UserLoginAttempt
					{
						TenantId = tenantId,
						TenancyName = tenancyName,

						UserId = loginResult.User?.Id,
						UserNameOrEmailAddress = userNameOrEmailAddress,

						Result = loginResult.Result,

						BrowserInfo = logInManager.ClientInfoProvider.BrowserInfo,
						ClientIpAddress = logInManager.ClientInfoProvider.ClientIpAddress,
						ClientName = logInManager.ClientInfoProvider.ComputerName
					};

					await userLoginAttemptRepository.InsertAsync(loginAttempt);
					await unitOfWorkManager.Current.SaveChangesAsync();
				}

				await uow.CompleteAsync();
			}
		}

		public static bool IsAuthenticated()
		{
			return Thread.CurrentPrincipal.Identity.IsAuthenticated;
		}

	}

}
