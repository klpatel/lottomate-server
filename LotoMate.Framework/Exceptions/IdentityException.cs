using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Exceptions
{
    public class IdentityException : BusinessRuleException
    {
        public IdentityException(string message) : base(message)
        {

        }

        public override string Message => base.Message;
    }
    public class InvalidCredentialException : BusinessRuleException
    {
        public InvalidCredentialException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
    public class InvalidTokenException : BusinessRuleException
    {
        public InvalidTokenException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
    public class AuthorisationFailException : BusinessRuleException
    {
        public AuthorisationFailException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }

    public class ResetPasswordException : BusinessRuleException
    {
        public ResetPasswordException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
    public class ChangePasswordException : BusinessRuleException
    {
        public ChangePasswordException(string message) : base(message)
        {

        }
        public override string Message => base.Message;
    }
}
