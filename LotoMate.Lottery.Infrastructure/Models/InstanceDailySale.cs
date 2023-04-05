using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class InstanceDailySale : BaseModel
    {
        public long Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public long? InstanceGameBookId { get; set; }
        public decimal? Price { get; set; }
        public int? OpenNo { get; set; }
        public int? CloseNo { get; set; }
        public int? TotalSale { get; set; }
        public int? TotalSalePrice { get; set; }
        public int? OpenUserId { get; set; }
        public int? ClosedUserId { get; set; }
        public int? StoreId { get; set; }

        public virtual InstanceGameBook InstanceGameBook { get; set; }
        public virtual AspNetUser ClosedUser { get; set; }
        public virtual AspNetUser OpenUser { get; set; }
        public virtual Store Store { get; set; }
    }
}
