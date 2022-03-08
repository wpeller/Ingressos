using System.Collections.Generic;
using Fgv.Acad.Financeiro.Auditing.Dto;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
