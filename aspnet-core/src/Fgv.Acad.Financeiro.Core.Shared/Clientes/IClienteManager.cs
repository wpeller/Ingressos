using Abp.Dependency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IClienteManager : ITransientDependency
    {
        Task<Cliente> ObterCliente(string cpf);
        Task<List<Cliente>> ObterTodos();
        Task<long> SalvarOuAlterar(Cliente cliente);
    }
}