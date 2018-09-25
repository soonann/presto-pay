using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrestoPay.Entity.DB_Entities;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace PrestoPay
{
    public partial class PpBizLoanRepaymentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            if (!IsPostBack)
            {
                DailyStackedGraphRepaymentPercentage();
            } // if(!IsPostBack)
        } //Page_Load


        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PpBizLoanSummary.aspx");
        }


        public void DailyStackedGraphRepaymentPercentage()
        {
            DailyRepaymentGraphChart.Visible = false;

            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // Check whether Session["UserEmail"] is valid
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["UserEmail"])

            string ACC_EMAIL = (string)Session["UserEmail"];

            // Check whether ACC_EMAIL is valid
            if (ACC_EMAIL == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (ACC_EMAIL)


            string BUSI_ID = "";

            BusinessDAO bsDao = new BusinessDAO();
            Business bsBusiIdObj = new Business();

            bsBusiIdObj = bsDao.GetBusinessIdByEmail(ACC_EMAIL);

            if (bsBusiIdObj != null)
            {
                BUSI_ID = bsBusiIdObj.busi_id;

                if (BUSI_ID == null)
                {
                    BUSI_ID = "";
                } // if (BUSI_ID)
            }
            else
            {
                BUSI_ID = "";
            } // if (bsBusiIdObj)

            // Check whether BUSI_ID is valid
            if (String.IsNullOrWhiteSpace(BUSI_ID))
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUSI_ID)


            // Check whether Session["LoanRepaymentSummaryGridView_Loan_Id"] is valid
            if (Session["LoanRepaymentSummaryGridView_Loan_Id"] == null)
            {
                setLoanRepaymentSummaryGridView_Loan_Id(BUSI_ID);
            } // if (Session["LoanRepaymentSummaryGridView_Loan_Id"])

            // Check whether Session["LoanRepaymentSummaryGridView_Loan_Id"] is valid
            if (Session["LoanRepaymentSummaryGridView_Loan_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["LoanRepaymentSummaryGridView_Loan_Id"])

            string LOAN_ID = (string)Session["LoanRepaymentSummaryGridView_Loan_Id"];

            // Check whether LOAN_ID is valid
            if (LOAN_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (LOAN_ID)

            lblBizLoanId.Text = LOAN_ID;


            DateTime dtmTempStartDate = StartDateCalendar.SelectedDate;
            if (dtmTempStartDate.Year == 1)
            {
                // Check whether Session["LoanRepaymentDetailsStartDate"] is valid
                if (Session["LoanRepaymentDetailsStartDate"] != null)
                {
                    DateTime dtmLoanRepaymentDetailsStartDate = (DateTime)Session["LoanRepaymentDetailsStartDate"];

                    // Check whether StartDateCalendar.SelectedDate is valid
                    if (dtmLoanRepaymentDetailsStartDate != null)
                    {
                        StartDateCalendar.SelectedDate = dtmLoanRepaymentDetailsStartDate;
                        if (StartDateCalendar.SelectedDate.Year != 1)
                        {
                            lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
                        }
                        else
                        {
                            lblStartDate.Text = "";
                        } // if (StartDateCalendar.SelectedDate.Year)
                    }
                    else
                    {
                        //if ((sender == null) && (e == null))
                        //{
                        //    // return;
                        //} // if(sender)
                    } // if (StartDateCalendar.SelectedDate)
                }
                else
                {
                    //if ((sender == null) && (e == null))
                    //{
                    //    // return;
                    //} // if(sender)
                } // if (Session["LoanRepaymentDetailsStartDate"])
            } // if (dtmTempStartDate.Year)


            DateTime dtmTempEndDate = EndDateCalendar.SelectedDate;
            if (dtmTempEndDate.Year == 1)
            {
                // Check whether Session["LoanRepaymentDetailsEndDate"] is valid
                if (Session["LoanRepaymentDetailsEndDate"] != null)
                {
                    DateTime dtmLoanRepaymentDetailsEndDate = (DateTime)Session["LoanRepaymentDetailsEndDate"];

                    // Check whether EndDateCalendar.SelectedDate is valid
                    if (dtmLoanRepaymentDetailsEndDate != null)
                    {
                        EndDateCalendar.SelectedDate = dtmLoanRepaymentDetailsEndDate;
                        if (EndDateCalendar.SelectedDate.Year != 1)
                        {
                            lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
                        }
                        else
                        {
                            lblEndDate.Text = "";
                        } // if (EndDateCalendar.SelectedDate.Year)
                    }
                    else
                    {
                        //if ((sender == null) && (e == null))
                        //{
                        //    // return;
                        //} // if(sender)
                    } // if (EndDateCalendar.SelectedDate)
                }
                else
                {
                    //if ((sender == null) && (e == null))
                    //{
                    //    // return;
                    //} // if(sender)
                } // if (Session["LoanRepaymentDetailsEndDate"])
            } // if (dtmTempEndDate.Year)


            DateTime dtmStartDate = StartDateCalendar.SelectedDate;
            DateTime dtmEndDate = EndDateCalendar.SelectedDate;


            int intDateTimeComparisonResult = DateTime.Compare(dtmStartDate, dtmEndDate);

            // string relationship = "";

            if (intDateTimeComparisonResult < 0)
            {
                // relationship = "is earlier than";
            }
            else if (intDateTimeComparisonResult == 0)
            {
                // relationship = "is the same time as";
            }
            else
            {
                // relationship = "is later than";
                Lbl_err.Text = "Error: Your Loan Repayment Set Up Start Date " + dtmStartDate.ToString() + " is later than End Date " + dtmEndDate.ToString() + ".";
                PanelErrorResult.Visible = true;

                return;
            } // if (intDateTimeComparisonResult)


            List<Loan> loanList = new List<Loan>();
            LoanDetailsDAO loanDetailsDAO = new LoanDetailsDAO();

            loanList = loanDetailsDAO.ReadDailyTotalLoanRepaymentAmountByLoanId(lblBizLoanId.Text, dtmStartDate, dtmEndDate);

            double dummy_1 = 0;


            // DailyRepaymentGraphChart.Series.Add("DailyRepaymentGraphSeries1");
            Series series1 = DailyRepaymentGraphChart.Series["DailyRepaymentGraphSeries1"];


            // DailyRepaymentGraphChart.Legends["DailyRepaymentGraphLegend"].Docking = Docking.Bottom;

            if (loanList != null)
            {
                int rec_cnt = loanList.Count;
                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        int intTotalRepaymentAmountCount = loanList[i].loan_totalRepaymentAmountCount;
                        double dblTotalAmountRepaid = loanList[i].loan_totalAmountRepaid;

                        string strRepaymentDate = "Repayment Date";
                        DateTime dtmRepaymentDate = loanList[i].loan_repaymentDate;

                        // Populate Column Chart                  
                        series1.ChartType = SeriesChartType.Column;
                        series1.Points.AddXY(dtmRepaymentDate, dblTotalAmountRepaid);
                        series1.ChartArea = "DailyRepaymentGraphSeriesChartArea";

                        DailyRepaymentGraphChart.Visible = true;
                    } // for (i)
                } // if(rec_cnt)
            }
            else
            {
                DailyRepaymentGraphChart.Visible = false;

                Lbl_err.Text = "Error: No " + LOAN_ID  + " Loan Repayment information has been found.";
                PanelErrorResult.Visible = true;
            } // if(loanList)

        } // DailyStackedGraphRepaymentPercentage()


        protected void BtnSubmitDateRange_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            DailyStackedGraphRepaymentPercentage();
        } // BtnSubmitDateRange_Click()


        protected void StartDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year != 1) && (EndDateCalendar.SelectedDate.Year == 1))
            {
                EndDateCalendar.SelectedDate = StartDateCalendar.SelectedDate.AddDays(30);

                // Save EndDateCalendar.SelectedDate in Session["LoanRepaymentDetailsEndDate"]
                Session["LoanRepaymentDetailsEndDate"] = EndDateCalendar.SelectedDate;

                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            } // if (EndDateCalendar.SelectedDate.Year)

            DailyStackedGraphRepaymentPercentage();
        } // StartDateCalendar_SelectionChanged()


        protected void EndDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year == 1) && (EndDateCalendar.SelectedDate.Year != 1))
            {
                StartDateCalendar.SelectedDate = EndDateCalendar.SelectedDate.AddDays(-30);

                // Save StartDateCalendar.SelectedDate in Session["LoanRepaymentDetailsStartDate"]
                Session["LoanRepaymentDetailsStartDate"] = StartDateCalendar.SelectedDate;

                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            } // if (StartDateCalendar.SelectedDate.Year)

            DailyStackedGraphRepaymentPercentage();
        } // EndDateCalendar_SelectionChanged()


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


        protected void setLoanRepaymentSummaryGridView_Loan_Id(string BUSI_ID)
        {
            string LOAN_ID = "";

            //    Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanApplicationDAO updateLoanApplicationDAO = new LoanApplicationDAO();

            updateLoanObj = updateLoanApplicationDAO.UpdateLoanRepaymentStatusByBusiId(BUSI_ID);

            // Check DB if there is any existing outstanding loan
            List<Loan> loanList = new List<Loan>();
            LoanSummaryDAO loanSummaryDAO = new LoanSummaryDAO();

            loanList = loanSummaryDAO.CheckLoanRepaymentSummaryByBusiId(BUSI_ID);

            if (loanList != null)
            {
                int rec_cnt = loanList.Count;

                if (rec_cnt > 0)
                {
                    int i = rec_cnt - 1;

                    LOAN_ID = loanList[i].loan_id;
                } // if(rec_cnt)
            } // if(loanList)

            // Check whether the LOAN_ID is valid
            if (LOAN_ID != "")
            {
                // Assign the ID to the session variable, PpBizLoanRepaymentDetails.aspx will pick up from PageLoad
                Session["LoanRepaymentSummaryGridView_Loan_Id"] = LOAN_ID;
            } // if (LOAN_ID)
        } // setLoanRepaymentSummaryGridView_Loan_Id()

        protected void StartDateCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DailyRepaymentGraphChart.Visible = false;
        } // StartDateCalendar_VisibleMonthChanged()

        protected void EndDateCalendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            DailyRepaymentGraphChart.Visible = false;
        } // EndDateCalendar_VisibleMonthChanged()
    } // PpBizLoanRepaymentDetails
} // PrestoPay