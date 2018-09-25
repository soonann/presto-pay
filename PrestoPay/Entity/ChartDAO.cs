using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PrestoPay.Entity
{
    public class ChartDAO
    {
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

       


        public int GetNoOfBusinesses() {


            int noOfBusinesses = 0;
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT COUNT(*) AS Enterprise FROM Business; ");
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(sqlCommand.ToString(), myConn);
            myConn.Open();
            try
            {
                noOfBusinesses = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                noOfBusinesses = 0;
            }
            myConn.Close();
            return noOfBusinesses;

        }

        public int GetNoOfIndustry()
        {


            int noOfBusinesses = 0;
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT COUNT(DISTINCT busi_category) AS Enterprise FROM Business ; ");
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(sqlCommand.ToString(), myConn);
            myConn.Open();
            try
            {
                noOfBusinesses = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                noOfBusinesses = 0;
            }
            myConn.Close();
            return noOfBusinesses;

        }


        public int GetNoOfPersonal()
        {


            int noOfPersonal = 0;
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT COUNT(*) AS Personal FROM Account WHERE busi_id IS NULL; ");
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlCommand cmd = new SqlCommand(sqlCommand.ToString(), myConn);
            myConn.Open();
            try
            {
                noOfPersonal = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                noOfPersonal = 0;
            }
            myConn.Close();
            return noOfPersonal;

        }






        public DataTable GetPrestoAnnualProfits()
        {
             
            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT DATEPART(yy,t.trans_date) AS date_year, SUM(t.trans_amt) amt_total FROM  [Transaction] t WHERE t.trans_from IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) OR ");
            sqlCommand.AppendLine("t.trans_to IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) GROUP BY DATEPART(yy,t.trans_date)");
       
          
            // amt_total and date_year
            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);

            // fill dataset
            da.Fill(dt);
            int rec_cnt = dt.Rows.Count;
            return dt;
          
        }

        public DataTable GetPrestoMonthProfitsOfYear(string year)
        {

            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT DATEPART(mm,t.trans_date) AS date_month , SUM(trans_amt) as amt_total FROM  [Transaction] t WHERE");
            sqlCommand.AppendLine("(t.trans_from IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id)OR");
            sqlCommand.AppendLine("t.trans_to IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) )");
            sqlCommand.AppendLine("AND DATEPART(yy,t.trans_date) = @paramYear GROUP BY DATEPART(mm,t.trans_date)");
            // amt_total and date_month

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);
            // fill dataset
            da.Fill(dt);
            return dt;
            
        }

        public DataTable GetIndustriesCount()
        {

            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT COUNT(*) AS no_of_busi, busi_category AS busi_industry FROM Business GROUP BY busi_category; ");
            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
 
            // fill dataset
            da.Fill(dt);
            return dt;

        }

        public DataTable GetIndustryAmountByMY(string month,string year)
        {

          

            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT SUM(t.trans_amt) AS amt_total, b.busi_category FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id INNER");
            sqlCommand.AppendLine("JOIN[Transaction] t on t.trans_to = acc_email WHERE(DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth) GROUP BY  b.busi_category;");
            // amt_total and date_month

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);
            // fill dataset
            da.Fill(dt);
            return dt;
        }


        public DataTable GetPaymentByIndustryAndDate(string month, string year,string industry)
        {
           


            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine(" SELECT SUM(t.trans_amt) AS amt_total, b.busi_category,trans_type FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id INNER");
            sqlCommand.AppendLine("JOIN[Transaction] t on t.trans_to = acc_email  WHERE(DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth)");
            sqlCommand.AppendLine("GROUP BY  b.busi_category,trans_type HAVING b.busi_category = @paramIndustry;");
            // amt_total busi_category,trans_type

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);
            da.SelectCommand.Parameters.AddWithValue("@paramIndustry", industry);
            // fill dataset
            da.Fill(dt);
            return dt;
        }

        public DataTable GetCategoryGroupingForMonth(string month, string year)
        {

            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT SUM(t.trans_amt) AS amt_total, budgetCategory FROM[Transaction] t INNER JOIN Account a on t.trans_from = a.acc_email");
            sqlCommand.AppendLine("WHERE(DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth)");
            sqlCommand.AppendLine("GROUP BY budgetCategory HAVING budgetCategory IS NOT NULL;");
            // amt_total busi_category,trans_type

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);

            // fill dataset
            da.Fill(dt);
            return dt;

        }

        public DataTable GV_IndustryPie(string month, string year)
        {

            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("   SELECT SUM(t.trans_amt) AS amt_total, b.busi_category ,b.busi_companyName ,b.busi_id FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id INNER");
            sqlCommand.AppendLine("JOIN[Transaction] t on t.trans_to = acc_email  WHERE(DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth)");
            sqlCommand.AppendLine("GROUP BY  b.busi_id , busi_companyName ,b.busi_category;");
            // amt_total busi_category,trans_type

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);

            // fill dataset
            da.Fill(dt);
            return dt;
        }

        public DataTable GV_IndustryPay(string month,string year, string method)
        {
           


                     SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine(" SELECT SUM(t.trans_amt) AS amt_total, b.busi_category,trans_type FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id INNER");
            sqlCommand.AppendLine("JOIN[Transaction] t on t.trans_to = acc_email  WHERE(DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth)");
            sqlCommand.AppendLine("GROUP BY  b.busi_category, trans_type HAVING b.busi_category = @paramMethod");
            // amt_total busi_category,trans_type

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);
            da.SelectCommand.Parameters.AddWithValue("@paramMethod", method);

            // fill dataset
            da.Fill(dt);
            return dt;
        }

        public DataTable GV_Personal(string month, string year)
        {


            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT SUM(t.trans_amt) AS amt_total,budgetCategory FROM [Transaction] t INNER JOIN Account a on t.trans_from = a.acc_email ");
            sqlCommand.AppendLine("WHERE (DATEPART(yy, t.trans_date) = @paramYear AND DATEPART(mm, t.trans_date) = @paramMonth)  ");
            sqlCommand.AppendLine("GROUP BY budgetCategory HAVING budgetCategory IS NOT NULL;");
            // amt_total busi_category,trans_type

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramMonth", month);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);
   

            // fill dataset
            da.Fill(dt);
            return dt;
        }

        public DataTable GV_TransCount()
        {
          
            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT DATEPART(yy,t.trans_date) AS date_year, COUNT(t.trans_id) amt_total FROM  [Transaction] t WHERE t.trans_from IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) OR ");
            sqlCommand.AppendLine("t.trans_to IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) GROUP BY DATEPART(yy,t.trans_date)");

            // amt_total busi_category,trans_type
            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
           


            // fill dataset
            da.Fill(dt);
            return dt;
        }

        public DataTable GV_TransMonthCount(string year)
        {
           


            SqlDataAdapter da;
            DataTable dt = new DataTable();

            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("SELECT DATEPART(mm,t.trans_date) AS date_month , COUNT(trans_id) amt_total FROM  [Transaction] t WHERE");
            sqlCommand.AppendLine("(t.trans_from IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id) OR");
            sqlCommand.AppendLine("t.trans_to IN (SELECT acc_email FROM Business b INNER JOIN Account a on a.busi_id = b.busi_id )");
            sqlCommand.AppendLine(") AND DATEPART(yy,t.trans_date) = @paramYear GROUP BY DATEPART(mm,t.trans_date)");
            // amt_total busi_category,trans_type
            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("@paramYear", year);


            // fill dataset
            da.Fill(dt);
            return dt;
        }


















    }
}