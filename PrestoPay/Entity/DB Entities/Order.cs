using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Order
    {
        public string Order_id { get; set; }
        public string Order_ApiKey { get; set; }
        public string Order_RefNo { get; set; }
        public DateTime Order_DateOrdered { get; set; }
        public string Order_UrlRedirect { get; set; }
        public string Order_PrestoKey { get; set; }
        public int Order_Paid { get; set; }

    }
}