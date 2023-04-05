using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class Address
    {
        public Address()
        {
            Clients = new HashSet<RBAClient>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public virtual ICollection<RBAClient> Clients { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
