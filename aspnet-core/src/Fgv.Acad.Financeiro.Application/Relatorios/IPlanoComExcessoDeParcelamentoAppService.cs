using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Relatorios
{
    public interface IPlanoComExcessoDeParcelamentoAppService: IApplicationService
    {
        FileDto ExportToFileExcel(int totalRegistrosDaConsulta, FiltroPlanoComExcessoDeParcelamentoDto filtro);
        FileDto ExportToFilePDF(FiltroPlanoComExcessoDeParcelamentoDto filtro);
        Task<OutputPlanoComExcessoDeParcelamentoDto> ObterAlunosComExcessoDeParcelamento(FiltroPlanoComExcessoDeParcelamentoDto input);

    }
}
