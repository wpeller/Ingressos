using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis
{
	public interface IHttpClientApiRequest
	{
		Task<T> SendAsync<TInput, T>(HttpClientApiRequestInput<TInput> input);
		T Send<TInput, T>(HttpClientApiRequestInput<TInput> input);
		Task<JObject> SendAsync<TInput>(HttpClientApiRequestInput<TInput> input);
		JObject Send<TInput>(HttpClientApiRequestInput<TInput> input);
	}
}
