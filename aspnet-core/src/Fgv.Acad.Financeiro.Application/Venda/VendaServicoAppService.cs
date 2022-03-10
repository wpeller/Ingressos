using Fgv.Acad.Financeiro.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Eventos
{
    public class VendaServicoAppService : FinanceiroAppServiceBase, IVendaServicoAppService
    {
        private readonly IVendasManager _vendaManager;
        private readonly IClienteManager _clienteManager;
        private readonly IEventoManager _tipoIngresosManager;


        public VendaServicoAppService(IVendasManager eventoManager
            , IClienteManager clienteManager
            , IEventoManager tipoIngresosManager)
        {

            _vendaManager = eventoManager;
            _clienteManager = clienteManager;
            _tipoIngresosManager = tipoIngresosManager;
        }

        public GenericResultObject<List<VendaDto>> ObterTodos()
        {

            List<Venda> listaEvento = _vendaManager.ObterTodos().Result;
            List<VendaDto> listaEventosDto = ObjectMapper.Map<List<VendaDto>>(listaEvento);

            GenericResultObject<List<VendaDto>> retorno = new GenericResultObject<List<VendaDto>>
            {
                Item = listaEventosDto
            };

            return retorno;

        }
 


        public GenericResultObject<long> Salvar(NovaVendaDto novaVenda)
        {
            long id = 0;



            GenericResultObject<long> retorno = new GenericResultObject<long>(0, true, string.Empty);


            Venda venda = new Venda();

            venda.Cliente =   _clienteManager.ObterCliente(novaVenda.cpf).Result ;
            //venda.TipoIngresso = _tipoIngresosManager.ObterCliente(novaVenda.cpf).Result;

            venda.TipoIngresso = new TipoIngresso { Id = 1 };

            id = _vendaManager.SalvarOuAlterar(venda).Result;

            retorno.Mensagem = "Venda salva com sucesso.";


            retorno.Item = id;
            retorno.Sucesso = true;
            return retorno;

        }



    }
}
