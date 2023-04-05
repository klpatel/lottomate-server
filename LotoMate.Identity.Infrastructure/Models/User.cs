﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Identity.Infrastructure.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        public string LastName { get; set; }
        public User()
        {
        }

    }
}
