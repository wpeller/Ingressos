using Abp.Authorization;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.WebServices.UnidadeServico;
using Fgv.Acad.Financeiro.Dto;
using Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces;
using Fgv.Acad.Financeiro.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fgv.Acad.Financeiro.Relatorios
{

    [AbpAuthorize()]
    public class PlanoComExcessoDeParcelamentoAppService : FinanceiroDomainServiceBase, IPlanoComExcessoDeParcelamentoAppService
    {
        private readonly IPlanoComExcessoDeParcelamentoExcelExporter _planoComExcessoDeParcelamentoExcelExporter;
        private readonly IPlanoComExcessoDeParcelamentoPdfExporter _planoComExcessoDeParcelamentoPdfExporter;
        private readonly IFinanceiroAppService _financeiroServico;
        private readonly IUnidadeServicoAppService _unidadeServico;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public PlanoComExcessoDeParcelamentoAppService(
            IPlanoComExcessoDeParcelamentoExcelExporter planoComExcessoDeParcelamentoExcelExporter,
            IPlanoComExcessoDeParcelamentoPdfExporter planoComExcessoDeParcelamentoPdfExporter,
            IFinanceiroAppService financeiroServico,
            IUnidadeServicoAppService unidadeServico,
            ITempFileCacheManager tempFileCacheManager)
        {
            _planoComExcessoDeParcelamentoExcelExporter = planoComExcessoDeParcelamentoExcelExporter;
            _planoComExcessoDeParcelamentoPdfExporter = planoComExcessoDeParcelamentoPdfExporter;
            _tempFileCacheManager = tempFileCacheManager;
            _financeiroServico = financeiroServico;
            _unidadeServico = unidadeServico;
        }

        public FileDto ExportToFileExcel(int totalRegistrosDaConsulta, FiltroPlanoComExcessoDeParcelamentoDto filtro)
        {
            this.AjustarFlagsPresencialOuOnline(ref filtro);
            var arquivo = _planoComExcessoDeParcelamentoExcelExporter.ExportToFile(totalRegistrosDaConsulta, filtro);
            return arquivo;
        }

        public FileDto ExportToFilePDF(FiltroPlanoComExcessoDeParcelamentoDto filtro)
        {
            this.AjustarFlagsPresencialOuOnline(ref filtro);
            var retorno = _planoComExcessoDeParcelamentoPdfExporter.ExportarPDF(filtro);
            return retorno;
        }

        public Task<OutputPlanoComExcessoDeParcelamentoDto> ObterAlunosComExcessoDeParcelamento(FiltroPlanoComExcessoDeParcelamentoDto filtro)
        {
            this.AjustarFlagsPresencialOuOnline(ref filtro);
            var retorno = _financeiroServico.ObterAlunosComExcessoDeParcelamento(filtro);
            return retorno;
        }

        private void AjustarFlagsPresencialOuOnline(ref FiltroPlanoComExcessoDeParcelamentoDto input)
        {
            var configuracaoPapel = _unidadeServico.BuscarPapelPorTipo(input.Mnemonico).Result;

            if (configuracaoPapel.PapelEhAcademicoOuFinanceiro)
            {
                input.EhPresencial = true;
                input.EhOnline = false;
            }
            else if (configuracaoPapel.PapelEhSecretariaOuFinanceiroOuCoordenacaoFGVOnline)
            {
                input.EhOnline = true;
                input.EhPresencial = false;
            }
            else if (configuracaoPapel.PapelEhSuperintendenciaNucleo
                    || configuracaoPapel.PapelEhSuperintendenciaNucleoRJ
                    || configuracaoPapel.PapelEhSuperintendenciaNucleoSP
                    || configuracaoPapel.PapelEhSuperintendenciaNucleoBH
                    || configuracaoPapel.PapelEhSuperintendenciaNucleoBR)
            {
                input.EhPresencial = true;
                input.EhOnline = false;
            }
            else
            {
                input.EhPresencial = true;
                input.EhOnline = true;
            }

            if (input.ListaDeUnidades == null || input.ListaDeUnidades.Count <= 0)
            {
                var unidades = _unidadeServico.TrataUnidades(input.Mnemonico).Result;
                input.ListaDeUnidades = unidades;
            }
        }
    }
}
