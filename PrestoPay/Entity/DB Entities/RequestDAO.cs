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
    public class RequestDAO
    {
        public List<Request> RetrieveAllRequestOfUser(string email,bool sender){

            List<Request> rqList = new List<Request>();
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            if(sender)
             sb.AppendLine("SELECT * FROM Request WHERE Request_sender = @paramEmail AND NOT Request_State = 2 ");
            else
             sb.AppendLine("SELECT * FROM Request WHERE Request_recipient = @paramEmail AND NOT Request_State = 2 ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataReader rd;
            try
            {

                cmd.Parameters.Add("@paramEmail", SqlDbType.NVarChar).Value = email;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    rqList.Add(new Request() {
                        Request_sender = rd["Request_sender"].ToString(),
                        RequestId = Convert.ToInt32(rd["RequestId"]),
                        Request_DateTime = Convert.ToDateTime(rd["Request_DateTime"]),
                        Request_recipient = rd["Request_recipient"].ToString(),
                        Request_State = Convert.ToInt32(rd["Request_State"])
                        

                    });

                }

            }
            catch (Exception ex)
            {
                rqList = null;

            }
            conn.Close();

            return rqList;
        }

        public bool MakeNewRequest(string from,string to)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO Request (Request_sender,Request_DateTime,Request_recipient,Request_State) VALUES (@paramSend,@paramDate,@paramRec,@paramState)", conn);
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramSend", SqlDbType.NVarChar).Value = from;
                cmd.Parameters.Add("@paramDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@paramRec", SqlDbType.NVarChar).Value = to ;
                cmd.Parameters.Add("@paramState", SqlDbType.Int).Value = 0;
               

                count = cmd.ExecuteNonQuery();
            }
            catch ( Exception ex)
            {
                count = 0;
            }

            conn.Close();

            return (count > 0);
            


        }


        public Request RetrieveRequestById(int id)
        {

            Request rq = new Request();
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();          
            sb.AppendLine("SELECT * FROM Request WHERE RequestId = @paramId ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataReader rd;
            try
            {

                cmd.Parameters.Add("@paramId", SqlDbType.Int).Value = id;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                  rq = new Request()
                    {
                        Request_sender = rd["Request_sender"].ToString(),
                        RequestId = Convert.ToInt32(rd["RequestId"]),
                        Request_DateTime = Convert.ToDateTime(rd["Request_DateTime"]),
                        Request_recipient = rd["Request_recipient"].ToString(),
                        Request_State = Convert.ToInt32(rd["Request_State"])


                    };

                }

            }
            catch (Exception ex)
            {
                rq = null;

            }
            conn.Close();

            return rq;
        }


        public bool UpdateRequestStateById(int id,int state)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE Request SET Request_State = @paramState WHERE RequestId = @paramId", conn);
            conn.Open();

            try
            {
            
                cmd.Parameters.Add("@paramState", SqlDbType.Int).Value = state;
                cmd.Parameters.Add("@paramId", SqlDbType.Int).Value = id;


                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                count = 0;
            }

            conn.Close();

            return (count > 0);
        }



    }
}
 