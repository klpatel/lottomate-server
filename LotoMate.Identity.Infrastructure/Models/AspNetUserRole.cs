using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Identity.Infrastructure.Models
{
    public partial class AspNetUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? ClientId { get; set; }
        public int? StoreId { get; set; }

        public virtual RBAClient Client { get; set; }
        public virtual AspNetRole Role { get; set; }
        public virtual Store Store { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
