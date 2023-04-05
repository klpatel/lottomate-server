using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Identity.Infrastructure.Models
{
    public partial class UserClientRole : BaseModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public int? StoreId { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
