using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public class AspnetUserRepository : Repository<AspNetUser, IdentityContext>, IAspnetUserRepository
    {
        public AspnetUserRepository(IdentityContext context) : base(context)
        {
        }
    }
}
