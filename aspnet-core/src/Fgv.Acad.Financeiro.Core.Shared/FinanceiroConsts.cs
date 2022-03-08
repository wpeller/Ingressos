namespace Fgv.Acad.Financeiro
{
    public class FinanceiroConsts
    {
        public const string LocalizationSourceName = "Financeiro";
        public const string ConnectionStringName = "Default";
        public const bool MultiTenancyEnabled = false;
        public const int PaymentCacheDurationInMinutes = 30;
        public const string ModuleName = "Core2";
        public const string ModuleDescription = "Core 2.0";
        public const string ClaimSiga2UserName = "siga2UserName";
        public const string ClaimSiga2Name = "siga2Name";

		//Usado apenas se for trabalhar com multiplos contextos de banco de dados.
        public const string ConnectionStringNameIWI = "DefaultDbBaseIWI";

	}
}
