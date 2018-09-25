using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PrestoPay.Entity.DB_Entities
{
    public class CategorisedTransactionDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public int WriteCategorisedTransactionByTransID(string TRANS_ID, string BUDGET_CATEGORY, string BUDGET_SUBCATEGORY)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL update command to change column of tdRenewMode for the selected record in TDMaster using     
            //         parameterised query in values clause

            sqlStr.AppendLine("UPDATE [dbo].[Transaction] ");
            sqlStr.AppendLine("SET budgetCategory = @paraBudgetCategory, ");
            sqlStr.AppendLine("budgetSubCategory = @paraBudgetSubCategory ");
            sqlStr.AppendLine("WHERE trans_id = @paraTrans_id ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraBudgetCategory", BUDGET_CATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraBudgetSubCategory", BUDGET_SUBCATEGORY);
            sqlCmd.Parameters.AddWithValue("@paraTrans_id", TRANS_ID);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // WriteCategorisedTransactionByTransID()
    } //CategorisedTransactionDAO
} // namespace PrestoPay.Entity.DB_Entities