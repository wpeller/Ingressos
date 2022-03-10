using Fgv.Acad.Financeiro.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class ClienteServicoAppService : FinanceiroAppServiceBase, IClienteServicoAppService
    {
        private readonly IClienteManager _clienteManager;


        public ClienteServicoAppService(IClienteManager eventoManager)
        {

            _clienteManager = eventoManager;
        }

        public GenericResultObject<List<ClienteDto>> ObterTodos()
        {

            List<Cliente> listaEvento = _clienteManager.ObterTodos().Result;
            List<ClienteDto> listaEventosDto = ObjectMapper.Map<List<ClienteDto>>(listaEvento);

            GenericResultObject<List<ClienteDto>> retorno = new GenericResultObject<List<ClienteDto>>
            {
                Item = listaEventosDto
            };

            return retorno;

        }

        public GenericResultObject<ClienteDto> ObterPorCpf(string  cpf)
        {
            GenericResultObject<ClienteDto> retorno = new GenericResultObject<ClienteDto>();

            if (string.IsNullOrEmpty(cpf)) {
                retorno.Sucesso = false;
                retorno.Mensagem = "Cpf não informado.";
                return retorno;
            }


            Cliente listaEvento = _clienteManager.ObterCliente(cpf).Result ;
            ClienteDto listaEventosDto = ObjectMapper.Map<ClienteDto>(listaEvento);

            retorno.Item = listaEventosDto;

            return retorno;

        }


        public GenericResultObject<long> SalvarOuAlterar(ClienteDto eventoDto)
        {
            long id = 0;
            Cliente evento;

            GenericResultObject<long> retorno = new GenericResultObject<long>(0, true, string.Empty);



            evento = ObjectMapper.Map<Cliente>(eventoDto);

            id = _clienteManager.SalvarOuAlterar(evento).Result;



            retorno.Mensagem = "Cliente salvo com sucesso.";


            retorno.Item = id;
            retorno.Sucesso = true;
            return retorno;

        }



    }
}
