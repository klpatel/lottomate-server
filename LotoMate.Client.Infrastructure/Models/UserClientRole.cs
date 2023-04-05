using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Infrastructure.Models
{
    public partial class UserClientRole
    {
        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public int? StoreId { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
