using LotoMate.Lottery.Reports.ViewModels;
using LotoMate.Reports.Library.PdfProcessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Lottery.Reports.ReportData
{
    public interface IReportData
    {
        ReportDetails GetDetails<T>(T reportModel) where T: BaseVM;

    }
}
