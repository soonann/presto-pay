using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PrestoPayApi.Models
{
    public class Hashing
    {


        public static string GetHash(string toBeHashed)
        {

            using (MD5 md5Hash = MD5.Create())
            {

                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }



        }

        // Verify a hash against a string.
        public static bool IsValueOfHash(string input, string hash)
        {
            string hashOfInput = GetHash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;


        }

    }

    public class Entities
    {
    }
    public class PaymentInformation
    {
        public String ApiKey { get; set; }
        public String MerchantReferenceNo { get; set; }
        public String OnCompleteUrl { get; set; }
        public List<Item> ItemList { get; set; }

    }
    public class Item
    {


        public string ItemDescription { get; set; }
        public double ItemPrice { get; set; }


    }

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

                cmd.Parameters.Add("@paramID", SqlDbType.NVarChar).Value = nextID;
                cmd.Parameters.Add("@paramRefNo", SqlDbType.NVarChar).Value = or.Order_RefNo;
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


        public int UpdateOrderPaidByPrestoKey(string PrestoKey, int paid)
        {

            int result = 0;

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
    public class ItemDAO
    {

        public int InsertItem(string description, double price, string order_id)
        {



            int update = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO [Item] (ItemDescription,ItemPrice,Order_id) VALUES(@paramDescription,@paramPrice,@paramOrder_id)", conn);
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramDescription", SqlDbType.NVarChar).Value = description;
                cmd.Parameters.Add("@paramPrice", SqlDbType.Decimal).Value = Math.Round(price, 2);
                cmd.Parameters.Add("@paramOrder_id", SqlDbType.NVarChar).Value = order_id;
                update = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                update = 0;

            }
            conn.Close();

            return update;

        }

        public List<Item> RetrieveItemsByOrderID(string order_id)
        {
            var iList = new List<Item>();
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Item] WHERE Order_id = @paramOrderId", conn);
            SqlDataReader rd;
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramOrderId", SqlDbType.NVarChar).Value = order_id;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    var itemTemp = new Item();
                    itemTemp.ItemDescription = rd["ItemDescription"].ToString();
                    itemTemp.ItemPrice = Convert.ToDouble(rd["ItemPrice"]);
                    iList.Add(itemTemp);

                }



            }
            catch (Exception ex)
            {
                iList = null;

            }
            conn.Close();



            return iList;
        }


    }
    public class Payment
    {
        public Payment()
        {
            OrderDetails = new Order();
            ItemList = new List<Item>();
        }

        public Order OrderDetails { get; set; }
        public List<Item> ItemList { get; set; }



        public bool RetrieveOrderAndItemsByPrestoKey(string PrestoKey)
        {
            bool valid = true;

            try
            {

                this.OrderDetails = new OrderDAO().RetrieveOrderByPrestoKey(PrestoKey);
                this.ItemList = new ItemDAO().RetrieveItemsByOrderID(OrderDetails.Order_id);
                if (OrderDetails.Order_Paid == 1)
                    valid = false;

            }
            catch (Exception ex)
            {
                valid = false;
            }


            return valid;

        }

        public string InsertOrderAndItems()
        {
            int count = 0;

            var itemDAO = new ItemDAO();
            string newOrderId = new OrderDAO().CreateNewOrder(OrderDetails);

            if (newOrderId == null)
                return String.Empty;

            foreach (Item i in ItemList)
            {
                count += itemDAO.InsertItem(i.ItemDescription, i.ItemPrice, newOrderId);
            }


            return newOrderId;




        }
    }
    public class Order
    {
        public string Order_id { get; set; }
        public string Order_ApiKey { get; set; }
        public string Order_RefNo { get; set; }
        public DateTime Order_DateOrdered { get; set; }
        public string Order_UrlRedirect { get; set; }
        public string Order_PrestoKey { get; set; }
        public int Order_Paid { get; set; }

    }

    public class APIErrorLog
    {

        public int Log_id { get; set; }
        public DateTime Log_Timestamp { get; set; }
        public string Log_ApiKey { get; set; }
        public string Log_ErrorMessage { get; set; }


    }

    public class APIErrorLogDAO
    {
        public void AddErrorLog(string apikey, string errormsg)
        {
            int affectedRows = 0;
            string key = "";
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            //Qr_Encrypted, @ParamQr_Encrypted,
            SqlCommand cmd = new SqlCommand("INSERT INTO [APIErrorLog] (Log_Timestamp,Log_ApiKey,Log_ErrorMessage) VALUES(@paramTimeNow,@paramApiKey,@paramErrorMessage)", conn);
            try
            {


                //cmd.Parameters.Add("@ParamQr_Id", SqlDbType.Int).Value = ;
                DateTime dtNow = DateTime.Now;
                cmd.Parameters.Add("@paramTimeNow", SqlDbType.DateTime).Value = dtNow;
                cmd.Parameters.Add("@paramApiKey", SqlDbType.NVarChar).Value = apikey;
                cmd.Parameters.Add("@paramErrorMessage", SqlDbType.NVarChar).Value = errormsg;

                conn.Open();
                affectedRows = cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                affectedRows = 0;
            }

            conn.Close();



        }

    }

    public class BusinessApiKey
    {
        public string BusiApiKey_Key { get; set; }
        public DateTime BusiApiKey_DateCreated { get; set; }
        public string busi_id { get; set; }


    }

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