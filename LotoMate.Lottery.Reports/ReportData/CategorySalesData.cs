using LotoMate.Framework.Exceptions;
using LotoMate.Lottery.Reports.ViewModels;
using LotoMate.Reports.Library.PdfProcessor;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace LotoMate.Lottery.Reports.ReportData
{
    public class CategorySalesData : IReportData
    {
        private readonly ILogger logger;
        public CategorySalesData(ILogger logger)
        {
            this.logger = logger;
        }
        public ReportDetails GetDetails<T>(T reportModel) where T : BaseVM
        {
            CategorySalesHeader header = reportModel as CategorySalesHeader;

            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div style='font-family:calibri'>
                                <div class='header'><h1>Daily Categorised Games Sale</h1></div>
                                <div style='float:left; clear: both; margin-bottom:10px'>
                                 <table align='left'>
                                    <tr><td>Store Name : {0}</td></tr>
                                    <tr><td>Date : {1}</td></tr>
                                </table>
                                </div>",
                                    header.StoreName, header.TransactionDate);
                sb.Append(@"<div style='float:left; clear: both; margin-bottom:10px'>
                                <table align='left'>
                                    <tr>
                                        <th>No</th>
                                        <th>Category</th>
                                        <th>Total</th>
                                    </tr>");
                decimal? totalDbAmount = 0, totalCrAmount = 0;
                foreach (var sale in header.DailySales)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td><td>{1}</td><td>{2}</td>
                                  </tr>",
                                      sale.GameSalesCategoryId, sale.Category, sale.Total);
                    if (sale.DebitOrCredit.Value)
                        totalCrAmount += sale.Total;
                    else
                        totalDbAmount += sale.Total;

                }
                sb.AppendFormat(@"
                                </table>
                                </div>
                                <div style='float:left; clear: both; margin-bottom:10px'>
                                <table>
                                    <tr><strong><td>Total Sale : {0}</td>
                                    <td>Total Cash : {1}</td></strong></tr>
                                </table>
                                </div>
                            </div>
                            </body>
                        </html>", totalDbAmount, totalCrAmount);

                return new ReportDetails() { Body = sb.ToString() };

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "No contents found or error while generating report for the storeid and/or the date!");
                throw new ReportDataException("No contents found or error while generating report for the storeid and/or the date!");
            }
        }
    }
}
