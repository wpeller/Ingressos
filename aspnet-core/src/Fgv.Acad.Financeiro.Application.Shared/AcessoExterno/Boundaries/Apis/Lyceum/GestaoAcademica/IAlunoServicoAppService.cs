using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoAcademica
{
    public interface IAlunoServicoAppService : IApplicationService
    {
        Task<OutputListaAlunosAptosParaTrancamentoDisciplinaDto> ListarAlunosAptosParaTrancamentoDisciplina(InputListarAlunosAptosParaTrancamentoDisciplinaDto input);
    }
}
