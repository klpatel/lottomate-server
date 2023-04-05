using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class DuplicateNameException : BusinessRuleException
    {
        public DuplicateNameException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
}
