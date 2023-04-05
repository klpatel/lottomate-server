using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class InstanceGameMaster : BaseModel
    {
        public InstanceGameMaster()
        {
            InstanceGameBooks = new HashSet<InstanceGameBook>();
        }

        public long Id { get; set; }
        public int? StoreId { get; set; }
        public string GameNumber { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? BookTotal { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<InstanceGameBook> InstanceGameBooks { get; set; }
    }
}
