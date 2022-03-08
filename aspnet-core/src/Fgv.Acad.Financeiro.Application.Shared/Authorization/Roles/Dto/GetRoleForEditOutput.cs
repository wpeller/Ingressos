using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.Authorization.Permissions.Dto;

namespace Fgv.Acad.Financeiro.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}