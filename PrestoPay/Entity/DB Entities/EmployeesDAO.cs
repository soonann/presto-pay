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
    public class EmployeesDAO
    {
        public List<Employees> RetrieveAllEmployeesOfUser(string emailOrId, bool business)
        {

            List<Employees> rqList = new List<Employees>();
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            StringBuilder sb = new StringBuilder();
            if (business)
                sb.AppendLine("SELECT * FROM Employees WHERE busi_id = @paramIdorEmail");
            else
                sb.AppendLine("SELECT * FROM Employees WHERE Employee_Email = @paramIdorEmail ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
            conn.Open();
            SqlDataReader rd;
            try
            {

                cmd.Parameters.Add("@paramIdorEmail", SqlDbType.NVarChar).Value = emailOrId;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    rqList.Add(new Employees()
                    {
                        EmployeeId = Convert.ToInt32(rd["EmployeeId"]),
                        Employee_Email = rd["Employee_Email"].ToString(),
                        Employee_Pay = Convert.ToInt32(rd["Employee_Pay"]),
                        Employee_View = Convert.ToInt32(rd["Request_View"]),
                        busi_id = rd["busi_id"].ToString()


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

        public bool AddNewEmployee(string busiId,string empEmail)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("INSERT INTO Employees (Employee_Email,Employee_Pay,Employee_View,busi_id) VALUES (@paramEmail,@paramPay,@paramView,@paramBusiId)", conn);
            conn.Open();

            try
            {
                cmd.Parameters.Add("@paramEmail", SqlDbType.NVarChar).Value = empEmail;
                cmd.Parameters.Add("@paramPay", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@paramView", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@paramBusiId", SqlDbType.VarChar).Value = busiId;


                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                count = 0;
            }

            conn.Close();

            return (count > 0);



        }


        public bool UpdateEmployeeStateById(int id, int Pay, int View)
        {
            int count = 0;
            string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("UPDATE Employees SET Employee_Pay = @paramPay , Employee_View = @paramView WHERE EmployeeId = @paramId)", conn);
            conn.Open();

            try
            {

                cmd.Parameters.Add("@paramPay", SqlDbType.Int).Value = Pay;
                cmd.Parameters.Add("@paramView", SqlDbType.Int).Value = View;          
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