using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MerchantWebsite.Db
{
    public class Business
    {
        public int id { get; set; }
        public string ApiKey { get; set; }
        public string Business_name { get; set; }

        public string GetPaymentKey()
        {

            string apikey="";
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT BusinessPayment_ApiKey FROM [Business] WHERE Id=1", conn);
            conn.Open();

            try
            {

              
               apikey = cmd.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            {
                apikey = "";


            }
            conn.Close();

            return apikey;


        }


        public bool CheckIfValidKey(string ApiKey)
        {

            bool exists = true;
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Business] WHERE ApiKey = @key", conn);
            conn.Open();

            try
            {

                cmd.Parameters.Add("@key", SqlDbType.NVarChar).Value = ApiKey;
                count = Convert.ToInt32(cmd.ExecuteScalar());
                

            }
            catch (Exception ex)
            {
                exists = false;
                

            }
            conn.Close();

            if (count > 0)
            {
                exists = true;
            }

            return exists;



        }

    }
}