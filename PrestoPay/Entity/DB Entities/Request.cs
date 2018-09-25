using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Request
    {

        public int RequestId { get; set; }
        public string Request_sender { get; set; }
        public string Request_recipient { get; set; }
        public DateTime Request_DateTime { get; set; }
        public int Request_State { get; set; }

    }
}