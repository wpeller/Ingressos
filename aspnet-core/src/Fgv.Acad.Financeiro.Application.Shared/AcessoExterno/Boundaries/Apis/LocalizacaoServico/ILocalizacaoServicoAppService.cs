using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico.Dto;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico
{
    public interface ILocalizacaoServicoAppService : IApplicationService
    {
        Task<ValidarGlobalizacaoDto> ValidarGlobalizacao(ValidarGlobalizacaoDto _validar);
    }
}
