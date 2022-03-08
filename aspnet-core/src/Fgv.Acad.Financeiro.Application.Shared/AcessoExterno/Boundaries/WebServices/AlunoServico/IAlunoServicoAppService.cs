using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.AlunoServico
{
    public interface IAlunoServicoAppService : IApplicationService
    {
        Task<OutputRetornoAlunoDto> ObterAlunosPor(FiltroAlunoInput filtro);
        
    }
}
