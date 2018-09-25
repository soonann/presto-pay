using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Loan
    {
        public string loan_id { get; set; }
        public string busi_id { get; set; }
        public double loan_saleRevAtApp { get; set; }
        public double loan_amt { get; set; }       
        public DateTime loan_applicationDate { get; set; }
        public double loan_repaymentRate { get; set; }
        public double loan_percentageKeep { get; set; }
        public double loan_oneTimeFixedFee { get; set; }
        public double loan_totalAmountToBeRepaid { get; set; }
        public string loan_applicationStatus { get; set; }
        public string loan_reasonForApplicationStatus { get; set; }
        public DateTime loan_approvalDate { get; set; }
        public string loan_approvedByAdminUserName { get; set; }
        public int loan_totalRepaymentAmountCount { get; set; }
        public double loan_totalAmountRepaid { get; set; }
        public int loan_totalRepaymentStatusCount { get; set; }
        public DateTime loan_repaymentDate { get; set; }
        public string loan_repaymentStatus { get; set; }
        public string loan_reasonForRepaymentStatus { get; set; }
        public double loan_totalTransactionAmount { get; set; }        
        public string loan_successMsg { get; set; }
        public string loan_errorMsg { get; set; }

        

    }
}