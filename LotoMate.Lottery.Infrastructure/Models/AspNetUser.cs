using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Lottery.Infrastructure.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            InstanceDailySaleClosedUsers = new HashSet<InstanceDailySale>();
            InstanceDailySaleOpenUsers = new HashSet<InstanceDailySale>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<InstanceDailySale> InstanceDailySaleClosedUsers { get; set; }
        public virtual ICollection<InstanceDailySale> InstanceDailySaleOpenUsers { get; set; }
    }
}
