using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Business
    {
        public string busi_id { get; set; }
        public string acc_email { get; set; }
        public string busi_companyName { get; set; }
        public string busi_type { get; set; }
        public string busi_category { get; set; }
        public string busi_countryOfReg { get; set; }
        public string busi_defaultItem { get; set; }
    }
}