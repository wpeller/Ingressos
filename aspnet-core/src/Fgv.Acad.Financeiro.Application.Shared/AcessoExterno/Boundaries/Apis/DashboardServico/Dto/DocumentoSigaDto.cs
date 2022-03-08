using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto
{
	public class DocumentoSigaDto
	{
		public long? Id { get; set; }
		public string Nome { get; set; }
		public string Arquivo { get; set; }
		public Guid guid { get; set; }
		public byte[] fileInfo { get; set; }
		public CategoriaDocumentoSigaDto Categoria { get; set; }
		public int? OrdemExibicao { get; set; }
	}
}
