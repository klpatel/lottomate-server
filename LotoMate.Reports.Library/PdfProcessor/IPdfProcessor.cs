using System.IO;
using System.Threading.Tasks;

namespace LotoMate.Reports.Library.PdfProcessor
{
    public interface IPdfProcessor
    {
        Task<string> Generate<TGen>(TGen templateGenerator) where TGen : ITemplateGenerator;
        Task<MemoryStream> GenerateStream<TGen>(TGen templateGenerator) where TGen : ITemplateGenerator;
    }

    public class ReportDetails
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Body { get; set; }

    }
         
}
