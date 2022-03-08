using Abp.Application.Services.Dto;

namespace Fgv.Acad.Financeiro.Authorization.Accounts.Dto
{
    //### This class is mapped in CustomDtoMapper ###
    public class CurrentTenantInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}