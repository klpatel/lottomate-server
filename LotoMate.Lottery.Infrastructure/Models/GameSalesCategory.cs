using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class GameSalesCategory
    {
        public GameSalesCategory()
        {
            CategorisedSales = new HashSet<CategorisedSale>();
        }

        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string CategoryName { get; set; }
        public bool? DebitOrCredit { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<CategorisedSale> CategorisedSales { get; set; }
    }
}
