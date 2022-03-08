namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto
{
    public class FiltroInfoTurmasDisciplinasDto
    {

        public string codigoDisciplina { get; set; }
        public string codigoTurma { get; set; }
        public EstrategiaListarInfoTurmasDisciplinas? estrategiaListarInfoTurmasDisciplinas { get; set; }
        public PaginacaoDto paginacaoDto { get; set; }
    }


    public enum EstrategiaListarInfoTurmasDisciplinas
    {
        PorIntervaloEntreDatasDeInicio,
        PorIntervaloEntreDatasDeInicioENomeDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoTurmaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoCurso,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoCursoECodigoDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoCursoENomeDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoCursoECodigoTurmaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsino,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoENomeDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoUnidadeEnsinoECodigoTurmaLike,
        PorIntervaloEntreDatasDeInicioECodigoCurso,
        PorIntervaloEntreDatasDeInicioECodigoCursoENomeDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoCursoECodigoDisciplinaLike,
        PorIntervaloEntreDatasDeInicioECodigoCursoECodigoTurmaLike,
        PorInepPresencial,
        PorInepPresencialECodigoCurso,
        PorInepPresencialECodigoTurma,
        PorInepPresencialECodigoTurmaLike,
        PorInepPresencialENomeCursoLike,
        PorInepPresencialENomeDisciplinaLike,
        PorTurma,
        PorCodigoTurmaCodigoDisciplinaAnoPeriodo,
        PorListaGruposCursoNomeMunicipioCodigoDisciplina,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioCodigoUnidadeEnsinoLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioNomeUnidadeEnsinoLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioCodigoTurmaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioNomeUnidadeEnsinoLikeCodigoTurmaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioNomeDisciplinaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioCodigoTurmaLikeNomeDisciplinaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioCodigoUnidadeEnsinoLikeNomeDisciplinaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioCodigoUnidadeEnsinoLikeCodigoTurmaLikeNomeDisciplinaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipioNomeUnidadeEnsinoLikeCodigoTurmaLikeNomeDisciplinaLike,
        PorListaGruposCursoIntervaloEntreDatasDeInicioNomeMunicipio,
        PorCodTurmaAmbienteAprendizado,
        PorCodTurmaAmbienteAprendizadoLikeEAno
    }

    public class PaginacaoDto
    {
        public int inicio { get; set; }
        public int total { get; set; }
    }
}
