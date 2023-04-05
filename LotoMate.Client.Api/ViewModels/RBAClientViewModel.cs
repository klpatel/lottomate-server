using System;
using System.Collections.Generic;

#nullable disable

namespace LotoMate.Client.Api.ViewModels
{
    public partial class RBAClientViewModel
    {
        public int Id { get; set; }
        public string ClientFname { get; set; }
        public string ClientLname { get; set; }
        public string EmailId { get; set; }
        public string IsActive { get; set; }
        
    }
}
