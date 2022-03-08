using System.Threading.Tasks;
using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico.Dto;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.AutorizacaoServico
{
    public interface IAutorizacaoServicoAppService : IApplicationService
    {
        Task<FuncionalidadesPermissoes> ObterPermissoes(AutorizacaoInput input);
    }
}
