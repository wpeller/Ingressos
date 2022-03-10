using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.Dto
{
    public class GenericVoid
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public GenericVoid(bool _Sucesso = true, string _Mensagem = null)
        {
            Sucesso = _Sucesso;
            Mensagem = _Mensagem;
        }
    }
}
