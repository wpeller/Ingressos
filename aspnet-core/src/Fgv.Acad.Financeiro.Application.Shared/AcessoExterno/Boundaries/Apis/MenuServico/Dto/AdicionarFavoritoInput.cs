namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto
{
	public class AdicionarFavoritoInput
	{
		public long IdPapel { get; set; }
		public long IdRecurso { get; set; }
		public string CodigoExternoUsuario { get; set; }
	}
}
