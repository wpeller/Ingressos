using System;
using Abp.UI;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Tic.ApiConnectorCore;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Fgv.Acad.Financeiro.IdentityManager
{
    public class IdentityManager
    {
        private readonly IAppConfigurationAccessor _configuration;

        public IdentityManager(IAppConfigurationAccessor configuration)
        {
            _configuration = configuration;
        }

        public IdentityManagerDto ValidateToken(string token)
        {
            var conector = CreateConnector();
            var service = _configuration.Configuration["IdentityManager:UrlValidateToken"];

            if (string.IsNullOrEmpty(service)) throw new UserFriendlyException("Configuration not found: 'IdentityManager:UrlValidateToken'");

            conector.AddParameter("token", token);
            var result = conector.Consuming(service)["result"].ToString();

            return ConvertAuthentication(result);
        }

        private Connector CreateConnector()
        {
            var url = _configuration.Configuration["IdentityManager:UrlBase"];
            var user = _configuration.Configuration["IdentityManager:Usuario"];
            var password = _configuration.Configuration["IdentityManager:Senha"];

            if (string.IsNullOrEmpty(url)) throw new UserFriendlyException("Configuration not found: 'IdentityManager:UrlBase'");
            if (string.IsNullOrEmpty(user)) throw new UserFriendlyException("Configuration not found: 'IdentityManager:Usuario'");
            if (string.IsNullOrEmpty(password)) throw new UserFriendlyException("Configuration not found: 'IdentityManager:Senha'");

            var conector = new Connector(url);

            try
            {
                conector.ConnectAspNetZero(user, password);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException($"Error request {url}: {e.Message}", e);
            }

            return conector;
        }

        private IdentityManagerDto ConvertAuthentication(string resultIdentityManager)
        {
            var result = new IdentityManagerDto { Success = !resultIdentityManager.StartsWith("NOK") };

            if (result.Success)
            {
                result.User = resultIdentityManager;
                return result;
            }

            var split = resultIdentityManager.Split('&');
            if (split.Length < 1)
                throw new UserFriendlyException("Error validation user");

            var data = split[1];
            data = data.Replace("data=", "");

            result.User = JObject.Parse(data)["Usuario"].ToString();
            result.Password = JObject.Parse(data)["Senha"].ToString();

            return result;
        }

        public string CriarToken(string usuario, string papel)
        {
            var conector = CreateConnector();
            var servico = _configuration.Configuration["IdentityManager:UrlCreateToken"];
            var idAplicacao = _configuration.Configuration["IdentityManager:IdAplicacaoSiga2"];

            if (string.IsNullOrEmpty(servico)) throw new UserFriendlyException("Não foi definida a chave de configuração do sistema: 'IdentityManager:UrlCreateToken'");
            if (string.IsNullOrEmpty(idAplicacao)) throw new UserFriendlyException("Não foi definida a chave de configuração do sistema: 'IdentityManager:IdAplicacaoSiga2'");

            conector.AddParameter("username", $"{usuario}|{papel}");
            conector.AddParameter("id", idAplicacao);

            var retorno = conector.Consuming(servico);
            var tokenCompleto = retorno["result"].ToString();

            var separadores = new string[] { "?token=", "&token=", "/token=", "/token/" };
            var separador = separadores.FirstOrDefault(x => tokenCompleto.Contains(x));

            if (string.IsNullOrWhiteSpace(separador))
                return tokenCompleto;

            var s = tokenCompleto.Split(separador);

            if (s == null || s.Length <= 1)
                return tokenCompleto;

            return s[1];
        }
    }
}