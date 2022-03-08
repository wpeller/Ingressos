using Abp.Dependency;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces
{
    public interface IPlanoComExcessoDeParcelamentoExcelExporter: ITransientDependency
    {
        FileDto ExportToFile(int totalRegistrosDaConsulta, FiltroPlanoComExcessoDeParcelamentoDto filtro);
        FileDto ExportToFile(InputPlanoComExcessoDeParcelamentoDto input);

    }
}
