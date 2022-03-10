using Fgv.Acad.Financeiro.Dto;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IVendaServicoAppService
    {
        GenericResultObject<long> EstornarVenda(VendaEstornoDto vendaEstorno);
        GenericResultObject<List<VendaDto>> ObterTodosNaoEstornados();
    }
}