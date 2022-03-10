using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IEventoManager
    {
        Task<List<Evento>> ObterTodosAtivos();
        Task<long> SalvarOuAlterar(Evento evento);
    }
}