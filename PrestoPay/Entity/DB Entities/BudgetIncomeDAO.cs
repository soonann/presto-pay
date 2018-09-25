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
    public class BudgetIncomeDAO
    {        
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public BudgetDashBoard ReadTotalBudgetIncomeAmountReceivedByEmailAndCategory(string ACC_EMAIL, string INCOME_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_incomeAmountCount, SUM(budget_incomeAmountReceived) AS TotalBudget_incomeAmountReceived ");
            sqlStr.AppendLine("FROM BudgetIncome ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_incomeCategory = @paraBudget_incomeCategory) AND (budget_incomeDate BETWEEN @paraStart_date AND @paraEnd_date) ");
            sqlStr.AppendLine("GROUP BY acc_email ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
            da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_incomeAmountReceived");

            // Step 7: Select command return a row from TableBudget_incomeAmountReceived contain the selected TotalBudget_incomeAmountReceived
            int rec_cnt = ds.Tables["TableBudget_incomeAmountReceived"].Rows.Count;
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            budgetDashBoardObj.acc_email = ACC_EMAIL;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_incomeAmountReceived
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_incomeAmountReceived"].Rows[0];
                budgetDashBoardObj.budget_incomeAmountCount = Convert.ToInt16(row["TotalBudget_incomeAmountCount"]);
                budgetDashBoardObj.budget_incomeAmountReceived = Convert.ToDouble(row["TotalBudget_incomeAmountReceived"]);
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // ReadTotalBudgetIncomeAmountReceivedByEmailAndCategory()


        public BudgetDashBoard ReadTotalTransactionIncomeAmountReceivedByEmailAndCategory(string ACC_EMAIL, string INCOME_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT trans_to, COUNT(*) AS TotalBudget_incomeAmountCount, SUM(trans_amt) AS TotalBudget_incomeAmountReceived ");
            sqlStr.AppendLine("FROM [Transaction] ");
            sqlStr.AppendLine("WHERE (trans_to = @paraAcc_email) AND (budgetCategory = @paraBudget_incomeCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
            sqlStr.AppendLine("GROUP BY trans_to ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
            da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_incomeAmountReceived");

            // Step 7: Select command return a row from TableBudget_incomeAmountReceived contain the selected TotalBudget_incomeAmountReceived
            int rec_cnt = ds.Tables["TableBudget_incomeAmountReceived"].Rows.Count;
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            budgetDashBoardObj.acc_email = ACC_EMAIL;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_incomeAmountReceived
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_incomeAmountReceived"].Rows[0];
                budgetDashBoardObj.budget_incomeAmountCount = Convert.ToInt16(row["TotalBudget_incomeAmountCount"]);
                budgetDashBoardObj.budget_incomeAmountReceived = Convert.ToDouble(row["TotalBudget_incomeAmountReceived"]);
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // ReadTotalTransactionIncomeAmountReceivedByEmailAndCategory()
    } // BudgetIncomeDAO
} // PrestoPay.Entity.DB_Entities