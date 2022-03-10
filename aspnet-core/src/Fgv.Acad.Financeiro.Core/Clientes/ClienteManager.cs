using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{


    public class ClienteManager : IClienteManager
    {

        private readonly IRepository<Cliente, long> _eventoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ClienteManager(IRepository<Cliente, long> repository,
                                        IUnitOfWorkManager unitOfWorkManager)
        {
            _eventoRepository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task< List<Cliente>> ObterTodos() {

            return await _eventoRepository.GetAll()
                .ToListAsync();


        }


        public async Task<long> SalvarOuAlterar(Cliente cliente)
        {


            if (cliente != null)
            {

                Cliente output = await _eventoRepository.InsertOrUpdateAsync(cliente);

                _unitOfWorkManager.Current.SaveChanges();

                return output.Id;

            }

            return 0;

        }


        public async Task<Cliente> ObterCliente(string cpf)
        {

            return _eventoRepository.GetAll()                 
                 .Where(x => x.CPF.Equals(cpf))
                .FirstOrDefault();


        }

    }
}
