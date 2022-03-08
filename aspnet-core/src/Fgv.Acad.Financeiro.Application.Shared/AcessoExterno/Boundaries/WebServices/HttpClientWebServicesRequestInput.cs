using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices
{
	public class HttpClientWebServicesRequestInput<TInput>
	{
		public string Interface { get; set; }
		public string Url { get; set; }
		public string Method { get; set; }
		public bool UseTns { get; set; }
		public Dictionary<string, string> Headers { get; set; }
		public TInput Parametters { get; set; }
		public string NameSpace { get; set; }
		public string ParametterName { get; set; }
	}
}
