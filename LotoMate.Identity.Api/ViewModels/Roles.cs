using System;
using System.Collections.Generic;

namespace LotoMate.Identity.API.ViewModels
{
    public partial class Roles
    {
        public Roles()
        {
            //Aspnetroleclaims = new HashSet<Roleclaims>();
            //Aspnetuserroles = new HashSet<Userroles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        //public virtual ICollection<Roleclaims> Aspnetroleclaims { get; set; }
        //public virtual ICollection<Userroles> Aspnetuserroles { get; set; }
    }
}
