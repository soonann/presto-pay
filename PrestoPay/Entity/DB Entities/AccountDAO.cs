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
    public class AccountDAO
    {

        public string retrieveName(String email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT [acc_name] FROM [Account] WHERE acc_email = @paramAcc_email", conn);
            cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar).Value = email;
            String name = "";
            conn.Open();
            try
            {
                name = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

            }

            conn.Close();
            return name;
        }
        public List<Account> retrieveAllAccounts()
        {
            Account ac;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Account]", conn);
            List<Account> acList = new List<Account>();

            try
            {
               
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    
                    string db_email = reader["acc_email"].ToString();
                    string db_password = reader["acc_password"].ToString();
                    string db_name = reader["acc_name"].ToString();
                    double db_walletBal = Convert.ToDouble(reader["acc_walletBal"]);
                    DateTime db_dob = Convert.ToDateTime(reader["acc_dob"]);
                    string busi_id = reader["busi_id"].ToString();
                    ac = new Account( db_email, db_password, db_name, db_walletBal, db_dob, busi_id);
                    acList.Add(ac);
                }

                
                
               
                reader.Close();

            }
            catch (Exception ex)
            {
                ac = null;
            }

            conn.Close();
           

            return acList;
        }


        public double RetrieveWalletBalanceByEmail(String email)
        {          
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT [acc_walletBal] FROM [Account] WHERE acc_email = @paramAcc_email", conn);
            cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar).Value = email;
            double db_walletBal = 0;
            conn.Open();
            try
            {              
                db_walletBal = Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                
            }

            conn.Close();
            return db_walletBal;
        }

        public bool DeductAmountFromWallet(String email ,double amt)
        {
            Account ac = RetrieveAccountByEmail(email);
            if (ac.walletBal >= amt)
            {
                ac.walletBal -= amt;
                ac.walletBal = Convert.ToDouble(Math.Round(ac.walletBal, 2));
            }               
            else
                return false;
            
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE [Account] SET acc_walletBal = @paramAcc_bal WHERE acc_email = @paramAcc_email", conn);
            bool success = true;
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar).Value = ac.email;          
                cmd.Parameters.Add("@paramAcc_bal", SqlDbType.Decimal).Value = ac.walletBal;
                if (cmd.ExecuteNonQuery() <= 0)
                    success = false;
            }
            catch (Exception ex)
            {
                success = false;
            }

            conn.Close();           
            return success;

        }

        public bool AddAmountToWallet(String email, double amt) {

            Account ac = RetrieveAccountByEmail(email);
            ac.walletBal += amt;
            ac.walletBal = Convert.ToDouble(Math.Round(ac.walletBal, 2));
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE [Account] SET acc_walletBal = @paramAcc_bal WHERE acc_email = @paramAcc_email", conn);

            bool success = true;
            conn.Open();
            try
            {
                cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar).Value = ac.email;
                cmd.Parameters.Add("@paramAcc_bal", SqlDbType.Decimal).Value = ac.walletBal;
                if (cmd.ExecuteNonQuery() <= 0)
                    success = false;
            }
            catch (Exception ex)
            {
                success = false;
            }

            conn.Close();
            return success;
        }

        public int UpdateAccountDetails(Account ac)
        {
            /*
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE [Account] SET (" +
                "acc_email = @paramAcc_email," +
                "acc_password = @paramAcc_password," +
                "acc_name = @paramAcc_name," +
                "acc_walletBal = @paramAcc_bal," +
                "acc_dob = @paramAcc_dob) WHERE acc_id = @paramAcc_id", conn);

            int num = 0;
            conn.Open();
            try
            {
                cmd.Parameters.Add("@paramAcc_id", SqlDbType.Int).Value = ac.id;
                cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar).Value = ac.email;
                cmd.Parameters.Add("@paramAcc_password", SqlDbType.NVarChar).Value = ac.password;
                cmd.Parameters.Add("@paramAcc_name", SqlDbType.NVarChar).Value = ac.name;
                cmd.Parameters.Add("@paramAcc_dob", SqlDbType.DateTime).Value = ac.dob;

                num = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            



            return num;
            */
            return 0;
        }

        public Account RetrieveAccountByEmail(string email)
        {
            Account ac;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Account] WHERE acc_email = @paramAcc_email",conn);
            cmd.Parameters.Add("@paramAcc_email",SqlDbType.NVarChar);

            try
            {
                cmd.Parameters["@paramAcc_email"].Value = email;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
             
                string db_email = reader["acc_email"].ToString();
                string db_password = reader["acc_password"].ToString();
                string db_name = reader["acc_name"].ToString();
                double db_walletBal = Convert.ToDouble(reader["acc_walletBal"]);
                DateTime db_dob = Convert.ToDateTime(reader["acc_dob"]);
                string db_busiId = reader["busi_id"].ToString();
                if (String.IsNullOrEmpty(db_busiId))
                    ac = new Account(db_email, db_password, db_name, db_walletBal, db_dob, null);
                else
                    ac = new Account(db_email, db_password, db_name, db_walletBal, db_dob, db_busiId);
                reader.Close();

            }
            catch (Exception ex)
            {
                ac = null;
            }

            conn.Close();
            return ac;

        }

        public string RetrieveUserEmailByPresto(string key)
        {

            string email;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT acc_email FROM [Account] AS Acc");
            sb.AppendLine("INNER JOIN [BusinessApiKey] AS bak");
            sb.AppendLine("ON bak.busi_id = Acc.busi_id INNER JOIN [Order] od ");
            sb.AppendLine("on Order_ApiKey = BusiApiKey_Key WHERE Order_PrestoKey=@ParamPrestoKey");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
           // cmd.Parameters.Add("@paramAcc_email", SqlDbType.NVarChar);

            try
            {
                cmd.Parameters.Add("@ParamPrestoKey",SqlDbType.NVarChar).Value = key;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                string db_email = reader["acc_email"].ToString();
                email = db_email;
                reader.Close();

            }
            catch (Exception ex)
            {
                email = null;
            }

            conn.Close();
            return email;


        }



    }
}