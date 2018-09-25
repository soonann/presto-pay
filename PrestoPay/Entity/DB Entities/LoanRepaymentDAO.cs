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
    public class LoanRepaymentDAO
    {

        // Step 1: Place the DBConnect to class variable to be shared by all the methodsin this class
        string DBConnect = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        public double PerformLoanRepaymentByAccountEmail(string email, double amt, string TRANS_ID)
        {
            string strTrans_id = TRANS_ID;
            string strReference_id = "";
            double remainingAmount = amt;

            if (email != null)
            {
                string ACC_EMAIL = email;

                string BUSI_ID = "";
                BusinessDAO bsDao = new BusinessDAO();
                Business bsBusiIdObj = new Business();

                bsBusiIdObj = bsDao.GetBusinessIdByEmail(ACC_EMAIL);

                if(bsBusiIdObj != null)
                {
                    BUSI_ID = bsBusiIdObj.busi_id;

                    if (BUSI_ID != "")
                    {
                        //    Update DB if there is an existing outstanding loan
                        Loan updateLoanObj1 = new Loan();
                        LoanApplicationDAO updateLoanApplicationDAO1 = new LoanApplicationDAO();

                        updateLoanObj1 = updateLoanApplicationDAO1.UpdateLoanRepaymentStatusByBusiId(BUSI_ID);

                        //    Check DB if there is any existing outstanding loan
                        List<Loan> loanList = new List<Loan>();
                        LoanSummaryDAO loanSummaryDAO = new LoanSummaryDAO();

                        loanList = loanSummaryDAO.CheckLoanRepaymentSummaryByBusiId(BUSI_ID);

                        double dummy_1 = 0;

                        string strLoanId = "";
                        DateTime dtmApplicationDate = new DateTime();
                        string strApplicationStatus = "";
                        double dblRepaymentRate = 0.0;
                        double dblTotalAmountToBeRepaid = 0.0;
                        double dblTotalAmountRepaid = 0.0;
                        string strRepaymentStatus = "";

                        DateTime dtmRepaymentDate = DateTime.Now;

                        double dblRepaymentAmount = 0.0;
                        double dblCalculatedRepaymentAmount = 0.0;
                        double dblOutstandingRepaymentAmount = 0.0;

                        if (loanList != null)
                        {
                            int rec_cnt = loanList.Count;

                            if (rec_cnt > 0)
                            {
                                for (int i = 0; i < rec_cnt; i++)
                                {
                                    strLoanId = loanList[i].loan_id;
                                    dtmApplicationDate = Convert.ToDateTime(loanList[i].loan_applicationDate);
                                    strApplicationStatus = loanList[i].loan_applicationStatus;
                                    dblRepaymentRate = loanList[i].loan_repaymentRate;
                                    dblTotalAmountToBeRepaid = Convert.ToDouble(loanList[i].loan_totalAmountToBeRepaid);
                                    dblTotalAmountRepaid = Convert.ToDouble(loanList[i].loan_totalAmountRepaid);
                                    strRepaymentStatus = loanList[i].loan_repaymentStatus;

                                    dblCalculatedRepaymentAmount = (dblRepaymentRate/100.0) * amt;
                                    dblOutstandingRepaymentAmount = dblTotalAmountToBeRepaid - dblTotalAmountRepaid;

                                    if ((strRepaymentStatus == "OUTSTANDING") &&
                                        (dblCalculatedRepaymentAmount > 0.0) &&
                                        (dblOutstandingRepaymentAmount > 0.0))
                                    {
                                        if (dblOutstandingRepaymentAmount > dblCalculatedRepaymentAmount)
                                        {
                                            dblRepaymentAmount = dblCalculatedRepaymentAmount;
                                        }
                                        else
                                        {
                                            dblRepaymentAmount = dblOutstandingRepaymentAmount;
                                        } // if (dblOutstandingRepaymentAmount)

                                        if (dblRepaymentAmount > remainingAmount)
                                        {
                                            dblRepaymentAmount = remainingAmount;
                                        } // if (dblRepaymentAmount)

                                        if (remainingAmount >= dblRepaymentAmount)
                                        {
                                            // Write loan_totalAmountRepaid and loan_repaymentStatus to Loan DB
                                            string loanRmptId = "";
                                            LoanRepaymentDAO loanWriteRmptAmtDao = new LoanRepaymentDAO();

                                            loanRmptId = loanWriteRmptAmtDao.InsertLoanRepaymentAmountIntoLoanRepaymentTableByLoanId(strLoanId, strTrans_id, strReference_id, dblRepaymentAmount, dtmRepaymentDate);

                                            if (loanRmptId != null)
                                            {
                                                remainingAmount = remainingAmount - dblRepaymentAmount;
                                            } // if(loanRmptId)
                                        } // if (remainingAmount)
                                    } // if ((strRepaymentStatus))
                                } // for (i)

                                //    Update DB if there is an existing outstanding loan
                                Loan updateLoanObj2 = new Loan();
                                LoanApplicationDAO updateLoanApplicationDAO2 = new LoanApplicationDAO();

                                updateLoanObj2 = updateLoanApplicationDAO2.UpdateLoanRepaymentStatusByBusiId(BUSI_ID);
                            }
                            else
                            {
                                remainingAmount = amt;
                            } // if(rec_cnt)
                        }
                        else
                        {
                            remainingAmount = amt;
                        } // if(loanList)
                    }
                    else
                    {
                        remainingAmount = amt;
                    } // if (BUSI_ID)
                }
                else
                {
                    remainingAmount = amt;
                } // if(bsBusiIdObj)
            }
            else
            {
                remainingAmount = amt;
            } // if (email)

            if (remainingAmount < 0.0)
            {
                remainingAmount = 0.0;
            } // if (remainingAmount)

            return remainingAmount;
        } // PerformLoanRepaymentByAccountEmail()


        public string InsertLoanRepaymentAmountIntoLoanRepaymentTableByLoanId(string LOAN_ID, string TRANS_ID, string REFERENCE_ID, double REPAYMENT_AMOUNT, DateTime REPAYMENT_DATE)
        {
            // Get the next LOAN_ID
            string RPMT_ID = GetNextLoanRepaymentId();

            int result = 0;    // Execute NonQuery return an integer value

            StringBuilder sqlStr = new StringBuilder();
            SqlCommand sqlCmd = new SqlCommand();
            // Step1 : Create SQL insert command to add record to Loan using     

            //         parameterised query in values clause
            //
            sqlStr.AppendLine("INSERT INTO LoanRepayment (rpmt_id, loan_id, trans_id, reference_id, rpmt_amt, rpmt_date) ");

            sqlStr.AppendLine("VALUES ( @paraRpmt_id, @paraLoan_id, @paraTrans_id, @paraReference_id, @paraRpmt_amt, @paraRpmt_date) ");


            // Step 2 :Instantiate SqlConnection instance and SqlCommand instance

            SqlConnection myConn = new SqlConnection(DBConnect);

            sqlCmd = new SqlCommand(sqlStr.ToString(), myConn);

            // Step 3 : Add each parameterised query variable with value
            //          complete to add all parameterised queries
            sqlCmd.Parameters.AddWithValue("@paraRpmt_id", RPMT_ID);
            sqlCmd.Parameters.AddWithValue("@paraLoan_id", LOAN_ID);
            sqlCmd.Parameters.AddWithValue("@paraTrans_id", TRANS_ID);
            sqlCmd.Parameters.AddWithValue("@paraReference_id", REFERENCE_ID);
            sqlCmd.Parameters.AddWithValue("@paraRpmt_amt", REPAYMENT_AMOUNT);
            sqlCmd.Parameters.AddWithValue("@paraRpmt_date", REPAYMENT_DATE);

            // Step 4 Open connection the execute NonQuery of sql command   

            myConn.Open();
            result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            // Check whether the record has been written into the Loan table successfully
            if (result > 0)
            {
                return RPMT_ID;
            }
            else
            {
                return null;
            } // if(result)
        } // InsertLoanRepaymentAmountIntoLoanRepaymentTableByLoanId()


        public string GetNextLoanRepaymentId()
        {
            string RPMT_ID = "";

            // Step 2 : declare a LoanRepayment, DataSet instance and dataTable instance
            DataSet ds = new DataSet();

            // Step 3 :Create SQLcommand to select all columns from LoanRepayment Table by parameterised RPMT_ID
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine("SELECT TOP 1 rpmt_id FROM LoanRepayment ORDER BY rpmt_id DESC ");

            // Step 4 :Instantiate SqlConnection instance and SqlDataAdapter instance
            SqlConnection myConn = new SqlConnection(DBConnect);
            SqlDataAdapter da = new SqlDataAdapter(sqlStr.ToString(), myConn);

            // Step 5 :add value to parameter 
            // da.SelectCommand.Parameters.AddWithValue("@paraRpmt_id", RPMT_ID);

            // Step 6: fill dataset
            da.Fill(ds, "TableRpmt_id");

            // Step 7: Select command return a row from TableRpmt_id contain the selected RPMT_ID
            int rec_cnt = ds.Tables["TableRpmt_id"].Rows.Count;

            LoanRepayment loanRepaymentObj = new LoanRepayment();

            int intRpmt_id = 0;

            if (rec_cnt > 0)
            {
                // LoanRepayment loanIdObj = new LoanRepayment();
                // Step 8 Set attribute of LoanRepayment instance for the record in TableRpmt_id
                // DataRow is set to Rows[0] because only one row is returned

                DataRow row = ds.Tables["TableRpmt_id"].Rows[0];

                // The loanRepaymentObj.rpmt_id contains "LR00000002" RPMT_ID
                loanRepaymentObj.rpmt_id = Convert.ToString(row["rpmt_id"]);

                // get the length of the current string
                int intLength = loanRepaymentObj.rpmt_id.Length;

                while (intLength > 0)
                {
                    // Check whether the loanRepaymentObj.rpmt_id contains numeric "00000002" RPMT_ID
                    bool result = int.TryParse(loanRepaymentObj.rpmt_id, out intRpmt_id);
                    if (result == true)
                    {
                        intRpmt_id = int.Parse(loanRepaymentObj.rpmt_id);
                        break;
                    }
                    else
                    {
                        // The loanRepaymentObj.rpmt_id contains non numeric "LR00000002" RPMT_ID

                        // The loanRepaymentObj.rpmt_id contains non numeric "R00000002" RPMT_ID
                        loanRepaymentObj.rpmt_id = loanRepaymentObj.rpmt_id.Substring(1);

                        // get the length of the new string
                        intLength = loanRepaymentObj.rpmt_id.Length;
                    } // if (result)
                } // while (intLength)

                // The intRpmt_id contains 2 RPMT_ID

                // The intRpmt_id contains 3 RPMT_ID
                intRpmt_id++;

                // The RPMT_ID contains LR00000003 RPMT_ID
                RPMT_ID = "LR" + intRpmt_id.ToString("D8");
            }
            else
            {
                loanRepaymentObj = null;

                // The LoanRepayment table is empty
                // Start with the first LR00000001 RPMT_ID
                RPMT_ID = "LR00000001";
            } //if (rec_cnt)

            return RPMT_ID;
        } // GetNextLoanRepaymentId()

    } // LoanRepaymentDAO
} // PrestoPay.Entity