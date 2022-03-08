using Abp.AspNetZeroCore.Net;
using Abp.Authorization;
using Abp.UI;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro;
using Fgv.Acad.Financeiro.AcessoExterno.Boundaries.Apis.Lyceum.GestaoFinanceiro.Dto;
using Fgv.Acad.Financeiro.Dto;
using Fgv.Acad.Financeiro.Relatorios.Exporting.Interfaces;
using Fgv.Acad.Financeiro.Storage;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Fgv.Acad.Financeiro.Relatorios.Exporting
{
    public class PlanoComExcessoDeParcelamentoPdfExporter : IPlanoComExcessoDeParcelamentoPdfExporter
    {
        private readonly IFinanceiroAppService _financeiroAppService;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public PlanoComExcessoDeParcelamentoPdfExporter(IFinanceiroAppService financeiroAppService, ITempFileCacheManager tempFileCacheManager)
        {
            _financeiroAppService = financeiroAppService;
            _tempFileCacheManager = tempFileCacheManager;
        }

        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }

        private PdfTemplate PdfFooter(PdfContentByte cb)
        {
            // Create the template and assign height
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);
            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();
            // Begin writing the footer
            tmpFooter.BeginText();
            // Set the font and size
            tmpFooter.SetFontAndSize(f_cb, 8);
            // Write out details from the payee table
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FGV - Fundação Getúlio Vargas", 0, 50, 0);

            tmpFooter.EndText();
            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(800, 60);
            cb.Stroke();
            // Return the footer template
            return tmpFooter;
        }

        public FileDto ExportarPDF(FiltroPlanoComExcessoDeParcelamentoDto filtro)
        {

            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var familiaArquivos = (JObject)appsettingsjson["FamiliaArquivo"];
            var caminhoStorage = familiaArquivos.Property("CaminhoStorage").Value.ToString();

            try
            {
                //garantindo o umero de regist
                filtro.TotalDeRegistrosPorPagina = 1000000;

                // Read the data
                var result = _financeiroAppService.ObterAlunosComExcessoDeParcelamento(filtro).Result;
                byte[] array;
                // Create references for each of the on-row tables to make it easier to access the values
                // in the table using syntax like this: drHead["certificadosId"].ToString()
                var data = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                using (MemoryStream fs = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 1);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Add meta information to the document
                    document.AddAuthor("Siga2");
                    document.AddCreator("");
                    document.AddKeywords("");
                    document.AddSubject("");
                    document.AddTitle("Relatório de planos com excesso de parcelamento");

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Makes it possible to add text to a specific place in the document using 
                    // a X & Y placement syntax.
                    PdfContentByte cb = writer.DirectContentUnder;
                    // Add a footer template to the document
                    cb.AddTemplate(PdfFooter(cb), 30, 1);

                    // Add a logo to the certificados
                    //
                    var path = caminhoStorage; //+ @"C:\Projetos\Fgv\Requerimento\sust_2021_4A\aspnet-core\src\Fgv.Acad.Requerimento.Web.Host\App_Data\Images\";
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(path + "logo_fgv_formacao_executiva.png");
                    png.SetAbsolutePosition(40, 539);
                    png.ScalePercent(50);
                    cb.AddImage(png);

                    // First we must activate writing
                    cb.BeginText();
                    int left_margin = 40;
                    int top_margin = 490;
                    writeText(cb, "Data / hora geração: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), left_margin, top_margin, f_cb, 8);
                    writeText(cb, "Relatório de planos com excesso de parcelamento", left_margin + 240, top_margin, f_cb, 10);
                    // NOTE! You need to call the EndText() method before we can write graphics to the document!
                    cb.EndText();

                    // Separate the header from the rows with a line
                    // Draw a line by setting the line width and position
                    cb.SetLineWidth(0f);
                    cb.MoveTo(40, 480);
                    cb.LineTo(800, 480);
                    cb.Stroke();
                    // Don't forget to call the BeginText() method when done doing graphics!
                    cb.BeginText();

                    // Before we write the lines, it's good to assign a "last position to write"
                    // variable to validate against if we need to make a page break while outputting.
                    // Change it to 510 to write to test a page break; the fourth line on a new page
                    int lastwriteposition = 90;

                    DrawLines(document, cb, 474);
                    createHeaderColumns(cb, 470);

                    // First item line position starts here
                    top_margin = 460;

                    // Loop thru the table of items and set the linespacing to 12 points.
                    // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                    foreach (var item in result.Itens)
                    {
                        writeText(cb, item.UnidadeFisica, left_margin, top_margin, f_cb, 8);
                        writeText(cb, item.Turma, left_margin + 64, top_margin, f_cb, 8);
                        writeText(cb, (item.NomeCurso.Length > 24 ? item.NomeCurso.Substring(0, 24) : item.NomeCurso), left_margin + 165, top_margin, f_cb, 8);
                        writeText(cb, (item.NomeAluno.Length > 20 ? item.NomeAluno.Substring(0, 20) : item.NomeAluno), left_margin + 270, top_margin, f_cb, 8);
                        writeText(cb, item.CPF, left_margin + 368, top_margin, f_cb, 8);
                        writeText(cb, item.Matricula, left_margin + 425, top_margin, f_cb, 8);
                        writeText(cb, (item.ResponsavelFinanceiro.Length > 15 ? item.ResponsavelFinanceiro.Substring(0, 15) : item.ResponsavelFinanceiro), left_margin + 475, top_margin, f_cb, 8);
                        writeText(cb, item.NumeroDeCobrancas.ToString(), left_margin + 565, top_margin, f_cb, 8);
                        writeText(cb, item.NumeroDeParcelasEmExcesso.ToString(), left_margin + 610, top_margin, f_cb, 8);
                        writeText(cb, item.IdentificadorNormaPrecoMinimo + '/' + item.AnoNormaPrecoMinimo.ToString(), left_margin + 665, top_margin, f_cb, 8);
                        writeText(cb, item.DataMatricula.ToString("dd/MM/yyyy"), left_margin + 706, top_margin, f_cb, 8);

                        // This is the line spacing, if you change the font size, you might want to change this as well.
                        top_margin -= 12;

                        // Implement a page break function, checking if the write position has reached the lastwriteposition
                        if (top_margin <= lastwriteposition)
                        {
                            // We need to end the writing before we change the page
                            cb.EndText();
                            // Make the page break
                            document.NewPage();
                            // Start the writing again

                            cb.AddTemplate(PdfFooter(cb), 30, 1);

                            DrawLines(document, cb, 534);
                            createHeaderColumns(cb, 530);

                            cb.BeginText();
                            // Assign the new write location on page two!
                            // Here you might want to implement a new header function for the new page
                            top_margin = 520;
                        }
                    }

                    top_margin -= 80;
                    left_margin = 350;
                    // End the writing of text
                    cb.EndText();

                    // Close the document, the writer and the filestream!
                    document.Close();

                    writer.Flush();
                    writer.Close();
                    fs.Close();

                    array = fs.GetBuffer();
                    //fs.Flush();

                    //fs.Dispose();
                }
                var filename = "RelatorioPlanosComExcessoParcelamento.pdf";
                var file = new FileDto(filename, MimeTypeNames.ApplicationPdf);

                file.FileToken = Guid.NewGuid().ToString();

                _tempFileCacheManager.SetFile(file.FileToken, array);

                return file;

            }
            catch (Exception error)
            {
                throw new UserFriendlyException(error.Message, error);
            }
        }

        private void createHeaderColumns(PdfContentByte cb, int top_margin)
        {
            // Loop thru the rows in the rows table
            // Start by writing out the line headers
            int left_margin = 40;

            // Line headers
            writeText(cb, "Unidade", left_margin, top_margin, f_cb, 8);
            writeText(cb, "Turma", left_margin + 64, top_margin, f_cb, 8);
            writeText(cb, "Curso", left_margin + 165, top_margin, f_cb, 8);
            writeText(cb, "Aluno", left_margin + 270, top_margin, f_cb, 8);
            writeText(cb, "CPF", left_margin + 368, top_margin, f_cb, 8);
            writeText(cb, "Matrícula", left_margin + 425, top_margin, f_cb, 8);
            writeText(cb, "Responsável financeiro", left_margin + 475, top_margin, f_cb, 8);
            writeText(cb, "T. Parcelas", left_margin + 565, top_margin, f_cb, 8);
            writeText(cb, "Parc. Excesso", left_margin + 610, top_margin, f_cb, 8);
            writeText(cb, "Número IN", left_margin + 665, top_margin, f_cb, 8);
            writeText(cb, "Data Matrícula", left_margin + 706, top_margin, f_cb, 8);
        }

        private void DrawLines(Document pdfDoc, PdfContentByte cb, int size)
        {
            cb.SaveState();
            cb.SetLineWidth(10f);

            BaseColor color = new BaseColor(159, 208, 246);

            cb.SetColorStroke(color);
            cb.SetLineWidth(10f);

            cb.MoveTo(40, size);
            cb.LineTo(800, size);

            cb.MoveTo(40, size);
            cb.LineTo(800, size);

            cb.Stroke();
            cb.RestoreState();

            //
            //cb.MoveTo(800, 480);
            //cb.LineTo(pdfDoc.PageSize.Width, 800);

        }

        public FileDto ExportarPDF(InputPlanoComExcessoDeParcelamentoDto input)
        {

            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var familiaArquivos = (JObject)appsettingsjson["FamiliaArquivo"];
            var caminhoStorage = familiaArquivos.Property("CaminhoStorage").Value.ToString();

            try
            {
                byte[] array;
                // Create references for each of the on-row tables to make it easier to access the values
                // in the table using syntax like this: drHead["certificadosId"].ToString()
                var data = DateTime.Now.ToString("ddMMyyyy_hhmmss");
                using (MemoryStream fs = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 1);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    // Add meta information to the document
                    document.AddAuthor("Siga2");
                    document.AddCreator("");
                    document.AddKeywords("");
                    document.AddSubject("");
                    document.AddTitle(input.TituloArquivo);

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Makes it possible to add text to a specific place in the document using 
                    // a X & Y placement syntax.
                    PdfContentByte cb = writer.DirectContentUnder;
                    // Add a footer template to the document
                    cb.AddTemplate(PdfFooter(cb), 30, 1);

                    // Add a logo to the certificados
                    //
                    var path = caminhoStorage; //+ @"C:\Projetos\Fgv\Requerimento\sust_2021_4A\aspnet-core\src\Fgv.Acad.Requerimento.Web.Host\App_Data\Images\";
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(path + "logo_fgv_formacao_executiva.png");
                    png.SetAbsolutePosition(40, 539);
                    png.ScalePercent(50);
                    cb.AddImage(png);

                    // First we must activate writing
                    cb.BeginText();
                    int left_margin = 40;
                    int top_margin = 490;
                    writeText(cb, "Data / hora geração: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), left_margin, top_margin, f_cn, 8);
                    writeText(cb, input.TituloArquivo, left_margin + 240, top_margin, f_cb, 10);
                    // NOTE! You need to call the EndText() method before we can write graphics to the document!
                    cb.EndText();

                    // Separate the header from the rows with a line
                    // Draw a line by setting the line width and position
                    cb.SetLineWidth(0f);
                    cb.MoveTo(40, 480);
                    cb.LineTo(800, 480);
                    cb.Stroke();
                    // Don't forget to call the BeginText() method when done doing graphics!
                    cb.BeginText();

                    // Before we write the lines, it's good to assign a "last position to write"
                    // variable to validate against if we need to make a page break while outputting.
                    // Change it to 510 to write to test a page break; the fourth line on a new page
                    int lastwriteposition = 90;


                    DrawLines(document, cb, 474);
                    createHeaderColumns(cb, 470);

                    // First item line position starts here
                    top_margin = 460;

                    // Loop thru the table of items and set the linespacing to 12 points.
                    // Note that we use the -= operator, the coordinates goes from the bottom of the page!
                    foreach (var item in input.Itens)
                    {
                        writeText(cb, item.UnidadeFisica, left_margin, top_margin, f_cb, 8);
                        writeText(cb, item.Turma, left_margin + 64, top_margin, f_cb, 8);
                        writeText(cb, (item.NomeCurso.Length > 24 ? item.NomeCurso.Substring(0, 24) : item.NomeCurso), left_margin + 165, top_margin, f_cb, 8);
                        writeText(cb, (item.NomeAluno.Length > 20 ? item.NomeAluno.Substring(0, 20) : item.NomeAluno), left_margin + 270, top_margin, f_cb, 8);
                        writeText(cb, item.CPF, left_margin + 368, top_margin, f_cb, 8);
                        writeText(cb, item.Matricula, left_margin + 425, top_margin, f_cb, 8);
                        writeText(cb, (item.ResponsavelFinanceiro.Length > 15 ? item.ResponsavelFinanceiro.Substring(0, 15) : item.ResponsavelFinanceiro), left_margin + 475, top_margin, f_cb, 8);
                        writeText(cb, item.NumeroDeCobrancas.ToString(), left_margin + 565, top_margin, f_cb, 8);
                        writeText(cb, item.NumeroDeParcelasEmExcesso.ToString(), left_margin + 610, top_margin, f_cb, 8);
                        writeText(cb, item.IdentificadorNormaPrecoMinimo + '/' + item.AnoNormaPrecoMinimo.ToString(), left_margin + 665, top_margin, f_cb, 8);
                        writeText(cb, item.DataMatricula.ToString("dd/MM/yyyy"), left_margin + 706, top_margin, f_cb, 8);

                        // This is the line spacing, if you change the font size, you might want to change this as well.
                        top_margin -= 12;

                        // Implement a page break function, checking if the write position has reached the lastwriteposition
                        if (top_margin <= lastwriteposition)
                        {
                            // We need to end the writing before we change the page
                            cb.EndText();
                            // Make the page break
                            document.NewPage();
                            // Start the writing again

                            cb.AddTemplate(PdfFooter(cb), 30, 1);

                            DrawLines(document, cb, 534);
                            createHeaderColumns(cb, 530);

                            cb.BeginText();
                            // Assign the new write location on page two!
                            // Here you might want to implement a new header function for the new page
                            top_margin = 520;
                        }
                    }

                    top_margin -= 80;
                    left_margin = 350;
                    // End the writing of text
                    cb.EndText();

                    // Close the document, the writer and the filestream!
                    document.Close();

                    writer.Flush();
                    writer.Close();
                    fs.Close();

                    array = fs.GetBuffer();
                    //fs.Flush();

                    //fs.Dispose();
                }
                var filename = String.Format("{0}.pdf", input.NomeArquivo);
                var file = new FileDto(filename, MimeTypeNames.ApplicationPdf);

                file.FileToken = Guid.NewGuid().ToString();

                _tempFileCacheManager.SetFile(file.FileToken, array);

                return file;

            }
            catch (Exception error)
            {
                throw new UserFriendlyException(error.Message, error);
            }
        }


    }


}


