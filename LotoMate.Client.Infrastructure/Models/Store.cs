using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class Store : BaseModel
    {
        public Store()
        {
            
        }

        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string StoreNumber { get; set; }
        public string StoreName { get; set; }
        public string RegisteredName { get; set; }
        public string Tinno { get; set; }
        public byte? BusinessTypeId { get; set; }
        public byte? BusinessCategoryId { get; set; }
        public int? ContactId { get; set; }
        public int? AddressId { get; set; }
        public bool? IsActive { get; set; }
        
        public virtual Address Address { get; set; }
        public virtual Contact Contact { get; set; }
        
    }
}
