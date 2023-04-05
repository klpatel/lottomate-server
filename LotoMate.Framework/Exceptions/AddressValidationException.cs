using System;

namespace LotoMate.Exceptions
{
    public class AddressValidationException : LotoMateValidationException
    {
        public AddressValidationException(string message) : base(message)
        {
            
        }
        public override string Message => base.Message;
    }

}
