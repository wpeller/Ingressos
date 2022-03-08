using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Fgv.Acad.Financeiro.Localization
{
    public static class FinanceiroLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    FinanceiroConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(FinanceiroLocalizationConfigurer).GetAssembly(),
                        "Fgv.Acad.Financeiro.Localization.Financeiro"
                    )
                )
            );
        }
    }
}