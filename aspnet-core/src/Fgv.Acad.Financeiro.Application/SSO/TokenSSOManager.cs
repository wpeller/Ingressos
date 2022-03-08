using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto;
using Fgv.Acad.Financeiro.Applications;
using Fgv.Acad.Financeiro.Applications.Dto;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Tic.ApiConnectorCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Fgv.Acad.Financeiro.SSO
{
    public class TokenSSOManager : ITokenSSOManager
    {
        #region "Classes Injetadas"
        private readonly IConfigurationRoot _appConfiguration;
        private readonly ILogger<TokenSSOManager> _log;
        private readonly IdentityManager.IdentityManager _identityManagerComunicacao;
        private readonly IApplicationManager _applicationManager;
        #endregion

        public TokenSSOManager(IAppConfigurationAccessor configurationAccessor, IdentityManager.IdentityManager identityManagerComunicacao,
             ILogger<TokenSSOManager> log, IApplicationManager applicationManager)
        {
            this._appConfiguration = configurationAccessor.Configuration;
            this._identityManagerComunicacao = identityManagerComunicacao;
            this._log = log;
            this._applicationManager = applicationManager;
        }

        public string CriarToken(string usuario, string papel, bool tokenIdentityManager)
        {
            ItemMenuDto itemMenu = null;

            if (tokenIdentityManager)
            {
                itemMenu = new ItemMenuDto()
                {
                    Recurso = new RecursoDto()
                    {
                        ObjModulo = new ModuloDto()
                        {
                            TokenIdentityManager = true
                        }
                    }
                };
            }

            return CriarToken(usuario, papel, itemMenu);
        }

        public string CriarToken(string usuario, string papel, long idItemMenu)
        {
            if (idItemMenu < 0)
                return null;

            var urlWebApiMenuServico = _appConfiguration["Siga2:URLsWebApiBoundaries:Administracao:MenuServico"];

            if (string.IsNullOrWhiteSpace(urlWebApiMenuServico))
                return null;

            var apiConnector = new Connector(urlWebApiMenuServico, Method.Post, "ObterItemDeMenuPorId");

            var input = new ObterItemDeMenuPorIdInput() { IdItemMenu = idItemMenu };
            apiConnector.AddParameter(input);

            var itemMenu = apiConnector.Consuming<ItemMenuDto>();

            if (itemMenu == null) return null;

            return CriarToken(usuario, papel, itemMenu);
        }

        public string CriarToken(string usuario, string papel, ItemMenuDto itemMenu)
        {
            if (itemMenu == null || itemMenu.Recurso == null || itemMenu.Recurso.ObjModulo == null || !itemMenu.Recurso.ObjModulo.TokenIdentityManager)
            {
                var _papel = string.IsNullOrWhiteSpace(papel) ? 0 : long.Parse(papel);
                long _recursoId = 0;
                var _moduloAcesso = "";
                var _rota = "";

                if (itemMenu != null)
                {
                    _moduloAcesso = itemMenu.ModuloAcesso ?? "hub";
                    if (itemMenu.Recurso != null)
                    {
                        _recursoId = itemMenu.Recurso.Id;
                        _rota = itemMenu.Recurso.Rota ?? "";
                    }
                }

                return CriarTokenApp(usuario, _papel, _recursoId, _moduloAcesso, _rota);
            }

            return CriarTokenIM(usuario, papel);
        }

        public string CriarTokenIM(string usuario, string papel)
        {
            var token = _identityManagerComunicacao.CriarToken(usuario, papel);
            token += "&IM=1";
            token += "&t=" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            _log.LogInformation($"token gerado pelo IM: {token}");

            return token;
        }

        public string CriarTokenApp(string usuario, long papel, long idRecurso, string modulo, string rota)
        {
            var input = new ApplicationTokenData()
            {
                IdPapel = papel,
                IdRecurso = idRecurso,
                Modulo = modulo,
                Rota = rota,
                UsuarioLogado = usuario,
            };

            var app = _applicationManager.GetByName(input.Modulo.ToLower());

            if (app == null)
                return null;

            string data = JsonConvert.SerializeObject(input);

            var token = CreateToken(app.Name, app.SecretWord, "", data);
            token += "&IM=0";
            token += "&t=" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            _log.LogInformation($"token gerado pelo AspNetZero: {token}");

            return token;
        }

        public string CreateToken(string application, string secretword, string username, string data)
        {
            var unencodedMessage = $"user={username}";

            var epochTimeSpan = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            unencodedMessage += "&dt=" + epochTimeSpan;

            unencodedMessage += "&app=" + application;

            if (!string.IsNullOrEmpty(data))
            {
                unencodedMessage += "&data=" + data;
            }

            var messageToEncode = unencodedMessage + "&" + secretword;

            var md5HashString = CalculateMd5Hash(messageToEncode);

            var md5MessageString = unencodedMessage + "&md5=" + md5HashString;

            var base64MessageString = Base64Encode(md5MessageString);

            return base64MessageString;
        }

        public TokenSSOValidarResult ValidarToken(string token)
        {
            try
            {
                return ValidateToken(token);
            }
            catch (Exception ex)
            {
                var obj = new { Mensagem = ex.GetBaseException().Message };
                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.Desconhecido,
                        Mensagem = JsonConvert.SerializeObject(obj)
                    }
                };
            }
        }

        private TokenSSOValidarResult ValidateToken(string token)
        {
            var tokenDescriptografado = Base64Decode(token);

            var tokenDictionary = ConvertTokenToDictionary(tokenDescriptografado);

            var application = _applicationManager.GetByName(tokenDictionary["app"]);

            if (application == null)
            {
                var obj = new
                {
                    Mensagem = "Application não encontrada.",
                    App = tokenDictionary["app"]
                };

                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.ApplicationDoesNotExists,
                        Mensagem = JsonConvert.SerializeObject(obj)
                    }
                };
            }

            return ValidateToken(application.SecretWord, application.SecondsToExpire, tokenDescriptografado, tokenDictionary);
        }

        private TokenSSOValidarResult ValidateToken(string secretword, int secondstoexpire, string tokenDescriptografado, Dictionary<string, string> tokenDictionary)
        {
            var md5HashToken = tokenDictionary["md5"];

            var messageToEncode = tokenDescriptografado.Replace($"&md5={md5HashToken}", string.Empty) + $"&{secretword}";

            var md5HashString = CalculateMd5Hash(messageToEncode);

            if (md5HashToken != md5HashString)
            {
                var obj = new
                {
                    Mensagem = "Erro MD5",
                    md5HashToken,
                    md5HashString,
                };

                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.MD5InvalidToken,
                        Mensagem = JsonConvert.SerializeObject(obj)
                    }
                };
            }

            var actualTimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            var epochTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(tokenDictionary["dt"]));
            var diff = (actualTimeSpan - epochTimeSpan).TotalSeconds;

            if (diff > secondstoexpire)
            {
                var obj = new
                {
                    Mensagem = "Token Expirado",
                    actualTimeSpan,
                    epochTimeSpan,
                    diff,
                    secondstoexpire
                };

                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.TokenHasExpired,
                        Mensagem = JsonConvert.SerializeObject(obj)
                    }
                };
            }

            if (!tokenDictionary.ContainsKey("data"))
            {
                var obj = new
                {
                    Mensagem = "TokenData não encontrado."
                };

                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.DataNotFound,
                        Mensagem = JsonConvert.SerializeObject(obj)
                    }
                };
            }

            var _data = JsonConvert.DeserializeObject<TokenSSOValidarData>(tokenDictionary["data"]);

            if (string.IsNullOrWhiteSpace(_data.UsuarioLogado))
            {
                return new TokenSSOValidarResult()
                {
                    Erro = new TokenSSOValidarErro()
                    {
                        Codigo = TokenSSOValidarErroEnum.LoginUserNotFound,
                        Mensagem = "Usuario logado não informado na propriedade data do token."
                    }
                };
            }

            return new TokenSSOValidarResult()
            {
                Data = _data
            };
        }

        private Dictionary<string, string> ConvertTokenToDictionary(string tokenDescriptografado)
        {
            if (!tokenDescriptografado.Contains("&"))
                return new Dictionary<string, string>();

            var s = tokenDescriptografado.Split('&');

            return s.Where(x => x.Contains("=")).ToDictionary(x => x.Split('=')[0], x => x.Split('=')[1]);
        }

        private string Base64Encode(string value)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string value)
        {
            var base64EncodedBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private string CalculateMd5Hash(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            const char padding = '0';

            var md5 = new MD5CryptoServiceProvider();
            var hashBytes = md5.ComputeHash(bytes);

            var hashString = hashBytes.Aggregate(string.Empty, (current, t) => current + Convert.ToString(t, 16).PadLeft(2, padding));

            return hashString.PadLeft(32, padding);
        }
    }
}
