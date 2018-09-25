using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetSetUpIncome
    {
        public string budget_id { get; set; }
        public string budget_incomeCategory { get; set; }
        public string budget_incomeSubCategory { get; set; }
        public double budget_incomeSubCategoryAmountAllocated { get; set; }
    }
}