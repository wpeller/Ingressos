using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Abp.UI;
using Fgv.Tic.WsConnectorCore;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices
{
	public class HttpClientWebServicesRequest : IHttpClientWebServicesRequest
	{
		private readonly Connector _connector;

		public HttpClientWebServicesRequest(Connector connector)
		{
			_connector = connector;
		}
	
		void SetConnector<TInput>(HttpClientWebServicesRequestInput<TInput> input)
		{
			_connector.Interface = input.Interface;
			_connector.Method = input.Method;
			_connector.NameSpace = input.NameSpace;
			_connector.Url = input.Url;
			_connector.UsingTns = input.UseTns;

			if (input.Headers?.Count > 0)
			{
				foreach (var item in input.Headers)
				{
					_connector.AddHeader(item.Key, item.Value);
				}
			}

			if (!(input.Parametters is IDictionary &&
			    input.Parametters.GetType().IsGenericType &&
			    input.Parametters.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>))))
			{
				 _connector.AddParameter(input.ParametterName, input.Parametters);
			}


		}

		public async Task<T> SendAsync<TInput, T>(HttpClientWebServicesRequestInput<TInput> input)
		{
			SetConnector(input);
			return await ExecutarAsync<T>();
		}

		private async Task<T> ExecutarAsync<T>()
		{
			try
			{
				return await _connector.ConsumingAsync<T>();
			}
			catch (WebException e)
			{
				using (var s = e.Response.GetResponseStream())
				{
					using (var reader = new StreamReader(s))
					{
						var xml = new XmlDocument();
						xml.LoadXml(reader.ReadToEnd());

						throw new UserFriendlyException($"Erro ao executar o method {_connector.Method}::: " +
						                                $"{xml.FirstChild.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(1).InnerText}",
							e);
					}
				}
			}
			catch (Exception e)
			{
				throw new UserFriendlyException("Ocorreu um error ao executar sua requisição: --> ",
					"Erro ao executar a transação!", e);

			}
		}

		private T Executar<T>()
		{
			try
			{
				return _connector.Consuming<T>();
			}
			catch (WebException e)
			{
				using (var s = e.Response.GetResponseStream())
				{
					using (var reader = new StreamReader(s))
					{
						var xml = new XmlDocument();
						xml.LoadXml(reader.ReadToEnd());

						throw new UserFriendlyException($"Erro ao executar o method {_connector.Method}::: " +
						                                $"{xml.FirstChild.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(1).InnerText}",
							e);
					}
				}
			}
			catch (Exception e)
			{
				throw new UserFriendlyException("Ocorreu um error ao executar sua requisição: --> ",
					"Erro ao executar a transação!", e);

			}
		}

		public T Send<TInput, T>(HttpClientWebServicesRequestInput<TInput> input)
		{
			SetConnector(input);
			return Executar<T>();
		}

		public async Task<XmlDocument> SendAsync<TInput>(HttpClientWebServicesRequestInput<TInput> input)
		{
			SetConnector(input);
			return await ExecutarAsync();
		}

		public XmlDocument Send<TInput>(HttpClientWebServicesRequestInput<TInput> input)
		{
			SetConnector(input);
			return Executar();
		}

		private async Task<XmlDocument> ExecutarAsync()
		{
			try
			{
				return await _connector.ConsumingAsync();
			}
			catch (WebException e)
			{
				using (var s = e.Response.GetResponseStream())
				{
					using (var reader = new StreamReader(s))
					{
						var xml = new XmlDocument();
						xml.LoadXml(reader.ReadToEnd());

						throw new UserFriendlyException($"Erro ao executar o method {_connector.Method}::: " +
						                                $"{xml.FirstChild.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(1).InnerText}",
							e);
					}
				}
			}
			catch (Exception e)
			{
				throw new UserFriendlyException("Ocorreu um error ao executar sua requisição: --> ",
					"Erro ao executar a transação!", e);

			}
		}

		private XmlDocument Executar()
		{
			try
			{
				return _connector.Consuming();
			}
			catch (WebException e)
			{
				using (var s = e.Response.GetResponseStream())
				{
					using (var reader = new StreamReader(s))
					{
						var xml = new XmlDocument();
						xml.LoadXml(reader.ReadToEnd());

						throw new UserFriendlyException($"Erro ao executar o method {_connector.Method}::: " +
						                                $"{xml.FirstChild.ChildNodes.Item(0).ChildNodes.Item(0).ChildNodes.Item(1).InnerText}",
							e);
					}
				}
			}
			catch (Exception e)
			{
				throw new UserFriendlyException("Ocorreu um error ao executar sua requisição: --> ",
					"Erro ao executar a transação!", e);

			}
		}
	}
}
