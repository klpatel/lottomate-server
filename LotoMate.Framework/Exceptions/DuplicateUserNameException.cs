using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class DuplicateUserNameException : BusinessRuleException
    {
        public DuplicateUserNameException(string message=null) : base(message)
        {
            
        }

        public override string Message => base.Message;
    }
}
