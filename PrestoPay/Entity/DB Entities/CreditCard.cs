using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class CreditCard
    {
        public string creditcardnum { get; set; }
        public DateTime dateofexpiry { get; set; }
        public string  acc_email { get; set; }


    }
}