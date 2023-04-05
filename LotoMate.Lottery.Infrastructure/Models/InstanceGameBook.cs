using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class InstanceGameBook : BaseModel
    {
        public InstanceGameBook()
        {
            InstanceDailySales = new HashSet<InstanceDailySale>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long InstanceGameId { get; set; }
        public int? StoreId { get; set; }
        public string DisplayNumber { get; set; }
        public string BookNumber { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? SettleDate { get; set; }
        public bool? IsActive { get; set; }
        public int? ActivateUserId { get; set; }
        public int? SettleUserId { get; set; }
        public virtual InstanceGameMaster InstanceGame { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<InstanceDailySale> InstanceDailySales { get; set; }
    }
}
