using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class Store 
    {
        public Store()
        {
            CategorisedSales = new HashSet<CategorisedSale>();
            DailySummaries = new HashSet<DailySummary>();
            GameSalesCategories = new HashSet<GameSalesCategory>();
            InstanceDailySales = new HashSet<InstanceDailySale>();
            InstanceGameBooks = new HashSet<InstanceGameBook>();
            InstanceGameMasters = new HashSet<InstanceGameMaster>();
        }

        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string StoreNumber { get; set; }
        public string StoreName { get; set; }
        public string RegisteredName { get; set; }
        public string Tinno { get; set; }
        public byte? BusinessTypeId { get; set; }
        public byte? BusinessCategoryId { get; set; }
        public bool? IsActive { get; set; }
        
        public virtual ICollection<CategorisedSale> CategorisedSales { get; set; }
        public virtual ICollection<DailySummary> DailySummaries { get; set; }
        public virtual ICollection<GameSalesCategory> GameSalesCategories { get; set; }
        public virtual ICollection<InstanceDailySale> InstanceDailySales { get; set; }
        public virtual ICollection<InstanceGameBook> InstanceGameBooks { get; set; }
        public virtual ICollection<InstanceGameMaster> InstanceGameMasters { get; set; }
    }
}
