namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico.Dto
{
    public class OutputPapelPorTipoDto
    {
        public bool PapelEhControladoriaOuCaps { get; set; }
        public bool PapelEhAcademicoOuFinanceiro { get; set; }
        public bool PapelEhSuperintendenciaNucleo { get; set; }
        public bool PapelEhSuperintendenciaNucleoRJ { get; set; }
        public bool PapelEhSuperintendenciaNucleoSP { get; set; }
        public bool PapelEhSuperintendenciaNucleoBR { get; set; }
        public bool PapelEhSuperintendenciaNucleoBH { get; set; }
        public bool PapelEhSuperintendenciaRede { get; set; }
        public bool PapelEhSuperintendenteRedeMGM { get; set; }
        public bool PapelEhSuperintendenciaPEC { get; set; }
        public bool PapelEhAuditoria { get; set; }
        public bool PapelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline { get; set; }
        public string CodigoUnidadePapel { get; set; }
        public string NomeUnidadePapel { get; set; }
        public string CodigoExterno { get; set; }
    }
}
