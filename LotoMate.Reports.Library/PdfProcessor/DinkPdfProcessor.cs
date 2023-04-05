using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using System.Threading.Tasks;

namespace LotoMate.Reports.Library.PdfProcessor
{
    public class DinkPdfProcessor : IPdfProcessor
    {
        private readonly IConverter converter;
        public DinkPdfProcessor(IConverter converter)
        {
            this.converter = converter;
        }
        public async Task<string> Generate<TGen>(TGen templateGenerator) where TGen : ITemplateGenerator
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Daily Sales Report",
                Out = @"D:\PDFCreator\Sales_Report2.pdf", 
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true, 
                HtmlContent = templateGenerator.GetHTMLReport(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            converter.Convert(pdf);

            return @"D:\PDFCreator\Sales_Report2.pdf"; //return file path
        }
        public async Task<MemoryStream> GenerateStream<TGen>(TGen templateGenerator) where TGen : ITemplateGenerator
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Daily Sales Report"
                //Out = @"D:\PDFCreator\Sales_Report.pdf",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = templateGenerator.GetHTMLReport(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            var buffer = converter.Convert(pdf);
            return new MemoryStream(buffer);
            
        }
    }
    
}
