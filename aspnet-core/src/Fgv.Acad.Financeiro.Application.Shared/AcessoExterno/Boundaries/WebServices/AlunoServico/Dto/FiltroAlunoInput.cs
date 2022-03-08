using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico.Dto
{
    public class FiltroAlunoInput
    {
        public string Order { get; set; }
        public bool VendaNacional { get; set; }
        public string CodigoExternoPapel { get; set; }
        public bool papelEhCaps { get; set; }
        public bool papelEhCoordenacaoFgvOnline { get; set; }
        public bool papelEhSuperintendenciaRede { get; set; }
        public bool papelEhSuperintendenciaNucleo { get; set; }
        public int Skip { get; set; }
        public bool papelEhDirecaoGestaoAcademica { get; set; }
        public bool papelEhAgenteVendas { get; set; }
        public bool papelEhFgvOnline { get; set; }
        public string CodigoFaculdade { get; set; }
        public string CodigoUnidadeEnsino { get; set; }
        public string CodigoAluno { get; set; }
        public string CpfPassaporte { get; set; }
        public string NomeAluno { get; set; }
        public bool papelEhAcademicoFgvOnline { get; set; }
        public int RegistrosPorPagina { get; set; }
        public string Mnemonico { get; set; }        
        public List<string> ListaUnidades { get; set; }
    }
}



