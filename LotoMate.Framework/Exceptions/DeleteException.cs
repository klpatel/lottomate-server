using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class DeleteException : Exception
    {
        public DeleteException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
}
