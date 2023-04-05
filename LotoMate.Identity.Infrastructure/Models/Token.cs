using System;

namespace LotoMate.Identity.Infrastructure.Models
{
    public class Token
    {
        public int? ClientId { get; set; }
        public int? StoreId { get; set; }
        public string   AccessToken { get; set; }
        public string   RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public string   Username { get; set; }
        public string UserFullName { get; set; }
        public int UserId{ get; set; }
        public string[] Roles { get; set; }
        
    }
}
