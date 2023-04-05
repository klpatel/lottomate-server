using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class DailySummary : BaseModel
    {
        public long Id { get; set; }
        public int? StoreId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Sale { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Cancel { get; set; }
        public decimal? Total { get; set; }
        public decimal? InHand { get; set; }
        public decimal? Difference { get; set; }

        public virtual Store Store { get; set; }
    }
}
