using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class InstanceGameUpdateVM 
    {
     
        public long Id { get; set; }
        [Required]
        public int? StoreId { get; set; }
        [Required]
        public string GameNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public bool? IsActive { get; set; }
        [Required]
        public int? BookTotal { get; set; }
        
    }
}
