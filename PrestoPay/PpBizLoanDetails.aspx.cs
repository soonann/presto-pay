using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;
using System.Data;

namespace PrestoPay
{
    public partial class PpBizLoanDetails : System.Web.UI.Page
    {
        //internal static readonly string aspx;

        protected void Page_Load(object sender, EventArgs e)
        {
                DonutGraphLoanPercentageRepaid();
            // DailyStackedGraphRepaymentPercentage();
        } //Page_Load


        public void DonutGraphLoanPercentageRepaid()
        {
            lblBizLoanId.Text = (string)Session["LoanRepaymentSummaryGridView_Loan_Id"];

            //    Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanDetailsDAO updateLoanDetailsDAO = new LoanDetailsDAO();

            updateLoanObj = updateLoanDetailsDAO.UpdateLoanRepaymentDetailsByLoanId(lblBizLoanId.Text);


            //    Check DB if there is any existing outstanding loan
            Loan loanObj = new Loan();
            LoanDetailsDAO loanDetailsDAO = new LoanDetailsDAO();

            loanObj = loanDetailsDAO.CheckLoanRepaymentDetailsByLoanId(lblBizLoanId.Text);

            double dummy_1 = 0;


            // Populate infomations on labels
            lblAnnualSales.Text = "";
            lblLoanAmount.Text = "";
            lblDateOfApplication.Text = "";
            lblDateOfApproval.Text = "";

            // populate infomations on gridview
            DataTable dt = new DataTable();
            dt.Columns.Add("Repayment Percentage");
            dt.Columns.Add("Percentage You Keep");
            dt.Columns.Add("One Time Fixed Fee");
            dt.Columns.Add("Total To Be Repaid");
            dt.Columns.Add("Total Amount Repaid");
            dt.Columns.Add("Outstanding Amount To Be Repaid");
            dt.Columns.Add("Repayment Status");

            Series series = RepaymentPercentChart1.Series["RepaymetPercentSeries"];
            RepaymentPercentChart1.Legends["Legend1"].Docking = Docking.Bottom;

            if (loanObj != null)
            {
                dt.Rows.Add();

                string strOutstandingRepaymentAmountToBeRepaid = "Outstanding Repayment Amount";
                double dblOutstandingRepaymentAmountToBeRepaid = (loanObj.loan_totalAmountToBeRepaid - loanObj.loan_totalAmountRepaid);
                if (dblOutstandingRepaymentAmountToBeRepaid < 0.0)
                {
                    dblOutstandingRepaymentAmountToBeRepaid = 0.0;
                } // if (dblOutstandingRepaymentAmountToBeRepaid)

                string strTotalRepaymentAmountRepaid = "Total Repayment Amount Repaid";
                double dblTotalRepaymentAmountRepaid = loanObj.loan_totalAmountRepaid;

                // Populate infomations on labels
                lblAnnualSales.Text = loanObj.loan_saleRevAtApp.ToString();
                lblLoanAmount.Text = loanObj.loan_amt.ToString();
                lblDateOfApplication.Text = loanObj.loan_applicationDate.ToString();
                lblDateOfApproval.Text = loanObj.loan_approvalDate.ToString();

                dt.Rows[0]["Repayment Percentage"] = loanObj.loan_repaymentRate;
                dt.Rows[0]["Percentage You Keep"] = loanObj.loan_percentageKeep;
                dt.Rows[0]["One Time Fixed Fee"] = loanObj.loan_oneTimeFixedFee;
                dt.Rows[0]["Total To Be Repaid"] = loanObj.loan_totalAmountToBeRepaid;
                dt.Rows[0]["Total Amount Repaid"] = loanObj.loan_totalAmountRepaid;
                dt.Rows[0]["Outstanding Amount To Be Repaid"] = dblOutstandingRepaymentAmountToBeRepaid;
                dt.Rows[0]["Repayment Status"] = loanObj.loan_repaymentStatus;

                // Populate Donut Chart
                series.Points.AddXY(strOutstandingRepaymentAmountToBeRepaid, dblOutstandingRepaymentAmountToBeRepaid);
                series.Points.AddXY(strTotalRepaymentAmountRepaid, dblTotalRepaymentAmountRepaid);
            } // if(loanObj)

            IndividualLoanDetailGridView.DataSource = dt;
            IndividualLoanDetailGridView.AutoGenerateColumns = false;
            IndividualLoanDetailGridView.DataBind();
        } // DonutGraphLoanPercentageRepaid()

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PpBizLoanSummary.aspx");
        }


        protected void BtnViewLoanDetails_Click(object sender, EventArgs e)
        {
            DonutGraphLoanPercentageRepaid();
        } // BtnViewLoanDetails_Click()


        public string GetDateString(string strDateTimeString)
        {
            string strDateString = "";
            string strTempDateString = "";

            // strTempDateTimeString contains "1/7/2017 12:00AM"
            string strTempDateTimeString = strDateTimeString;

            int intLength = strTempDateTimeString.Length;

            while (intLength > 0)
            {
                strTempDateString = strTempDateTimeString.Substring(0, 1);

                if (strTempDateString == " ")
                {
                    break;
                } // if(strTempDateString)

                strDateString = strDateString + strTempDateString;

                strTempDateTimeString = strTempDateTimeString.Substring(1);
                intLength = strTempDateTimeString.Length;
            } // while (intLength)

            // strDateString contains "1/7/2017"
            return strDateString;
        } // GetDateString()

    } // PpBizLoanDetails
} // PrestoPay