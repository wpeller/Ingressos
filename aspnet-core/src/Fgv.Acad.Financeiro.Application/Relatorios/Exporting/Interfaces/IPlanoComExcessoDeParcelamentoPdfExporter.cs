using Abp.Dependency;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces
{
    public interface IPlanoComExcessoDeParcelamentoPdfExporter: ITransientDependency
    {
        FileDto ExportarPDF(FiltroPlanoComExcessoDeParcelamentoDto filtro);
        FileDto ExportarPDF(InputPlanoComExcessoDeParcelamentoDto input);
    }
}
