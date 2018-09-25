using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PrestoPay.Entity
{
    public class TransactionDAO
    {
        public static String NextTransactionId()
        {
            int current = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT MAX(CAST(SUBSTRING(trans_id, 3 , LEN(trans_id) - 2) AS INT )) FROM [Transaction]",conn);
            conn.Open();        
            try
            {
              current = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }
            conn.Close();
            if (current == 0)
                return String.Empty;
            current++;
            string zeroString = "";
            string tempNext = current.ToString();
            int zeroCount = 6-tempNext.Length;
            
            for(int i = 0; i < zeroCount; i++)
            {
                zeroString += "0";
            }

            String nextTransId = "TX" + zeroString + tempNext;
            if (nextTransId != null)
                return nextTransId;
            else
                return String.Empty;
            

        }

        public List<Transaction> RetrieveTransactionsOfUser(string email)
        {
            List<Transaction> tList;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Transaction] WHERE trans_from = @paramFrom OR trans_to = @paramTo", conn);
            cmd.Parameters.Add("@paramFrom", SqlDbType.NVarChar);
            cmd.Parameters.Add("@paramTo", SqlDbType.NVarChar);
            
            try
            {
                tList = new List<Transaction>();
                cmd.Parameters["@paramFrom"].Value = email;
                cmd.Parameters["@paramTo"].Value = email;
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
                    tList.Add(new Transaction(db_id, db_amt, db_description, db_type, db_from, db_to, db_date));
                }               
                reader.Close();

            }
            catch (Exception ex)
            {
                tList = null;
            }

            conn.Close();
            return tList;

        }

        public String InsertTransaction(double amt, string desc,string type, string from, string to)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sCmd = new StringBuilder();
            sCmd.AppendLine("INSERT INTO [dbo].[Transaction] ([trans_id], [trans_amt], [trans_description], [trans_type], [trans_from], [trans_to], [trans_date])");
            sCmd.AppendLine("VALUES (@param_id,@param_amt,@param_desc,@param_type,@param_from,@param_to, GETDATE() )");
            SqlCommand cmd = new SqlCommand(sCmd.ToString(), conn);
            String next_id = NextTransactionId();
            if (next_id == null)
                return String.Empty;
            cmd.Parameters.Add("@param_id", SqlDbType.NVarChar).Value = next_id;
            cmd.Parameters.Add("@param_amt", SqlDbType.Decimal).Value = amt;
            cmd.Parameters.Add("@param_desc", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("@param_type", SqlDbType.NVarChar).Value = type;
            cmd.Parameters.Add("@param_from", SqlDbType.NVarChar).Value = from;
            cmd.Parameters.Add("@param_to", SqlDbType.NVarChar).Value = to;           
            try
            {              
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {

            }

            conn.Close();

            if (count == 0)
                return String.Empty;
            else
                return next_id;

        }


        public String InsertDatedTransaction(double amt, string desc, string type, string from, string to,DateTime dt)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sCmd = new StringBuilder();
            sCmd.AppendLine("INSERT INTO [dbo].[Transaction] ([trans_id], [trans_amt], [trans_description], [trans_type], [trans_from], [trans_to], [trans_date])");
            sCmd.AppendLine("VALUES (@param_id,@param_amt,@param_desc,@param_type,@param_from,@param_to,@param_dt )");
            SqlCommand cmd = new SqlCommand(sCmd.ToString(), conn);
            String next_id = NextTransactionId();
            if (next_id == null)
                return String.Empty;
            cmd.Parameters.Add("@param_id", SqlDbType.NVarChar).Value = next_id;
            cmd.Parameters.Add("@param_amt", SqlDbType.Decimal).Value = amt;
            cmd.Parameters.Add("@param_desc", SqlDbType.NVarChar).Value = desc;
            cmd.Parameters.Add("@param_type", SqlDbType.NVarChar).Value = type;
            cmd.Parameters.Add("@param_from", SqlDbType.NVarChar).Value = from;
            cmd.Parameters.Add("@param_to", SqlDbType.NVarChar).Value = to;
            cmd.Parameters.Add("@param_dt", SqlDbType.DateTime).Value = dt;
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            conn.Close();

            if (count == 0)
                return String.Empty;
            else
                return next_id;

        }

    }
}