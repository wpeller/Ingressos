
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Fgv.Acad.Financeiro.Applications.Dtos;
using Fgv.Acad.Financeiro.Dto;
using Abp.Application.Services.Dto;
using Fgv.Acad.Financeiro.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fgv.Acad.Financeiro.Applications
{
    
    public class ApplicationAppService : FinanceiroAppServiceBase, IApplicationAppService
    {
        private readonly IRepository<Application, Guid> _applicationRepository;
        private readonly IApplicationManager _applicationManager;
        
        public ApplicationAppService(IRepository<Application, Guid> applicationRepository, IApplicationManager applicationManager)
        {
            _applicationRepository = applicationRepository;
            _applicationManager = applicationManager;
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
		public async Task<PagedResultDto<GetApplicationForView>> GetAll(GetAllApplicationsInput input)
        {

            var filteredApplications = _applicationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Name.Contains(input.Filter) || e.UrlPath.Contains(input.Filter) || e.SecretWordEncrypted.Contains(input.Filter))
                        .WhereIf(input.MinSecondsToExpireFilter != null, e => e.SecondsToExpire >= input.MinSecondsToExpireFilter)
                        .WhereIf(input.MaxSecondsToExpireFilter != null, e => e.SecondsToExpire <= input.MaxSecondsToExpireFilter);


            var query = (from o in filteredApplications
                         select new GetApplicationForView()
                         {
                             Application = ObjectMapper.Map<ApplicationDto>(o)
                         });

            var totalCount = await query.CountAsync();

            var applications = await query
                .OrderBy(input.Sorting ?? "application.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetApplicationForView>(
                totalCount,
                applications
            );
        }

        [AbpAuthorize(AppPermissions.Pages_Applications, 
	        AppPermissions.Pages_Applications_Edit)]
        public async Task<GetApplicationForEditOutput> GetApplicationForEdit(EntityDto<Guid> input)
        {
            var application = await _applicationManager.GetByIdAsync(input.Id);

            var output = new GetApplicationForEditOutput { Application = ObjectMapper.Map<CreateOrEditApplicationDto>(application) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Applications)]
		public async Task CreateOrEdit(CreateOrEditApplicationDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Applications, 
	        AppPermissions.Pages_Applications_Create)]
        private async Task Create(CreateOrEditApplicationDto input)
        {
            var application = ObjectMapper.Map<Application>(input);



            await _applicationRepository.InsertAsync(application);
        }

        [AbpAuthorize(AppPermissions.Pages_Applications, 
	        AppPermissions.Pages_Applications_Edit)]
        private async Task Update(CreateOrEditApplicationDto input)
        {
            var application = await _applicationRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, application);

            await _applicationRepository.UpdateAsync(application);
        }

        [AbpAuthorize(AppPermissions.Pages_Applications, 
	        AppPermissions.Pages_Applications_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _applicationRepository.DeleteAsync(input.Id);
        }

		public async Task<ApplicationTokenOutput> CreateToken(ApplicationTokenData input)
		{
			var user = await GetCurrentUserAsync();
			var app = _applicationManager.GetByName(input.Modulo.ToLower());

			if (app == null)
			{
				throw new UserFriendlyException(L("ApplicationDoesNotExists"));
			}

			var output = new ApplicationTokenOutput()
			{
				Token = _applicationManager.CreateToken(app.Name, app.SecretWord, user.UserName,
					JsonConvert.SerializeObject(input)),
				Url = app.UrlPath
			};
			return output;
		}

		public async Task<string> ValidateToken(string token)
        {
            return await Task.Run(() => _applicationManager.ValidateToken(token));
        }
    }
}
