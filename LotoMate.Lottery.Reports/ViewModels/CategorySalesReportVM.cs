using System;
using System.Collections.Generic;

namespace LotoMate.Lottery.Reports.ViewModels
{
    public class CategorySalesReportVM
    {        
        public long Id { get; set; }
        public int? GameSalesCategoryId { get; set; }

        public decimal? Total { get; set; }
        public string Category { get; set; }
        public bool? DebitOrCredit { get; set; }

    }
    public class CategorySalesHeader : BaseVM
    {
        public int? StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public ICollection<CategorySalesReportVM> DailySales { get; set; }
        
    }

}
