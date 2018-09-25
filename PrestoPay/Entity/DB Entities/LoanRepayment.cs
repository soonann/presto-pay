using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class LoanRepayment
    {
        public string rpmt_id { get; set; }
        public string loan_id { get; set; }
        public string trans_id { get; set; }
        public string reference_id { get; set; }
        public double rpmt_amt { get; set; }
        public DateTime rpmt_date { get; set; }

    }
}