using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class LoanCalculationTable
    {
        public string arrRepaymentPercentage { get; set; }
        public string arrPercentageYouKeep { get; set; }
        public double arrTotalFixedFee { get; set; }
        public double arrTotalRepayAmount { get; set; }

    }
}