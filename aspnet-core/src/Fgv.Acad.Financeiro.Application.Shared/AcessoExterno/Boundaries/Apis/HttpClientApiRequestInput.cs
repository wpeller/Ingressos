using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis
{
	public class HttpClientApiRequestInput<TInput>
	{
		public HttpClientApiMethod Method { get; set; }
		public string ApplicationName { get; set; }
		public string ApiUserName { get; set; }
		public string ApiUserNamePassword { get; set; }
		public string Url { get; set; }
		public bool UseSiga2Key { get; set; }
		public TInput InputRequest { get; set; }
		public Dictionary<string, object> InputRequesDictionary { get; set; }
		public string ApiService { get; set; }
	}
}
