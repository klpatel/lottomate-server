using System;
using System.Collections.Generic;

namespace LotoMate.Identity.API.ViewModels
{
    public class ErrorViewModel
    {
        public int HttpStatusCode { get; }
        public string Path { get; }
        public ExceptionViewModel Exception { get; }
        public IEnumerable<string> Details { get; }

        public ErrorViewModel(int httpStatusCode, string path, Exception exception = null, IEnumerable<string> details = null)
        {
            HttpStatusCode = httpStatusCode;
            Path = path;
            Exception = exception != null ? Exception = new ExceptionViewModel(exception) : null;
            Details = details != null ? new List<string>(details) : null;
        }
    }
}
