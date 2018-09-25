using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetDashBoard
    {
        public string budget_id { get; set; }
        public string acc_email { get; set; }
        public DateTime budget_startDate { get; set; }
        public DateTime budget_endDate { get; set; }

        public double budget_incomeAmountAllocated { get; set; }
        public int budget_incomeAmountCount { get; set; }
        public double budget_incomeAmountReceived { get; set; }

        public double budget_fixedCostAmountAllocated { get; set; }
        public int budget_fixedCostAmountCount { get; set; }
        public double budget_fixedCostAmountSpent { get; set; }

        public double budget_flexSpendingAmountAllocated { get; set; }
        public int budget_flexSpendingAmountCount { get; set; }
        public double budget_flexSpendingAmountSpent { get; set; }

        public double budget_debtRepaymentAmountAllocated { get; set; }
        public int budget_debtRepaymentAmountCount { get; set; }
        public double budget_debtRepaymentAmountSpent { get; set; }

        public double budget_priorityGoalsAmountAllocated { get; set; }
        public int budget_priorityGoalsAmountCount { get; set; }
        public double budget_priorityGoalsAmountSpent { get; set; }

        public double budget_totalExpenditureAmountAllocated { get; set; }
        public int budget_totalExpenditureAmountCount { get; set; }
        public double budget_totalExpenditureAmountSpent { get; set; }

        public double budget_totalExpenditureAmountLeftOver { get; set; }

        public double budget_fixedCostSubCategoryAmountAllocated { get; set; }
        public int budget_fixedCostSubCategoryAmountCount { get; set; }
        public double budget_fixedCostSubCategoryAmountSpent { get; set; }

        public double budget_flexSpendingSubCategoryAmountAllocated { get; set; }
        public int budget_flexSpendingSubCategoryAmountCount { get; set; }
        public double budget_flexSpendingSubCategoryAmountSpent { get; set; }

        public double budget_debtRepaymentSubCategoryAmountAllocated { get; set; }
        public int budget_debtRepaymentSubCategoryAmountCount { get; set; }
        public double budget_debtRepaymentSubCategoryAmountSpent { get; set; }

        public double budget_priorityGoalsSubCategoryAmountAllocated { get; set; }
        public int budget_priorityGoalsSubCategoryAmountCount { get; set; }
        public double budget_priorityGoalsSubCategoryAmountSpent { get; set; }

        public double budget_totalExpenditureSubCategoryAmountAllocated { get; set; }
        public int budget_totalExpenditureSubCategoryAmountCount { get; set; }
        public double budget_totalExpenditureSubCategoryAmountSpent { get; set; }

        public double budget_totalExpenditureSubCategoryAmountLeftOver { get; set; }
    }
}