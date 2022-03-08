using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico.Dto
{
	public class TrocaDeSenhaInput
	{
		public int TipoSenha { get; set; }
		public string Login { get; set; }
		public string SenhaAtual { get; set; }
		public string SenhaNova { get; set; }
		public string ConfirmacaoSenhaNova { get; set; }
		public string Ip { get; set; }
	}
}
