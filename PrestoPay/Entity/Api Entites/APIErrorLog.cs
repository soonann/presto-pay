using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay
{
    public class APIErrorLog
    {

        public int Log_id { get; set; }
        public DateTime Log_Timestamp { get; set; }
        public string Log_ApiKey { get; set; }
        public string Log_ErrorMessage { get; set; }


    }
}