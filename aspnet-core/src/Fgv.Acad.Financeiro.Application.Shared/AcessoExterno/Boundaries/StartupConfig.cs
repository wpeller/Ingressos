using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries
{
    public class StartupConfig
    {
        private static JsonSerializer _JsonSerializer;
        public static JsonSerializer JsonSerializer
        {
            get
            {
                if (_JsonSerializer == null)
                {
                    _JsonSerializer = new JsonSerializer()
                    {
                        DateTimeZoneHandling = DateTimeZoneHandling.Local,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFK",
                    };
                }

                return _JsonSerializer;
            }
        }

        public static JsonSerializerSettings JsonSerializerSettings { get; set; }
    }
}
