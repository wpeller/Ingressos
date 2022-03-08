using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.Applications.Dtos;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Applications
{
    public interface IApplicationAppService : IApplicationService 
    {
        Task<PagedResultDto<GetApplicationForView>> GetAll(GetAllApplicationsInput input);

		Task<GetApplicationForEditOutput> GetApplicationForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditApplicationDto input);

		Task Delete(EntityDto<Guid> input);
        Task<ApplicationTokenOutput> CreateToken(ApplicationTokenData input);
        Task<string> ValidateToken(string token);



    }
}