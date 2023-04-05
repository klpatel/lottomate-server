using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class BusinessCategory
    {
        public byte Id { get; set; }
        public string BusinessCategoryName { get; set; }
        public string Description { get; set; }
    }
}
