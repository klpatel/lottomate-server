namespace LotoMate.Reports.Library.PdfProcessor
{
    public class SalesTemplateGenerator : ITemplateGenerator
    {
        private readonly ReportDetails reportData;
        public SalesTemplateGenerator(ReportDetails reportData)
        {
            this.reportData = reportData;
        }
        public string GetHTMLReport()
        {
            return this.reportData.Body;
        }
                
    }
}