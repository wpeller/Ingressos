using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.Authorization.Permissions.Dto;

namespace Fgv.Acad.Financeiro.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
