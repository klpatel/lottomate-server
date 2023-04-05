using System;

namespace LotoMate.Identity.API.ViewModels
{
    public class ExceptionViewModel
    {
        public ExceptionViewModel(Exception ex)
        {
            Type = ex?.GetType().ToString();
            Message = ex?.Message;
            StackTrace = ex?.StackTrace;
            InnerException = ex?.InnerException != null ? new ExceptionViewModel(ex.InnerException) : null;
        }

        public string Type { get; }

        public string Message { get; }

        public string StackTrace { get; }

        public ExceptionViewModel InnerException { get; }
    }
}
