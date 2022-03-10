using Abp.Dependency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IEventoManager : ITransientDependency
    {
        Task<List<Evento>> ObterTodosAtivos();
        Task<long> SalvarOuAlterar(Evento evento);
    }
}