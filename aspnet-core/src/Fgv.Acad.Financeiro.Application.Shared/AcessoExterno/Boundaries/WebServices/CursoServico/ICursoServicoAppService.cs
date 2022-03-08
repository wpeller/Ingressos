using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.CursoServico
{
    public interface ICursoServicoAppService : IApplicationService
    {
        Task<List<ProgramaCursoDto>> ObterProgramaCurso();
        Task<OutputCursoDto> ObterCursoPor(InputFiltroCursoDto filtro);
        Task<OutputCurriculoCursoDto> ObterCurriculoCursoPor(InputFiltroCurriculoCursoDto filtro);
    }
}
