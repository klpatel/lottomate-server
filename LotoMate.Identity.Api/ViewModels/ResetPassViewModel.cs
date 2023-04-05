using LotoMate.Framework.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LotoMate.Identity.API.ViewModels
{
    public partial class ResetPassViewModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ConfirmationCode { get; set; }
        
    }
  
}
