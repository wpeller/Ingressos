using Fgv.Acad.Financeiro.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IEventoServicoAppService
    {
        GenericResultObject<List<Evento>> ObterTodosAtivos();
    }
}