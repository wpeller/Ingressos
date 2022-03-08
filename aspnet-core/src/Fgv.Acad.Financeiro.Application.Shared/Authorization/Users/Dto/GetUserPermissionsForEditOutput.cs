using System.Collections.Generic;
using Fgv.Acad.Financeiro.Authorization.Permissions.Dto;

namespace Fgv.Acad.Financeiro.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}