using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class RBAClient : BaseModel
    {
        public int Id { get; set; }
        public string ClientFname { get; set; }
        public string ClientMname { get; set; }
        public string ClientLname { get; set; }
        public int? AddressId { get; set; }
        public int? ContactId { get; set; }
        public string IsActive { get; set; }
        
        public virtual Address Address { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
