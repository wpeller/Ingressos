using Abp.Application.Services;
using Abp.Dependency;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro
{
    public interface IFinanceiroAppService: ITransientDependency
    {
        Task<OutputPlanoComExcessoDeParcelamentoDto> ObterAlunosComExcessoDeParcelamento(FiltroPlanoComExcessoDeParcelamentoDto input);
    }
}
