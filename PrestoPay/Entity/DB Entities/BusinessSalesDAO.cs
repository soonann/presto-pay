using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;


namespace PrestoPay.Entity.DB_Entities
{
    public class BusinessSalesDAO
    {
        // Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public BusinessSales ReadBusinessSalesByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a BusinessSales, DataSet instance and dataTable instance
            BusinessSales bs = new BusinessSales();
            DataSet ds = new DataSet();
            DataTable bsData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from BusinessSales Table by parameterised BUSI_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("select count(*) as bs_salesAmountCount, SUM(salesAmount) AS bs_totalSalesAmount ");
            sqlStr.AppendLine("FROM Business INNER JOIN BusinessSales on Business.busi_id = BusinessSales.busi_id ");
            sqlStr.AppendLine("WHERE (Business.busi_id = @paraBusi_id) AND (((CONVERT(int, CONVERT(varchar, GETDATE(), 112)) - CONVERT(int, CONVERT(varchar, salesReportingDate, 112)))/10000) BETWEEN 0 AND 3) ");
            sqlStr.AppendLine("GROUP BY Business.busi_id ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBS");

            // Step 7: Select command return a row from TableBS contain the selected TD
            int rec_cnt = ds.Tables["TableBS"].Rows.Count;
            BusinessSales mybs = new BusinessSales();
            if (rec_cnt > 0)
            {
                // BusinessSales mybs = new BusinessSales();
                // Step 8 Set attribute of BusinessSales instance for the record in TableBS
                // DataRow is set to Rows[0] because only one row is returned
                //
                DataRow row = ds.Tables["TableBS"].Rows[0];
                mybs.busi_id = BUSI_ID;
                mybs.bs_salesAmountCount = Convert.ToInt16(row["bs_salesAmountCount"]);
                mybs.bs_totalSalesAmount = Convert.ToDouble(row["bs_totalSalesAmount"]);
            }
            else
            {
                mybs = null;
            }
            return mybs;

        } // ReadBusinessSalesByBusiId()

    } // BusinessSalesDAO
} // PrestoPay.Entity.DB_Entities