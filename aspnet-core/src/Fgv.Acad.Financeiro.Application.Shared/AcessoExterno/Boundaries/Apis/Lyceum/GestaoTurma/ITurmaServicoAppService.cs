using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoTurma
{
    public interface ITurmaServicoAppService : IApplicationService
    {
        Task<OutputListaTurmasAptasParaTrancamentoDisciplinaDto> ListarTurmasAptasParaTrancamentoDisciplina(InputListarTurmasAptasParaTrancamentoDisciplinaDto input);
        Task<string> ValidarSeListaTurmaDisciplinaEhRetroativa(List<string> listaDeTurmas, string codigoDisciplina);
    }
}
