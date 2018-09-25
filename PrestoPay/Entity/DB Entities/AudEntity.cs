using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class AudEntity
    {
        public int id { get; set; }
        public DateTime timeStamp { get; set; }
        public string acc_email { get; set; }
        public String description { get; set; }


    }
}