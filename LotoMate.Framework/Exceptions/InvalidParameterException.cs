using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class InvalidParameterException :  BusinessRuleException
    {
        public InvalidParameterException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
}
