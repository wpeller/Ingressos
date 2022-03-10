using System;
using System.Collections.Generic;
using System.Text;

namespace Fgv.Acad.Financeiro.Dto
{
    public class GenericResultObject<T> : GenericVoid
    {
        public T Item { get; set; }

        public GenericResultObject() : base()
        {
            Item = default(T);
        }

        public GenericResultObject(T _Item, bool _Sucesso = true, string _Mensagem = null) : base(_Sucesso, _Mensagem)
        {
            Item = _Item;
        }
    }
}
