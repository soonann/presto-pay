using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;

namespace PrestoPay.Entity.DB_Entities
{
    public class BusinessDAO
    {

        //Get connection string from web.config
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public Business getBusinessById(string BUSI_ID)
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            //Create Adapter
            //WRITE SQL Statement to retrieve all columns from Business by customer Id using query parameter
            StringBuilder sqlCommand = new StringBuilder();
            sqlCommand.AppendLine("Select * from Business where");
            sqlCommand.AppendLine("busi_id = @paraBusi_id");

            // create a Business instance
            Business obj = new Business();   

            SqlConnection myConn = new SqlConnection(DBConnect);
            da = new SqlDataAdapter(sqlCommand.ToString(), myConn);
            da.SelectCommand.Parameters.AddWithValue("paraBusi_id", BUSI_ID);

            // fill dataset
            da.Fill(ds, "BusinessTable");
            int rec_cnt = ds.Tables["BusinessTable"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["BusinessTable"].Rows[0];  // Sql command returns only one record
                obj.busi_id = row["busi_id"].ToString();
                obj.busi_type = row["busi_type"].ToString();
                obj.busi_category = row["busi_category"].ToString(); 
                obj.busi_countryOfReg = row["busi_countryOfReg"].ToString();
                obj.busi_companyName = row["busi_companyName"].ToString();
                obj.busi_defaultItem = row["busi_defaultItem"].ToString();
            }
            else
            {
                obj = null;
            }

            return obj;
        } // getBusinessById()

        public Business GetBusinessCompanyNameByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a Business, DataSet instance and dataTable instance
            Business bsObj = new Business();
            DataSet ds = new DataSet();
            DataTable loanData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from Business Table by parameterised BUSI_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT busi_id, busi_companyName ");
            sqlStr.AppendLine("FROM Business ");
            sqlStr.AppendLine("WHERE busi_id = @paraBusi_id ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBusiness");

            // Step 7: Select command return a row from TableBusiness contain the selected BusinessRepayment
            int rec_cnt = ds.Tables["TableBusiness"].Rows.Count;
            Business bsCompanyNameObj = new Business();
            bsCompanyNameObj.busi_id = BUSI_ID;

            if (rec_cnt > 0)
            {
                // Business bsCompanyNameObj = new Business();
                // Step 8 Set attribute of Business instance for the record in TableBusiness
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBusiness"].Rows[0];
                bsCompanyNameObj.busi_companyName = Convert.ToString(row["busi_companyName"]);
            }
            else
            {
                bsCompanyNameObj = null;
            } //if (rec_cnt)

            return bsCompanyNameObj;
        }

        public Business ReadBusinessCompanyNameByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a Business, DataSet instance and dataTable instance
            Business bsObj = new Business();
            DataSet ds = new DataSet();
            DataTable loanData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from Business Table by parameterised BUSI_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT busi_id, busi_companyName ");
            sqlStr.AppendLine("FROM Business ");
            sqlStr.AppendLine("WHERE busi_id = @paraBusi_id ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableBusiness");

            // Step 7: Select command return a row from TableBusiness contain the selected BusinessRepayment
            int rec_cnt = ds.Tables["TableBusiness"].Rows.Count;
            Business bsCompanyNameObj = new Business();
            bsCompanyNameObj.busi_id = BUSI_ID;

            if (rec_cnt > 0)
            {
                // Business bsCompanyNameObj = new Business();
                // Step 8 Set attribute of Business instance for the record in TableBusiness
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableBusiness"].Rows[0];
                bsCompanyNameObj.busi_companyName = Convert.ToString(row["busi_companyName"]);
            }
            else
            {
                bsCompanyNameObj = null;
            } //if (rec_cnt)

            return bsCompanyNameObj;
        } // ReadBusinessCompanyNameByBusiId()

        public Business GetBusinessIdByEmail(string ACC_EMAIL)
        {
            // Step 2 : declare a Business, DataSet instance and dataTable instance
            Business bsBusiIdObj = new Business();
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from Business Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT busi_id ");
            sqlStr.AppendLine("FROM [Account] ");
            sqlStr.AppendLine("WHERE acc_email = @paraAcc_email ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);

            // Step 6: fill dataset
            da.Fill(ds, "TableBusiness");

            // Step 7: Select command return a row from TableBusiness contain the selected BusinessRepayment
            int rec_cnt = ds.Tables["TableBusiness"].Rows.Count;

            string BUSI_ID = "";

            if (rec_cnt > 0)
            {
                // Business bsBusiIdObj = new Business();
                // Step 8 Set attribute of Business instance for the record in TableBusiness
                // DataRow is set to Rows[0] because only 1 row is returned

                DataRow row = ds.Tables["TableBusiness"].Rows[0];

                if (String.IsNullOrWhiteSpace(Convert.ToString(row["busi_id"])))
                {
                    BUSI_ID = "";
                    bsBusiIdObj = null;
                }
                else
                {
                    BUSI_ID = Convert.ToString(row["busi_id"]);
                    bsBusiIdObj.busi_id = BUSI_ID;
                } // if(String.IsNullOrWhiteSpace()
            }
            else
            {
                BUSI_ID = "";
                bsBusiIdObj = null;
            } //if (rec_cnt)

            return bsBusiIdObj;
        }


        public List<Business> GetBusinessIdByAccountEmail(string ACC_EMAIL)
        {
            // Step 2 : declare a Business, DataSet instance and dataTable instance
            Business bsObj = new Business();
            DataSet ds = new DataSet();
            DataTable loanData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from Business Table by parameterised ACC_EMAIL

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT busi_id, acc_email ");
            sqlStr.AppendLine("FROM Business ");
            sqlStr.AppendLine("WHERE acc_email = @paraAcc_email ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraAcc_email", ACC_EMAIL);

            // Step 6: fill dataset
            da.Fill(ds, "TableBusiness");

            // Step 7: Select command return a row from TableBusiness contain the selected BusinessRepayment
            int rec_cnt = ds.Tables["TableBusiness"].Rows.Count;
            
            List<Business> bsBusiIdList = new List<Business>();
            string BUSI_ID = "";

            if (rec_cnt > 0)
            {
                // Business bsAccountIdObj = new Business();
                // Step 8 Set attribute of Business instance for the record in TableBusiness
                // DataRow is set to Rows[i] because more than 1 row may be returned

                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables["TableBusiness"].Rows[i];
                    BUSI_ID = Convert.ToString(row["busi_id"]);

                    Business bsBusiIdObj = new Business();
                    bsBusiIdObj.busi_id = BUSI_ID;

                    bsBusiIdList.Add(bsBusiIdObj);
                } // for (int i)
            }
            else
            {
                BUSI_ID = "";
                // bsBusiIdObj = null;
                bsBusiIdList = null;
            } //if (rec_cnt)

            return bsBusiIdList;
        } // ReadBusinessIdByAccountId()





    }
}