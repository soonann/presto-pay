using PrestoPay.Entity.View_Entities;
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
    public class OrderDAO
    {

        public static String NextOrderId()
        {
            int current = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT MAX(CAST(SUBSTRING(Order_id, 3 , LEN(Order_id) - 2) AS INT )) FROM [Order]", conn);
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
                return "OD000001";
            current++;
            string zeroString = "";
            string tempNext = current.ToString();
            int zeroCount = 6 - tempNext.Length;

            for (int i = 0; i < zeroCount; i++)
            {
                zeroString += "0";
            }

            String nextOrderId = "OD" + zeroString + tempNext;
            if (nextOrderId != null)
                return nextOrderId;
            else
                return String.Empty;


        }



        public string CreateNewOrder(Order or)
        {

            int update = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO [ORDER] (Order_id,Order_ApiKey,Order_RefNo,Order_DateOrdered,Order_UrlRedirect,Order_PrestoKey,Order_Paid) VALUES(@paramID,@paramApiKey,@paramRefNo,@paramDate,@paramUrlRedirect,@paramPrestoKey,@paramPaid)", conn);
            conn.Open();
            string nextID = NextOrderId();
            string returnedWebUrl = Hashing.GetHash(nextID + "_" + or.Order_DateOrdered);
            try
            {

                cmd.Parameters.Add("@paramID", SqlDbType.NVarChar).Value = nextID ;
                cmd.Parameters.Add("@paramRefNo", SqlDbType.NVarChar).Value = or.Order_RefNo ;
                cmd.Parameters.Add("@paramApiKey", SqlDbType.NVarChar).Value = or.Order_ApiKey;
                cmd.Parameters.Add("@paramDate", SqlDbType.DateTime).Value = or.Order_DateOrdered;
                cmd.Parameters.Add("@paramUrlRedirect", SqlDbType.NVarChar).Value = or.Order_UrlRedirect;
                cmd.Parameters.Add("@paramPrestoKey", SqlDbType.NVarChar).Value = returnedWebUrl;
                cmd.Parameters.Add("@paramPaid", SqlDbType.Int).Value = or.Order_Paid;
                update = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                update = 0;

            }
            conn.Close();

           
            return nextID;

        }


        public string RetrievePrestoKeyByOrderId(string order_id)
        {

            string prestoKey = null;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT Order_PrestoKey FROM [Order] WHERE Order_id = @paramId", conn);
         
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramId", SqlDbType.NVarChar).Value = order_id;
                prestoKey = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                prestoKey = null;

            }
            conn.Close();

            return prestoKey;
        }

        public Order RetrieveOrderByPrestoKey(string PrestoKey)
        {

            Order or = new Order();
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Order] WHERE Order_PrestoKey = @paramOrderKey", conn);
            SqlDataReader rd;
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramOrderKey", SqlDbType.NVarChar).Value = PrestoKey;
                rd = cmd.ExecuteReader();

                    rd.Read();
                    or.Order_id = rd["Order_id"].ToString();
                    or.Order_ApiKey = rd["Order_ApiKey"].ToString();
                    or.Order_RefNo = rd["Order_RefNo"].ToString();
                    or.Order_DateOrdered = Convert.ToDateTime(rd["Order_DateOrdered"]);
                    or.Order_UrlRedirect = rd["Order_UrlRedirect"].ToString();
                    or.Order_PrestoKey = rd["Order_PrestoKey"].ToString();
                    or.Order_Paid = Convert.ToInt32(rd["Order_Paid"]);

            }
            catch (Exception ex)
            {
                or = null;

            }
            conn.Close();

            return or;

        }


        public int UpdateOrderPaidByPrestoKey(string PrestoKey,int paid){

            int result=0;
        
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE [ORDER] SET Order_Paid = @paramPaid WHERE Order_PrestoKey = @paramPrestoKey", conn);
    
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramPaid", SqlDbType.NVarChar).Value = paid;
                cmd.Parameters.Add("@paramPrestoKey", SqlDbType.NVarChar).Value = PrestoKey;
                result = cmd.ExecuteNonQuery();

  

            }
            catch (Exception ex)
            {
                result = 0;
            }
            conn.Close();

            return result;

        }


    }
}