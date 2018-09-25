using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetSetUpExpenditure
    {
        public string budget_id { get; set; }
        public string budget_expenditureCategory { get; set; }
        public string budget_expenditureSubCategory { get; set; }
        public double budget_expenditureSubCategoryAmountAllocated { get; set; }
    }
}