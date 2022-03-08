using Abp.Reflection.Extensions;
using Newtonsoft.Json;
using System.IO;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries
{
    public class TestConfig
    {
        public const string IgnorarTestes = "TestConfig.IgnorarTestes";
        //public const string IgnorarTestes = null;

        static string _PathRaiz;
        public static string PathRaiz
        {
            get
            {
                if (_PathRaiz == null)
                {
                    _PathRaiz = typeof(TestConfig).GetAssembly().GetDirectoryPathOrNull();

                    var s = _PathRaiz.Split("\\AcessoExterno\\");
                    _PathRaiz = s[0];

                    s = _PathRaiz.Split("\\bin\\");
                    _PathRaiz = s[0];
                }

                return _PathRaiz;
            }
        }

        static bool? _SkipTest;
        public static string SkipTest
        {
            get
            {
                if (_SkipTest == null)
                    _SkipTest = (string.IsNullOrWhiteSpace(AppSettings.FGVSM_Ambiente) || AppSettings.FGVSM_Ambiente.ToUpper().Contains("PROD"));

                return _SkipTest.GetValueOrDefault() ? "Ignorar tests no ambiente de produção." : null;
            }
        }

        static AppSettings_Test _AppSettings;
        public static AppSettings_Test AppSettings
        {
            get
            {
                if (_AppSettings == null)
                {
                    var texto = File.ReadAllText(Path.Combine(PathRaiz, "appsettings.json"));

                    _AppSettings = JsonConvert.DeserializeObject<AppSettings_Test>(texto);
                }

                return _AppSettings;
            }
        }
    }

    public class AppSettings_Test
    {
        public string FGVSM_Ambiente { get; set; }
    }
}
