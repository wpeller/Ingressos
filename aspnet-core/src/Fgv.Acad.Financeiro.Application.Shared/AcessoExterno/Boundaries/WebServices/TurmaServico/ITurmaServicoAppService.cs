using Abp.Application.Services;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.TurmaServico
{
	public interface ITurmaServicoAppService : IApplicationService
	{		
		Task<OutputRetornoTurmaDto> ObterTurmaPor(FiltroTurmaInput filtro);
	}
}
