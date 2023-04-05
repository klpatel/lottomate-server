using System;

namespace LotoMate.Exceptions
{
    public class DuplicateEmailException : LotoMateValidationException
    {
        public DuplicateEmailException(string message = null) : base(message)
        {
            
        }
        public override string Message => base.Message;
    }
}
