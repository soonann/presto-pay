using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetExpenditure
    {
        public string acc_email { get; set; }
        public string budget_expenditureId { get; set; }
        public string budget_expenditureCategory { get; set; }
        public string budget_expenditureSubCategory { get; set; }
        public double budget_expenditureAmountSpent { get; set; }
        public DateTime budget_expenditureDate { get; set; }
        public string budget_expenditureRemarks { get; set; }
    }
}