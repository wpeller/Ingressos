using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto
{
    public class PlanoComExcessoDeParcelamentoDto
    {
        public string Matricula { get; set; }
        public string NomeAluno { get; set; }
        public string CodigoCurso { get; set; }
        public string NomeCurso { get; set; }
        public string CodigoCurriculo { get; set; }
        public string Programa { get; set; }
        public string Turma { get; set; }
        public string CPF { get; set; }
        public string UnidadeFisica { get; set; }
        public string UnidadeResponsavel { get; set; }
        public string CodigoMunicipio { get; set; }
        public Int64 CodigoNormaPrecoMinimo { get; set; }
        public int NumeroDeCobrancas { get; set; }
        public Int16 MaximoParcelamento { get; set; }
        public int NumeroDeParcelasEmExcesso { get; set; }
        public string ResponsavelFinanceiro { get; set; }
        public string IdentificadorNormaPrecoMinimo { get; set; }
        public Int32 AnoNormaPrecoMinimo { get; set; }
        public DateTime DataMatricula { get; set; }
        public string UnidadeEnsino { get; set; }
        public bool EhOnline { get; set; }
        public bool EhPresencial { get; set; }
    }
}
