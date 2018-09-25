using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BusinessSales
    {
        
        public string salesReportingId { get; set; }
        public string busi_id { get; set; }
        public DateTime salesReportingDate { get; set; }
        public double salesAmount { get; set; }

        public int bs_salesAmountCount { get; set; }
        public double bs_totalSalesAmount { get; set; }
        
        
    }
}