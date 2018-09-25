using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class Transaction
    {

        public string trans_id { get; set; }
        public double trans_amt { get; set; }
        public string trans_description { get; set; }
        public string trans_type { get; set; }
        public string trans_from { get; set; }
        public string trans_to { get; set; }
        public DateTime trans_date { get; set; }

        public Transaction(string trans_id, double trans_amt, string trans_description, string trans_type, string trans_from, string trans_to, DateTime trans_date)
        {
            this.trans_id = trans_id;
            this.trans_amt = trans_amt;
            this.trans_description = trans_description;
            this.trans_type = trans_type;
            this.trans_from = trans_from;
            this.trans_to = trans_to;
            this.trans_date = trans_date;
        }
    }
}