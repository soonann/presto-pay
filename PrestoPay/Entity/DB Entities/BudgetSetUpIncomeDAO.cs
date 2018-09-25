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
    public class BudgetSetUpIncomeDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public int InsertBudgetSetUpIncomeByBudgetId(string BUDGET_ID, string INCOME_CATEGORY, string INCOME_SUBCATEGORY, double INCOME_AMOUNT_ALLOCATED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetSetUpIncome (budget_id, budget_incomeCategory, budget_incomeSubCategory, budget_incomeSubCategoryAmountAllocated) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraBudget_incomeCategory, @paraBudget_incomeSubCategory, @paraBudget_incomeSubCategoryAmountAllocated) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeSubCategory", INCOME_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeSubCategoryAmountAllocated", INCOME_AMOUNT_ALLOCATED);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetSetUpIncomeByBudgetId()


        public BudgetDashBoard ReadTotalBudgetSetUpIncomeAmountAllocatedByBudgetIdAndCategory(string BUDGET_ID, string INCOME_CATEGORY)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised BUDGET_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT budget_id, COUNT(*) AS TotalBudget_incomeAmountCount, SUM(budget_incomeSubCategoryAmountAllocated) AS TotalBudget_incomeAmountAllocated ");
            sqlStr.AppendLine("FROM BudgetSetUpIncome ");
            sqlStr.AppendLine("WHERE (budget_id = @paraBudget_id) AND (budget_incomeCategory = @paraBudget_incomeCategory) ");
            sqlStr.AppendLine("GROUP BY budget_id ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_incomeAmountAllocated");

            // Step 7: Select command return a row from TableBudget_incomeAmountAllocated contain the selected TotalBudget_incomeAmountAllocated
            int rec_cnt = ds.Tables["TableBudget_incomeAmountAllocated"].Rows.Count;
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            budgetDashBoardObj.budget_id = BUDGET_ID;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_incomeAmountAllocated
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_incomeAmountAllocated"].Rows[0];
                budgetDashBoardObj.budget_incomeAmountCount = Convert.ToInt16(row["TotalBudget_incomeAmountCount"]);
                budgetDashBoardObj.budget_incomeAmountAllocated = Convert.ToDouble(row["TotalBudget_incomeAmountAllocated"]);
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // ReadTotalBudgetSetUpIncomeAmountAllocatedByBudgetIdAndCategory()
    } // BudgetSetUpIncomeDAO
} // PrestoPay.Entity.DB_Entities