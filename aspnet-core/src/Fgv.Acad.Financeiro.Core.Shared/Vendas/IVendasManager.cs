using Abp.Dependency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IVendasManager: ITransientDependency
    {
        Task<List<Venda>> ObterTodos();
        Task<long> SalvarOuAlterar(Venda venda);
    }
}