using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BudgetExpenditurePrestopayTransactionDAO
    {
        public List<CategorisedTransaction> RetrieveTransactionsOfUserByDate(string email, DateTime START_DATE, DateTime END_DATE)
        {
            DateTime dtmStartDate = START_DATE;
            DateTime dtmEndDate = END_DATE.AddDays(1);


            // Must Check dtmStartDate and dtmEndDate


            List<CategorisedTransaction> tList;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Transaction] WHERE (trans_from = @paramFrom OR trans_to = @paramTo) AND (trans_date BETWEEN @paraStart_date AND @paraEnd_date)", conn);
            cmd.Parameters.Add("@paramFrom", SqlDbType.NVarChar);
            cmd.Parameters.Add("@paramTo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@paraStart_date", SqlDbType.DateTime);
            cmd.Parameters.Add("@paraEnd_date", SqlDbType.DateTime);

            try
            {
                tList = new List<CategorisedTransaction>();
                cmd.Parameters["@paramFrom"].Value = email;
                cmd.Parameters["@paramTo"].Value = email;
                cmd.Parameters["@paraStart_date"].Value = dtmStartDate;
                cmd.Parameters["@paraEnd_date"].Value = dtmEndDate;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string db_id = reader["trans_id"].ToString();
                    double db_amt = Convert.ToDouble(reader["trans_amt"]);
                    string db_description = reader["trans_description"].ToString();
                    string db_type = reader["trans_type"].ToString();
                    string db_from = reader["trans_from"].ToString();
                    string db_to = reader["trans_to"].ToString();
                    DateTime db_date = Convert.ToDateTime(reader["trans_date"]);

                    string db_budgetCategory = "";
                    string db_budgetSubCategory = "";

                    bool db_IsEmpty = (String.IsNullOrWhiteSpace(reader["budgetCategory"].ToString()) || String.IsNullOrWhiteSpace(reader["budgetSubCategory"].ToString()));

                    if (db_IsEmpty)
                    {
                        db_budgetCategory = "";
                        db_budgetSubCategory = "";
                    }
                    else
                    {
                        db_budgetCategory = reader["budgetCategory"].ToString();
                        db_budgetSubCategory = reader["budgetSubCategory"].ToString();
                    } // if (db_IsEmpty)

                    tList.Add(new CategorisedTransaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date, db_budgetCategory, db_budgetSubCategory));
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                tList = null;
            }

            conn.Close();
            return tList;

        } // RetrieveTransactionsOfUserByDate()

    } // BudgetExpenditurePrestopayTransactionDAO
}