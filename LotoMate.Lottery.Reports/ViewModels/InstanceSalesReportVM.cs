using System;
using System.Collections.Generic;

namespace LotoMate.Lottery.Reports.ViewModels
{
    public class InstanceSalesReportVM
    {
        public long Id { get; set; }
        public long? InstanceGameBookId { get; set; }
        public string GameName { get; set; }
        public decimal? Price { get; set; }
        public int? OpenNo { get; set; }
        public int? CloseNo { get; set; }
        public int? TotalSale { get; set; }
        public decimal? TotalSalePrice { get; set; }
    }
    public class InstanceSalesHeader : BaseVM
    {
        public int? StoreId { get; set; }
        public string StoreName { get; set; }
        public int? OpenUserId { get; set; }
        public string OpenUserName { get; set; }
        public int? ClosedUserId { get; set; }
        public string CloseUserName { get; set; }
        public DailySaleState SaleState { get; set; }
        public DateTime? TransactionDate { get; set; }
        public ICollection<InstanceSalesReportVM> SalesDetail { get; set; }
    }

    public enum DailySaleState
    {
        Open=0,
        Close=1
    }
}
