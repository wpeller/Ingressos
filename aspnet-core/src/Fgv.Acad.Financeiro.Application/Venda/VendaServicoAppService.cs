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

        public GenericResultObject<List<VendaDto>> ObterTodosNaoEstornados()
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
            Venda vendaOutput;



            GenericResultObject<long> retorno = new GenericResultObject<long>(0, true, string.Empty);


            Venda venda = new Venda();

            Cliente cliente = _clienteManager.ObterCliente(novaVenda.cpf).Result;

            venda.IdTipoIngresso = 1;
            venda.IdCliente = cliente.Id;

            vendaOutput = _vendaManager.SalvarOuAlterar(venda).Result;

            retorno.Mensagem = "Venda salva com sucesso.";


            retorno.Item = vendaOutput.Id;
            retorno.Sucesso = true;
            return retorno;

        }


        public GenericResultObject<long> EstornarVenda(VendaEstornoDto vendaEstorno)
        {
            Venda venda;

            GenericResultObject<long> retorno = new GenericResultObject<long>(0, true, string.Empty);


            if (vendaEstorno.idVenda == 0) {
                retorno.Sucesso = false;
                retorno.Mensagem = "Id não informado.";
                return retorno;
            }

            venda = _vendaManager.SalvarEstorno(vendaEstorno.idVenda).Result;

            retorno.Mensagem = "Venda estornada com sucesso.";


            retorno.Item = venda.Id;
            retorno.Sucesso = true;
            return retorno;

        }

    }
}
