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
    public class BudgetIncomeCategoryDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public List<BudgetIncomeCategory> ReadBudgetIncomeCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string INCOME_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetIncomeCategory
            //           DataSet instance and dataTable instance 

            List<BudgetIncomeCategory> budgetIncomeCategoryList = new List<BudgetIncomeCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetIncomeCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_incomeCategory, budget_incomeSubCategory ");
            sqlStr.AppendLine("FROM BudgetIncomeCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_incomeCategory = @paraBudget_incomeCategory) ");
            sqlStr.AppendLine("ORDER BY budget_incomeSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetIncomeCategory");

            // Step 7: Iterate the rows from TableBudgetIncomeCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetIncomeCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableBudgetIncomeCategory"].Rows)
                {
                    BudgetIncomeCategory budgetIncomeCategoryObj = new BudgetIncomeCategory();

                    // Step 8: Set attribute of BudgetIncomeCategory instance for each row of record in TableBudgetIncomeCategory
                    budgetIncomeCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetIncomeCategoryObj.budget_incomeCategory = Convert.ToString(row["budget_incomeCategory"]);
                    budgetIncomeCategoryObj.budget_incomeSubCategory = Convert.ToString(row["budget_incomeSubCategory"]);

                    //  Step 9: Add each BudgetIncomeCategory instance to array list
                    if((budgetIncomeCategoryObj.budget_incomeCategory != "") && (budgetIncomeCategoryObj.budget_incomeSubCategory != ""))
                    {
                        budgetIncomeCategoryList.Add(budgetIncomeCategoryObj);
                    } // if((budgetIncomeCategoryObj.budget_incomeCategory)
                } // foreach (DataRow row)
            }
            else
            {
                budgetIncomeCategoryList = null;
            } // if (rec_cnt)

            return budgetIncomeCategoryList;
        } // ReadBudgetIncomeCategoryAndSubCategoryByCategoryAndSubCategory()


        public BudgetIncomeCategory ReadBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string INCOME_CATEGORY, string INCOME_SUBCATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetIncomeCategory
            //           DataSet instance and dataTable instance 

            BudgetIncomeCategory budgetIncomeCategoryObj = new BudgetIncomeCategory();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetIncomeCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_incomeCategory, budget_incomeSubCategory ");
            sqlStr.AppendLine("FROM BudgetIncomeCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_incomeCategory = @paraBudget_incomeCategory) AND (budget_incomeSubCategory = @paraBudget_incomeSubCategory) ");
            sqlStr.AppendLine("ORDER BY budget_incomeSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_incomeSubCategory", INCOME_SUBCATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetIncomeCategory");

            // Step 7: Iterate the rows from TableBudgetIncomeCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetIncomeCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                // Step 8: Set attribute of BudgetIncomeCategory instance for each row of record in TableBudgetIncomeCategory
                DataRow row = ds.Tables["TableBudgetIncomeCategory"].Rows[0];
                budgetIncomeCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetIncomeCategoryObj.budget_incomeCategory = Convert.ToString(row["budget_incomeCategory"]);
                budgetIncomeCategoryObj.budget_incomeSubCategory = Convert.ToString(row["budget_incomeSubCategory"]);
            }
            else
            {
                budgetIncomeCategoryObj = null;
            } // if (rec_cnt)

            return budgetIncomeCategoryObj;
        } // ReadBudgetIncomeCategoryAndSubCategoryByCategoryAndSubCategory()


        public int InsertBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string INCOME_CATEGORY, string INCOME_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetIncomeCategory (acc_email, budget_incomeCategory, budget_incomeSubCategory) ");

            sqlStr.AppendLine("VALUES (@paraAcc_email, @paraBudget_incomeCategory, @paraBudget_incomeSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeSubCategory", INCOME_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public int DeleteBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string INCOME_CATEGORY, string INCOME_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();           
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("DELETE FROM BudgetIncomeCategory ");

            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_incomeCategory = @paraBudget_incomeCategory) AND (budget_incomeSubCategory = @paraBudget_incomeSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeCategory", INCOME_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_incomeSubCategory", INCOME_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory()



    } // BudgetIncomeCategoryDAO
} // PrestoPay.Entity.DB_Entities