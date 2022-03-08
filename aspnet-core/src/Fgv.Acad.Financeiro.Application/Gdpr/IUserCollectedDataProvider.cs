using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Fgv.Acad.Financeiro.Dto;

namespace Fgv.Acad.Financeiro.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
