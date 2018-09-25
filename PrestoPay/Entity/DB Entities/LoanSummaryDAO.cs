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
    public class LoanSummaryDAO
    {
        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public Loan UpdateLoanRepaymentStatusByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a Loan, DataSet instance and dataTable instance
            Loan loanObj = new Loan();
            DataSet ds = new DataSet();
            DataTable loanData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised BUSI_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT loan_id, loan_applicationStatus, loan_totalAmountToBeRepaid ");
            sqlStr.AppendLine("FROM Loan ");
            sqlStr.AppendLine("WHERE Loan.busi_id = @paraBusi_id ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoanRepayment");

            // Step 7: Select command return a row from TableLoanRepayment contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableLoanRepayment"].Rows.Count;
            Loan myloanObj = new Loan();
            if (rec_cnt > 0)
            {
                // Loan myloanObj = new Loan();
                // Step 8 Set attribute of Loan instance for the record in TableLoanRepayment
                // DataRow is set to Rows[i] because more than one row may be returned

                // DataRow row = ds.Tables["TableLoanRepayment"].Rows[0];
                myloanObj.busi_id = BUSI_ID;
                myloanObj.loan_repaymentStatus = "OTHERS";

                for (int i = 0; i < rec_cnt; i++)
                {
                    DataRow row = ds.Tables["TableLoanRepayment"].Rows[i];
                    myloanObj.loan_id = Convert.ToString(row["loan_id"]);
                    myloanObj.loan_applicationStatus = Convert.ToString(row["loan_applicationStatus"]);
                    myloanObj.loan_totalAmountToBeRepaid = Convert.ToDouble(row["loan_totalAmountToBeRepaid"]);



                    // Check LoanRepayment DB to get the total loan repayment amount
                    Loan loanTotalRmptAmtObj = new Loan();
                    LoanSummaryDAO loanTotalRmptAmtDao = new LoanSummaryDAO();

                    loanTotalRmptAmtObj = loanTotalRmptAmtDao.ReadTotalLoanRepaymentAmountByLoanId(myloanObj.loan_id);

                    if (loanTotalRmptAmtObj != null)
                    {
                        myloanObj.loan_totalRepaymentAmountCount = loanTotalRmptAmtObj.loan_totalRepaymentAmountCount;
                        myloanObj.loan_totalAmountRepaid = loanTotalRmptAmtObj.loan_totalAmountRepaid;
                    }
                    else
                    {
                        myloanObj.loan_totalRepaymentAmountCount = 0;
                        myloanObj.loan_totalAmountRepaid = 0.0;
                    } // if (loanTotalRmptAmtObj)

                    if (myloanObj.loan_applicationStatus == "PENDING")
                    {
                        myloanObj.loan_repaymentStatus = "PENDING";
                    }
                    else if (myloanObj.loan_applicationStatus == "APPROVED")
                    {
                        if (myloanObj.loan_totalAmountRepaid >= myloanObj.loan_totalAmountToBeRepaid)
                        {
                           myloanObj.loan_repaymentStatus = "FULL";
                        }
                        else
                        {
                            myloanObj.loan_repaymentStatus = "OUTSTANDING";
                        } //  if (myloanObj.loan_totalAmountRepaid)
                    }
                    else if (myloanObj.loan_applicationStatus == "REJECTED")
                    {
                        myloanObj.loan_repaymentStatus = "REJECTED";
                    }
                    else if (myloanObj.loan_applicationStatus == "CANCELLED")
                    {
                        myloanObj.loan_repaymentStatus = "CANCELLED";
                    }
                    else
                    {
                        myloanObj.loan_repaymentStatus = "OTHERS";
                    } // if (myloanObj.loan_applicationStatus)



                    // Write loan_totalAmountRepaid and loan_repaymentStatus to Loan DB
                    int loanWriteTotalRmptAmtResult = 0;
                    LoanSummaryDAO loanWriteTotalRmptAmtDao = new LoanSummaryDAO();

                    loanWriteTotalRmptAmtResult = loanWriteTotalRmptAmtDao.WriteLoanRepaymentStatusByLoanId(myloanObj.loan_id, myloanObj.loan_totalRepaymentAmountCount, myloanObj.loan_totalAmountRepaid, myloanObj.loan_repaymentStatus);
                    if (loanWriteTotalRmptAmtResult == 1)
                    {
                        //myloanObj.loan_successMsg = "UpdateLoanRepaymentStatusByBusiId: Loan Repayment Status has been written sucessfully!";
                        myloanObj.loan_successMsg = "Loan Repayment Status has been written sucessfully!";
                    }
                    else
                    {
                        //myloanObj.loan_errorMsg = "UpdateLoanRepaymentStatusByBusiId Error: Unable to write Loan Repayment Status, please inform System Administrator!";
                        myloanObj.loan_errorMsg = "Error: Unable to write Loan Repayment Status, please inform System Administrator!";
                    } // if (loanWriteTotalRmptAmtResult()
                } // for(i)
            }
            else
            {
                myloanObj = null;
             } //if (rec_cnt)

            return myloanObj;
        } // UpdateLoanRepaymentStatusByBusiId()


        public Loan ReadTotalLoanRepaymentAmountByLoanId(string LOAN_ID)
        {
            // Step 2 : declare a Loan, DataSet instance and dataTable instance
            Loan loanObj = new Loan();
            DataSet ds = new DataSet();
            DataTable loanData = new DataTable();

            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised LOAN_ID

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT loan_id, COUNT(*) AS TotalRpmtAmtCount, SUM(rpmt_amt) AS TotalRpmtAmt ");
            sqlStr.AppendLine("FROM LoanRepayment ");
            sqlStr.AppendLine("WHERE loan_id = @paraLoan_id ");
            sqlStr.AppendLine("GROUP BY loan_id ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraLoan_id", LOAN_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoanRepaymentAmount");

            // Step 7: Select command return a row from TableLoanRepaymentAmount contain the selected LoanRepayment
            int rec_cnt = ds.Tables["TableLoanRepaymentAmount"].Rows.Count;
            Loan loanTotalRmptAmtObj = new Loan();
            loanTotalRmptAmtObj.loan_id = LOAN_ID;

            if (rec_cnt > 0)
            {
                // Loan loanTotalRmptAmtObj = new Loan();
                // Step 8 Set attribute of Loan instance for the record in TableLoanRepaymentAmount
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableLoanRepaymentAmount"].Rows[0];
                loanTotalRmptAmtObj.loan_totalRepaymentAmountCount = Convert.ToInt16(row["TotalRpmtAmtCount"]);
                loanTotalRmptAmtObj.loan_totalAmountRepaid = Convert.ToDouble(row["TotalRpmtAmt"]);
            }
            else
            {
                loanTotalRmptAmtObj = null;
            } //if (rec_cnt)

            return loanTotalRmptAmtObj;
        } // ReadTotalLoanRepaymentAmountByLoanId()



        public int WriteLoanRepaymentStatusByLoanId(string LOAN_ID, int LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT,  double LOAN_TOTAL_AMOUNT_REPAID, string  LOAN_REPAYMENT_STATUS)
        {
            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL update command to change column of tdRenewMode for the selected record in TDMaster using     
            //         parameterised query in values clause
            //
            sqlStr.AppendLine("UPDATE Loan ");
            sqlStr.AppendLine("SET loan_totalRepaymentAmountCount = @paraLoan_totalRepaymentAmountCount, ");
            sqlStr.AppendLine("loan_totalAmountRepaid = @paraLoan_totalAmountRepaid, ");
            sqlStr.AppendLine("loan_repaymentStatus = @paraLoan_repaymentStatus ");
            sqlStr.AppendLine("WHERE loan_id = @paraLoan_id ");

            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalRepaymentAmountCount", LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalAmountRepaid", LOAN_TOTAL_AMOUNT_REPAID);
            sqlCmd.Parameters.AddWithValue("@paraLoan_repaymentStatus", LOAN_REPAYMENT_STATUS);
            sqlCmd.Parameters.AddWithValue("@paraLoan_id", LOAN_ID);

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        } // WriteLoanRepaymentStatusByLoanId()

        public List<Loan> CheckLoanRepaymentSummaryByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a list to hold collection of customer's Loan
            //           DataSet instance and dataTable instance 

            List<Loan> loanRepaymentSummaryList = new List<Loan>();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();


            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised BUSI_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT loan_id, loan_applicationDate, loan_repaymentRate, loan_applicationStatus, loan_totalAmountToBeRepaid, loan_totalAmountRepaid, loan_repaymentStatus ");
            sqlStr.AppendLine("FROM Loan ");
            sqlStr.AppendLine("WHERE busi_id = @paraBusi_id ");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoanRepaymentSummary");

            // Step 7: Iterate the rows from TableLoanRepaymentSummary above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableLoanRepaymentSummary"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableLoanRepaymentSummary"].Rows)
                {
                    Loan myloanRpmtSummaryObj = new Loan();
                    myloanRpmtSummaryObj.busi_id = BUSI_ID;

                    // Step 8: Set attribute of Loan instance for each row of record in TableLoanRepaymentSummary
                    myloanRpmtSummaryObj.loan_id = Convert.ToString(row["loan_id"]);
                    myloanRpmtSummaryObj.loan_applicationDate = Convert.ToDateTime(row["loan_applicationDate"]);
                    myloanRpmtSummaryObj.loan_repaymentRate = Convert.ToDouble(row["loan_repaymentRate"]);
                    myloanRpmtSummaryObj.loan_applicationStatus = Convert.ToString(row["loan_applicationStatus"]);
                    myloanRpmtSummaryObj.loan_totalAmountToBeRepaid = Convert.ToDouble(row["loan_totalAmountToBeRepaid"]);
                    myloanRpmtSummaryObj.loan_totalAmountRepaid = Convert.ToDouble(row["loan_totalAmountRepaid"]);
                    myloanRpmtSummaryObj.loan_repaymentStatus = Convert.ToString(row["loan_repaymentStatus"]);
                    
                    //  Step 9: Add each Loan instance to array list
                    loanRepaymentSummaryList.Add(myloanRpmtSummaryObj);
                }
            }
            else
            {
                loanRepaymentSummaryList = null;
            }

            return loanRepaymentSummaryList;
        } // CheckLoanRepaymentSummaryByBusiId()
    } // LoanSummaryDAO
} // PrestoPay.Entity