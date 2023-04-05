using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Identity.API.ViewModels
{
    public class Enumerators
    {
        
        public enum PasswordStatus
        {
            	Temporary=97,
	            Valid=98,
	            Locked=99,
	            Expired=100,
        }
        public enum UserAccountStatus
        {
            Active = 1,
            InActive =2,
        }
    }
}
