using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetSetUpExpenditureDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public int InsertBudgetSetUpFixedCostByBudgetId(string BUDGET_ID, string FIXED_COST_CATEGORY, string FIXED_COST_SUBCATEGORY, double FIXED_COST_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpExpenditure (budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, @paraBudget_expenditureSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FIXED_COST_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategoryAmountAllocated", FIXED_COST_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpFixedCostByBudgetId()


        public int InsertBudgetSetUpFlexSpendingByBudgetId(string BUDGET_ID, string FLEX_SPENDING_CATEGORY, string FLEX_SPENDING_SUBCATEGORY, double FLEX_SPENDING_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpExpenditure (budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, @paraBudget_expenditureSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FLEX_SPENDING_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategoryAmountAllocated", FLEX_SPENDING_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpFlexSpendingByBudgetId()


        public int InsertBudgetSetUpDebtRepaymentByBudgetId(string BUDGET_ID, string DEBT_REPAYMENT_CATEGORY, string DEBT_REPAYMENT_SUBCATEGORY, double DEBT_REPAYMENT_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpExpenditure (budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, @paraBudget_expenditureSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", DEBT_REPAYMENT_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategoryAmountAllocated", DEBT_REPAYMENT_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpDebtRepaymentByBudgetId()


        public int InsertBudgetSetUpPriorityGoalsByBudgetId(string BUDGET_ID, string PRIORITY_GOALS_CATEGORY, string PRIORITY_GOALS_SUBCATEGORY, double PRIORITY_GOALS_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpExpenditure (budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, @paraBudget_expenditureSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", PRIORITY_GOALS_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategoryAmountAllocated", PRIORITY_GOALS_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpPriorityGoalsByBudgetId()


        public int InsertBudgetSetUpExpenditureByBudgetId(string BUDGET_ID, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, double EXPENDITURE_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpExpenditure (budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, @paraBudget_expenditureSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategoryAmountAllocated", EXPENDITURE_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpExpenditureByBudgetId()


        public BudgetDashBoard ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory(string BUDGET_ID, string EXPENDITURE_CATEGORY)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory()


        public BudgetDashBoard ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(string BUDGET_ID, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureSubCategoryAmountAllocated) AS TotalBudget_expenditureAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("GROUP BY budget_id ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.budget_id = BUDGET_ID;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated = Convert.ToDouble(row["TotalBudget_expenditureAmountAllocated"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory()


        public List<BudgetSetUpExpenditure> ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory(string BUDGET_ID, string EXPENDITURE_CATEGORY)
        {
            // Step 2 : declare a BudgetSetUpExpenditure, DataSet instance and dataTable instance
            List<BudgetSetUpExpenditure> budgetSetUpExpenditureList = new List<BudgetSetUpExpenditure>();

            DataSet ds = new DataSet();

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            // Check whether the budgetSetUpExpenditureList is valid
            if (budgetSetUpExpenditureList != null)
            {
                int rec_cnt = budgetSetUpExpenditureList.Count;

                if (rec_cnt == 0)
                {
                    budgetSetUpExpenditureList = null;
                } // if (rec_cnt)
            } // if (budgetSetUpExpenditureList)

            return budgetSetUpExpenditureList;
        } // ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory()


        public List<BudgetSetUpExpenditure> ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(string BUDGET_ID, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY)
        {
            // Step 2 : declare a BudgetSetUpExpenditure, DataSet instance and dataTable instance
            List<BudgetSetUpExpenditure> budgetSetUpExpenditureList = new List<BudgetSetUpExpenditure>();

            DataSet ds = new DataSet();

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetSetUpExpenditure Table by parameterised BUDGET_ID

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT budget_id, budget_expenditureCategory, budget_expenditureSubCategory, budget_expenditureSubCategoryAmountAllocated ");
                sqlStr.AppendLine("FROM BudgetSetUpExpenditure ");
                sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountAllocated");

                // Step 7: Select command return a row from TableBudget_expenditureAmountAllocated contain the selected TotalBudget_expenditureAmountAllocated
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();
                    // Step 8 Set attribute of BudgetSetUpExpenditure instance for the record in TableBudget_expenditureAmountAllocated
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountAllocated"].Rows[i];

                        BudgetSetUpExpenditure budgetSetUpExpenditureObj = new BudgetSetUpExpenditure();

                        budgetSetUpExpenditureObj.budget_id = Convert.ToString(row["budget_id"]);
                        budgetSetUpExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetSetUpExpenditureObj.budget_expenditureSubCategoryAmountAllocated = Convert.ToDouble(row["budget_expenditureSubCategoryAmountAllocated"]);

                        budgetSetUpExpenditureList.Add(budgetSetUpExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetSetUpExpenditureList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            // Check whether the budgetSetUpExpenditureList is valid
            if (budgetSetUpExpenditureList != null)
            {
                int rec_cnt = budgetSetUpExpenditureList.Count;

                if (rec_cnt == 0)
                {
                    budgetSetUpExpenditureList = null;
                } // if (rec_cnt)
            } // if (budgetSetUpExpenditureList)

            return budgetSetUpExpenditureList;
        } // ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory()

    } // BudgetSetUpExpenditureDAO
} // PrestoPay.Entity.DB_Entities