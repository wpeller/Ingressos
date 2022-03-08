using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LogIDE.Dtos
{
	public class InputRegistraLogLoginDto
	{
		public string sistema { get; set; }
		public DateTime dataHoraAcao { get; set; }
		public string usuarioLogado { get; set; }
		public string servidor { get; set; }
		public string ipAcesso { get; set; }
	}
}
