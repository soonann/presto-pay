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
    public class BudgetExpenditureDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public List<BudgetExpenditure> ReadBudgetExpenditureAmountSpentByEmailAndCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetExpenditure, DataSet instance and dataTable instance
            List<BudgetExpenditure> budgetExpenditureList = new List<BudgetExpenditure>();

            DataSet ds = new DataSet();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetExpenditureList;
        } // ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory()


        public List<BudgetExpenditure> ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetExpenditure, DataSet instance and dataTable instance
            List<BudgetExpenditure> budgetExpenditureList = new List<BudgetExpenditure>();

            DataSet ds = new DataSet();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");

                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetExpenditure Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY budget_expenditureId ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of BudgetExpenditure instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        BudgetExpenditure budgetExpenditureObj = new BudgetExpenditure();

                        budgetExpenditureObj.acc_email = Convert.ToString(row["acc_email"]);
                        budgetExpenditureObj.budget_expenditureId = Convert.ToString(row["budget_expenditureId"]);
                        budgetExpenditureObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                        budgetExpenditureObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
                        budgetExpenditureObj.budget_expenditureAmountSpent = Convert.ToDouble(row["budget_expenditureAmountSpent"]);
                        budgetExpenditureObj.budget_expenditureDate = Convert.ToDateTime(row["budget_expenditureDate"]);
                        budgetExpenditureObj.budget_expenditureRemarks = Convert.ToString(row["budget_expenditureRemarks"]);

                        budgetExpenditureList.Add(budgetExpenditureObj);
                    } // for (i)
                }
                else
                {
                    budgetExpenditureList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetExpenditureList;
        } // ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory()


        public BudgetDashBoard ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalBudgetExpenditureAmountSpentByEmailAndCategory()


        public BudgetDashBoard ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT acc_email, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(budget_expenditureAmountSpent) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM BudgetExpenditure ");
                sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) AND (budget_expenditureDate BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY acc_email ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory()


        public List<CategorisedTransaction> ReadTransactionExpenditureAmountSpentByEmailAndCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a CategorisedTransaction, DataSet instance and dataTable instance
            List<CategorisedTransaction> categorisedTransactionList = new List<CategorisedTransaction>();

            DataSet ds = new DataSet();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return categorisedTransactionList;
        } // ReadTransactionExpenditureAmountSpentByEmailAndCategory()


        public List<CategorisedTransaction> ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a CategorisedTransaction, DataSet instance and dataTable instance
            List<CategorisedTransaction> categorisedTransactionList = new List<CategorisedTransaction>();

            DataSet ds = new DataSet();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (budgetSubCategory = @paraBudget_expenditureSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (budgetSubCategory = @paraBudget_expenditureSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (budgetSubCategory = @paraBudget_expenditureSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from CategorisedTransaction Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT * ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (budgetSubCategory = @paraBudget_expenditureSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("ORDER BY trans_id ASC ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;

                if (rec_cnt > 0)
                {
                    // Step 8 Set attribute of CategorisedTransaction instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[i] because more than one row may be returned

                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[i];

                        string db_id = Convert.ToString(row["trans_id"]);
                        double db_amt = Convert.ToDouble(row["trans_amt"]);
                        string db_description = Convert.ToString(row["trans_description"]);
                        string db_type = Convert.ToString(row["trans_type"]);
                        string db_from = Convert.ToString(row["trans_from"]);
                        string db_to = Convert.ToString(row["trans_to"]);
                        DateTime db_date = Convert.ToDateTime(row["trans_date"]);

                        string db_budgetCategory = Convert.ToString(row["budgetCategory"]);
                        string db_budgetSubCategory = Convert.ToString(row["budgetSubCategory"]);

                        CategorisedTransaction categorisedTransactionObj = new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory);

                        categorisedTransactionList.Add(categorisedTransactionObj);
                    } // for (i)
                }
                else
                {
                    categorisedTransactionList = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return categorisedTransactionList;
        } // ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory()


        public BudgetDashBoard ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_expenditureCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");


                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalTransactionExpenditureAmountSpentByEmailAndCategory()


        public BudgetDashBoard ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(string ACC_EMAIL, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            // Step 2 : declare a BudgetDashBoard, DataSet instance and dataTable instance
            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoard budgetDashBoardObj = null;

            DataSet ds = new DataSet();
            // DataTable budgetDashBoardData = new DataTable();

            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);


            // Must Check dtmStartDate and dtmEndDate


            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_Category) AND (budgetSubCategory = @paraBudgetSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_Category", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudgetSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_Category) AND (budgetSubCategory = @paraBudgetSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_Category", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudgetSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_Category) AND (budgetSubCategory = @paraBudgetSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_Category", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudgetSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Step 3 :Create SQLcommand to select all columns from BudgetDashBoard Table by parameterised ACC_EMAIL

                StringBuilder sqlStr = new StringBuilder();
                sqlStr.AppendLine("SELECT trans_from, COUNT(*) AS TotalBudget_expenditureAmountCount, SUM(trans_amt) AS TotalBudget_expenditureAmountSpent ");
                sqlStr.AppendLine("FROM [Transaction] ");
                sqlStr.AppendLine("WHERE (trans_from = @paraAcc_email) AND (budgetCategory = @paraBudget_Category) AND (budgetSubCategory = @paraBudgetSubCategory) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date) ");
                sqlStr.AppendLine("GROUP BY trans_from ");

                // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

                SqlConnection myConn = new SqlConnection(DBConnect);
                SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

                // Step 5 :add value to parameter 

                da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
                da.SelectCommand.Parameters.AddWithValue("@paraBudget_Category", EXPENDITURE_CATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraBudgetSubCategory", EXPENDITURE_SUBCATEGORY);
                da.SelectCommand.Parameters.AddWithValue("@paraStart_date", dtmStartDate);
                da.SelectCommand.Parameters.AddWithValue("@paraEnd_date", dtmEndDate);

                // Step 6: fill dataset
                da.Fill(ds, "TableBudget_expenditureAmountSpent");

                // Step 7: Select command return a row from TableBudget_expenditureAmountSpent contain the selected TotalBudget_expenditureAmountSpent
                int rec_cnt = ds.Tables["TableBudget_expenditureAmountSpent"].Rows.Count;
                budgetDashBoardObj = new BudgetDashBoard();
                budgetDashBoardObj.acc_email = ACC_EMAIL;

                if (rec_cnt > 0)
                {
                    // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    // Step 8 Set attribute of BudgetDashBoard instance for the record in TableBudget_expenditureAmountSpent
                    // DataRow is set to Rows[0] because only one row is returned

                    DataRow row = ds.Tables["TableBudget_expenditureAmountSpent"].Rows[0];
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = Convert.ToInt16(row["TotalBudget_expenditureAmountCount"]);
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = Convert.ToDouble(row["TotalBudget_expenditureAmountSpent"]);
                }
                else
                {
                    budgetDashBoardObj = null;
                } //if (rec_cnt)
            } // if(EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory()


        public BudgetDashBoard CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(string ACC_EMAIL, string BUDGET_ID, string EXPENDITURE_CATEGORY, string EXPENDITURE_SUBCATEGORY, DateTime START_DATE, DateTime END_DATE)
        {
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();

            budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated = 0.0;
            budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = 0;
            budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = 0.0;

            budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated = 0.0;
            budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = 0;
            budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = 0.0;

            budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated = 0.0;
            budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = 0;
            budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = 0.0;

            budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated = 0.0;
            budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = 0;
            budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = 0.0;

            if (EXPENDITURE_CATEGORY == "Fixed Cost")
            {
                // Read Total Budget SetUp Fixed Cost SubCategory Amount Allocated
                BudgetDashBoard budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated = new BudgetDashBoard();
                BudgetSetUpExpenditureDAO budgetSetUpFixedCostDAO = new BudgetSetUpExpenditureDAO();
                budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated = budgetSetUpFixedCostDAO.ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY);

                if (budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated != null)
                {
                    // budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated.budget_fixedCostSubCategoryAmountCount;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated = budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated.budget_fixedCostSubCategoryAmountAllocated;
                }
                else
                {
                    // budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated = 0.0;
                } // if (budgetDashBoardTotalBudgetSetUpFixedCostSubCategoryAmountAllocated)

                // Read Total Budget Fixed Cost SubCategory Amount Spent
                BudgetDashBoard budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent = new BudgetDashBoard();
                BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent = budgetFixedCostDAO.ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent.budget_fixedCostSubCategoryAmountCount;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent.budget_fixedCostSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent)


                double budget_totalFixedCostSubCategoryAmountAllocated = budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated;
                int budget_totalFixedCostSubCategoryAmountCount = budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount;
                double budget_totalFixedCostSubCategoryAmountSpent = budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent;


                // Read Total Transaction Fixed Cost SubCategory Amount Spent
                // BudgetDashBoard budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent = new BudgetDashBoard();
                // BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent = budgetFixedCostDAO.ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent.budget_fixedCostSubCategoryAmountCount;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent.budget_fixedCostSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetFixedCostSubCategoryAmountSpent)

                
                budget_totalFixedCostSubCategoryAmountCount += budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount;
                budget_totalFixedCostSubCategoryAmountSpent += budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent;

                // Save Total Budget SetUp Fixed Cost SubCategory Amount Allocated and Total Budget Fixed Cost SubCategory Amount Spent in budgetDashBoardObj object
                budgetDashBoardObj.budget_fixedCostSubCategoryAmountAllocated = budget_totalFixedCostSubCategoryAmountAllocated;
                budgetDashBoardObj.budget_fixedCostSubCategoryAmountCount = budget_totalFixedCostSubCategoryAmountCount;
                budgetDashBoardObj.budget_fixedCostSubCategoryAmountSpent = budget_totalFixedCostSubCategoryAmountSpent;
            }
            else if (EXPENDITURE_CATEGORY == "Flex Spending")
            {
                // Read Total Budget SetUp Flex Spending SubCategory Amount Allocated
                BudgetDashBoard budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated = new BudgetDashBoard();
                BudgetSetUpExpenditureDAO budgetSetUpFlexSpendingDAO = new BudgetSetUpExpenditureDAO();
                budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated = budgetSetUpFlexSpendingDAO.ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY);

                if (budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated != null)
                {
                    // budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated.budget_flexSpendingSubCategoryAmountCount;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated = budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated.budget_flexSpendingSubCategoryAmountAllocated;
                }
                else
                {
                    // budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated = 0.0;
                } // if (budgetDashBoardTotalBudgetSetUpFlexSpendingSubCategoryAmountAllocated)

                // Read Total Budget Flex Spending SubCategory Amount Spent
                BudgetDashBoard budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent = new BudgetDashBoard();
                BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent = budgetFlexSpendingDAO.ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent.budget_flexSpendingSubCategoryAmountCount;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent.budget_flexSpendingSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent)


                double budget_totalFlexSpendingSubCategoryAmountAllocated = budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated;
                int budget_totalFlexSpendingSubCategoryAmountCount = budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount;
                double budget_totalFlexSpendingSubCategoryAmountSpent = budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent;


                // Read Total Transaction Flex Spending SubCategory Amount Spent
                // BudgetDashBoard budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent = new BudgetDashBoard();
                // BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent = budgetFlexSpendingDAO.ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent.budget_flexSpendingSubCategoryAmountCount;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent.budget_flexSpendingSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetFlexSpendingSubCategoryAmountSpent)


                budget_totalFlexSpendingSubCategoryAmountCount += budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount;
                budget_totalFlexSpendingSubCategoryAmountSpent += budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent;

                // Save Total Budget SetUp Flex Spending SubCategory Amount Allocated and Total Budget Flex Spending SubCategory Amount Spent in budgetDashBoardObj object
                budgetDashBoardObj.budget_flexSpendingSubCategoryAmountAllocated = budget_totalFlexSpendingSubCategoryAmountAllocated;
                budgetDashBoardObj.budget_flexSpendingSubCategoryAmountCount = budget_totalFlexSpendingSubCategoryAmountCount;
                budgetDashBoardObj.budget_flexSpendingSubCategoryAmountSpent = budget_totalFlexSpendingSubCategoryAmountSpent;
            }
            else if (EXPENDITURE_CATEGORY == "Debt Repayment")
            {
                // Read Total Budget SetUp Debt Repayment SubCategory Amount Allocated
                BudgetDashBoard budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated = new BudgetDashBoard();
                BudgetSetUpExpenditureDAO budgetSetUpDebtRepaymentDAO = new BudgetSetUpExpenditureDAO();
                budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated = budgetSetUpDebtRepaymentDAO.ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY);

                if (budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated != null)
                {
                    // budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated.budget_debtRepaymentSubCategoryAmountCount;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated = budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated.budget_debtRepaymentSubCategoryAmountAllocated;
                }
                else
                {
                    // budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated = 0.0;
                } // if (budgetDashBoardTotalBudgetSetUpDebtRepaymentSubCategoryAmountAllocated)

                // Read Total Budget Debt Repayment SubCategory Amount Spent
                BudgetDashBoard budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent = new BudgetDashBoard();
                BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent = budgetDebtRepaymentDAO.ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent.budget_debtRepaymentSubCategoryAmountCount;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent.budget_debtRepaymentSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent)


                double budget_totalDebtRepaymentSubCategoryAmountAllocated = budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated;
                int budget_totalDebtRepaymentSubCategoryAmountCount = budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount;
                double budget_totalDebtRepaymentSubCategoryAmountSpent = budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent;


                // Read Total Transaction Debt Repayment SubCategory Amount Spent
                // BudgetDashBoard budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent = new BudgetDashBoard();
                // BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent = budgetDebtRepaymentDAO.ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent.budget_debtRepaymentSubCategoryAmountCount;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent.budget_debtRepaymentSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetDebtRepaymentSubCategoryAmountSpent)


                budget_totalDebtRepaymentSubCategoryAmountCount += budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount;
                budget_totalDebtRepaymentSubCategoryAmountSpent += budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent;

                // Save Total Budget SetUp Debt Repayment SubCategory Amount Allocated and Total Budget Debt Repayment SubCategory Amount Spent in budgetDashBoardObj object
                budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountAllocated = budget_totalDebtRepaymentSubCategoryAmountAllocated;
                budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountCount = budget_totalDebtRepaymentSubCategoryAmountCount;
                budgetDashBoardObj.budget_debtRepaymentSubCategoryAmountSpent = budget_totalDebtRepaymentSubCategoryAmountSpent;
            }
            else if (EXPENDITURE_CATEGORY == "Priority Goals")
            {
                // Read Total Budget SetUp Priority Goals SubCategory Amount Allocated
                BudgetDashBoard budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated = new BudgetDashBoard();
                BudgetSetUpExpenditureDAO budgetSetUpPriorityGoalsDAO = new BudgetSetUpExpenditureDAO();
                budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated = budgetSetUpPriorityGoalsDAO.ReadTotalBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY);

                if (budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated != null)
                {
                    // budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated.budget_priorityGoalsSubCategoryAmountCount;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated = budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated.budget_priorityGoalsSubCategoryAmountAllocated;
                }
                else
                {
                    // budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated = 0.0;
                } // if (budgetDashBoardTotalBudgetSetUpPriorityGoalsSubCategoryAmountAllocated)

                // Read Total Budget Priority Goals SubCategory Amount Spent
                BudgetDashBoard budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent = new BudgetDashBoard();
                BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent = budgetPriorityGoalsDAO.ReadTotalBudgetExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent.budget_priorityGoalsSubCategoryAmountCount;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent.budget_priorityGoalsSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent)


                double budget_totalPriorityGoalsSubCategoryAmountAllocated = budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated;
                int budget_totalPriorityGoalsSubCategoryAmountCount = budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount;
                double budget_totalPriorityGoalsSubCategoryAmountSpent = budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent;


                // Read Total Transaction Priority Goals SubCategory Amount Spent
                // BudgetDashBoard budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent = new BudgetDashBoard();
                // BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent = budgetPriorityGoalsDAO.ReadTotalTransactionExpenditureSubCategoryAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, EXPENDITURE_CATEGORY, EXPENDITURE_SUBCATEGORY, START_DATE, END_DATE);

                if (budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent != null)
                {
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent.budget_priorityGoalsSubCategoryAmountCount;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent.budget_priorityGoalsSubCategoryAmountSpent;
                }
                else
                {
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = 0;
                    budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = 0.0;
                } // if (budgetDashBoardTotalBudgetPriorityGoalsSubCategoryAmountSpent)


                budget_totalPriorityGoalsSubCategoryAmountCount += budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount;
                budget_totalPriorityGoalsSubCategoryAmountSpent += budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent;

                // Save Total Budget SetUp Priority Goals SubCategory Amount Allocated and Total Budget Priority Goals SubCategory Amount Spent in budgetDashBoardObj object
                budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountAllocated = budget_totalPriorityGoalsSubCategoryAmountAllocated;
                budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountCount = budget_totalPriorityGoalsSubCategoryAmountCount;
                budgetDashBoardObj.budget_priorityGoalsSubCategoryAmountSpent = budget_totalPriorityGoalsSubCategoryAmountSpent;
            } // if (EXPENDITURE_CATEGORY)

            return budgetDashBoardObj;
        } // CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory()

    } // BudgetExpenditureDAO
} // PrestoPay.Entity.DB_Entities