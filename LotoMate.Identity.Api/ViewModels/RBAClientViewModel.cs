using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Identity.API.ViewModels
{
    public partial class RBAClientViewModel
    {
        #region Client
        public int Id { get; set; }
        public string ClientFname { get; set; }
        public string ClientMname { get; set; }
        public string ClientLname { get; set; }
        #endregion

        #region Contact
        public string EmailId { get; set; }
        public string Phone { get; set; }
        #endregion

        #region User
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }

        #endregion
        //public string IsActive { get; set; }
        
    }
}
