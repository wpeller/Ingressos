using Abp.Authorization;
using Abp.Extensions;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Abp.UI;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.Configuration;
using Fgv.Acad.Financeiro.DataExporting.Excel.EpPlus;
using Fgv.Acad.Financeiro.Dto;
using Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces;
using Fgv.Acad.Financeiro.Storage;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Drawing;

namespace Fgv.Acad.Financeiro.Relatorios.Exporting
{
    public class PlanoComExcessoDeParcelamentoExcelExporter : EpPlusExcelExporterBase, IPlanoComExcessoDeParcelamentoExcelExporter
    {
        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;
        private readonly IFinanceiroAppService _financeiroServico;
        private readonly IAppConfigurationAccessor _configuration;

        public PlanoComExcessoDeParcelamentoExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession, IFinanceiroAppService financeiroServico,
            ITempFileCacheManager tempFileCacheManager, IAppConfigurationAccessor configuration)
            : base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
            _financeiroServico = financeiroServico;
            _configuration = configuration;
        }

        public FileDto ExportToFile(int totalRegistrosDaConsulta, FiltroPlanoComExcessoDeParcelamentoDto filtro)
        {
            // garantindo o numero de registros
            filtro.TotalDeRegistrosPorPagina = 1000000;
            filtro.Skip = 0;

            var caminhoStorage = _configuration.Configuration["FamiliaArquivo:CaminhoStorage"];

            var resultado = _financeiroServico.ObterAlunosComExcessoDeParcelamento(filtro).Result;

            try
            {
                var file = CreateExcelPackage(
                    "RelatorioPlanosComExcessoDeParcelamento.xlsx",
                    excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add("Relatório de planos com excesso de parcelamento");
                        sheet.PrinterSettings.Orientation = eOrientation.Landscape;
                        sheet.OutLineApplyStyle = true;

                        Image logo = Image.FromFile(caminhoStorage + "logo_fgv_formacao_executiva.png");

                        var picture = sheet.Drawings.AddPicture("logo_fgv_formacao_executiva.png", logo);
                        picture.SetPosition(0, 0, 0, 0);

                        sheet.View.ShowGridLines = false;
                        sheet.OutLineApplyStyle = true;

                        sheet.Cells.Style.Font.Size = 10;
                        sheet.Cells.Style.Font.Name = "Arial";

                        sheet.Cells[5, 1, 5, 6].Merge = true;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Bold = true;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Size = 10;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Name = "Arial";
                        sheet.Cells[5, 1, 5, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        sheet.Cells[5, 1, 5, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        sheet.Cells[5, 1].Value = "Relatório de planos com excesso de parcelamento";                        

                        sheet.View.ShowGridLines = false;
                        sheet.Cells[6, 1, 6, 3].Merge = true;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Bold = true;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Size = 8;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Name = "Arial";
                        sheet.Cells[6, 1, 6, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[6, 1, 6, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        sheet.Cells[6, 1].Value = "Data / hora geração: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                        

                        using (ExcelRange Rng = sheet.Cells[7, 1, 7, 11])
                        {
                            Rng.Style.Font.Name = "Arial";
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Color.SetColor(Color.White);
                            Rng.Style.Font.Size = 10;
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(70, 130, 180));
                            Rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        // sheet.Row(7).Height = 29.40;

                        int startRow = 7;
                        int startColumn = 1;

                        AddHeader(
                           sheet,
                           startRow,
                           startColumn,
                           ("Unidade"),
                           ("Turma"),
                           ("Curso"),
                           ("Aluno"),
                           ("CPF"),
                           ("Matrícula"),
                           ("Responsável Financeiro"),
                           ("Total Parcelas"),
                           ("Parc. Excesso"),
                           ("Número da IN"),
                           ("Data Matrícula")
                       );

                        AddObjects(
                            sheet, 8, resultado.Itens,
                            _ => _.UnidadeFisica,
                            _ => _.Turma,
                            _ => _.NomeCurso,
                            _ => _.NomeAluno,
                            _ => _.CPF,
                            _ => _.Matricula,
                            _ => _.ResponsavelFinanceiro,
                            _ => _.NumeroDeCobrancas,
                            _ => _.NumeroDeParcelasEmExcesso,
                            _ => _.IdentificadorNormaPrecoMinimo + '/' + _.AnoNormaPrecoMinimo,
                            _ => _.DataMatricula.ToString ("dd/MM/yyyy")
                        );

                        // Formatting cells
                        // var timeColumn = sheet.Column(8);
                        // timeColumn.Style.Numberformat.Format = "dd-MM-yyyy";

                        sheet.Column(5).Width = 12;

                        for (var i = 1; i <= 11; i++)
                        {
                            if (i.IsIn(5)) //Don't AutoFit Parameters and Exception
                            {
                                continue;
                            }

                            sheet.Column(i).AutoFit();
                        }
                    });

                return file;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message, ex);
            }

            return null;
        }

        public FileDto ExportToFile(InputPlanoComExcessoDeParcelamentoDto input)
        {
            var caminhoStorage = _configuration.Configuration["FamiliaArquivo:CaminhoStorage"];
            try
            {
                var file = CreateExcelPackage(
                    String.Format("{0}.xlsx", input.NomeArquivo),
                    excelPackage =>
                    {
                        var sheet = excelPackage.Workbook.Worksheets.Add("Relatório de planos com excesso de parcelamento");
                        sheet.PrinterSettings.Orientation = eOrientation.Landscape;
                        sheet.OutLineApplyStyle = true;

                        Image logo = Image.FromFile(caminhoStorage + "logo_fgv_formacao_executiva.png");

                        var picture = sheet.Drawings.AddPicture("logo_fgv_formacao_executiva.png", logo);
                        picture.SetPosition(0, 0, 0, 0);

                        sheet.View.ShowGridLines = false;
                        sheet.OutLineApplyStyle = true;

                        sheet.Cells.Style.Font.Size = 10;
                        sheet.Cells.Style.Font.Name = "Arial";

                        sheet.Cells[5, 1, 5, 6].Merge = true;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Bold = true;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Size = 10;
                        sheet.Cells[5, 1, 5, 6].Style.Font.Name = "Arial";
                        sheet.Cells[5, 1, 5, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        sheet.Cells[5, 1, 5, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        sheet.Cells[5, 1].Value = "Relatório de planos com excesso de parcelamento";                       

                        sheet.View.ShowGridLines = false;
                        sheet.Cells[6, 1, 6, 3].Merge = true;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Bold = true;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Size = 8;
                        sheet.Cells[6, 1, 6, 3].Style.Font.Name = "Arial";
                        sheet.Cells[6, 1, 6, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        sheet.Cells[6, 1, 6, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        sheet.Cells[6, 1].Value = "Data / hora geração: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");                        

                        using (ExcelRange Rng = sheet.Cells[7, 1, 7, 11])
                        {
                            Rng.Style.Font.Name = "Arial";
                            Rng.Style.Font.Bold = true;
                            Rng.Style.Font.Color.SetColor(Color.White);
                            Rng.Style.Font.Size = 10;
                            Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            Rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(70, 130, 180));
                            Rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        // sheet.Row(7).Height = 29.40;

                        int startRow = 7;
                        int startColumn = 1;

                        AddHeader(
                            sheet,
                            startRow,
                            startColumn,
                            ("Unidade"),
                            ("Turma"),
                            ("Curso"),
                            ("Aluno"),
                            ("CPF"),
                            ("Matrícula"),
                            ("Responsável Financeiro"),
                            ("Total Parcelas"),
                            ("Parcelas Excesso"),
                            ("Número da IN")
                        );

                        AddObjects(
                            sheet, 8, input.Itens,
                            _ => _.UnidadeFisica,
                            _ => _.Turma,
                            _ => _.NomeCurso,
                            _ => _.NomeAluno,
                            _ => _.CPF,
                            _ => _.Matricula,
                            _ => _.ResponsavelFinanceiro,
                            _ => _.NumeroDeCobrancas,
                            _ => _.NumeroDeParcelasEmExcesso,
                            _ => _.IdentificadorNormaPrecoMinimo + '/' + _.AnoNormaPrecoMinimo
                        );

                        // Formatting cells
                        // var timeColumn = sheet.Column(8);
                        // timeColumn.Style.Numberformat.Format = "dd-MM-yyyy";

                        sheet.Column(5).Width = 12;

                        for (var i = 1; i <= 11; i++)
                        {
                            if (i.IsIn(5)) //Don't AutoFit Parameters and Exception
                            {
                                continue;
                            }

                            sheet.Column(i).AutoFit();
                        }
                    });

                return file;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message, ex);
            }

            return null;
        }

    }
}
