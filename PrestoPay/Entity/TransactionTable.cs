using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class TransactionTable
    {
        public String TransactionID { get; set; }
        public String Email { get; set; }
        public String Date { get; set; }
        public String Description { get; set; }
        public double Receipt { get; set; }
        public double Payment { get; set; }
    }
}