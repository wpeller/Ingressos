using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico.Dto
{
    public class AlunoDto
    {
        public string NomeCurso { get; set; }
        public string NomeMunicipioUnidadeEnsino { get; set; }
        public string CodigoUFUnidadeEnsino { get; set; }
        public string CodigoNivel { get; set; }
        public string CodigoConveniada { get; set; }
        public DateTime? DataSolicitacao { get; set; }
        public string TotaldisciplinaGrade { get; set; }
        public string HorasCurriculo { get; set; }
        public char? AlunoBolsista { get; set; }
        public DateTime? DataFimCurso { get; set; }
        public string DescricaoMunicipioUnidade { get; set; }
        public bool? TemDisciplinaDeCumprimento { get; set; }
        public bool? AlunoDeTransferencia { get; set; }
        public string SituacaoMatricula { get; set; }
        public string TipoUnidadeEnsino { get; set; }
        public bool? AlunoEhOnline { get; set; }
        public string unidadeEnsinoPerfil { get; set; }
        public string Frente { get; set; }
        public bool CursoAlunoPossuiTeseDissertacao { get; set; }
        public string CursoModalidade { get; set; }
        public string UfUnidade { get; set; }
        public DateTime? DataInicioCurso { get; set; }
        public string DtLimiteConclusao { get; set; }
        public DateTime? DataReaberturaTrancamento { get; set; }
        public string Codigo { get; set; }
        public string CodigoTurmaPreferencial { get; set; }
        public string CodigoInstituicao { get; set; }
        public string CodigoCurso { get; set; }
        public long? CodigoPessoa { get; set; }
        public short? CodigoSerie { get; set; }
        public string CodigoTurno { get; set; }
        public DateTime? DataIngresso { get; set; }
        public string EmailAcademico { get; set; }
        public string CodigoCurriculo { get; set; }
        public string CodigoUnidadeFisica { get; set; }
        public string CodigoUnidadeEnsino { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Passaporte { get; set; }
        public string TipoMatricula { get; set; }
        public int? AnoUltimaFormacao { get; set; }
        public string SituacaoAluno { get; set; }
        public bool? TrancamentoPorIntervalo { get; set; }
        public DateTime? DataInicioTrancamento { get; set; }
        public DateTime? DataFimTrancamento { get; set; }
        public string Disciplina { get; set; }
        public string NomeDisciplina { get; set; }
    }
}