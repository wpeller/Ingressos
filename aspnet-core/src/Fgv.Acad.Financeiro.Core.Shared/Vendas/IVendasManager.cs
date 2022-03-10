using Abp.Dependency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IVendasManager: ITransientDependency
    {
        Task<List<Venda>> ObterTodos();
        Task<Venda> SalvarEstorno(long idVenda);
        Task<Venda> SalvarOuAlterar(Venda venda);
    }
}