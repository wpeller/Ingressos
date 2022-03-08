using System.Collections.Generic;
using Fgv.Acad.Financeiro.Authorization.Users.Dto;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}