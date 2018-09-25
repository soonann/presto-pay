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
    public class BudgetExpenditureCategoryDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public List<BudgetExpenditureCategory> ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string FIXED_COST_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;

            string strFixedCostSubCategory = "";

            int intFixedCostSubCategoryRowCount = 0;

            if (rec_cnt > 0)
            {
                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                    budgetFixedCostCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                    {
                        strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                        budgetFixedCostCategoryList.Add(budgetFixedCostCategoryObj);

                        intFixedCostSubCategoryRowCount += 1;
                    } // if(strFixedCostSubCategory)
                } // for (i)
            }
            else
            {
                budgetFixedCostCategoryList = null;
            } // if (rec_cnt)

            return budgetFixedCostCategoryList;
        } // ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory()


        public List<BudgetExpenditureCategory> CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string FIXED_COST_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            string strFixedCostSubCategory = "";

            int rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                    budgetFixedCostCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                    {
                        strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                        bool blnFixedCostSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFixedCostCategoryList != null)
                        {
                            rec_cnt2 = budgetFixedCostCategoryList.Count;
                        } // if (budgetFixedCostCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFixedCostSubCategory already exist in the budgetFixedCostCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFixedCostCategoryObj2 = budgetFixedCostCategoryList[j];

                                if (strFixedCostSubCategory == budgetFixedCostCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFixedCostSubCategoryFound = true;
                                    break;
                                } // if (strFixedCostSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFixedCostSubCategoryFound == false)
                        {
                            budgetFixedCostCategoryList.Add(budgetFixedCostCategoryObj);
                        } // if (blnFixedCostSubCategoryFound)
                    } // if(strFixedCostSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            string strAcc_email = "";

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", strAcc_email);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            // string strFixedCostSubCategory = "";
            strFixedCostSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                    budgetFixedCostCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                    {
                        strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                        bool blnFixedCostSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFixedCostCategoryList != null)
                        {
                            rec_cnt2 = budgetFixedCostCategoryList.Count;
                        } // if (budgetFixedCostCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFixedCostSubCategory already exist in the budgetFixedCostCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFixedCostCategoryObj2 = budgetFixedCostCategoryList[j];

                                if (strFixedCostSubCategory == budgetFixedCostCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFixedCostSubCategoryFound = true;
                                    break;
                                } // if (strFixedCostSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFixedCostSubCategoryFound == false)
                        {
                            budgetFixedCostCategoryList.Add(budgetFixedCostCategoryObj);
                        } // if (blnFixedCostSubCategoryFound)
                    } // if(strFixedCostSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditure ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            // string strFixedCostSubCategory = "";
            strFixedCostSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                    budgetFixedCostCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                    {
                        strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                        bool blnFixedCostSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFixedCostCategoryList != null)
                        {
                            rec_cnt2 = budgetFixedCostCategoryList.Count;
                        } // if (budgetFixedCostCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFixedCostSubCategory already exist in the budgetFixedCostCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFixedCostCategoryObj2 = budgetFixedCostCategoryList[j];

                                if (strFixedCostSubCategory == budgetFixedCostCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFixedCostSubCategoryFound = true;
                                    break;
                                } // if (strFixedCostSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFixedCostSubCategoryFound == false)
                        {
                            budgetFixedCostCategoryList.Add(budgetFixedCostCategoryObj);
                        } // if (blnFixedCostSubCategoryFound)
                    } // if(strFixedCostSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT budgetCategory, budgetSubCategory ");
            sqlStr.AppendLine("FROM [Transaction] ");
            sqlStr.AppendLine("WHERE (budgetCategory = @paraBudgetCategory) ");
            sqlStr.AppendLine("ORDER BY budgetSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudgetCategory", FIXED_COST_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            // string strFixedCostSubCategory = "";
            strFixedCostSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                    budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budgetCategory"]);
                    budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budgetSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                    {
                        strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                        bool blnFixedCostSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFixedCostCategoryList != null)
                        {
                            rec_cnt2 = budgetFixedCostCategoryList.Count;
                        } // if (budgetFixedCostCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFixedCostSubCategory already exist in the budgetFixedCostCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFixedCostCategoryObj2 = budgetFixedCostCategoryList[j];

                                if (strFixedCostSubCategory == budgetFixedCostCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFixedCostSubCategoryFound = true;
                                    break;
                                } // if (strFixedCostSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFixedCostSubCategoryFound == false)
                        {
                            budgetFixedCostCategoryList.Add(budgetFixedCostCategoryObj);
                        } // if (blnFixedCostSubCategoryFound)
                    } // if(strFixedCostSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            int rec_cnt3 = 0;
            if (budgetFixedCostCategoryList != null)
            {
                rec_cnt3 = budgetFixedCostCategoryList.Count;
            } // if (budgetFixedCostCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt3 <= 0)
            {
                budgetFixedCostCategoryList = null;
            } // if (rec_cnt3)


            return budgetFixedCostCategoryList;
        } // CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory()


        public List<BudgetExpenditureCategory> CheckBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string FLEX_SPENDING_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            string strFlexSpendingSubCategory = "";

            int rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFlexSpendingCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                    budgetFlexSpendingCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                    {
                        strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                        bool blnFlexSpendingSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFlexSpendingCategoryList != null)
                        {
                            rec_cnt2 = budgetFlexSpendingCategoryList.Count;
                        } // if (budgetFlexSpendingCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFlexSpendingSubCategory already exist in the budgetFlexSpendingCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFlexSpendingCategoryObj2 = budgetFlexSpendingCategoryList[j];

                                if (strFlexSpendingSubCategory == budgetFlexSpendingCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFlexSpendingSubCategoryFound = true;
                                    break;
                                } // if (strFlexSpendingSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFlexSpendingSubCategoryFound == false)
                        {
                            budgetFlexSpendingCategoryList.Add(budgetFlexSpendingCategoryObj);
                        } // if (blnFlexSpendingSubCategoryFound)
                    } // if(strFlexSpendingSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            string strAcc_email = "";

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", strAcc_email);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            // string strFlexSpendingSubCategory = "";
            strFlexSpendingSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFlexSpendingCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                    budgetFlexSpendingCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                    {
                        strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                        bool blnFlexSpendingSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFlexSpendingCategoryList != null)
                        {
                            rec_cnt2 = budgetFlexSpendingCategoryList.Count;
                        } // if (budgetFlexSpendingCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFlexSpendingSubCategory already exist in the budgetFlexSpendingCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFlexSpendingCategoryObj2 = budgetFlexSpendingCategoryList[j];

                                if (strFlexSpendingSubCategory == budgetFlexSpendingCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFlexSpendingSubCategoryFound = true;
                                    break;
                                } // if (strFlexSpendingSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFlexSpendingSubCategoryFound == false)
                        {
                            budgetFlexSpendingCategoryList.Add(budgetFlexSpendingCategoryObj);
                        } // if (blnFlexSpendingSubCategoryFound)
                    } // if(strFlexSpendingSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditure ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            // string strFlexSpendingSubCategory = "";
            strFlexSpendingSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFlexSpendingCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                    budgetFlexSpendingCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                    {
                        strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                        bool blnFlexSpendingSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFlexSpendingCategoryList != null)
                        {
                            rec_cnt2 = budgetFlexSpendingCategoryList.Count;
                        } // if (budgetFlexSpendingCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFlexSpendingSubCategory already exist in the budgetFlexSpendingCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFlexSpendingCategoryObj2 = budgetFlexSpendingCategoryList[j];

                                if (strFlexSpendingSubCategory == budgetFlexSpendingCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFlexSpendingSubCategoryFound = true;
                                    break;
                                } // if (strFlexSpendingSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFlexSpendingSubCategoryFound == false)
                        {
                            budgetFlexSpendingCategoryList.Add(budgetFlexSpendingCategoryObj);
                        } // if (blnFlexSpendingSubCategoryFound)
                    } // if(strFlexSpendingSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT budgetCategory, budgetSubCategory ");
            sqlStr.AppendLine("FROM [Transaction] ");
            sqlStr.AppendLine("WHERE (budgetCategory = @paraBudgetCategory) ");
            sqlStr.AppendLine("ORDER BY budgetSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudgetCategory", FLEX_SPENDING_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            // string strFlexSpendingSubCategory = "";
            strFlexSpendingSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetFlexSpendingCategory"].Rows[i];

                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                    budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budgetCategory"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budgetSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                    {
                        strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                        bool blnFlexSpendingSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetFlexSpendingCategoryList != null)
                        {
                            rec_cnt2 = budgetFlexSpendingCategoryList.Count;
                        } // if (budgetFlexSpendingCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strFlexSpendingSubCategory already exist in the budgetFlexSpendingCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetFlexSpendingCategoryObj2 = budgetFlexSpendingCategoryList[j];

                                if (strFlexSpendingSubCategory == budgetFlexSpendingCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnFlexSpendingSubCategoryFound = true;
                                    break;
                                } // if (strFlexSpendingSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnFlexSpendingSubCategoryFound == false)
                        {
                            budgetFlexSpendingCategoryList.Add(budgetFlexSpendingCategoryObj);
                        } // if (blnFlexSpendingSubCategoryFound)
                    } // if(strFlexSpendingSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            int rec_cnt3 = 0;
            if (budgetFlexSpendingCategoryList != null)
            {
                rec_cnt3 = budgetFlexSpendingCategoryList.Count;
            } // if (budgetFlexSpendingCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt3 <= 0)
            {
                budgetFlexSpendingCategoryList = null;
            } // if (rec_cnt3)


            return budgetFlexSpendingCategoryList;
        } // CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory()


        public List<BudgetExpenditureCategory> CheckBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string DEBT_REPAYMENT_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            string strDebtRepaymentSubCategory = "";

            int rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows[i];

                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                    budgetDebtRepaymentCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                    {
                        strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                        bool blnDebtRepaymentSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetDebtRepaymentCategoryList != null)
                        {
                            rec_cnt2 = budgetDebtRepaymentCategoryList.Count;
                        } // if (budgetDebtRepaymentCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strDebtRepaymentSubCategory already exist in the budgetDebtRepaymentCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetDebtRepaymentCategoryObj2 = budgetDebtRepaymentCategoryList[j];

                                if (strDebtRepaymentSubCategory == budgetDebtRepaymentCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnDebtRepaymentSubCategoryFound = true;
                                    break;
                                } // if (strDebtRepaymentSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnDebtRepaymentSubCategoryFound == false)
                        {
                            budgetDebtRepaymentCategoryList.Add(budgetDebtRepaymentCategoryObj);
                        } // if (blnDebtRepaymentSubCategoryFound)
                    } // if(strDebtRepaymentSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            string strAcc_email = "";

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", strAcc_email);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            // string strDebtRepaymentSubCategory = "";
            strDebtRepaymentSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows[i];

                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                    budgetDebtRepaymentCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                    {
                        strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                        bool blnDebtRepaymentSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetDebtRepaymentCategoryList != null)
                        {
                            rec_cnt2 = budgetDebtRepaymentCategoryList.Count;
                        } // if (budgetDebtRepaymentCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strDebtRepaymentSubCategory already exist in the budgetDebtRepaymentCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetDebtRepaymentCategoryObj2 = budgetDebtRepaymentCategoryList[j];

                                if (strDebtRepaymentSubCategory == budgetDebtRepaymentCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnDebtRepaymentSubCategoryFound = true;
                                    break;
                                } // if (strDebtRepaymentSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnDebtRepaymentSubCategoryFound == false)
                        {
                            budgetDebtRepaymentCategoryList.Add(budgetDebtRepaymentCategoryObj);
                        } // if (blnDebtRepaymentSubCategoryFound)
                    } // if(strDebtRepaymentSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditure ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            // string strDebtRepaymentSubCategory = "";
            strDebtRepaymentSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows[i];

                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                    budgetDebtRepaymentCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                    {
                        strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                        bool blnDebtRepaymentSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetDebtRepaymentCategoryList != null)
                        {
                            rec_cnt2 = budgetDebtRepaymentCategoryList.Count;
                        } // if (budgetDebtRepaymentCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strDebtRepaymentSubCategory already exist in the budgetDebtRepaymentCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetDebtRepaymentCategoryObj2 = budgetDebtRepaymentCategoryList[j];

                                if (strDebtRepaymentSubCategory == budgetDebtRepaymentCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnDebtRepaymentSubCategoryFound = true;
                                    break;
                                } // if (strDebtRepaymentSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnDebtRepaymentSubCategoryFound == false)
                        {
                            budgetDebtRepaymentCategoryList.Add(budgetDebtRepaymentCategoryObj);
                        } // if (blnDebtRepaymentSubCategoryFound)
                    } // if(strDebtRepaymentSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT budgetCategory, budgetSubCategory ");
            sqlStr.AppendLine("FROM [Transaction] ");
            sqlStr.AppendLine("WHERE (budgetCategory = @paraBudgetCategory) ");
            sqlStr.AppendLine("ORDER BY budgetSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudgetCategory", DEBT_REPAYMENT_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            // string strDebtRepaymentSubCategory = "";
            strDebtRepaymentSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows[i];

                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                    budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budgetCategory"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budgetSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                    {
                        strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                        bool blnDebtRepaymentSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetDebtRepaymentCategoryList != null)
                        {
                            rec_cnt2 = budgetDebtRepaymentCategoryList.Count;
                        } // if (budgetDebtRepaymentCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strDebtRepaymentSubCategory already exist in the budgetDebtRepaymentCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetDebtRepaymentCategoryObj2 = budgetDebtRepaymentCategoryList[j];

                                if (strDebtRepaymentSubCategory == budgetDebtRepaymentCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnDebtRepaymentSubCategoryFound = true;
                                    break;
                                } // if (strDebtRepaymentSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnDebtRepaymentSubCategoryFound == false)
                        {
                            budgetDebtRepaymentCategoryList.Add(budgetDebtRepaymentCategoryObj);
                        } // if (blnDebtRepaymentSubCategoryFound)
                    } // if(strDebtRepaymentSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            int rec_cnt3 = 0;
            if (budgetDebtRepaymentCategoryList != null)
            {
                rec_cnt3 = budgetDebtRepaymentCategoryList.Count;
            } // if (budgetDebtRepaymentCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt3 <= 0)
            {
                budgetDebtRepaymentCategoryList = null;
            } // if (rec_cnt3)


            return budgetDebtRepaymentCategoryList;
        } // CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory()


        public List<BudgetExpenditureCategory> CheckBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string PRIORITY_GOALS_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            string strPriorityGoalsSubCategory = "";

            int rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows[i];

                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                    budgetPriorityGoalsCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                    {
                        strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                        bool blnPriorityGoalsSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetPriorityGoalsCategoryList != null)
                        {
                            rec_cnt2 = budgetPriorityGoalsCategoryList.Count;
                        } // if (budgetPriorityGoalsCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strPriorityGoalsSubCategory already exist in the budgetPriorityGoalsCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetPriorityGoalsCategoryObj2 = budgetPriorityGoalsCategoryList[j];

                                if (strPriorityGoalsSubCategory == budgetPriorityGoalsCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnPriorityGoalsSubCategoryFound = true;
                                    break;
                                } // if (strPriorityGoalsSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnPriorityGoalsSubCategoryFound == false)
                        {
                            budgetPriorityGoalsCategoryList.Add(budgetPriorityGoalsCategoryObj);
                        } // if (blnPriorityGoalsSubCategoryFound)
                    } // if(strPriorityGoalsSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            string strAcc_email = "";

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", strAcc_email);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            // string strPriorityGoalsSubCategory = "";
            strPriorityGoalsSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows[i];

                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                    budgetPriorityGoalsCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                    {
                        strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                        bool blnPriorityGoalsSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetPriorityGoalsCategoryList != null)
                        {
                            rec_cnt2 = budgetPriorityGoalsCategoryList.Count;
                        } // if (budgetPriorityGoalsCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strPriorityGoalsSubCategory already exist in the budgetPriorityGoalsCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetPriorityGoalsCategoryObj2 = budgetPriorityGoalsCategoryList[j];

                                if (strPriorityGoalsSubCategory == budgetPriorityGoalsCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnPriorityGoalsSubCategoryFound = true;
                                    break;
                                } // if (strPriorityGoalsSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnPriorityGoalsSubCategoryFound == false)
                        {
                            budgetPriorityGoalsCategoryList.Add(budgetPriorityGoalsCategoryObj);
                        } // if (blnPriorityGoalsSubCategoryFound)
                    } // if(strPriorityGoalsSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditure ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            // string strPriorityGoalsSubCategory = "";
            strPriorityGoalsSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows[i];

                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                    budgetPriorityGoalsCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                    {
                        strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                        bool blnPriorityGoalsSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetPriorityGoalsCategoryList != null)
                        {
                            rec_cnt2 = budgetPriorityGoalsCategoryList.Count;
                        } // if (budgetPriorityGoalsCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strPriorityGoalsSubCategory already exist in the budgetPriorityGoalsCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetPriorityGoalsCategoryObj2 = budgetPriorityGoalsCategoryList[j];

                                if (strPriorityGoalsSubCategory == budgetPriorityGoalsCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnPriorityGoalsSubCategoryFound = true;
                                    break;
                                } // if (strPriorityGoalsSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnPriorityGoalsSubCategoryFound == false)
                        {
                            budgetPriorityGoalsCategoryList.Add(budgetPriorityGoalsCategoryObj);
                        } // if (blnPriorityGoalsSubCategoryFound)
                    } // if(strPriorityGoalsSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            // List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            // DataSet ds = new DataSet();
            ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised ACC_EMAIL
            // StringBuilder sqlStr = new StringBuilder();
            sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT budgetCategory, budgetSubCategory ");
            sqlStr.AppendLine("FROM [Transaction] ");
            sqlStr.AppendLine("WHERE (budgetCategory = @paraBudgetCategory) ");
            sqlStr.AppendLine("ORDER BY budgetSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            // SqlConnection myConn = new SqlConnection(DBConnect);
            myConn = new SqlConnection(DBConnect);

            // SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);
            da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudgetCategory", PRIORITY_GOALS_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            // string strPriorityGoalsSubCategory = "";
            strPriorityGoalsSubCategory = "";

            // int rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            rec_cnt1 = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt1 > 0)
            {
                for (int i = 0; i < rec_cnt1; i++)
                {
                    DataRow row = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows[i];

                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                    budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budgetCategory"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budgetSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                    {
                        strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                        bool blnPriorityGoalsSubCategoryFound = false;

                        int rec_cnt2 = 0;
                        if (budgetPriorityGoalsCategoryList != null)
                        {
                            rec_cnt2 = budgetPriorityGoalsCategoryList.Count;
                        } // if (budgetPriorityGoalsCategoryList)

                        // Check whether the number of Category and SubCategory rows is valid
                        if (rec_cnt2 > 0)
                        {
                            // Check whether the strPriorityGoalsSubCategory already exist in the budgetPriorityGoalsCategoryList
                            for (int j = 0; j < rec_cnt2; j++)
                            {
                                BudgetExpenditureCategory budgetPriorityGoalsCategoryObj2 = budgetPriorityGoalsCategoryList[j];

                                if (strPriorityGoalsSubCategory == budgetPriorityGoalsCategoryObj2.budget_expenditureSubCategory)
                                {
                                    blnPriorityGoalsSubCategoryFound = true;
                                    break;
                                } // if (strPriorityGoalsSubCategory)
                            } //  for (j)
                        } // if (rec_cnt2)

                        if (blnPriorityGoalsSubCategoryFound == false)
                        {
                            budgetPriorityGoalsCategoryList.Add(budgetPriorityGoalsCategoryObj);
                        } // if (blnPriorityGoalsSubCategoryFound)
                    } // if(strPriorityGoalsSubCategory)
                } // for (i)
            } // if (rec_cnt1)


            int rec_cnt3 = 0;
            if (budgetPriorityGoalsCategoryList != null)
            {
                rec_cnt3 = budgetPriorityGoalsCategoryList.Count;
            } // if (budgetPriorityGoalsCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt3 <= 0)
            {
                budgetPriorityGoalsCategoryList = null;
            } // if (rec_cnt3)


            return budgetPriorityGoalsCategoryList;
        } // CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory()


        public BudgetExpenditureCategory ReadBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FIXED_COST_CATEGORY, string FIXED_COST_SUBCATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FIXED_COST_SUBCATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFixedCostCategory");

            // Step 7: Iterate the rows from TableBudgetFixedCostCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetFixedCostCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFixedCostCategory
                DataRow row = ds.Tables["TableBudgetFixedCostCategory"].Rows[0];
                budgetFixedCostCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetFixedCostCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                budgetFixedCostCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
            }
            else
            {
                budgetFixedCostCategoryObj = null;
            } // if (rec_cnt)

            return budgetFixedCostCategoryObj;
        } // ReadBudgetFixedCostCategoryAndSubCategoryByCategoryAndSubCategory()


        public int InsertBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FIXED_COST_CATEGORY, string FIXED_COST_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetExpenditureCategory (acc_email, budget_expenditureCategory, budget_expenditureSubCategory) ");

            sqlStr.AppendLine("VALUES (@paraAcc_email, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FIXED_COST_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public int DeleteBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FIXED_COST_CATEGORY, string FIXED_COST_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("DELETE FROM BudgetExpenditureCategory ");

            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FIXED_COST_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FIXED_COST_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public List<BudgetExpenditureCategory> ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string FLEX_SPENDING_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableBudgetFlexSpendingCategory"].Rows)
                {
                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                    budgetFlexSpendingCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    budgetFlexSpendingCategoryList.Add(budgetFlexSpendingCategoryObj);
                } // foreach (DataRow row)
            }
            else
            {
                budgetFlexSpendingCategoryList = null;
            } // if (rec_cnt)

            return budgetFlexSpendingCategoryList;
        } // ReadBudgetFlexSpendingCategoryAndSubCategoryByCategoryAndSubCategory()


        public BudgetExpenditureCategory ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FLEX_SPENDING_CATEGORY, string FLEX_SPENDING_SUBCATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FLEX_SPENDING_SUBCATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetFlexSpendingCategory");

            // Step 7: Iterate the rows from TableBudgetFlexSpendingCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetFlexSpendingCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetFlexSpendingCategory
                DataRow row = ds.Tables["TableBudgetFlexSpendingCategory"].Rows[0];
                budgetFlexSpendingCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetFlexSpendingCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                budgetFlexSpendingCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
            }
            else
            {
                budgetFlexSpendingCategoryObj = null;
            } // if (rec_cnt)

            return budgetFlexSpendingCategoryObj;
        } // ReadBudgetFlexSpendingCategoryAndSubCategoryByCategoryAndSubCategory()


        public int InsertBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FLEX_SPENDING_CATEGORY, string FLEX_SPENDING_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetExpenditureCategory (acc_email, budget_expenditureCategory, budget_expenditureSubCategory) ");

            sqlStr.AppendLine("VALUES (@paraAcc_email, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FLEX_SPENDING_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public int DeleteBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string FLEX_SPENDING_CATEGORY, string FLEX_SPENDING_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("DELETE FROM BudgetExpenditureCategory ");

            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", FLEX_SPENDING_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", FLEX_SPENDING_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public List<BudgetExpenditureCategory> ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string DEBT_REPAYMENT_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableBudgetDebtRepaymentCategory"].Rows)
                {
                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                    budgetDebtRepaymentCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    budgetDebtRepaymentCategoryList.Add(budgetDebtRepaymentCategoryObj);
                } // foreach (DataRow row)
            }
            else
            {
                budgetDebtRepaymentCategoryList = null;
            } // if (rec_cnt)

            return budgetDebtRepaymentCategoryList;
        } // ReadBudgetDebtRepaymentCategoryAndSubCategoryByCategoryAndSubCategory()


        public BudgetExpenditureCategory ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string DEBT_REPAYMENT_CATEGORY, string DEBT_REPAYMENT_SUBCATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", DEBT_REPAYMENT_SUBCATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetDebtRepaymentCategory");

            // Step 7: Iterate the rows from TableBudgetDebtRepaymentCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetDebtRepaymentCategory
                DataRow row = ds.Tables["TableBudgetDebtRepaymentCategory"].Rows[0];
                budgetDebtRepaymentCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetDebtRepaymentCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
            }
            else
            {
                budgetDebtRepaymentCategoryObj = null;
            } // if (rec_cnt)

            return budgetDebtRepaymentCategoryObj;
        } // ReadBudgetDebtRepaymentCategoryAndSubCategoryByCategoryAndSubCategory()


        public int InsertBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string DEBT_REPAYMENT_CATEGORY, string DEBT_REPAYMENT_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetExpenditureCategory (acc_email, budget_expenditureCategory, budget_expenditureSubCategory) ");

            sqlStr.AppendLine("VALUES (@paraAcc_email, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", DEBT_REPAYMENT_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public int DeleteBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string DEBT_REPAYMENT_CATEGORY, string DEBT_REPAYMENT_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("DELETE FROM BudgetExpenditureCategory ");

            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", DEBT_REPAYMENT_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", DEBT_REPAYMENT_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public List<BudgetExpenditureCategory> ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(string ACC_EMAIL, string PRIORITY_GOALS_CATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableBudgetPriorityGoalsCategory"].Rows)
                {
                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();

                    // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                    budgetPriorityGoalsCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                    budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);

                    //  Step 9: Add each BudgetExpenditureCategory instance to array list
                    budgetPriorityGoalsCategoryList.Add(budgetPriorityGoalsCategoryObj);
                } // foreach (DataRow row)
            }
            else
            {
                budgetPriorityGoalsCategoryList = null;
            } // if (rec_cnt)

            return budgetPriorityGoalsCategoryList;
        } // ReadBudgetPriorityGoalsCategoryAndSubCategoryByCategoryAndSubCategory()


        public BudgetExpenditureCategory ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string PRIORITY_GOALS_CATEGORY, string PRIORITY_GOALS_SUBCATEGORY)
        {
            // Step 2 : declare a list to hold collection of customer's BudgetExpenditureCategory
            //           DataSet instance and dataTable instance 

            BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from BudgetExpenditureCategory Table by parameterised BUSI_
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT acc_email, budget_expenditureCategory, budget_expenditureSubCategory ");
            sqlStr.AppendLine("FROM BudgetExpenditureCategory ");
            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");
            sqlStr.AppendLine("ORDER BY budget_expenditureSubCategory ASC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);
            da.SelectCommand.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", PRIORITY_GOALS_SUBCATEGORY);

            // Step 6: fill dataset
            da.Fill(ds, "TableBudgetPriorityGoalsCategory");

            // Step 7: Iterate the rows from TableBudgetPriorityGoalsCategory above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows.Count;
            if (rec_cnt > 0)
            {
                // Step 8: Set attribute of BudgetExpenditureCategory instance for each row of record in TableBudgetPriorityGoalsCategory
                DataRow row = ds.Tables["TableBudgetPriorityGoalsCategory"].Rows[0];
                budgetPriorityGoalsCategoryObj.acc_email = Convert.ToString(row["acc_email"]);
                budgetPriorityGoalsCategoryObj.budget_expenditureCategory = Convert.ToString(row["budget_expenditureCategory"]);
                budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory = Convert.ToString(row["budget_expenditureSubCategory"]);
            }
            else
            {
                budgetPriorityGoalsCategoryObj = null;
            } // if (rec_cnt)

            return budgetPriorityGoalsCategoryObj;
        } // ReadBudgetPriorityGoalsCategoryAndSubCategoryByCategoryAndSubCategory()


        public int InsertBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string PRIORITY_GOALS_CATEGORY, string PRIORITY_GOALS_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("INSERT INTO BudgetExpenditureCategory (acc_email, budget_expenditureCategory, budget_expenditureSubCategory) ");

            sqlStr.AppendLine("VALUES (@paraAcc_email, @paraBudget_expenditureCategory, @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", PRIORITY_GOALS_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // InsertBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory()


        public int DeleteBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(string ACC_EMAIL, string PRIORITY_GOALS_CATEGORY, string PRIORITY_GOALS_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //

            sqlStr.AppendLine("DELETE FROM BudgetExpenditureCategory ");

            sqlStr.AppendLine("WHERE (acc_email = @paraAcc_email) AND (budget_expenditureCategory = @paraBudget_expenditureCategory) AND (budget_expenditureSubCategory = @paraBudget_expenditureSubCategory) ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureCategory", PRIORITY_GOALS_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudget_expenditureSubCategory", PRIORITY_GOALS_SUBCATEGORY);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // DeleteBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory()
    } // BudgetExpenditureCategoryDAO
} // PrestoPay.Entity.DB_Entities