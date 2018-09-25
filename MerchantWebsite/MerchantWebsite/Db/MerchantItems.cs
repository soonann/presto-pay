
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MerchantWebsite.Db;



namespace MerchantWebsite.Db
{
    public class MerchantItems
    {
        

        public int InsertItem(string description, double price, string order_id)
        {



            int update = 0;
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO [MerchantItems] (ItemDescription,ItemPrice,Order_id) VALUES(@paramDescription,@paramPrice,@paramOrder_id)", conn);
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
            string connStr = ConfigurationManager.ConnectionStrings["MerchantConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("SELECT * FROM [MerchantItems] WHERE Order_id = @paramOrderId", conn);
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
}