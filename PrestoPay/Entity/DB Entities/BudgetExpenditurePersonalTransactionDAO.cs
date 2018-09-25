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
    public class BudgetExpenditurePersonalTransactionDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public string GetNextBudgetExpenditurePersonalID()
        {
            string BUDGET_EXPENDITURE_ID = "";

            // Step 2 : declare a BudgetExpenditure, DataSet instance and dataTable instance
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised BUDGET_EXPENDITURE_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT TOP 1 budget_expenditureId FROM BudgetExpenditure ORDER BY budget_expenditureId DESC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 
            // da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_EXPENDITURE_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_expenditureId");

            // Step 7: Select command return a row from TableBudget_expenditureId contain the selected BUDGET_EXPENDITURE_ID
            int rec_cnt = ds.Tables["TableBudget_expenditureId"].Rows.Count;

            BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

            int intBudget_id = 0;

            if (rec_cnt > 0)
            {
                // BudgetExpenditure loanIdObj = new BudgetExpenditure();
                // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureId
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_expenditureId"].Rows[0];

                // The budgetExpenditureObj.budget_expenditureId contains "BE00000002" BUDGET_EXPENDITURE_ID
                budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);

                // get the length of the current string
                int intLength = budgetExpenditureObj.budget_expenditureId.Length;

                while (intLength > 0)
                {
                    // Check whether the budgetExpenditureObj.budget_expenditureId contains numeric "00000002" BUDGET_EXPENDITURE_ID
                    bool result = int.TryParse(budgetExpenditureObj.budget_expenditureId, out intBudget_id);
                    if (result == true)
                    {
                        intBudget_id = int.Parse(budgetExpenditureObj.budget_expenditureId);
                        break;
                    }
                    else
                    {
                        // The budgetExpenditureObj.budget_expenditureId contains non numeric "BE00000002" BUDGET_EXPENDITURE_ID

                        // The budgetExpenditureObj.budget_expenditureId contains non numeric "E00000002" BUDGET_EXPENDITURE_ID
                        budgetExpenditureObj.budget_expenditureId = budgetExpenditureObj.budget_expenditureId.Substring(1);

                        // get the length of the new string
                        intLength = budgetExpenditureObj.budget_expenditureId.Length;
                    } // if (result)
                } // while (intLength)

                // The intBudget_id contains 2 BUDGET_EXPENDITURE_ID

                // The intBudget_id contains 3 BUDGET_EXPENDITURE_ID
                intBudget_id++;

                // The BUDGET_EXPENDITURE_ID contains BE00000003 BUDGET_EXPENDITURE_ID
                BUDGET_EXPENDITURE_ID = "BE" + intBudget_id.ToString("D8");
            }
            else
            {
                budgetExpenditureObj = null;

                // The BudgetExpenditure table is empty
                // Start with the first BE00000001 BUDGET_EXPENDITURE_ID
                BUDGET_EXPENDITURE_ID = "BE00000001";
            } //if (rec_cnt)

            return BUDGET_EXPENDITURE_ID;
        } // GetNextBudgetExpenditurePersonalID()


        public string InsertPersonalExpenseTransactionIntoBudgetExpenditureTableByEmail(string ACC_EMAIL,
                                                                                            string BUDGET_EXPENDITURE_CATEGORY,
                                                                                            string BUDGET_EXPENDITURE_SUB_CATEGORY,
                                                                                            double BUDGET_EXPENDITURE_AMOUNT_SPENT,
                                                                                            DateTime BUDGET_EXPENDITURE_DATE,
                                                                                            string BUDGET_EXPENDITURE_REMARKS)
        {
            // Get the next BUDGET_EXPENDITURE_ID
            string BUDGET_EXPENDITURE_ID = GetNextBudgetExpenditurePersonalID();

            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetExpenditure (budget_expenditureId, acc_email, budget_expenditureCategory, budget_expenditureSubCategory, ");
            sqlStr.AppendLine("budget_expenditureAmountSpent, budget_expenditureDate, budget_expenditureRemarks) ");


            sqlStr.AppendLine("VALUES (@paraBudget_expenditureId, @paraAcc_email, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory, ");
            sqlStr.AppendLine("@paraBudget_expenditureAmountSpent, @paraBudget_expenditureDate, @paraBudget_expenditureRemarks)");


            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureId", BUDGET_EXPENDITURE_ID);
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", BUDGET_EXPENDITURE_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", BUDGET_EXPENDITURE_SUB_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureAmountSpent", BUDGET_EXPENDITURE_AMOUNT_SPENT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureDate", BUDGET_EXPENDITURE_DATE);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureRemarks", BUDGET_EXPENDITURE_REMARKS);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            // Check whether the record has been written into the Loan table successfully
            if (result > 0)
            {
                return BUDGET_EXPENDITURE_ID;
            }
            else
            {
                return null;
            } // if(result)
        } // InsertPersonalExpenseTransactionIntoBudgetExpenditureTableByEmail()


        public string GetNextBudgetIncomePersonalID()
        {
            string BUDGET_INCOME_ID = "";

            // Step 2 : declare a BudgetIncome, DataSet instance and dataTable instance
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetIncome Table by parameterised BUDGET_INCOME_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT TOP 1 budget_incomeId FROM BudgetIncome ORDER BY budget_incomeId DESC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 
            // da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_INCOME_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_incomeId");

            // Step 7: Select command return a row from TableBudget_incomeId contain the selected BUDGET_INCOME_ID
            int rec_cnt = ds.Tables["TableBudget_incomeId"].Rows.Count;

            BudgetIncome budgetIncomeObj = new BudgetIncome();

            int intBudget_id = 0;

            if (rec_cnt > 0)
            {
                // BudgetIncome loanIdObj = new BudgetIncome();
                // Step 8 Set attribute of BudgetIncome instance for the record in TableBudget_incomeId
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_incomeId"].Rows[0];

                // The budgetIncomeObj.budget_incomeId contains "BI00000002" BUDGET_INCOME_ID
                budgetIncomeObj.budget_incomeId = Convert.ToString(row["budget_incomeId"]);

                // get the length of the current string
                int intLength = budgetIncomeObj.budget_incomeId.Length;

                while (intLength > 0)
                {
                    // Check whether the budgetIncomeObj.budget_incomeId contains numeric "00000002" BUDGET_INCOME_ID
                    bool result = int.TryParse(budgetIncomeObj.budget_incomeId, out intBudget_id);
                    if (result == true)
                    {
                        intBudget_id = int.Parse(budgetIncomeObj.budget_incomeId);
                        break;
                    }
                    else
                    {
                        // The budgetIncomeObj.budget_incomeId contains non numeric "BI00000002" BUDGET_INCOME_ID

                        // The budgetIncomeObj.budget_incomeId contains non numeric "I00000002" BUDGET_INCOME_ID
                        budgetIncomeObj.budget_incomeId = budgetIncomeObj.budget_incomeId.Substring(1);

                        // get the length of the new string
                        intLength = budgetIncomeObj.budget_incomeId.Length;
                    } // if (result)
                } // while (intLength)

                // The intBudget_id contains 2 BUDGET_INCOME_ID

                // The intBudget_id contains 3 BUDGET_INCOME_ID
                intBudget_id++;

                // The BUDGET_INCOME_ID contains BI00000003 BUDGET_INCOME_ID
                BUDGET_INCOME_ID = "BI" + intBudget_id.ToString("D8");
            }
            else
            {
                budgetIncomeObj = null;

                // The BudgetIncome table is empty
                // Start with the first BI00000001 BUDGET_INCOME_ID
                BUDGET_INCOME_ID = "BI00000001";
            } //if (rec_cnt)

            return BUDGET_INCOME_ID;
        } // GetNextBudgetIncomePersonalID()


        public string InsertPersonalIncomeTransactionIntoBudgetIncomeTableByEmail(string ACC_EMAIL,
                                                                                    string BUDGET_INCOME_CATEGORY,
                                                                                    string BUDGET_INCOME_SUB_CATEGORY,
                                                                                    double BUDGET_INCOME_AMOUNT_RECEIVED,
                                                                                    DateTime BUDGET_INCOME_DATE,
                                                                                    string BUDGET_INCOME_REMARKS)
        {
            // Get the next BUDGET_INCOME_ID
            string BUDGET_INCOME_ID = GetNextBudgetIncomePersonalID();

            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();            
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to DB using     

            //         parameterised query in values clause
            //
            sqlStr.AppendLine("INSERT INTO BudgetIncome (budget_incomeId, acc_email, budget_incomeCategory, budget_incomeSubCategory, ");
            sqlStr.AppendLine("budget_incomeAmountReceived, budget_incomeDate, budget_incomeRemarks) ");


            sqlStr.AppendLine("VALUES (@paraBudget_incomeId, @paraAcc_email,  @paraBudget_incomeCategory, @paraBudget_incomeSubCategory, ");
            sqlStr.AppendLine("@paraBudget_incomeAmountReceived, @paraBudget_incomeDate, @paraBudget_incomeRemarks)");


            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeId", BUDGET_INCOME_ID);
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeCategory", BUDGET_INCOME_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeSubCategory", BUDGET_INCOME_SUB_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountReceived", BUDGET_INCOME_AMOUNT_RECEIVED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeDate", BUDGET_INCOME_DATE);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeRemarks", BUDGET_INCOME_REMARKS);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            // Check whether the record has been written into the Loan table successfully
            if (result > 0)
            {
                return BUDGET_INCOME_ID;
            }
            else
            {
                return null;
            } // if(result)
        } // InsertPersonalIncomeTransactionIntoBudgetIncomeTableByEmail()

    }
}