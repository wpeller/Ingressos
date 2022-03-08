using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.MenuServico.Dto
{
	public class ItemMenuDto
	{
        public long Id { get; set; }
        public string Titulo { get; set; }
        public List<ItemMenuDto> Filhos { get; set; }
        public string BreadCrumb { get; set; }
        public string Url { get; set; }
        public string Rota { get; set; }
        public string ModuloAcesso { get; set; }
        public RecursoDto Recurso { get; set; }
        public string Nome { get; set; }
        public string OrdemGeral { get; set; }
    }
}
