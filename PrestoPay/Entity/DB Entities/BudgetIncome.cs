using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetIncome
    {
        public string acc_email { get; set; }
        public string budget_incomeId { get; set; }
        public string budget_incomeCategory { get; set; }
        public string budget_incomeSubCategory { get; set; }
        public double budget_incomeAmountReceived { get; set; }
        public DateTime budget_incomeDate { get; set; }
        public string budget_incomeRemarks { get; set; }
    }
}