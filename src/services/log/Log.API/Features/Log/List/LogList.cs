using System;

namespace Log.API.Features.Log.List
{
    public class LogList
    {
        public string Ip { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExceptionDate { get; set; }
    }
}
