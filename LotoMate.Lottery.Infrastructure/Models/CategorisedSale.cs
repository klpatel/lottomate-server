using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class CategorisedSale : BaseModel
    {
        public long Id { get; set; }
        public int? StoreId { get; set; }
        public int? GameSalesCategoryId { get; set; }
        public decimal? Total { get; set; }
        public DateTime? TransactionDate { get; set; }
        public virtual GameSalesCategory GameSalesCategory { get; set; }
        public virtual Store Store { get; set; }
    }
}
