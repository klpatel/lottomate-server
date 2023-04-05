using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class BusinessType
    {
        public byte Id { get; set; }
        public string BusinessTypeName { get; set; }
        public string Description { get; set; }
    }
}
