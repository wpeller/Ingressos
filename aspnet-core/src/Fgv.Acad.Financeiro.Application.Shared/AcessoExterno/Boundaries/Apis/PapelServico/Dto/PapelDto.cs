using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto
{
	public class PapelDto
	{
		public long Id { get; set; }
		public string Nome { get; set; }
		public bool IsChecked { get; set; }
		public string Mnemonico { get; set; }
		public bool? AtribuivelViaTela { get; set; }
		public long? Tipo { get; set; }
		public List<PapelDto> Filhos { get; set; }
	}
}
