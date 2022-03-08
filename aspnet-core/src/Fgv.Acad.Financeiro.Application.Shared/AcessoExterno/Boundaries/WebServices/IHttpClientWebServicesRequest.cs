using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices
{
	public interface IHttpClientWebServicesRequest
	{
		Task<T> SendAsync<TInput, T>(HttpClientWebServicesRequestInput<TInput> input);
		T Send<TInput, T>(HttpClientWebServicesRequestInput<TInput> input);
		Task<XmlDocument> SendAsync<TInput>(HttpClientWebServicesRequestInput<TInput> input);
		XmlDocument Send<TInput>(HttpClientWebServicesRequestInput<TInput> input);

	}
}
