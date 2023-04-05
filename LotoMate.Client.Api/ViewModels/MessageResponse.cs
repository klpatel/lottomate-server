using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Client.Api.ViewModels
{
    public class MessageResponse
    {
        public int Status;
        public Errors errors { get; set; }
        public MessageResponse(int statusCode, string responseMessage)
        {
            this.Status = statusCode;
            this.errors = new Errors() { Messages = new List<string>() { responseMessage} };
        }
        public MessageResponse(int statusCode, Errors errors)
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
