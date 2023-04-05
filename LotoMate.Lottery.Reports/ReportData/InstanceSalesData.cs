using LotoMate.Framework.Exceptions;
using LotoMate.Lottery.Reports.ViewModels;
using LotoMate.Reports.Library.PdfProcessor;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace LotoMate.Lottery.Reports.ReportData
{
    public class InstanceSalesData : IReportData
    {
        private readonly ILogger logger;
        public InstanceSalesData(ILogger logger)
        {
            this.logger = logger;
        }
        public ReportDetails GetDetails<T>(T reportModel) where T : BaseVM
        {
            InstanceSalesHeader header = reportModel as InstanceSalesHeader;

            try
            {
                var sb = new StringBuilder();
                sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div style='font-family:calibri'>
                                <div class='header'><h1>Daily Instance Games Sale</h1></div>
                                <div style='float:left; clear: both; margin-bottom:10px'>
                                 <table align='left'>
                                    <tr><td>Store Name : {0}</td><td>Opened By : {2}</td></tr>
                                    <tr><td>Date : {1}</td><td>Closed By : {3}</td></tr>
                                </table>
                                </div>",
                                    header.StoreName, header.TransactionDate, header.OpenUserName, header.CloseUserName);
                sb.Append(@"<div style='float:left; clear: both; margin-bottom:10px'>
                                <table align='left'>
                                    <tr>
                                        <th>Book No</th>
                                        <th>Game Name</th>
                                        <th>Price</th>
                                        <th>Open No</th>
                                        <th>Close No</th>
                                        <th>Total Sale</th>
                                        <th>Total Amount</th>
                                    </tr>");
                int? totalSale = 0; decimal? totalAmount = 0;
                foreach (var sale in header.SalesDetail)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td>
                                  </tr>",
                                      sale.InstanceGameBookId, sale.GameName, sale.Price, sale.OpenNo, sale.CloseNo,
                                      sale.TotalSale, sale.TotalSalePrice);
                    totalSale += sale.TotalSale;
                    totalAmount += sale.TotalSalePrice;

                }
                sb.AppendFormat(@"
                                </table>
                                </div>
                                <div style='float:left; clear: both; margin-bottom:10px'>
                                <table>
                                    <tr><strong><td>Total Sale : {0}</td>
                                    <td>Total Sale : {1}</td></strong></tr>
                                </table>
                                </div>
                            </div>
                            </body>
                        </html>", totalSale, totalAmount);

                return new ReportDetails() { Body = sb.ToString() };

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "No contents found or error while generating report for the storeid and/or the date!");
                throw new ReportDataException("No contents found or error while generating report for the storeid and/or the date!");
            }
        }
    }
}
