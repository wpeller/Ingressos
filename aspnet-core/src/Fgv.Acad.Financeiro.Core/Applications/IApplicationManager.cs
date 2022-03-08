using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Fgv.Acad.Financeiro.Applications
{
    public interface IApplicationManager : IDomainService
    {
        IQueryable<Application> Applications { get; }

        Task CreateAsync(Application application);

        Task DeleteAsync(Application application);

        Task<Application> GetByIdAsync(Guid id);

        Task<Application> GetByNameAsync(string name);

        Application GetByName(string name);

        Task<List<Application>> GetAllAsync();

        Task UpdateAsync(Application application);

        string CreateToken(string application, string secretword, string username);

        string CreateToken(string application, string secretword, string username, string data);

        string ValidateToken(string token);

        string ValidateToken(string token, string secretword, int secondstoexpire);
    }
}