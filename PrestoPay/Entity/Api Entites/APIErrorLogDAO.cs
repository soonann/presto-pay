using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.Api_Entites
{
    public class APIErrorLogDAO
    {
        public void AddErrorLog(string apikey,string errormsg)
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
}