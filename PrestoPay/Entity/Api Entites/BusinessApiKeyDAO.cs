using PrestoPay.Hash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.Api_Entites
{
    public class BusinessApiKeyDAO
    {

        public string GenerateNewApiKeyForBusiness(string busi_id)
        {

            int affectedRows = 0;
            string key = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            //Qr_Encrypted, @ParamQr_Encrypted,
            SqlCommand cmd = new SqlCommand("INSERT INTO [BusinessApiKey] (BusiApiKey_Key,busi_id,BusiApiKey_DateCreated) VALUES(@paramKey,@paramBusi_id,@paramDateCreated)", conn);
            try
            {


                //cmd.Parameters.Add("@ParamQr_Id", SqlDbType.Int).Value = ;
                DateTime dtNow = DateTime.Now;
                string busiIdWithDatetime = busi_id + "_" + dtNow;
                key = Hashing.GetHash(busiIdWithDatetime);

               
                cmd.Parameters.Add("@paramKey", SqlDbType.NVarChar).Value = key;
                cmd.Parameters.Add("@paramBusi_id", SqlDbType.VarChar).Value = busi_id;
                cmd.Parameters.Add("@paramDateCreated", SqlDbType.DateTime).Value = dtNow;

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

        public string RetrieveLatestKeyOfBusiness(string busi_id)
        {
            string key = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [BusinessApiKey] WHERE busi_id = @paramBusi_id ORDER BY BusiApiKey_DateCreated DESC ", conn);
            try
            {
                cmd.Parameters.Add("@paramBusi_id", SqlDbType.VarChar).Value = busi_id;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                key = reader["BusiApiKey_Key"].ToString();           
                reader.Close();
                

            }
            catch (Exception ex)
            {
                key = "";
            }

            conn.Close();


            return key;
        }


        public string RetrieveBusinessIdByApiKey(string key)
        {
            string busi_id = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT busi_id FROM [BusinessApiKey] WHERE BusiApiKey_Key = @paramKey ", conn);
            try
            {
                cmd.Parameters.Add("@paramKey", SqlDbType.NVarChar).Value = key;
                conn.Open();
                busi_id = cmd.ExecuteScalar().ToString();
            

            }
            catch (Exception ex)
            {
                busi_id = ex.Message.ToString();
            }

            conn.Close();


            return busi_id;
        }



    }
}