using System;
using System.Collections.Generic;

namespace LotoMate.Identity.API.ViewModels
{
    public partial class Userlogins
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}
