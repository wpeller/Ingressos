using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.Dto
{
    public class GenericResultList<T> : GenericVoid
    {
        public List<T> Lista { get; set; }

        public GenericResultList(List<T> _Lista = null, bool _Sucesso = true, string _Mensagem = null) : base(_Sucesso, _Mensagem)
        {
            Lista = _Lista ?? new List<T>();
        }
    }
}
