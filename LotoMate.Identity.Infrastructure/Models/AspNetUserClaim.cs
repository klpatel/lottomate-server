using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Identity.Infrastructure.Models
{
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
