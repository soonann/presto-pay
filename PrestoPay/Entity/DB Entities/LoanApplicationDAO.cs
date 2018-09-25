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
    public class LoanApplicationDAO
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
                    LoanApplicationDAO loanTotalRmptAmtDao = new LoanApplicationDAO();

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
                    int result = 0;
                    LoanApplicationDAO loanWriteTotalRmptAmtDao = new LoanApplicationDAO();

                    result = loanWriteTotalRmptAmtDao.WriteLoanRepaymentStatusByLoanId(myloanObj.loan_id, myloanObj.loan_totalRepaymentAmountCount, myloanObj.loan_totalAmountRepaid, myloanObj.loan_repaymentStatus);
                    if (result == 1)
                    {
                        //myloanObj.loan_successMsg = "UpdateLoanRepaymentStatusByBusiId: Loan Repayment Status has been written sucessfully!";
                        myloanObj.loan_successMsg = "Loan Repayment Status has been written sucessfully!";
                    }
                    else
                    {
                        //myloanObj.loan_errorMsg = "UpdateLoanRepaymentStatusByBusiId Error: Unable to write Loan Repayment Status, please inform System Administrator!";
                        myloanObj.loan_errorMsg = "Error: Unable to write Loan Repayment Status, please inform System Administrator!";
                    } // if (result)
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


        public Loan CheckLoanRepaymentLimitsByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a list to hold collection of customer's Loan
            //           DataSet instance and dataTable instance 

            Loan loanRepaymentLimitsObj = new Loan();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();


            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised BUSI_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT COUNT(*) AS TotalApprovedLoanCount, SUM(loan_totalAmountToBeRepaid) AS TotalApprovedLoanAmount, SUM(loan_totalAmountRepaid) AS TotalAmountRepaid ");
            sqlStr.AppendLine("FROM Loan ");
            sqlStr.AppendLine("WHERE (busi_id = @paraBusi_id) AND (loan_applicationStatus = 'APPROVED') ");
            sqlStr.AppendLine("GROUP BY busi_id");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoanRepaymentLimits");

            // Step 7: Iterate the rows from TableLoanRepaymentLimits above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableLoanRepaymentLimits"].Rows.Count;
            if (rec_cnt > 0)
            {
                DataRow row = ds.Tables["TableLoanRepaymentLimits"].Rows[0];

                // Step 8: Set attribute of Loan instance for each row of record in TableLoanRepaymentLimits
                // loanRepaymentLimitsObj.loan_totalApprovedLoanCount = Convert.ToInt16(row["TotalApprovedLoanCount"]);
                loanRepaymentLimitsObj.loan_totalAmountToBeRepaid = Convert.ToInt16(row["TotalApprovedLoanAmount"]);
                loanRepaymentLimitsObj.loan_totalAmountRepaid = Convert.ToInt16(row["TotalAmountRepaid"]);
            }
            else
            {
                loanRepaymentLimitsObj = null;
            }

            return loanRepaymentLimitsObj;
        } // CheckLoanRepaymentLimitsByBusiId()


        public List<Loan> CheckLoanRepaymentStatusByBusiId(string BUSI_ID)
        {
            // Step 2 : declare a list to hold collection of customer's Loan
            //           DataSet instance and dataTable instance 

            List<Loan> loanRepaymentStatusList = new List<Loan>();
            DataSet ds = new DataSet();
            DataTable tdData = new DataTable();


            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised BUSI_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT COUNT(*) AS TotalRepaymentStatusCount, loan_repaymentStatus ");
            sqlStr.AppendLine("FROM Loan ");
            sqlStr.AppendLine("WHERE busi_id = @paraBusi_id ");
            sqlStr.AppendLine("GROUP BY loan_repaymentStatus");


            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance

            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 

            da.SelectCommand.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoanRepaymentStatus");

            // Step 7: Iterate the rows from TableLoanRepaymentStatus above to create a collection of TD
            //         for this particular customer 

            int rec_cnt = ds.Tables["TableLoanRepaymentStatus"].Rows.Count;
            if (rec_cnt > 0)
            {
                foreach (DataRow row in ds.Tables["TableLoanRepaymentStatus"].Rows)
                {
                    Loan myloanRpmtStatusObj = new Loan();

                    // Step 8: Set attribute of Loan instance for each row of record in TableLoanRepaymentStatus
                    myloanRpmtStatusObj.loan_totalRepaymentStatusCount = Convert.ToInt16(row["TotalRepaymentStatusCount"]);
                    myloanRpmtStatusObj.loan_repaymentStatus = Convert.ToString(row["loan_repaymentStatus"]);

                    //  Step 9: Add each Loan instance to array list
                    loanRepaymentStatusList.Add(myloanRpmtStatusObj);
                }
            }
            else
            {
                loanRepaymentStatusList = null;
            }

            return loanRepaymentStatusList;
        } // CheckLoanRepaymentStatusByBusiId()


        public string GetNextLoanId()
        {
            string LOAN_ID = "";

            // Step 2 : declare a Loan, DataSet instance and dataTable instance
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from Loan Table by parameterised LOAN_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT TOP 1 loan_id FROM Loan ORDER BY loan_id DESC ");
            
            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 
            // da.SelectCommand.Parameters.AddWithValue("@paraLoan_id", LOAN_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableLoan_id");

            // Step 7: Select command return a row from TableLoan_id contain the selected LOAN_ID
            int rec_cnt = ds.Tables["TableLoan_id"].Rows.Count;

            Loan loanObj = new Loan();

            int intLoan_id = 0;

            if (rec_cnt > 0)
            {
                // Loan loanIdObj = new Loan();
                // Step 8 Set attribute of Loan instance for the record in TableLoan_id
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableLoan_id"].Rows[0];

                // The loanObj.loan_id contains "LN00000002" LOAN_ID
                loanObj.loan_id = Convert.ToString(row["loan_id"]);

                // get the length of the current string
                int intLength = loanObj.loan_id.Length;

                while (intLength > 0)
                {
                    // Check whether the loanObj.loan_id contains numeric "00000002" LOAN_ID
                    bool result = int.TryParse(loanObj.loan_id, out intLoan_id);
                    if (result == true)
                    {
                        intLoan_id = int.Parse(loanObj.loan_id);
                        break;
                    }
                    else
                    {
                        // The loanObj.loan_id contains non numeric "LN00000002" LOAN_ID

                        // The loanObj.loan_id contains non numeric "N00000002" LOAN_ID
                        loanObj.loan_id = loanObj.loan_id.Substring(1);

                        // get the length of the new string
                        intLength = loanObj.loan_id.Length;
                    } // if (result)
                } // while (intLength)

                // The intLoan_id contains 2 LOAN_ID

                // The intLoan_id contains 3 LOAN_ID
                intLoan_id++;

                // The LOAN_ID contains LN00000003 LOAN_ID
                LOAN_ID = "LN" + intLoan_id.ToString("D8");
            }
            else
            {
                loanObj = null;

                // The Loan table is empty
                // Start with the first LN00000001 LOAN_ID
                LOAN_ID = "LN00000001";
            } //if (rec_cnt)

            return LOAN_ID;
        } // GetNextLoanId()


        public string InsertLoanApplicationIntoLoanTableByBusiId(string BUSI_ID,
                                                                double LOAN_SALES_REV_AT_APP, 
                                                                double LOAN_AMT, 
                                                                DateTime LOAN_APPLICATION_DATE, 
                                                                double LOAN_REPAYMENT_RATE, 
                                                                double  LOAN_PERCENTAGE_KEEP,
                                                                double LOAN_ONE_TIME_FIXED_FEE,
                                                                double LOAN_TOTAL_AMOUNT_TO_BE_REPAID, 
                                                                string LOAN_APPLICATION_STATUS,
                                                                string LOAN_REASON_FOR_APPLICATION_STATUS,
                                                                DateTime LOAN_APPROVAL_DATE,
                                                                string LOAN_APPROVED_BY_ADMIN_USER_NAME, 
                                                                int LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT,
                                                                double LOAN_TOTAL_AMOUNT_REPAID,
                                                                int LOAN_TOTAL_REPAYMENT_STATUS_COUNT,
                                                                DateTime LOAN_REPAYMENT_DATE,
                                                                string LOAN_REPAYMENT_STATUS,
                                                                string LOAN_REASON_FOR_REPAYMENT_STATUS,
                                                                double LOAN_TOTAL_TRANSACTION_AMOUNT,
                                                                string LOAN_SUCCESS_MSG,
                                                                string LOAN_ERROR_MSG)
        {
            // Get the next LOAN_ID
            string LOAN_ID = GetNextLoanId();

            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //
            sqlStr.AppendLine("INSERT INTO Loan (loan_id, busi_id, loan_saleRevAtApp, loan_amt, ");
            sqlStr.AppendLine("loan_applicationDate, loan_repaymentRate, loan_percentageKeep, loan_oneTimeFixedFee, ");
            sqlStr.AppendLine("loan_totalAmountToBeRepaid, loan_applicationStatus, loan_reasonForApplicationStatus, loan_approvalDate, loan_approvedByAdminUserName, "); 
            sqlStr.AppendLine("loan_totalRepaymentAmountCount, loan_totalAmountRepaid, loan_totalRepaymentStatusCount, loan_repaymentDate, loan_repaymentStatus, ");
            sqlStr.AppendLine("loan_reasonForRepaymentStatus, loan_totalTransactionAmount, loan_successMsg, loan_errorMsg)");

            sqlStr.AppendLine("VALUES (@paraLoan_id, @paraBusi_id, @paraLoan_saleRevAtApp, @paraLoan_amt, ");
            sqlStr.AppendLine("@paraLoan_applicationDate, @paraLoan_repaymentRate, @paraLoan_percentageKeep, @paraLoan_oneTimeFixedFee, ");
            sqlStr.AppendLine("@paraLoan_totalAmountToBeRepaid, @paraLoan_applicationStatus, @paraLoan_reasonForApplicationStatus, @paraLoan_approvalDate, @paraLoan_approvedByAdminUserName, ");
            sqlStr.AppendLine("@paraLoan_totalRepaymentAmountCount, @paraLoan_totalAmountRepaid, @paraLoan_totalRepaymentStatusCount, @paraLoan_repaymentDate, @paraLoan_repaymentStatus, ");
            sqlStr.AppendLine("@paraLoan_reasonForRepaymentStatus , @paraLoan_totalTransactionAmount, @paraLoan_successMsg, @paraLoan_errorMsg)");



            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraLoan_id", LOAN_ID);
            sqlCmd.Parameters.AddWithValue("@paraBusi_id", BUSI_ID);
            sqlCmd.Parameters.AddWithValue("@paraLoan_saleRevAtApp", LOAN_SALES_REV_AT_APP);
            sqlCmd.Parameters.AddWithValue("@paraLoan_amt", LOAN_AMT);
            sqlCmd.Parameters.AddWithValue("@paraLoan_applicationDate", LOAN_APPLICATION_DATE);
            sqlCmd.Parameters.AddWithValue("@paraLoan_repaymentRate", LOAN_REPAYMENT_RATE);
            sqlCmd.Parameters.AddWithValue("@paraLoan_percentageKeep", LOAN_PERCENTAGE_KEEP);
            sqlCmd.Parameters.AddWithValue("@paraLoan_oneTimeFixedFee", LOAN_ONE_TIME_FIXED_FEE);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalAmountToBeRepaid", LOAN_TOTAL_AMOUNT_TO_BE_REPAID);
            sqlCmd.Parameters.AddWithValue("@paraLoan_applicationStatus", LOAN_APPLICATION_STATUS);
            sqlCmd.Parameters.AddWithValue("@paraLoan_reasonForApplicationStatus", LOAN_REASON_FOR_APPLICATION_STATUS);
            sqlCmd.Parameters.AddWithValue("@paraLoan_approvalDate", LOAN_APPROVAL_DATE);
            sqlCmd.Parameters.AddWithValue("@paraLoan_approvedByAdminUserName", LOAN_APPROVED_BY_ADMIN_USER_NAME);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalRepaymentAmountCount", LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalAmountRepaid", LOAN_TOTAL_AMOUNT_REPAID);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalRepaymentStatusCount", LOAN_TOTAL_REPAYMENT_STATUS_COUNT);
            sqlCmd.Parameters.AddWithValue("@paraLoan_repaymentDate", LOAN_REPAYMENT_DATE);
            sqlCmd.Parameters.AddWithValue("@paraLoan_repaymentStatus", LOAN_REPAYMENT_STATUS);
            sqlCmd.Parameters.AddWithValue("@paraLoan_reasonForRepaymentStatus", LOAN_REASON_FOR_REPAYMENT_STATUS);
            sqlCmd.Parameters.AddWithValue("@paraLoan_totalTransactionAmount", LOAN_TOTAL_TRANSACTION_AMOUNT);
            sqlCmd.Parameters.AddWithValue("@paraLoan_successMsg", LOAN_SUCCESS_MSG);
            sqlCmd.Parameters.AddWithValue("@paraLoan_errorMsg", LOAN_ERROR_MSG);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            // Check whether the record has been written into the Loan table successfully
            if(result > 0)
            {
                return LOAN_ID;
            }
            else
            {
                return null;
            } // if(result)
        } // InsertLoanApplicationIntoLoanTableByBusiId()
    } // LoanApplicationDAO
} // PrestoPay.Entity