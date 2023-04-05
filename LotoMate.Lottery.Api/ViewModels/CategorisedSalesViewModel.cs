using System;
using System.Collections.Generic;

namespace LotoMate.Lottery.Api.ViewModels
{
    public class CategorisedSalesViewModel
    {
        public long Id { get; set; }
        public int? GameSalesCategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal? Total { get; set; }
    }
    public class CategorisedSalesHeader
    {
        public int? StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime? TransactionDate { get; set; }
        public ICollection<CategorisedSalesViewModel> CreditSalesDetail { get; set; }
        public ICollection<CategorisedSalesViewModel> DebitSalesDetail { get; set; }

    }


}
