using System;
using System.Collections.Generic;
using System.Text;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.UsuarioServico.Dto
{
	public class UsuarioSigaDoisDto
	{
		public int Id { get; set; }
		public string CodigoExterno { get; set; }
		public string Login { get; set; }
		public List<PapelDto> Papeis { get; set; }
		public string Email { get; set; }
		public string Nome { get; set; }
		public string SenhaFinanceira { get; set; }
		public string SenhaImpressaoContrato { get; set; }
		public bool? Ativo { get; set; }
		public DateTime? DataCriacao { get; set; }
		public bool? AcessaSGP { get; set; }
	}
}
