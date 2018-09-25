using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class BankDAO
    {


        public double GetCreditRemainingByUser(string accemail)
        {
            double bal = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT credit_remaining FROM Bank WHERE acc_email = @paramAcc");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramAcc", SqlDbType.NVarChar).Value = accemail;
                bal = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                bal = 0;
            }

            conn.Close();
            return bal;

        }

        public int UpdateUserRemainingCredit(string email, double amt)
        {
            int bal = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE Bank SET credit_remaining = @paramAmt WHERE acc_email = @paramAcc");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramAmt", SqlDbType.Decimal).Value = amt;
                cmd.Parameters.Add("@paramAcc", SqlDbType.NVarChar).Value = email;
                bal = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                bal = 0;
            }

            conn.Close();
            return bal;

        }

    }
}