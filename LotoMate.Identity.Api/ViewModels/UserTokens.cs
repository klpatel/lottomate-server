using System;
using System.Collections.Generic;

namespace LotoMate.Identity.API.ViewModels
{
    public partial class UserTokens
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}
