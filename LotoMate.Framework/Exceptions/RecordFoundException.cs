using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class RecordFoundException : DatabaseValidationException
    {
        public RecordFoundException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
}
