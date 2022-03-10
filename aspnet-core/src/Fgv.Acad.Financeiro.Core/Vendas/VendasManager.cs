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


        public async Task<List<Venda>> ObterTodos()
        {

            return await _repository.GetAll()
                  .Include(t => t.Cliente).ThenInclude(ta => ta.Vendas)
                 .Include(t => t.TipoIngresso).ThenInclude(ta => ta.Vendas)
                 .Where(c => c.DataCancelamentoVenda.HasValue == false)
                .ToListAsync();


        }


        public Venda ObterPorId(long id)
        {

            return _repository.GetAll()
                  .Where(c => c.DataCancelamentoVenda.HasValue == false)
                  .Where(c => c.Id == id)
                .FirstOrDefault();


        }


        public async Task<Venda> SalvarOuAlterar(Venda venda)
        {


            if (venda != null)
            {

                Venda output = await _repository.InsertOrUpdateAsync(venda);

                _unitOfWorkManager.Current.SaveChanges();

                return output;

            }

            return null;

        }



        public async Task<Venda> SalvarEstorno(long idVenda)
        {


            if (idVenda != 0)
            {
                Venda venda = this.ObterPorId(idVenda);

                venda.DataCancelamentoVenda = DateTime.Now;

                Venda output = await _repository.UpdateAsync(venda);

                _unitOfWorkManager.Current.SaveChanges();

                return output;

            }

            return null;

        }



    }
}
