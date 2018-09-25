using PrestoPay.Encrypt;
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
    public class QrPayKeyDAO
    {

        public QrPayKey RetrieveLatestQrPayKeyByUserId(string email) {

            QrPayKey qr;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [QrPayKey] WHERE acc_email = @paramEmail ORDER BY Qr_DateUpdated DESC ", conn);         
            try
            {
                cmd.Parameters.Add("@paramEmail", SqlDbType.NVarChar).Value = email;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int Qr_Id = Convert.ToInt32(reader["Qr_Id"]);
                //string Qr_Encrypted = reader["Qr_Encrypted"].ToString();
                string Qr_Key = reader["Qr_Key"].ToString();
                DateTime Qr_DateUpdated = Convert.ToDateTime(reader["Qr_DateUpdated"]);
                string acc_email = reader["acc_email"].ToString();
                //,Qr_Encrypted
                qr = new QrPayKey(Qr_Id,Qr_Key,Qr_DateUpdated,email);
                reader.Close();

            }
            catch (Exception ex)
            {
                qr = null;
            }

            conn.Close();


            return qr;

        }

        public string GenerateKeyForUser(string email) {

            int affectedRows = 0;
            string key = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            //Qr_Encrypted, @ParamQr_Encrypted,
            SqlCommand cmd = new SqlCommand("INSERT INTO [QrPayKey] (Qr_Key,Qr_DateUpdated,acc_email) VALUES(@ParamQr_Key,@ParamQr_DateUpdated,@Paramacc_email)", conn);
            try
            {


                //cmd.Parameters.Add("@ParamQr_Id", SqlDbType.Int).Value = ;
                DateTime dtNow = DateTime.Now;
                string emailWithDate = email +"_"+ dtNow;
                key = Hashing.GetHash(emailWithDate);
                
                

                //cmd.Parameters.Add("@ParamQr_Encrypted", SqlDbType.NVarChar).Value = Encrypted ;
                cmd.Parameters.Add("@ParamQr_Key", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@ParamQr_DateUpdated", SqlDbType.DateTime).Value = dtNow;
                cmd.Parameters.Add("@Paramacc_email", SqlDbType.NVarChar).Value = email;
              
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
            SqlCommand cmd = new SqlCommand("SELECT acc_email FROM [QrPayKey] WHERE Qr_Key = @paramHashVal", conn);
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