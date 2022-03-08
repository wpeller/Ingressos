using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico
{
    public interface IUnidadeServicoAppService : IApplicationService
	{
		Task<OutputUnidadeEnsinoDto> ObterUnidadesPor(FiltroUnidadeInput input);
		Task<OutputPapelPorTipoDto> BuscarPapelPorTipo(string mnemonico);
		Task<List<string>> TrataUnidades(string mnemonico);
	}
}
