using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.PapelServico.Dto;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.DashboardServico.Dto
{
    public class RecursoDto
	{
        public long Id { get; set; }
        public string Nome { get; set; }
        public short Modulo { get; set; }
        public string Url { get; set; }
        public string Rota { get; set; }
        public bool DominioSigaDois { get; set; }
        public int Tipo { get; set; }
        public List<PapelDto> Papeis { get; set; }
        public List<PapelDto> PapeisComRestricao { get; set; }
        public ModuloDto ObjModulo { get; set; }
    }
}
