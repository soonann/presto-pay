using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class TransactionDetailTable
    {

        public  String Email { get; set; }
        public String Date { get; set; }
        public String Description { get; set; }
        public double Receipt { get; set; }
        public double Payment { get; set; }


    }
}