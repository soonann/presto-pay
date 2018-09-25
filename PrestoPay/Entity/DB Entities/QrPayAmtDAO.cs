using PrestoPay.Hash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class QrPayAmtDAO
    {

        public double RetrieveAmountByKey(string key)
        {

            double amt;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Qr_Amount FROM [QrPayKeyAmount] WHERE Qr_Key = @paramQrKey", conn);
            try
            {
                cmd.Parameters.Add("@paramQrKey", SqlDbType.NVarChar).Value = key;
                conn.Open();
                amt = Convert.ToDouble(cmd.ExecuteScalar().ToString());
                

            }
            catch (Exception ex)
            {
                amt = 0;
            }

            conn.Close();


            return amt;

        }

        public string GenerateNewKeyForUser(string email, double amount)
        {

            int affectedRows = 0;
            string key = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            //Qr_Encrypted, @ParamQr_Encrypted,
            SqlCommand cmd = new SqlCommand("INSERT INTO [QrPayKeyAmount] (Qr_Key,Qr_DateUpdated,acc_email,Qr_Amount) VALUES(@ParamQr_Key,@ParamQr_DateUpdated,@Paramacc_email,@ParamQr_Amount)", conn);
            try
            {


                //cmd.Parameters.Add("@ParamQr_Id", SqlDbType.Int).Value = ;
                DateTime dtNow = DateTime.Now;
                string emailWithDate = email + "_" + dtNow;
                key = Hashing.GetHash(emailWithDate);

                double amountRounded = Math.Round(amount, 2);


                //cmd.Parameters.Add("@ParamQr_Encrypted", SqlDbType.NVarChar).Value = Encrypted ;
                cmd.Parameters.Add("@ParamQr_Key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@ParamQr_DateUpdated", SqlDbType.DateTime).Value = dtNow;
                cmd.Parameters.Add("@Paramacc_email", SqlDbType.NVarChar).Value = email;
               
                cmd.Parameters.Add("@ParamQr_Amount", SqlDbType.Decimal).Value = amountRounded;
                conn.Open();
                affectedRows = cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                affectedRows = 0;
            }

            conn.Close();

            if (affectedRows > 0)
                return key;
            else
                return null;
        }

        public string IndentifyUserByHash(string hash)
        {

            string userEmail;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT acc_email FROM [QrPayKeyAmount] WHERE Qr_Key = @paramHashVal", conn);
            try
            {
                cmd.Parameters.Add("@paramHashVal", SqlDbType.NVarChar).Value = hash;
                conn.Open();
                userEmail = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                userEmail = null;
            }

            conn.Close();


            return userEmail;

        }

    
}
}