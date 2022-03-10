using Fgv.Acad.Financeiro.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class EventoServicoAppService : FinanceiroAppServiceBase ,  IEventoServicoAppService
    {
        private IEventoManager _eventoManager;

        public EventoServicoAppService(IEventoManager eventoManager) {

            _eventoManager = eventoManager;
        }

        public  GenericResultObject<List<Evento>>  ObterTodosAtivos() {

            GenericResultObject<List<Evento>> retorno = new GenericResultObject<List<Evento>>
            {
                Item = _eventoManager.ObterTodosAtivos().Result
            };

            return retorno;

        }



        public GenericResultObject<long> SalvarOuAlterar(EventoDto eventoDto)
        {
            long id = 0;
            Evento evento;

            GenericResultObject<long> retorno = new GenericResultObject<long>(0, true, string.Empty);



            evento = ObjectMapper.Map<Evento>(eventoDto);

            id = _eventoManager.SalvarOuAlterar(evento).Result;
             
            

            retorno.Mensagem = "Evento salvo com sucesso.";

            
            retorno.Item = id;
            retorno.Sucesso = true;
            return retorno;

        }



    }
}
