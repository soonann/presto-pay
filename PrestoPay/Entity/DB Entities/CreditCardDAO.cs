using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class CreditCardDAO
    {

        public List<CreditCard> RetrieveCardsOfUser(string acc_email)
        {
            List<CreditCard> cList = new List<CreditCard>();
            
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM creditcard WHERE acc_email = @paramAcc");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataReader rd;
            try
            {

                cmd.Parameters.Add("@paramAcc", SqlDbType.NVarChar).Value = acc_email;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    cList.Add(new CreditCard()
                    {
                        creditcardnum = rd["creditcardnum"].ToString(),
                        dateofexpiry = Convert.ToDateTime(rd["dateofexpiry"]),
                        acc_email = rd["acc_email"].ToString()

                    });

                }

            }
            catch (Exception ex)
            {
                cList = null;

            }
            conn.Close();

            return cList;



        }

    }
}