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
    public class BudgetDashBoardDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public string InsertBudgetDashBoardByEmail(string ACC_EMAIL,
                                                    DateTime BUDGET_STARTDATE,
                                                    DateTime BUDGET_ENDDATE,

                                                    double BUDGET_INCOMEAMOUNTALLOCATED,
                                                    int BUDGET_INCOMEAMOUNTCOUNT,
                                                    double BUDGET_INCOMEAMOUNTRECEIVED,

                                                    double BUDGET_FIXEDCOSTAMOUNTALLOCATED,
                                                    int BUDGET_FIXEDCOSTAMOUNTCOUNT,
                                                    double BUDGET_FIXEDCOSTAMOUNTSPENT,

                                                    double BUDGET_FLEXSPENDINGAMOUNTALLOCATED,
                                                    int BUDGET_FLEXSPENDINGAMOUNTCOUNT,
                                                    double BUDGET_FLEXSPENDINGAMOUNTSPENT,

                                                    double BUDGET_DEBTREPAYMENTAMOUNTALLOCATED,
                                                    int BUDGET_DEBTREPAYMENTAMOUNTCOUNT,
                                                    double BUDGET_DEBTREPAYMENTAMOUNTSPENT,

                                                    double BUDGET_PRIORITYGOALSAMOUNTALLOCATED,
                                                    int BUDGET_PRIORITYGOALSAMOUNTCOUNT,
                                                    double BUDGET_PRIORITYGOALSAMOUNTSPENT,

                                                    double BUDGET_TOTALEXPENDITUREAMOUNTALLOCATED,
                                                    int BUDGET_TOTALEXPENDITUREAMOUNTCOUNT,
                                                    double BUDGET_TOTALEXPENDITUREAMOUNTSPENT,

                                                    double BUDGET_TOTALEXPENDITUREAMOUNTLEFTOVER)
        {
            string BUDGET_ID = GetNextBudgetId();

            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetDashBoard (budget_id, acc_email, budget_startDate, budget_endDate, budget_incomeAmountAllocated, ");
            sqlStr.AppendLine("budget_incomeAmountCount, budget_incomeAmountReceived, budget_fixedCostAmountAllocated, budget_fixedCostAmountCount, ");
            sqlStr.AppendLine("budget_fixedCostAmountSpent, budget_flexSpendingAmountAllocated, budget_flexSpendingAmountCount, budget_flexSpendingAmountSpent, ");
            sqlStr.AppendLine("budget_debtRepaymentAmountAllocated, budget_debtRepaymentAmountCount, budget_debtRepaymentAmountSpent, ");
            sqlStr.AppendLine("budget_priorityGoalsAmountAllocated, budget_priorityGoalsAmountCount, budget_priorityGoalsAmountSpent, ");

            sqlStr.AppendLine("budget_totalExpenditureAmountAllocated, budget_totalExpenditureAmountCount, budget_totalExpenditureAmountSpent, budget_totalExpenditureAmountLeftOver) ");

            sqlStr.AppendLine("VALUES (@paraBudget_id, @paraAcc_email, @paraBudget_startDate, @paraBudget_endDate, @paraBudget_incomeAmountAllocated, ");
            sqlStr.AppendLine("@paraBudget_incomeAmountCount, @paraBudget_incomeAmountReceived, @paraBudget_fixedCostAmountAllocated, @paraBudget_fixedCostAmountCount, ");
            sqlStr.AppendLine("@paraBudget_fixedCostAmountSpent, @paraBudget_flexSpendingAmountAllocated, @paraBudget_flexSpendingAmountCount, @paraBudget_flexSpendingAmountSpent, ");
            sqlStr.AppendLine("@paraBudget_debtRepaymentAmountAllocated, @paraBudget_debtRepaymentAmountCount, @paraBudget_debtRepaymentAmountSpent, ");
            sqlStr.AppendLine("@paraBudget_priorityGoalsAmountAllocated, @paraBudget_priorityGoalsAmountCount, @paraBudget_priorityGoalsAmountSpent, ");

            sqlStr.AppendLine("@paraBudget_totalExpenditureAmountAllocated, @paraBudget_totalExpenditureAmountCount, @paraBudget_totalExpenditureAmountSpent, @paraBudget_totalExpenditureAmountLeftOver) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_startDate", BUDGET_STARTDATE);
            sqlCmd.Parameters.AddWithValue("@paraBudget_endDate", BUDGET_ENDDATE);

            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountAllocated", BUDGET_INCOMEAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountCount", BUDGET_INCOMEAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountReceived", BUDGET_INCOMEAMOUNTRECEIVED);

            sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountAllocated", BUDGET_FIXEDCOSTAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountCount", BUDGET_FIXEDCOSTAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountSpent", BUDGET_FIXEDCOSTAMOUNTSPENT);

            sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountAllocated", BUDGET_FLEXSPENDINGAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountCount", BUDGET_FLEXSPENDINGAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountSpent", BUDGET_FLEXSPENDINGAMOUNTSPENT);

            sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountAllocated", BUDGET_DEBTREPAYMENTAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountCount", BUDGET_DEBTREPAYMENTAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountSpent", BUDGET_DEBTREPAYMENTAMOUNTSPENT);

            sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountAllocated", BUDGET_PRIORITYGOALSAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountCount", BUDGET_PRIORITYGOALSAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountSpent", BUDGET_PRIORITYGOALSAMOUNTSPENT);

            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountAllocated", BUDGET_TOTALEXPENDITUREAMOUNTALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountCount", BUDGET_TOTALEXPENDITUREAMOUNTCOUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountSpent", BUDGET_TOTALEXPENDITUREAMOUNTSPENT);

            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountLeftOver", BUDGET_TOTALEXPENDITUREAMOUNTLEFTOVER);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            // Check whether the record has been written into the Loan table successfully
            if (result > 0)
            {
                return BUDGET_ID;
            }
            else
            {
                return null;
            } // if(result)
        } // InsertBudgetDashBoardByEmail()
        

        public BudgetDashBoard UpdateBudgetDashBoardByEmail(string ACC_EMAIL)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            DataSet ds = new DataSet();
            DataTable budgetDashBoardData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * ");
            sqlStr.AppendLine("FROM BudgetDashBoard ");
            sqlStr.AppendLine("WHERE BudgetDashBoard.acc_email = @paraAcc_email ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDashBoard");

            // Step 7: Select command return a row from TableBudgetDashBoard contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableBudgetDashBoard"].Rows.Count;
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudgetDashBoard
                // DataRow is set to Rows[i] because more than one row may be returned

                // DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[0];
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[i];
                    budgetDashBoardObj.budget_id = Convert.ToString(row["budget_id"]);
                    budgetDashBoardObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDashBoardObj.budget_startDate = Convert.ToDateTime(row["budget_startDate"]);
                    budgetDashBoardObj.budget_endDate = Convert.ToDateTime(row["budget_endDate"]);

                    string BUDGET_ID = budgetDashBoardObj.budget_id;

                    // Update BudgetDashBoard in the DB
                    BudgetDashBoard budgetDashBoardObj1 = new BudgetDashBoard();
                    BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    budgetDashBoardObj1 = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);
                } // for(i)
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // UpdateBudgetDashBoardByEmail()


        public BudgetDashBoard UpdateBudgetDashBoardByBudgetId(string BUDGET_ID)
        {
            double budget_totalIncomeAmountAllocated = 0.0;
            int budget_totalIncomeAmountCount = 0;
            double budget_totalIncomeAmountReceived = 0.0;

            double budget_totalExpenditureAmountAllocated = 0.0;
            int budget_totalExpenditureAmountCount = 0;
            double budget_totalExpenditureAmountSpent = 0.0;
            double budget_totalExpenditureAmountLeftOver = 0.0;

            string budgetDashBoardDAOSuccessMsg = "";
            string budgetDashBoardDAOErrorMsg = "";

            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            DataSet ds = new DataSet();
            DataTable budgetDashBoardData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * ");
            sqlStr.AppendLine("FROM BudgetDashBoard ");
            sqlStr.AppendLine("WHERE BudgetDashBoard.budget_id = @paraBudget_id ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDashBoard");

            // Step 7: Select command return a row from TableBudgetDashBoard contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableBudgetDashBoard"].Rows.Count;
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudgetDashBoard
                // DataRow is set to Rows[i] because more than one row may be returned

                // DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[0];
                budgetDashBoardObj.budget_id = BUDGET_ID;

                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[i];
                    budgetDashBoardObj.budget_id = Convert.ToString(row["budget_id"]);
                    budgetDashBoardObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDashBoardObj.budget_startDate = Convert.ToDateTime(row["budget_startDate"]);
                    budgetDashBoardObj.budget_endDate = Convert.ToDateTime(row["budget_endDate"]);

                    string INCOME_CATEGORY = "Income";

                    // Read Total Budget SetUp Income Amount Allocated
                    BudgetDashBoard budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated = new BudgetDashBoard();
                    BudgetSetUpIncomeDAO budgetSetUpIncomeDAO = new BudgetSetUpIncomeDAO();
                    budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated = budgetSetUpIncomeDAO.ReadTotalBudgetSetUpIncomeAmountAllocatedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, INCOME_CATEGORY);

                    if (budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated != null)
                    {
                        // budgetDashBoardObj.budget_incomeAmountCount = budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated.budget_incomeAmountCount;
                        budgetDashBoardObj.budget_incomeAmountAllocated = budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated.budget_incomeAmountAllocated;
                    }
                    else
                    {
                        // budgetDashBoardObj.budget_incomeAmountCount = 0;
                        budgetDashBoardObj.budget_incomeAmountAllocated = 0.0;
                    } // if (budgetDashBoardTotalBudgetSetUpIncomeAmountAllocated)

                    // Read Total Budget Income Amount Received
                    BudgetDashBoard budgetDashBoardTotalBudgetIncomeAmountReceived = new BudgetDashBoard();
                    BudgetIncomeDAO budgetIncomeDAO = new BudgetIncomeDAO();
                    budgetDashBoardTotalBudgetIncomeAmountReceived = budgetIncomeDAO.ReadTotalBudgetIncomeAmountReceivedByEmailAndCategory(budgetDashBoardObj.acc_email, INCOME_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetIncomeAmountReceived != null)
                    {
                        budgetDashBoardObj.budget_incomeAmountCount = budgetDashBoardTotalBudgetIncomeAmountReceived.budget_incomeAmountCount;
                        budgetDashBoardObj.budget_incomeAmountReceived = budgetDashBoardTotalBudgetIncomeAmountReceived.budget_incomeAmountReceived;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_incomeAmountCount = 0;
                        budgetDashBoardObj.budget_incomeAmountReceived = 0.0;
                    } // if (budgetDashBoardTotalBudgetIncomeAmountReceived)


                    budget_totalIncomeAmountAllocated = budgetDashBoardObj.budget_incomeAmountAllocated;
                    budget_totalIncomeAmountCount = budgetDashBoardObj.budget_incomeAmountCount;
                    budget_totalIncomeAmountReceived = budgetDashBoardObj.budget_incomeAmountReceived;


                    // Read Total Budget Income Amount Received
                    // BudgetDashBoard budgetDashBoardTotalBudgetIncomeAmountReceived = new BudgetDashBoard();
                    // BudgetIncomeDAO budgetIncomeDAO = new BudgetIncomeDAO();
                    budgetDashBoardTotalBudgetIncomeAmountReceived = budgetIncomeDAO.ReadTotalTransactionIncomeAmountReceivedByEmailAndCategory(budgetDashBoardObj.acc_email, INCOME_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetIncomeAmountReceived != null)
                    {
                        budgetDashBoardObj.budget_incomeAmountCount = budgetDashBoardTotalBudgetIncomeAmountReceived.budget_incomeAmountCount;
                        budgetDashBoardObj.budget_incomeAmountReceived = budgetDashBoardTotalBudgetIncomeAmountReceived.budget_incomeAmountReceived;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_incomeAmountCount = 0;
                        budgetDashBoardObj.budget_incomeAmountReceived = 0.0;
                    } // if (budgetDashBoardTotalBudgetIncomeAmountReceived)


                    budget_totalIncomeAmountCount += budgetDashBoardObj.budget_incomeAmountCount;
                    budget_totalIncomeAmountReceived += budgetDashBoardObj.budget_incomeAmountReceived;


                    // Write Total Budget SetUp Income Amount Allocated and Total Budget Income Amount Received to BudgetDashBoard DB
                    int result = 0;
                    BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteBudgetDashBoardIncomeAmountAllocatedAndReceivedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, INCOME_CATEGORY, budget_totalIncomeAmountAllocated, budget_totalIncomeAmountCount, budget_totalIncomeAmountReceived);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Income Amount Allocated And Received have been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Income Amount Allocated And Received, please inform System Administrator!";
                    } // if (result)


                    string FIXED_COST_CATEGORY = "Fixed Cost";

                    // Read Total Budget SetUp Fixed Cost Amount Allocated
                    BudgetDashBoard budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated = new BudgetDashBoard();
                    BudgetSetUpExpenditureDAO budgetSetUpFixedCostDAO = new BudgetSetUpExpenditureDAO();
                    budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated = budgetSetUpFixedCostDAO.ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, FIXED_COST_CATEGORY);

                    if (budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated != null)
                    {
                        // budgetDashBoardObj.budget_fixedCostAmountCount = budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated.budget_fixedCostAmountCount;
                        budgetDashBoardObj.budget_fixedCostAmountAllocated = budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated.budget_fixedCostAmountAllocated;
                    }
                    else
                    {
                        // budgetDashBoardObj.budget_fixedCostAmountCount = 0;
                        budgetDashBoardObj.budget_fixedCostAmountAllocated = 0.0;
                    } // if (budgetDashBoardTotalBudgetSetUpFixedCostAmountAllocated)

                    // Read Total Budget Fixed Cost Amount Spent
                    BudgetDashBoard budgetDashBoardTotalBudgetFixedCostAmountSpent = new BudgetDashBoard();
                    BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetFixedCostAmountSpent = budgetFixedCostDAO.ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, FIXED_COST_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetFixedCostAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_fixedCostAmountCount = budgetDashBoardTotalBudgetFixedCostAmountSpent.budget_fixedCostAmountCount;
                        budgetDashBoardObj.budget_fixedCostAmountSpent = budgetDashBoardTotalBudgetFixedCostAmountSpent.budget_fixedCostAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_fixedCostAmountCount = 0;
                        budgetDashBoardObj.budget_fixedCostAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetFixedCostAmountSpent)


                    double budget_totalFixedCostAmountAllocated = budgetDashBoardObj.budget_fixedCostAmountAllocated;
                    int budget_totalFixedCostAmountCount = budgetDashBoardObj.budget_fixedCostAmountCount;
                    double budget_totalFixedCostAmountSpent = budgetDashBoardObj.budget_fixedCostAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountAllocated = budgetDashBoardObj.budget_fixedCostAmountAllocated;
                    budget_totalExpenditureAmountCount = budgetDashBoardObj.budget_fixedCostAmountCount;
                    budget_totalExpenditureAmountSpent = budgetDashBoardObj.budget_fixedCostAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;






                    // xxxcbh;






                    // Read Total Transaction Fixed Cost Amount Spent
                    // BudgetDashBoard budgetDashBoardTotalBudgetFixedCostAmountSpent = new BudgetDashBoard();
                    // BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetFixedCostAmountSpent = budgetFixedCostDAO.ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, FIXED_COST_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetFixedCostAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_fixedCostAmountCount = budgetDashBoardTotalBudgetFixedCostAmountSpent.budget_fixedCostAmountCount;
                        budgetDashBoardObj.budget_fixedCostAmountSpent = budgetDashBoardTotalBudgetFixedCostAmountSpent.budget_fixedCostAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_fixedCostAmountCount = 0;
                        budgetDashBoardObj.budget_fixedCostAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetFixedCostAmountSpent)


                    budget_totalFixedCostAmountCount += budgetDashBoardObj.budget_fixedCostAmountCount;
                    budget_totalFixedCostAmountSpent += budgetDashBoardObj.budget_fixedCostAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_fixedCostAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_fixedCostAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Write Total Budget SetUp Fixed Cost Amount Allocated and Total Budget Fixed Cost Amount Spent to BudgetDashBoard DB
                    result = 0;
                    // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory(budgetDashBoardObj.budget_id, FIXED_COST_CATEGORY, budget_totalFixedCostAmountAllocated, budget_totalFixedCostAmountCount, budget_totalFixedCostAmountSpent);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Fixed Cost Amount Allocated And Spent have been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Fixed Cost Amount Allocated And Spent, please inform System Administrator!";
                    } // if (result)


                    string FLEX_SPENDING_CATEGORY = "Flex Spending";

                    // Read Total Budget SetUp Flex Spending Amount Allocated
                    BudgetDashBoard budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated = new BudgetDashBoard();
                    BudgetSetUpExpenditureDAO budgetSetUpFlexSpendingDAO = new BudgetSetUpExpenditureDAO();
                    budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated = budgetSetUpFlexSpendingDAO.ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, FLEX_SPENDING_CATEGORY);

                    if (budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated != null)
                    {
                        // budgetDashBoardObj.budget_flexSpendingAmountCount = budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated.budget_flexSpendingAmountCount;
                        budgetDashBoardObj.budget_flexSpendingAmountAllocated = budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated.budget_flexSpendingAmountAllocated;
                    }
                    else
                    {
                        // budgetDashBoardObj.budget_flexSpendingAmountCount = 0;
                        budgetDashBoardObj.budget_flexSpendingAmountAllocated = 0.0;
                    } // if (budgetDashBoardTotalBudgetSetUpFlexSpendingAmountAllocated)

                    // Read Total Budget Flex Spending Amount Spent
                    BudgetDashBoard budgetDashBoardTotalBudgetFlexSpendingAmountSpent = new BudgetDashBoard();
                    BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetFlexSpendingAmountSpent = budgetFlexSpendingDAO.ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, FLEX_SPENDING_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetFlexSpendingAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_flexSpendingAmountCount = budgetDashBoardTotalBudgetFlexSpendingAmountSpent.budget_flexSpendingAmountCount;
                        budgetDashBoardObj.budget_flexSpendingAmountSpent = budgetDashBoardTotalBudgetFlexSpendingAmountSpent.budget_flexSpendingAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_flexSpendingAmountCount = 0;
                        budgetDashBoardObj.budget_flexSpendingAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetFlexSpendingAmountSpent)
               

                    double budget_totalFlexSpendingAmountAllocated = budgetDashBoardObj.budget_flexSpendingAmountAllocated;
                    int budget_totalFlexSpendingAmountCount = budgetDashBoardObj.budget_flexSpendingAmountCount;
                    double budget_totalFlexSpendingAmountSpent = budgetDashBoardObj.budget_flexSpendingAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountAllocated += budgetDashBoardObj.budget_flexSpendingAmountAllocated;
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_flexSpendingAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_flexSpendingAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Read Total Transaction Flex Spending Amount Spent
                    // BudgetDashBoard budgetDashBoardTotalBudgetFlexSpendingAmountSpent = new BudgetDashBoard();
                    // BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetFlexSpendingAmountSpent = budgetFlexSpendingDAO.ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, FLEX_SPENDING_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetFlexSpendingAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_flexSpendingAmountCount = budgetDashBoardTotalBudgetFlexSpendingAmountSpent.budget_flexSpendingAmountCount;
                        budgetDashBoardObj.budget_flexSpendingAmountSpent = budgetDashBoardTotalBudgetFlexSpendingAmountSpent.budget_flexSpendingAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_flexSpendingAmountCount = 0;
                        budgetDashBoardObj.budget_flexSpendingAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetFlexSpendingAmountSpent)


                    budget_totalFlexSpendingAmountCount += budgetDashBoardObj.budget_flexSpendingAmountCount;
                    budget_totalFlexSpendingAmountSpent += budgetDashBoardObj.budget_flexSpendingAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_flexSpendingAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_flexSpendingAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Write Total Budget SetUp Flex Spending Amount Allocated and Total Budget Flex Spending Amount Spent to BudgetDashBoard DB
                    result = 0;
                    // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory(budgetDashBoardObj.budget_id, FLEX_SPENDING_CATEGORY, budget_totalFlexSpendingAmountAllocated, budget_totalFlexSpendingAmountCount, budget_totalFlexSpendingAmountSpent);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Flex Spending Amount Allocated And Spent have been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Flex Spending Amount Allocated And Spent, please inform System Administrator!";
                    } // if (result)

                    
                    string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

                    // Read Total Budget SetUp Debt Repayment Amount Allocated
                    BudgetDashBoard budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated = new BudgetDashBoard();
                    BudgetSetUpExpenditureDAO budgetSetUpDebtRepaymentDAO = new BudgetSetUpExpenditureDAO();
                    budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated = budgetSetUpDebtRepaymentDAO.ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, DEBT_REPAYMENT_CATEGORY);

                    if (budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated != null)
                    {
                        // budgetDashBoardObj.budget_debtRepaymentAmountCount = budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated.budget_debtRepaymentAmountCount;
                        budgetDashBoardObj.budget_debtRepaymentAmountAllocated = budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated.budget_debtRepaymentAmountAllocated;
                    }
                    else
                    {
                        // budgetDashBoardObj.budget_debtRepaymentAmountCount = 0;
                        budgetDashBoardObj.budget_debtRepaymentAmountAllocated = 0.0;
                    } // if (budgetDashBoardTotalBudgetSetUpDebtRepaymentAmountAllocated)

                    // Read Total Budget Debt Repayment Amount Spent
                    BudgetDashBoard budgetDashBoardTotalBudgetDebtRepaymentAmountSpent = new BudgetDashBoard();
                    BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetDebtRepaymentAmountSpent = budgetDebtRepaymentDAO.ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, DEBT_REPAYMENT_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetDebtRepaymentAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_debtRepaymentAmountCount = budgetDashBoardTotalBudgetDebtRepaymentAmountSpent.budget_debtRepaymentAmountCount;
                        budgetDashBoardObj.budget_debtRepaymentAmountSpent = budgetDashBoardTotalBudgetDebtRepaymentAmountSpent.budget_debtRepaymentAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_debtRepaymentAmountCount = 0;
                        budgetDashBoardObj.budget_debtRepaymentAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetDebtRepaymentAmountSpent)
                    

                    double budget_totalDebtRepaymentAmountAllocated = budgetDashBoardObj.budget_debtRepaymentAmountAllocated;
                    int budget_totalDebtRepaymentAmountCount = budgetDashBoardObj.budget_debtRepaymentAmountCount;
                    double budget_totalDebtRepaymentAmountSpent = budgetDashBoardObj.budget_debtRepaymentAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountAllocated += budgetDashBoardObj.budget_debtRepaymentAmountAllocated;
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_debtRepaymentAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_debtRepaymentAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Read Total Transaction Debt Repayment Amount Spent
                    // BudgetDashBoard budgetDashBoardTotalBudgetDebtRepaymentAmountSpent = new BudgetDashBoard();
                    // BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetDebtRepaymentAmountSpent = budgetDebtRepaymentDAO.ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, DEBT_REPAYMENT_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetDebtRepaymentAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_debtRepaymentAmountCount = budgetDashBoardTotalBudgetDebtRepaymentAmountSpent.budget_debtRepaymentAmountCount;
                        budgetDashBoardObj.budget_debtRepaymentAmountSpent = budgetDashBoardTotalBudgetDebtRepaymentAmountSpent.budget_debtRepaymentAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_debtRepaymentAmountCount = 0;
                        budgetDashBoardObj.budget_debtRepaymentAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetDebtRepaymentAmountSpent)


                    budget_totalDebtRepaymentAmountCount += budgetDashBoardObj.budget_debtRepaymentAmountCount;
                    budget_totalDebtRepaymentAmountSpent += budgetDashBoardObj.budget_debtRepaymentAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_debtRepaymentAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_debtRepaymentAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Write Total Budget SetUp Debt Repayment Amount Allocated and Total Budget Debt Repayment Amount Spent to BudgetDashBoard DB
                    result = 0;
                    // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory(budgetDashBoardObj.budget_id, DEBT_REPAYMENT_CATEGORY, budget_totalDebtRepaymentAmountAllocated, budget_totalDebtRepaymentAmountCount, budget_totalDebtRepaymentAmountSpent);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Debt Repayment Amount Allocated And Spent have been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Debt Repayment Amount Allocated And Spent, please inform System Administrator!";
                    } // if (result)


                    string PRIORITY_GOALS_CATEGORY = "Priority Goals";

                    // Read Total Budget SetUp Priority Goals Amount Allocated
                    BudgetDashBoard budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated = new BudgetDashBoard();
                    BudgetSetUpExpenditureDAO budgetSetUpPriorityGoalsDAO = new BudgetSetUpExpenditureDAO();
                    budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated = budgetSetUpPriorityGoalsDAO.ReadTotalBudgetSetUpExpenditureAmountAllocatedByBudgetIdAndCategory(budgetDashBoardObj.budget_id, PRIORITY_GOALS_CATEGORY);

                    if (budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated != null)
                    {
                        // budgetDashBoardObj.budget_priorityGoalsAmountCount = budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated.budget_priorityGoalsAmountCount;
                        budgetDashBoardObj.budget_priorityGoalsAmountAllocated = budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated.budget_priorityGoalsAmountAllocated;
                    }
                    else
                    {
                        // budgetDashBoardObj.budget_priorityGoalsAmountCount = 0;
                        budgetDashBoardObj.budget_priorityGoalsAmountAllocated = 0.0;
                    } // if (budgetDashBoardTotalBudgetSetUpPriorityGoalsAmountAllocated)

                    // Read Total Budget Priority Goals Amount Spent
                    BudgetDashBoard budgetDashBoardTotalBudgetPriorityGoalsAmountSpent = new BudgetDashBoard();
                    BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetPriorityGoalsAmountSpent = budgetPriorityGoalsDAO.ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, PRIORITY_GOALS_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetPriorityGoalsAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_priorityGoalsAmountCount = budgetDashBoardTotalBudgetPriorityGoalsAmountSpent.budget_priorityGoalsAmountCount;
                        budgetDashBoardObj.budget_priorityGoalsAmountSpent = budgetDashBoardTotalBudgetPriorityGoalsAmountSpent.budget_priorityGoalsAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_priorityGoalsAmountCount = 0;
                        budgetDashBoardObj.budget_priorityGoalsAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetPriorityGoalsAmountSpent)
                    

                    double budget_totalPriorityGoalsAmountAllocated = budgetDashBoardObj.budget_priorityGoalsAmountAllocated;
                    int budget_totalPriorityGoalsAmountCount = budgetDashBoardObj.budget_priorityGoalsAmountCount;
                    double budget_totalPriorityGoalsAmountSpent = budgetDashBoardObj.budget_priorityGoalsAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountAllocated += budgetDashBoardObj.budget_priorityGoalsAmountAllocated;
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_priorityGoalsAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_priorityGoalsAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Read Total Transaction Priority Goals Amount Spent
                    // BudgetDashBoard budgetDashBoardTotalBudgetPriorityGoalsAmountSpent = new BudgetDashBoard();
                    // BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                    budgetDashBoardTotalBudgetPriorityGoalsAmountSpent = budgetPriorityGoalsDAO.ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory(budgetDashBoardObj.acc_email, PRIORITY_GOALS_CATEGORY, budgetDashBoardObj.budget_startDate, budgetDashBoardObj.budget_endDate);

                    if (budgetDashBoardTotalBudgetPriorityGoalsAmountSpent != null)
                    {
                        budgetDashBoardObj.budget_priorityGoalsAmountCount = budgetDashBoardTotalBudgetPriorityGoalsAmountSpent.budget_priorityGoalsAmountCount;
                        budgetDashBoardObj.budget_priorityGoalsAmountSpent = budgetDashBoardTotalBudgetPriorityGoalsAmountSpent.budget_priorityGoalsAmountSpent;
                    }
                    else
                    {
                        budgetDashBoardObj.budget_priorityGoalsAmountCount = 0;
                        budgetDashBoardObj.budget_priorityGoalsAmountSpent = 0.0;
                    } // if (budgetDashBoardTotalBudgetPriorityGoalsAmountSpent)


                    budget_totalPriorityGoalsAmountCount += budgetDashBoardObj.budget_priorityGoalsAmountCount;
                    budget_totalPriorityGoalsAmountSpent += budgetDashBoardObj.budget_priorityGoalsAmountSpent;

                    // Calculate the Total Budget Expenditure Left Over Amount
                    budget_totalExpenditureAmountCount += budgetDashBoardObj.budget_priorityGoalsAmountCount;
                    budget_totalExpenditureAmountSpent += budgetDashBoardObj.budget_priorityGoalsAmountSpent;
                    budget_totalExpenditureAmountLeftOver = budget_totalExpenditureAmountAllocated - budget_totalExpenditureAmountSpent;


                    // Write Total Budget SetUp Priority Goals Amount Allocated and Total Budget Priority Goals Amount Spent to BudgetDashBoard DB
                    result = 0;
                    // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory(budgetDashBoardObj.budget_id, PRIORITY_GOALS_CATEGORY, budget_totalPriorityGoalsAmountAllocated, budget_totalPriorityGoalsAmountCount, budget_totalPriorityGoalsAmountSpent);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Priority Goals Amount Allocated And Spent have been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Priority Goals Amount Allocated And Spent, please inform System Administrator!";
                    } // if (result)


                    // Write Total Budget Expenditure Left Over Amount to BudgetDashBoard DB
                    result = 0;
                    // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                    result = budgetDashBoardDAO.WriteTotalBudgetExpenditureLeftOverAmountByBudgetId(budgetDashBoardObj.budget_id, budget_totalExpenditureAmountAllocated, budget_totalExpenditureAmountCount, budget_totalExpenditureAmountSpent, budget_totalExpenditureAmountLeftOver);
                    if (result == 1)
                    {
                        budgetDashBoardDAOSuccessMsg = "Budget DashBoard Total Budget Expenditure Left Over Amount has been written sucessfully!";
                    }
                    else
                    {
                        budgetDashBoardDAOErrorMsg = "Error: Unable to write Budget DashBoard Total Budget Expenditure Left Over Amount, please inform System Administrator!";
                    } // if (result)
                } // for(i)
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // UpdateBudgetDashBoardByBudgetId()


        public int WriteBudgetDashBoardIncomeAmountAllocatedAndReceivedByBudgetIdAndCategory(string BUDGET_ID, string INCOME_CATEGORY, double INCOME_AMOUNT_ALLOCATED, int INCOME_AMOUNT_COUNT, double INCOME_AMOUNT_RECEIVED)
        {
            int result = 0;    // Execute NonQuery return an integer value

            if (INCOME_CATEGORY == "Income")
            {
                StringBuilder sqlStr = new StringBuilder();
                SqlCommand sqlCmd = new SqlCommand();
                // Step1 : Create SQL update command to change column for the selected record in DB using     
                //         parameterised query in values clause
                //
                sqlStr.AppendLine("UPDATE BudgetDashBoard ");
                sqlStr.AppendLine("SET budget_incomeAmountAllocated = @paraBudget_incomeAmountAllocated, ");
                sqlStr.AppendLine("budget_incomeAmountCount = @paraBudget_incomeAmountCount, ");
                sqlStr.AppendLine("budget_incomeAmountReceived = @paraBudget_incomeAmountReceived ");
                sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

                // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

                SqlConnection myConn = new SqlConnection(DBConnect);

                sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

                // Step 3 : Add each parameterised query variable with value
                //          complete to add all parameterised queries
                sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountAllocated", INCOME_AMOUNT_ALLOCATED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountCount", INCOME_AMOUNT_COUNT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_incomeAmountReceived", INCOME_AMOUNT_RECEIVED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

                // Step 4 Open connection the execute NonQuery of sql command   
                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                // Step 5 :Close connection
                myConn.Close();
            } // if(INCOME_CATEGORY)

            return result;
        } // WriteBudgetDashBoardIncomeAmountAllocatedAndReceivedByBudgetIdAndCategory()


        public int WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory(string BUDGET_ID, string EXPENDITURE_CATEGORY, double EXPENDITURE_AMOUNT_ALLOCATED, int EXPENDITURE_AMOUNT_COUNT, double EXPENDITURE_AMOUNT_SPENT)
        {
            int result = 0;    // Execute NonQuery return an integer value

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                StringBuilder sqlStr = new StringBuilder();
                SqlCommand sqlCmd = new SqlCommand();
                // Step1 : Create SQL update command to change column for the selected record in DB using     
                //         parameterised query in values clause
                //
                sqlStr.AppendLine("UPDATE BudgetDashBoard ");
                sqlStr.AppendLine("SET budget_fixedCostAmountAllocated = @paraBudget_fixedCostAmountAllocated, ");
                sqlStr.AppendLine("budget_fixedCostAmountCount = @paraBudget_fixedCostAmountCount, ");
                sqlStr.AppendLine("budget_fixedCostAmountSpent = @paraBudget_fixedCostAmountSpent ");
                sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

                // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

                SqlConnection myConn = new SqlConnection(DBConnect);

                sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

                // Step 3 : Add each parameterised query variable with value
                //          complete to add all parameterised queries
                sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountAllocated", EXPENDITURE_AMOUNT_ALLOCATED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountCount", EXPENDITURE_AMOUNT_COUNT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_fixedCostAmountSpent", EXPENDITURE_AMOUNT_SPENT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

                // Step 4 Open connection the execute NonQuery of sql command   
                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                // Step 5 :Close connection
                myConn.Close();
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                StringBuilder sqlStr = new StringBuilder();
                SqlCommand sqlCmd = new SqlCommand();
                // Step1 : Create SQL update command to change column for the selected record in DB using     
                //         parameterised query in values clause
                //
                sqlStr.AppendLine("UPDATE BudgetDashBoard ");
                sqlStr.AppendLine("SET budget_flexSpendingAmountAllocated = @paraBudget_flexSpendingAmountAllocated, ");
                sqlStr.AppendLine("budget_flexSpendingAmountCount = @paraBudget_flexSpendingAmountCount, ");
                sqlStr.AppendLine("budget_flexSpendingAmountSpent = @paraBudget_flexSpendingAmountSpent ");
                sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

                // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

                SqlConnection myConn = new SqlConnection(DBConnect);

                sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

                // Step 3 : Add each parameterised query variable with value
                //          complete to add all parameterised queries
                sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountAllocated", EXPENDITURE_AMOUNT_ALLOCATED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountCount", EXPENDITURE_AMOUNT_COUNT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_flexSpendingAmountSpent", EXPENDITURE_AMOUNT_SPENT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

                // Step 4 Open connection the execute NonQuery of sql command   
                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                // Step 5 :Close connection
                myConn.Close();
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                StringBuilder sqlStr = new StringBuilder();
                SqlCommand sqlCmd = new SqlCommand();
                // Step1 : Create SQL update command to change column for the selected record in DB using     
                //         parameterised query in values clause
                //
                sqlStr.AppendLine("UPDATE BudgetDashBoard ");
                sqlStr.AppendLine("SET budget_debtRepaymentAmountAllocated = @paraBudget_debtRepaymentAmountAllocated, ");
                sqlStr.AppendLine("budget_debtRepaymentAmountCount = @paraBudget_debtRepaymentAmountCount, ");
                sqlStr.AppendLine("budget_debtRepaymentAmountSpent = @paraBudget_debtRepaymentAmountSpent ");
                sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

                // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

                SqlConnection myConn = new SqlConnection(DBConnect);

                sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

                // Step 3 : Add each parameterised query variable with value
                //          complete to add all parameterised queries
                sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountAllocated", EXPENDITURE_AMOUNT_ALLOCATED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountCount", EXPENDITURE_AMOUNT_COUNT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_debtRepaymentAmountSpent", EXPENDITURE_AMOUNT_SPENT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

                // Step 4 Open connection the execute NonQuery of sql command   
                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                // Step 5 :Close connection
                myConn.Close();
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                StringBuilder sqlStr = new StringBuilder();
                SqlCommand sqlCmd = new SqlCommand();
                // Step1 : Create SQL update command to change column for the selected record in DB using     
                //         parameterised query in values clause
                //
                sqlStr.AppendLine("UPDATE BudgetDashBoard ");
                sqlStr.AppendLine("SET budget_priorityGoalsAmountAllocated = @paraBudget_priorityGoalsAmountAllocated, ");
                sqlStr.AppendLine("budget_priorityGoalsAmountCount = @paraBudget_priorityGoalsAmountCount, ");
                sqlStr.AppendLine("budget_priorityGoalsAmountSpent = @paraBudget_priorityGoalsAmountSpent ");
                sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

                // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

                SqlConnection myConn = new SqlConnection(DBConnect);

                sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

                // Step 3 : Add each parameterised query variable with value
                //          complete to add all parameterised queries
                sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountAllocated", EXPENDITURE_AMOUNT_ALLOCATED);
                sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountCount", EXPENDITURE_AMOUNT_COUNT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_priorityGoalsAmountSpent", EXPENDITURE_AMOUNT_SPENT);
                sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

                // Step 4 Open connection the execute NonQuery of sql command   
                myConn.Open();
                result = sqlCmd.ExecuteNonQuery();

                // Step 5 :Close connection
                myConn.Close();
            } // if(EXPENDITURE_CATEGORY)

            return result;
        } // WriteBudgetDashBoardExpenditureAmountAllocatedAndSpentByBudgetIdAndCategory()


        public int WriteTotalBudgetExpenditureLeftOverAmountByBudgetId(string BUDGET_ID, double TOTAL_EXPENDITURE_AMOUNT_ALLOCATED, int TOTAL_EXPENDITURE_AMOUNT_COUNT, double TOTAL_EXPENDITURE_AMOUNT_SPENT, double TOTAL_EXPENDITURE_AMOUNT_LEFTOVER)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL update command to change column for the selected record in DB using     
            //         parameterised query in values clause
            //
            sqlStr.AppendLine("UPDATE BudgetDashBoard ");
            sqlStr.AppendLine("SET budget_totalExpenditureAmountAllocated = @paraBudget_totalExpenditureAmountAllocated, ");
            sqlStr.AppendLine("budget_totalExpenditureAmountCount = @paraBudget_totalExpenditureAmountCount, ");
            sqlStr.AppendLine("budget_totalExpenditureAmountSpent = @paraBudget_totalExpenditureAmountSpent, ");
            sqlStr.AppendLine("budget_totalExpenditureAmountLeftOver = @paraBudget_totalExpenditureAmountLeftOver ");
            sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountAllocated", TOTAL_EXPENDITURE_AMOUNT_ALLOCATED);
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountCount", TOTAL_EXPENDITURE_AMOUNT_COUNT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountSpent", TOTAL_EXPENDITURE_AMOUNT_SPENT);
            sqlCmd.Parameters.AddWithValue("@paraBudget_totalExpenditureAmountLeftOver", TOTAL_EXPENDITURE_AMOUNT_LEFTOVER);
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // WriteTotalBudgetExpenditureLeftOverAmountByBudgetId()


        public List<BudgetDashBoard> CheckBudgetDashBoardByEmail(string ACC_EMAIL)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            List <BudgetDashBoard> budgetDashBoardList = new List<BudgetDashBoard>();
            DataSet ds = new DataSet();
            DataTable budgetDashBoardData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * ");
            sqlStr.AppendLine("FROM BudgetDashBoard ");
            sqlStr.AppendLine("WHERE BudgetDashBoard.acc_email = @paraAcc_email ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDashBoard");

            // Step 7: Select command return a row from TableBudgetDashBoard contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableBudgetDashBoard"].Rows.Count;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudgetDashBoard
                // DataRow is set to Rows[i] because more than one row may be returned

                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

                    DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[i];

                    budgetDashBoardObj.budget_id = Convert.ToString(row["budget_id"]);
                    budgetDashBoardObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDashBoardObj.budget_startDate = Convert.ToDateTime(row["budget_startDate"]);
                    budgetDashBoardObj.budget_endDate = Convert.ToDateTime(row["budget_endDate"]);

                    budgetDashBoardObj.budget_incomeAmountAllocated = Convert.ToDouble(row["budget_incomeAmountAllocated"]);
                    budgetDashBoardObj.budget_incomeAmountCount = Convert.ToInt16(row["budget_incomeAmountCount"]);
                    budgetDashBoardObj.budget_incomeAmountReceived = Convert.ToDouble(row["budget_incomeAmountReceived"]);

                    budgetDashBoardObj.budget_fixedCostAmountAllocated = Convert.ToDouble(row["budget_fixedCostAmountAllocated"]);
                    budgetDashBoardObj.budget_fixedCostAmountCount = Convert.ToInt16(row["budget_fixedCostAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostAmountSpent = Convert.ToDouble(row["budget_fixedCostAmountSpent"]);

                    budgetDashBoardObj.budget_flexSpendingAmountAllocated = Convert.ToDouble(row["budget_flexSpendingAmountAllocated"]);
                    budgetDashBoardObj.budget_flexSpendingAmountCount = Convert.ToInt16(row["budget_flexSpendingAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingAmountSpent = Convert.ToDouble(row["budget_flexSpendingAmountSpent"]);

                    budgetDashBoardObj.budget_debtRepaymentAmountAllocated = Convert.ToDouble(row["budget_debtRepaymentAmountAllocated"]);
                    budgetDashBoardObj.budget_debtRepaymentAmountCount = Convert.ToInt16(row["budget_debtRepaymentAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentAmountSpent = Convert.ToDouble(row["budget_debtRepaymentAmountSpent"]);

                    budgetDashBoardObj.budget_priorityGoalsAmountAllocated = Convert.ToDouble(row["budget_priorityGoalsAmountAllocated"]);
                    budgetDashBoardObj.budget_priorityGoalsAmountCount = Convert.ToInt16(row["budget_priorityGoalsAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsAmountSpent = Convert.ToDouble(row["budget_priorityGoalsAmountSpent"]);

                    budgetDashBoardObj.budget_totalExpenditureAmountAllocated = Convert.ToDouble(row["budget_totalExpenditureAmountAllocated"]);
                    budgetDashBoardObj.budget_totalExpenditureAmountCount = Convert.ToInt16(row["budget_totalExpenditureAmountCount"]);
                    budgetDashBoardObj.budget_totalExpenditureAmountSpent = Convert.ToDouble(row["budget_totalExpenditureAmountSpent"]);

                    budgetDashBoardObj.budget_totalExpenditureAmountLeftOver = Convert.ToDouble(row["budget_totalExpenditureAmountLeftOver"]);

                    //  Step 9: Add each Loan instance to array list
                    budgetDashBoardList.Add(budgetDashBoardObj);
                } // for(i)
            }
            else
            {
                budgetDashBoardList = null;
            } //if (rec_cnt)

            return budgetDashBoardList;
        } // CheckBudgetDashBoardByEmail()


        public BudgetDashBoard CheckBudgetDashBoardByBudgetId(string BUDGET_ID)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

            DataSet ds = new DataSet();
            DataTable budgetDashBoardData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised BUDGET_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT * ");
            sqlStr.AppendLine("FROM BudgetDashBoard ");
            sqlStr.AppendLine("WHERE BudgetDashBoard.budget_id = @paraBudget_id ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDashBoard");

            // Step 7: Select command return a row from TableBudgetDashBoard contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableBudgetDashBoard"].Rows.Count;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudgetDashBoard
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudgetDashBoard"].Rows[0];

                budgetDashBoardObj.budget_id = Convert.ToString(row["budget_id"]);
                budgetDashBoardObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetDashBoardObj.budget_startDate = Convert.ToDateTime(row["budget_startDate"]);
                budgetDashBoardObj.budget_endDate = Convert.ToDateTime(row["budget_endDate"]);

                budgetDashBoardObj.budget_incomeAmountAllocated = Convert.ToDouble(row["budget_incomeAmountAllocated"]);
                budgetDashBoardObj.budget_incomeAmountCount = Convert.ToInt16(row["budget_incomeAmountCount"]);
                budgetDashBoardObj.budget_incomeAmountReceived = Convert.ToDouble(row["budget_incomeAmountReceived"]);

                budgetDashBoardObj.budget_fixedCostAmountAllocated = Convert.ToDouble(row["budget_fixedCostAmountAllocated"]);
                budgetDashBoardObj.budget_fixedCostAmountCount = Convert.ToInt16(row["budget_fixedCostAmountCount"]);
                budgetDashBoardObj.budget_fixedCostAmountSpent = Convert.ToDouble(row["budget_fixedCostAmountSpent"]);

                budgetDashBoardObj.budget_flexSpendingAmountAllocated = Convert.ToDouble(row["budget_flexSpendingAmountAllocated"]);
                budgetDashBoardObj.budget_flexSpendingAmountCount = Convert.ToInt16(row["budget_flexSpendingAmountCount"]);
                budgetDashBoardObj.budget_flexSpendingAmountSpent = Convert.ToDouble(row["budget_flexSpendingAmountSpent"]);

                budgetDashBoardObj.budget_debtRepaymentAmountAllocated = Convert.ToDouble(row["budget_debtRepaymentAmountAllocated"]);
                budgetDashBoardObj.budget_debtRepaymentAmountCount = Convert.ToInt16(row["budget_debtRepaymentAmountCount"]);
                budgetDashBoardObj.budget_debtRepaymentAmountSpent = Convert.ToDouble(row["budget_debtRepaymentAmountSpent"]);

                budgetDashBoardObj.budget_priorityGoalsAmountAllocated = Convert.ToDouble(row["budget_priorityGoalsAmountAllocated"]);
                budgetDashBoardObj.budget_priorityGoalsAmountCount = Convert.ToInt16(row["budget_priorityGoalsAmountCount"]);
                budgetDashBoardObj.budget_priorityGoalsAmountSpent = Convert.ToDouble(row["budget_priorityGoalsAmountSpent"]);

                budgetDashBoardObj.budget_totalExpenditureAmountAllocated = Convert.ToDouble(row["budget_totalExpenditureAmountAllocated"]);
                budgetDashBoardObj.budget_totalExpenditureAmountCount = Convert.ToInt16(row["budget_totalExpenditureAmountCount"]);
                budgetDashBoardObj.budget_totalExpenditureAmountSpent = Convert.ToDouble(row["budget_totalExpenditureAmountSpent"]);

                budgetDashBoardObj.budget_totalExpenditureAmountLeftOver = Convert.ToDouble(row["budget_totalExpenditureAmountLeftOver"]);
            }
            else
            {
                budgetDashBoardObj = null;
            } //if (rec_cnt)

            return budgetDashBoardObj;
        } // CheckBudgetDashBoardByBudgetId()
        

        public int DeleteBudgetDashBoardByBudgetId(string BUDGET_ID)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL update command to change column for the selected record in DB using     
            //         parameterised query in values clause
            //
            sqlStr.AppendLine("DELETE FROM BudgetDashBoard ");

            sqlStr.AppendLine("WHERE budget_id = @paraBudget_id ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetDashBoardByBudgetId()


        public string GetNextBudgetId()
        {
            string BUDGET_ID = "";

            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised BUDGET_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT TOP 1 budget_id FROM BudgetDashBoard ORDER BY budget_id DESC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 
            // da.SelectCommand.Parameters.AddWithValue("@paraBudget_id", BUDGET_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudget_id");

            // Step 7: Select command return a row from TableBudget_id contain the selected BUDGET_ID
            int rec_cnt = ds.Tables["TableBudget_id"].Rows.Count;

            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

            int intBudget_id = 0;

            if (rec_cnt > 0)
            {
                // BudgetDashBoard loanIdObj = new BudgetDashBoard();
                // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_id
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBudget_id"].Rows[0];

                // The budgetDashBoardObj.budget_id contains "BG00000002" BUDGET_ID
                budgetDashBoardObj.budget_id = Convert.ToString(row["budget_id"]);

                // get the length of the current string
                int intLength = budgetDashBoardObj.budget_id.Length;

                while (intLength > 0)
                {
                    // Check whether the budgetDashBoardObj.budget_id contains numeric "00000002" BUDGET_ID
                    bool result = int.TryParse(budgetDashBoardObj.budget_id, out intBudget_id);
                    if (result == true)
                    {
                        intBudget_id = int.Parse(budgetDashBoardObj.budget_id);
                        break;
                    }
                    else
                    {
                        // The budgetDashBoardObj.budget_id contains non numeric "BG00000002" BUDGET_ID

                        // The budgetDashBoardObj.budget_id contains non numeric "G00000002" BUDGET_ID
                        budgetDashBoardObj.budget_id = budgetDashBoardObj.budget_id.Substring(1);

                        // get the length of the new string
                        intLength = budgetDashBoardObj.budget_id.Length;
                    } // if (result)
                } // while (intLength)

                // The intBudget_id contains 2 BUDGET_ID

                // The intBudget_id contains 3 BUDGET_ID
                intBudget_id++;

                // The BUDGET_ID contains BG00000003 BUDGET_ID
                BUDGET_ID = "BG" + intBudget_id.ToString("D8");
            }
            else
            {
                budgetDashBoardObj = null;

                // The BudgetDashBoard table is empty
                // Start with the first BG00000001 BUDGET_ID
                BUDGET_ID = "BG00000001";
            } //if (rec_cnt)

            return BUDGET_ID;
        } // GetNextBudgetId()
    } // BudgetDashBoardDAO
} // PrestoPay.Entity.DB_Entities