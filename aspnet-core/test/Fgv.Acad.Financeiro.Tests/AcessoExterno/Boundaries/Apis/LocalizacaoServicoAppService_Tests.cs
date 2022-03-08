using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.LocalizacaoServico.Dto;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Fgv.Acad.Financeiro.Tests.AcessoExterno.Boundaries.Apis
{
    public class LocalizacaoServicoAppService_Tests : AppTestBase
    {
        private readonly ILocalizacaoServicoAppService _service;
        public LocalizacaoServicoAppService_Tests()
        {
            _service = Resolve<ILocalizacaoServicoAppService>();
        }

        [FactCustom]
        public async Task Test_ValidarGlobalizacao()
        {
            var _data = DateTime.Now;
            var _result = await _service.ValidarGlobalizacao(new ValidarGlobalizacaoDto() { Data = _data });

            var format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFK";
            Assert.True(_data.ToString(format) == _result.Data.ToString(format));
        }
    }
}
