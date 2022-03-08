using System;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica.Dto
{
    public class AlunosAptosParaTrancamentoDisciplinaDto
    {
        public string CodigoAluno { get; set; }
        public string NomeAluno { get; set; }
        public string SituacaoMatricula { get; set; }
        public string Turma { get; set; }
        public string Disciplina { get; set; }
        public string SituacaoAluno { get; set; }
        public string Pendencia { get; set; }
        public string Cumprimento { get; set; }
        public DateTime? DataInicioTurma { get; set; }
        public DateTime? DataFimTurma { get; set; }
    }
}
