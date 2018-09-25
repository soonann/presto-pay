using PrestoPay.Hash;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MerchantWebsite.Db
{
    public class MerchantOrders
    {
        public MerchantOrders(string order_id, string reference_No, int payment_Status, DateTime dateTime_Created, DateTime dateTime_Paid, string apiKey_CalledBack)
        {
            Order_id = order_id;
            Reference_No = reference_No;
            Payment_Status = payment_Status;
            DateTime_Created = dateTime_Created;
            DateTime_Paid = dateTime_Paid;
            ApiKey_CalledBack = apiKey_CalledBack;
        }

        public MerchantOrders()
        {

        }

        public string Order_id { get; set; }
        public string Reference_No { get; set; }
        public int Payment_Status { get; set; }
        public DateTime DateTime_Created { get; set; }
        public DateTime DateTime_Paid { get; set; }
        public string ApiKey_CalledBack { get; set; }


        public static String NextOrderId()
        {
            int current = 0;
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT MAX(CAST(SUBSTRING(Order_id, 3 , LEN(Order_id) - 2) AS INT )) FROM [MerchantOrders]", conn);
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
                return "MX000001";
            current++;
            string zeroString = "";
            string tempNext = current.ToString();
            int zeroCount = 6 - tempNext.Length;

            for (int i = 0; i < zeroCount; i++)
            {
                zeroString += "0";
            }

            String nextOrderId = "MX" + zeroString + tempNext;
            if (nextOrderId != null)
                return nextOrderId;
            else
                return String.Empty;


        }


        public string createNewOrder(List<Item> it){

            int update = 0;
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO [MerchantOrders] (Order_id,Reference_No, Payment_Status, DateTime_Created) VALUES(@paramorder_id, @paramreference_No, @parampayment_Status, @paramdateTime_Created)", conn);
            conn.Open();
            string newOrderId = NextOrderId();
            DateTime current = DateTime.Now;
            string referenceNo = Hashing.GetHash(newOrderId + "_" + current);
            try
            {
                
                cmd.Parameters.Add("@paramorder_id", SqlDbType.NVarChar).Value = newOrderId ;
                cmd.Parameters.Add("@paramreference_No", SqlDbType.NVarChar).Value = referenceNo ;
                cmd.Parameters.Add("@parampayment_Status", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@paramdateTime_Created", SqlDbType.DateTime).Value = DateTime.Now;
                MerchantItems mi = new MerchantItems();
                update = cmd.ExecuteNonQuery();
                foreach (Item item in it)
                    mi.InsertItem(item.ItemDescription, item.ItemPrice, newOrderId);
            }
            catch (Exception ex)
            {
                update = 0;

            }
            conn.Close();

            return referenceNo;
        }



        public List<Item> RetrieveMerchantOrderByReferenceNo(string refNo)
        {
            List<Item> li;
            bool success = true;
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [MerchantOrders] WHERE Reference_No = @paramOrderKey", conn);
            SqlDataReader rd;
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramOrderKey", SqlDbType.NVarChar).Value =refNo;
                rd = cmd.ExecuteReader();

                rd.Read();
                this.Order_id = rd["Order_id"].ToString();
                this.Payment_Status = Convert.ToInt32(rd["Payment_Status"]);
                this.DateTime_Created = Convert.ToDateTime(rd["DateTime_Created"]);
                
                //this.DateTime_Paid = Convert.ToDateTime(rd["DateTime_Paid"]);
                //this.ApiKey_CalledBack = rd["ApiKey_CalledBack"].ToString();

                var mi = new MerchantItems();
                li = mi.RetrieveItemsByOrderID(this.Order_id);



                rd.Close();
            }
            catch (Exception ex)
            {

                success = false;
                li = null;
            }
            conn.Close();

            if(success)
                this.Reference_No = refNo;

            return li;

        }


        public int UpdateOrderPaidByReferenceNo(string apicalledback, string referenceNo, int paid)
        {

            int result = 0;

            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE [MerchantOrders] SET ApiKey_CalledBack = @Callback, Payment_Status = @Status,DateTime_Paid = @paid WHERE Reference_No = @paramRefNo", conn);

            conn.Open();

            try
            {

            
                cmd.Parameters.Add("@Callback", SqlDbType.NVarChar).Value = apicalledback;
                cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = paid ;
                cmd.Parameters.Add("@paid", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@paramRefNo", SqlDbType.NVarChar).Value = referenceNo;
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