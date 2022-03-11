using Fgv.Acad.Financeiro.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class EventoServicoAppService : FinanceiroAppServiceBase, IEventoServicoAppService 
    {
        private readonly IEventoManager _eventoManager;


        public EventoServicoAppService(IEventoManager eventoManager)
        {

            _eventoManager = eventoManager;
        }

        public GenericResultObject<List<EventoDto>> ObterTodosAtivos()
        {

            List<Evento> listaEvento = _eventoManager.ObterTodosAtivos().Result;
            List<EventoDto> listaEventosDto = ObjectMapper.Map<List<EventoDto>>(listaEvento);

            GenericResultObject<List<EventoDto>> retorno = new GenericResultObject<List<EventoDto>>
            {
                Item = listaEventosDto
            };

            return retorno;

        }

        public GenericResultObject<EventoDto> ObterPorId(long id)
        {
            GenericResultObject<EventoDto> retorno = new GenericResultObject<EventoDto>();

            if (id == 0) {
                retorno.Sucesso = false;
                retorno.Mensagem = "Id não informado";
                return retorno;
            }


            Evento listaEvento = _eventoManager.ObterEvento(id).Result;
            EventoDto listaEventosDto = ObjectMapper.Map<EventoDto>(listaEvento);

            retorno.Item = listaEventosDto;

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
