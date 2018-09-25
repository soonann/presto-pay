using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class AdminDAO
    {

        public Admin RetrieveAdminById(string adminId)
        {
            Admin ac;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Admin] WHERE AdminLogin = @paramAdminId", conn);
            cmd.Parameters.Add("@paramAdminId", SqlDbType.NVarChar);

            try
            {
                cmd.Parameters["@paramAdminId"].Value = adminId;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                int db_id = Convert.ToInt32(reader["AdminID"].ToString());
                string db_pw = reader["AdminPassword"].ToString();
                string db_name = reader["AdminName"].ToString();
                string db_login = reader["AdminLogin"].ToString();

                ac = new Admin() { AdminID = db_id, AdminLogin = db_login, AdminName = db_name, AdminPassword = db_pw };

                reader.Close();


            }
            catch (Exception ex)
            {
                ac = null;
            }
            conn.Close();
            return ac;



        }




    }
}