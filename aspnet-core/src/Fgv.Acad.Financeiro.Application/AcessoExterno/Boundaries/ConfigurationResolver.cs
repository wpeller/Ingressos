using System;
using System.Collections.Generic;
using System.Text;
using Fgv.Acad.Financeiro.Configuration;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries
{
    public class ConfigurationResolver
    {
        private readonly IAppConfigurationAccessor _configuration;

        public ConfigurationResolver(IAppConfigurationAccessor configuration)
        {
            _configuration = configuration;
        }

        public HttpClientConfigurationResolverOutput Get(params string[] urlTagName)
        {
            var appSettings = string.Join(":", urlTagName);

            var output = new HttpClientConfigurationResolverOutput
            {
                Url = _configuration.Configuration[string.Concat(appSettings, ":Url")],
                UserName = _configuration.Configuration[string.Concat(appSettings, ":UserName")],
                Password = _configuration.Configuration[string.Concat(appSettings, ":Password")]
            };
            return output;
        }

        public string GetValue(params string[] urlTagName)
        {
            var appSettings = string.Join(":", urlTagName);
            return _configuration.Configuration[appSettings];
        }

    }
}
