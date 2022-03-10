using Fgv.Acad.Financeiro.Dto;
using System.Collections.Generic;

namespace Fgv.Acad.Financeiro.Eventos
{
    public interface IClienteServicoAppService
    {
        GenericResultObject<ClienteDto> ObterPorCpf(string cpf);
        GenericResultObject<List<ClienteDto>> ObterTodos();
        GenericResultObject<long> SalvarOuAlterar(ClienteDto eventoDto);
    }
}