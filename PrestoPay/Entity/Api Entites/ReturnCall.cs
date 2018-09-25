using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class ReturnCall
    {

        public string getReturnKeyByEmail(string email)
        {
            string key = null;

 
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT ReturnCall_Key FROM [ReturnApiCall] WHERE acc_email = @emailParam ", conn);

            conn.Open();

            try
            {

                cmd.Parameters.Add("@emailParam", SqlDbType.NVarChar).Value = email;
                key = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                key = null;   

            }
            conn.Close();



            return key;
        }

    }
}