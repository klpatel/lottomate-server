using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Framework.EnumModels
{
    public class ErrorResponse
    {
        public int Status;
        public Errors errors { get; set; }
        public ErrorResponse(int statusCode, string responseMessage)
        {
            this.Status = statusCode;
            this.errors = new Errors() { Messages = new List<string>() { responseMessage} };
        }
        public ErrorResponse(int statusCode, Errors errors)
        {
            this.Status = statusCode;
            this.errors = errors;
        }
    }
    public class Errors
    {
        public List<string> Messages { get; set; }
    }


}
