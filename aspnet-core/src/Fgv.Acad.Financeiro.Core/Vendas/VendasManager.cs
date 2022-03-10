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


    public class VendasManager : IVendasManager
    {

        private readonly IRepository<Venda, long> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public VendasManager(IRepository<Venda, long> repository,
                                        IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task< List<Venda>> ObterTodos() {

            return await _repository.GetAll()
                .ToListAsync();


        }


        public async Task<long> SalvarOuAlterar(Venda venda)
        {


            if (venda != null)
            {

                Venda output = await _repository.InsertOrUpdateAsync(venda);

                _unitOfWorkManager.Current.SaveChanges();

                return output.Id;

            }

            return 0;

        }

 

    }
}
