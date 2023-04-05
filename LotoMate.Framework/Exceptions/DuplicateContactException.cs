using System;

namespace LotoMate.Exceptions
{
    public class DuplicateContactException : BusinessRuleException
    {
        public DuplicateContactException(string message) : base(message)
        {
            
        }
        public override string Message => base.Message;
    }
}
