using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class Account
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public double walletBal { get; set; }
        public DateTime dob { get; set; }
        public string busi_id { get; set; }

        public Account(string email, string password, string name, double walletBal, DateTime dob, string busi_id)
        {
     
            this.email = email;
            this.password = password;
            this.name = name;
            this.walletBal = walletBal;
            this.dob = dob;
            this.busi_id = busi_id;
        }
    }
}