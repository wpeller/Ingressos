using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{


    public class EventoManager : IEventoManager
    {

        private readonly IRepository<Evento, long> _eventoRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public EventoManager(IRepository<Evento,long> repository,
                                        IUnitOfWorkManager unitOfWorkManager)
        {
            _eventoRepository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task< List<Evento>> ObterTodosAtivos() {

            return _eventoRepository.GetAll().ToList();


        }


        public async Task<long> SalvarOuAlterar(Evento evento)
        {


            if (evento != null)
            {

                Evento output = await _eventoRepository.InsertOrUpdateAsync(evento);

                _unitOfWorkManager.Current.SaveChanges();

                return output.Id;

            }

            return 0;

        }

    }
}
