using Fgv.Acad.Financeiro.Dto;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IVendaServicoAppService
    {
        GenericResultObject<List<VendaDto>> ObterTodos();
    }
}