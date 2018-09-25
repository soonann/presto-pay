using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;

namespace PrestoPay
{
    
    public partial class PpBizLoanApplicationPage : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BtnBizIDSubmit_Click(null, null);
            } // if (!IsPostBack)

        } // Page_Load()


        protected void BtnBizIDSubmit_Click(object sender, EventArgs e)
        {
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


            tbBizID.Text = BUSI_ID;

            if (String.IsNullOrWhiteSpace(tbBizID.Text))
            {
                Lbl_err.Text = "Error: Business ID record not found!";
                PanelErrorResult.Visible = true;

                return;
            } // if (String.IsNullOrWhiteSpace(tbNameOfTransaction.Text))


            // BusinessDAO bsDao = new BusinessDAO();
            bsDao = new BusinessDAO();

            Business bsObj = new Business();

            bsObj = bsDao.GetBusinessCompanyNameByBusiId(tbBizID.Text);
            if (bsObj == null)
            {
                Lbl_err.Text = "Error: Business Company Name record not found!";
                PanelErrorResult.Visible = true;

                lblBizCompanyName.Text = String.Empty;
            }
            else
            {
                lblBizCompanyName.Text = bsObj.busi_companyName;
            } // if (bsObj)

            btnGetLoanLimitsAndStatusByBizId_Click(null, null);
        } // BtnBizIDSubmit_Click()


        protected void btnGetLoanLimitsAndStatusByBizId_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            GetLoanLimitsByBusiId();
            GetLoanStatusByBusiId();
        }

        private void GetLoanLimitsByBusiId()
        {
            double dblAverageAnnualSales = 0.0;

            double MaximumLoanAmountAllowed = 0.0;
            double MaximumLoanPercentageAllowed = 25.0;
            double TotalLoanAmountApproved = 0.0;
            double TotalAmountRepaid = 0.0;
            double LoanAmountAvailable = 0.0;

            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;
            LoanLimitsGridView1.Visible = false;

            BusinessSales bsObj = new BusinessSales();
            BusinessSalesDAO bsDao = new BusinessSalesDAO();

            bsObj = bsDao.ReadBusinessSalesByBusiId(tbBizID.Text);
            if (bsObj == null)
            {
                Lbl_err.Text = "Business record not found!";
                PanelErrorResult.Visible = true;

                dblAverageAnnualSales = 0.0;
            }
            else
            {
                PanelErrorResult.Visible = false;
                int tempSalesAmountCount = bsObj.bs_salesAmountCount;
                double tempTotalSalesAmount = bsObj.bs_totalSalesAmount;

                if (tempSalesAmountCount != 0)
                {
                    dblAverageAnnualSales = Math.Round((tempTotalSalesAmount / tempSalesAmountCount), 2);
                }
                else
                {
                    dblAverageAnnualSales = 0.0;
                }
            } // if (bsObj)


            MaximumLoanAmountAllowed = Math.Round(dblAverageAnnualSales * (MaximumLoanPercentageAllowed / 100.0), 2);
            MaximumLoanPercentageAllowed = 25.0;
            TotalLoanAmountApproved = 0.0;
            LoanAmountAvailable = 0.0;

            Session["SSbizID"] = tbBizID.Text.ToString();
            Session["SSaverageSalesAmount"] = Math.Round(dblAverageAnnualSales, 2);
            Session["MaximumLoanAmountAllowed"] = Math.Round(dblAverageAnnualSales * (MaximumLoanPercentageAllowed / 100.0), 2);
            Session["TotalLoanAmountApproved"] = 0.0;
            Session["LoanAmountAvailable"] = 0.0;


            //    Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanApplicationDAO updateLoanApplicationDAO = new LoanApplicationDAO();

            updateLoanObj = updateLoanApplicationDAO.UpdateLoanRepaymentStatusByBusiId(tbBizID.Text);



            //    Check DB if there is any existing outstanding loan
            Loan loanObj = new Loan();
            LoanApplicationDAO loanApplicationDAO = new LoanApplicationDAO();

            loanObj = loanApplicationDAO.CheckLoanRepaymentLimitsByBusiId(tbBizID.Text);

            //double dummy_1 = 0;

            if(loanObj != null)
            {
                TotalLoanAmountApproved = loanObj.loan_totalAmountToBeRepaid;
                TotalAmountRepaid = loanObj.loan_totalAmountRepaid;
            } // if(loanObj)

            MaximumLoanAmountAllowed = Math.Round(dblAverageAnnualSales * (MaximumLoanPercentageAllowed / 100.0), 2);
            LoanAmountAvailable = Math.Round(MaximumLoanAmountAllowed - TotalLoanAmountApproved + TotalAmountRepaid, 2);

            // Session["SSbizID"] = tbBizID.Text.ToString();
            Session["SSaverageSalesAmount"] = Math.Round(dblAverageAnnualSales, 2);
            Session["MaximumLoanAmountAllowed"] = Math.Round(MaximumLoanAmountAllowed, 2);
            Session["TotalLoanAmountApproved"] = Math.Round(TotalLoanAmountApproved, 2);
            Session["LoanAmountAvailable"] = Math.Round(LoanAmountAvailable, 2);

               
            double[] arrlistLoanRepaymentLimitsTable = { Math.Round(dblAverageAnnualSales, 2), Math.Round(MaximumLoanAmountAllowed, 2), Math.Round(TotalLoanAmountApproved, 2), Math.Round(LoanAmountAvailable, 2)};


            DataTable dt = new DataTable();
            dt.Columns.Add("Average Annual Sales");
            dt.Columns.Add("Maximum Loan Amount Allowed");
            dt.Columns.Add("Total Loan Amount Approved");
            dt.Columns.Add("Loan Amount Available");

            dt.Rows.Add();

            dt.Rows[0]["Average Annual Sales"] = arrlistLoanRepaymentLimitsTable[0];
            dt.Rows[0]["Maximum Loan Amount Allowed"] = arrlistLoanRepaymentLimitsTable[1];
            dt.Rows[0]["Total Loan Amount Approved"] = arrlistLoanRepaymentLimitsTable[2];
            dt.Rows[0]["Loan Amount Available"] = arrlistLoanRepaymentLimitsTable[3];

            LoanLimitsGridView1.DataSource = dt;
            LoanLimitsGridView1.AutoGenerateColumns = false;
            LoanLimitsGridView1.DataBind();
            LoanLimitsGridView1.Visible = true;
        } // GetLoanLimitsByBusiId()


        private void GetLoanStatusByBusiId()
        {
            PanelErrorResult.Visible = false;
            Lbl_err.Text = String.Empty;


            Session["SSbizID"] = tbBizID.Text.ToString();


            LoanRepaymentStatusGridView.Visible = false;

            // Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanApplicationDAO updateLoanApplicationDAO = new LoanApplicationDAO();

            updateLoanObj = updateLoanApplicationDAO.UpdateLoanRepaymentStatusByBusiId(tbBizID.Text);



            // Check DB if there is any existing outstanding loan
            List<Loan> loanList = new List<Loan>();
            LoanApplicationDAO loanApplicationDAO = new LoanApplicationDAO();

            loanList = loanApplicationDAO.CheckLoanRepaymentStatusByBusiId(tbBizID.Text);

            //double dummy_1 = 0;


            int PendingCount = 0;
            int FullCount = 0;
            int OutstandingCount = 0;
            int RejectedCount = 0;
            int CancelledCount = 0;
            int OthersCount = 0;

            if (loanList != null)
            {
                int rec_cnt = loanList.Count;

                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        if (loanList[i].loan_repaymentStatus == "PENDING")
                        {
                            PendingCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "FULL")
                        {
                            FullCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "OUTSTANDING")
                        {
                            OutstandingCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "REJECTED")
                        {
                            RejectedCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "CANCELLED")
                        {
                            CancelledCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "OTHERS")
                        {
                            OthersCount += loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else
                        {
                            OthersCount += loanList[i].loan_totalRepaymentStatusCount;
                        } // if (loanList[i].loan_repaymentStatus)
                    } // for (i)
                } // if(rec_cnt)
            } // if(loanList)


            int[] arrlistLoanRepaymentStatusTable = { PendingCount, FullCount, OutstandingCount, RejectedCount, CancelledCount, OthersCount };


            DataTable dt = new DataTable();
            dt.Columns.Add("PENDING");
            dt.Columns.Add("FULL");
            dt.Columns.Add("OUTSTANDING");
            dt.Columns.Add("REJECTED");
            dt.Columns.Add("CANCELLED");
            dt.Columns.Add("OTHERS");

            dt.Rows.Add();

            dt.Rows[0]["PENDING"] = arrlistLoanRepaymentStatusTable[0];
            dt.Rows[0]["FULL"] = arrlistLoanRepaymentStatusTable[1];
            dt.Rows[0]["OUTSTANDING"] = arrlistLoanRepaymentStatusTable[2];
            dt.Rows[0]["REJECTED"] = arrlistLoanRepaymentStatusTable[3];
            dt.Rows[0]["CANCELLED"] = arrlistLoanRepaymentStatusTable[4];
            dt.Rows[0]["OTHERS"] = arrlistLoanRepaymentStatusTable[5];

            LoanRepaymentStatusGridView.DataSource = dt;
            LoanRepaymentStatusGridView.AutoGenerateColumns = false;
            LoanRepaymentStatusGridView.DataBind();
            LoanRepaymentStatusGridView.Visible = true;
        } // GetLoanStatusByBusiId()

        protected void btnCalculateLoanAmount_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;
            GridViewLoanCalculation.Visible = false;

            if (tbDesiredLoanAmount.Text != null)
            {
                // put DLA in temp variable then store in session variable
                double dblDesiredLoanAmount = 0.0;
                string strDblDesiredLoanAmount = tbDesiredLoanAmount.Text;

                // get the length of the current string
                int intLength = strDblDesiredLoanAmount.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strDblDesiredLoanAmount, out dblDesiredLoanAmount);
                    if (result == true)
                    {
                        dblDesiredLoanAmount = double.Parse(strDblDesiredLoanAmount);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strDblDesiredLoanAmount = strDblDesiredLoanAmount.Substring(1);

                        // get the length of the new string
                        intLength = strDblDesiredLoanAmount.Length;
                    } // if (result)
                } // while (intLength)

                // Display the valid Desired Loan Amount
                tbDesiredLoanAmount.Text = dblDesiredLoanAmount.ToString();

                Session["SSdesiredLoanAmount"] = dblDesiredLoanAmount;
                double SessiondesiredLoanAmount = (double)Session["SSdesiredLoanAmount"];


                // Put session avg sales amt in variable 
                double SessionAverageSalesAmount = (double)Session["SSaverageSalesAmount"];

                // Find out max loan amt the user can borrow 
                double LoanAmountAvailable = (double)Session["LoanAmountAvailable"];


                // Check whether SessiondesiredLoanAmount is valid
                if (SessiondesiredLoanAmount <= 0.0)
                {
                    Lbl_err.Text = "Please enter a Desired Loan Amount that is greater than $0 and not more than $" + LoanAmountAvailable;
                    PanelErrorResult.Visible = true;
                }
                else if (SessiondesiredLoanAmount > LoanAmountAvailable)
                {
                    Lbl_err.Text = "Please enter a Desired Loan Amount that is not more than $" + LoanAmountAvailable;
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Step 1 of fixedFee: find out DLA is how much % in EAS
                    // 100/EAS x DLA = fixedFeePart1
                    double fixedFeePart1 = ((100 / SessionAverageSalesAmount) * SessiondesiredLoanAmount);

                    // Step 2 of fixedFee:
                    // repayment% x DLA = fixedFeePart2
                    // 30% - 0.070 x DLA
                    double fixedFeePart2_30Percent = (0.070 * SessiondesiredLoanAmount);
                    // 25% - 0.075 x DLA
                    double fixedFeePart2_25Percent = (0.075 * SessiondesiredLoanAmount);
                    // 20% - 0.080 x DLA
                    double fixedFeePart2_20Percent = (0.080 * SessiondesiredLoanAmount);
                    // 15% - 0.085 x DLA
                    double fixedFeePart2_15Percent = (0.085 * SessiondesiredLoanAmount);
                    // 10% - 0.090 x DLA
                    double fixedFeePart2_10Percent = (0.090 * SessiondesiredLoanAmount);

                    // total fixed fee : fixedFeePart1 + fixedFeePart2
                    // row 0 : (100/EAS x DLA) + (0.070 x DLA)
                    double totalFixedFee_30Percent = fixedFeePart2_30Percent + fixedFeePart1;
                    // row 1 : (100/EAS x DLA) + (0.075 x DLA)
                    double totalFixedFee_25Percent = fixedFeePart2_25Percent + fixedFeePart1;
                    // row 2 : (100/EAS x DLA) + (0.080 x DLA)
                    double totalFixedFee_20Percent = fixedFeePart2_20Percent + fixedFeePart1;
                    // row 3 : (100/EAS x DLA) + (0.085 x DLA)
                    double totalFixedFee_15Percent = fixedFeePart2_15Percent + fixedFeePart1;
                    // row 4 : (100/EAS x DLA) + (0.090 x DLA)
                    double totalFixedFee_10Percent = fixedFeePart2_10Percent + fixedFeePart1;


                    // totalRepayAmount
                    // row 0 : (100/EAS x DLA) + (0.070 x DLA)+ DLA
                    double totalRepayAmount_30Percent = totalFixedFee_30Percent + SessiondesiredLoanAmount;
                    // row 1 : (100/EAS x DLA) + (0.075 x DLA)+ DLA
                    double totalRepayAmount_25Percent = totalFixedFee_25Percent + SessiondesiredLoanAmount;
                    // row 2 : (100/EAS x DLA) + (0.080 x DLA)+ DLA
                    double totalRepayAmount_20Percent = totalFixedFee_20Percent + SessiondesiredLoanAmount;
                    // row 3 : (100/EAS x DLA) + (0.085 x DLA)+ DLA
                    double totalRepayAmount_15Percent = totalFixedFee_15Percent + SessiondesiredLoanAmount;
                    // row 4 : (100/EAS x DLA) + (0.090 x DLA)+ DLA
                    double totalRepayAmount_10Percent = totalFixedFee_10Percent + SessiondesiredLoanAmount;


                    double[,] arrlistLoanCalculationTable = 
                    {
                        {30,70,Math.Round(totalFixedFee_30Percent, 2),Math.Round(totalRepayAmount_30Percent, 2)},
                        {25,75,Math.Round(totalFixedFee_25Percent, 2),Math.Round(totalRepayAmount_25Percent, 2)},
                        {20,80,Math.Round(totalFixedFee_20Percent, 2),Math.Round(totalRepayAmount_20Percent, 2)},
                        {15,85,Math.Round(totalFixedFee_15Percent, 2),Math.Round(totalRepayAmount_15Percent, 2)},
                        {10,90,Math.Round(totalFixedFee_10Percent, 2),Math.Round(totalRepayAmount_10Percent, 2)}
                    };

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Repayment percentage (%)");
                    dt.Columns.Add("Percentage you keep (%)");
                    dt.Columns.Add("One-time fixed fee ($)");
                    dt.Columns.Add("Total to be repaid ($)");

                    for (int i = 0; i < arrlistLoanCalculationTable.GetLength(0); i++)
                    {
                        dt.Rows.Add();

                        string dummyString0  = arrlistLoanCalculationTable[i, 0].ToString();
                        dt.Rows[i]["Repayment percentage (%)"] = arrlistLoanCalculationTable[i, 0].ToString();

                        string dummyString1 = arrlistLoanCalculationTable[i, 1].ToString();
                        dt.Rows[i]["Percentage you keep (%)"] = arrlistLoanCalculationTable[i, 1].ToString();

                        string dummyString2 = arrlistLoanCalculationTable[i, 2].ToString();
                        dt.Rows[i]["One-time fixed fee ($)"] = arrlistLoanCalculationTable[i, 2].ToString();

                        string dummyString3 = arrlistLoanCalculationTable[i, 3].ToString();
                        dt.Rows[i]["Total to be repaid ($)"] = arrlistLoanCalculationTable[i, 3].ToString();
                    } // for (int i)

                    GridViewLoanCalculation.DataSource = dt;
                    GridViewLoanCalculation.AutoGenerateColumns = false;
                    GridViewLoanCalculation.DataBind();
                    GridViewLoanCalculation.Visible = true;

                } // if (SessiondesiredLoanAmount)
            }
            else
            {
                Lbl_err.Text = "Please enter a valid Desired Loan Amount ";
                PanelErrorResult.Visible = true;
            } // if (tbDesiredLoanAmount)
        } // btnCalculateLoanAmount_Click()


        protected void GridViewLoanCalculation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            //    when user click on SELECT button
            // this.Master.ShowAlertPopout("Loan Confirmation", "Do you wish to submit your Loan Application?", "confirm");
            submitLoanCalculation_WhenSelectedIndexChanged();
        } // GridViewLoanCalculation_SelectedIndexChanged()


        protected void Popout_Alert_Yes_Click(object sender, EventArgs e)
        {
            submitLoanCalculation_WhenSelectedIndexChanged();

            this.Master.HideAlertPopout();
        } // Popout_Alert_Yes_Click()


        protected void submitLoanCalculation_WhenSelectedIndexChanged()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // store BizID, EAS, DLA, Repayment percentage,Percentage you keep,One-time fixed fee,Total to be repaid as session variable

            // put DLA in temp variable then store in session variable
            string tempBizID = tbBizID.Text;
            Session["SSbizID"] = tempBizID;

            // put DLA in temp variable then store in session variable
            double tempDblDesiredLoanAmount = double.Parse(tbDesiredLoanAmount.Text);
            Session["SSdesiredLoanAmount"] = tempDblDesiredLoanAmount;

            // put EAS in temp variable then store in session variable
            //double tempDblAverageAnnualSales = double.Parse(lbAverageAnnualSales.Text);
            //Session["SSAverageAnnualSales"] = tempDblAverageAnnualSales;



            // Get the currently selected row using the SelectedRow property.
            GridViewRow row = GridViewLoanCalculation.SelectedRow;

            double Repaymentpercentage = double.Parse(row.Cells[0].Text);
            Session["SSrepaymentpercentage"] = Repaymentpercentage;

            double PercentageYouKeep = double.Parse(row.Cells[1].Text);
            Session["SSpercentageYouKeep"] = PercentageYouKeep;

            double OneTimeFixedFee = double.Parse(row.Cells[2].Text);
            Session["SSoneTimeFixedFee"] = OneTimeFixedFee;

            double TotalToBeRepaid = double.Parse(row.Cells[3].Text);
            Session["SStotalToBeRepaid"] = TotalToBeRepaid;


            //    Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanApplicationDAO updateLoanApplicationDAO = new LoanApplicationDAO();

            updateLoanObj = updateLoanApplicationDAO.UpdateLoanRepaymentStatusByBusiId(tbBizID.Text);


            //    Check DB if there is any existing outstanding loan
            List<Loan> loanList = new List<Loan>();
            LoanApplicationDAO loanApplicationDAO = new LoanApplicationDAO();

            loanList = loanApplicationDAO.CheckLoanRepaymentStatusByBusiId(tbBizID.Text);

            //double dummy_2 = 0;


            int PendingCount = 0;
            int FullCount = 0;
            int OutstandingCount = 0;
            int RejectedCount = 0;
            int CancelledCount = 0;
            int OthersCount = 0;

            if (loanList != null)
            {
                int rec_cnt = loanList.Count;
                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        if (loanList[i].loan_repaymentStatus == "PENDING")
                        {
                            PendingCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "FULL")
                        {
                            FullCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "OUTSTANDING")
                        {
                            OutstandingCount = loanList[i].loan_totalRepaymentStatusCount;

                            //if (OutstandingCount == 1)
                            //{
                            //    Lbl_err.Text = "Sorry, you have to repay back your loan before requesting for a new loan";
                            //    PanelErrorResult.Visible = true;
                            //}
                            //else if (OutstandingCount > 1)
                            //{
                            //    Lbl_err.Text = "Sorry, you have to repay back all of your loans before requesting for a new loan";
                            //    PanelErrorResult.Visible = true;
                            //} // if (OutstandingCount)
                            //return;
                        }
                        else if (loanList[i].loan_repaymentStatus == "REJECTED")
                        {
                            RejectedCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "CANCELLED")
                        {
                            CancelledCount = loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else if (loanList[i].loan_repaymentStatus == "OTHERS")
                        {
                            OthersCount += loanList[i].loan_totalRepaymentStatusCount;
                        }
                        else
                        {
                            OthersCount += loanList[i].loan_totalRepaymentStatusCount;
                        } // if (loanList[i].loan_repaymentStatus)
                    } // for (i)
                } // if(rec_cnt)
            } // if(loanList)


            string BUSI_ID = tempBizID;

            // Check whether (double)Session["SSaverageSalesAmount"] is valid
            if ((double)Session["SSaverageSalesAmount"] == 0)
            {
                Response.Redirect("~/Login.aspx");
            } // if (LOAN_SALES_REV_AT_APP)

            double LOAN_SALES_REV_AT_APP = (double)Session["SSaverageSalesAmount"];

            // Check whether LOAN_SALES_REV_AT_APP is valid
            if (LOAN_SALES_REV_AT_APP == 0)
            {
                Response.Redirect("~/Login.aspx");
            } // if (LOAN_SALES_REV_AT_APP)


            double LOAN_AMT = tempDblDesiredLoanAmount;
            DateTime LOAN_APPLICATION_DATE = DateTime.Now;
            double LOAN_REPAYMENT_RATE = Repaymentpercentage;
            double LOAN_PERCENTAGE_KEEP = PercentageYouKeep;
            double LOAN_ONE_TIME_FIXED_FEE = OneTimeFixedFee;
            double LOAN_TOTAL_AMOUNT_TO_BE_REPAID = TotalToBeRepaid;
            string LOAN_APPLICATION_STATUS = "PENDING";
            string LOAN_REASON_FOR_APPLICATION_STATUS = "";
            DateTime LOAN_APPROVAL_DATE = DateTime.Now;
            string LOAN_APPROVED_BY_ADMIN_USER_NAME = "";
            int LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT = 0;
            double LOAN_TOTAL_AMOUNT_REPAID = 0.0;
            int LOAN_TOTAL_REPAYMENT_STATUS_COUNT = 0;
            DateTime LOAN_REPAYMENT_DATE = DateTime.Now;
            string LOAN_REPAYMENT_STATUS = "PENDING";
            string LOAN_REASON_FOR_REPAYMENT_STATUS = "";
            double LOAN_TOTAL_TRANSACTION_AMOUNT = 0.0;
            string LOAN_SUCCESS_MSG = "";
            string LOAN_ERROR_MSG = "";


            LoanApplicationDAO loanIdDao = new LoanApplicationDAO();

            string LOAN_ID = loanIdDao.InsertLoanApplicationIntoLoanTableByBusiId(BUSI_ID,
                                                                                 LOAN_SALES_REV_AT_APP,
                                                                                 LOAN_AMT,
                                                                                 LOAN_APPLICATION_DATE,
                                                                                 LOAN_REPAYMENT_RATE,
                                                                                 LOAN_PERCENTAGE_KEEP,
                                                                                 LOAN_ONE_TIME_FIXED_FEE,
                                                                                 LOAN_TOTAL_AMOUNT_TO_BE_REPAID,
                                                                                 LOAN_APPLICATION_STATUS,
                                                                                 LOAN_REASON_FOR_APPLICATION_STATUS,
                                                                                 LOAN_APPROVAL_DATE,
                                                                                 LOAN_APPROVED_BY_ADMIN_USER_NAME,
                                                                                 LOAN_TOTAL_REPAYMENT_AMOUNT_COUNT,
                                                                                 LOAN_TOTAL_AMOUNT_REPAID,
                                                                                 LOAN_TOTAL_REPAYMENT_STATUS_COUNT,
                                                                                 LOAN_REPAYMENT_DATE,
                                                                                 LOAN_REPAYMENT_STATUS,
                                                                                 LOAN_REASON_FOR_REPAYMENT_STATUS,
                                                                                 LOAN_TOTAL_TRANSACTION_AMOUNT,
                                                                                 LOAN_SUCCESS_MSG,
                                                                                 LOAN_ERROR_MSG);


            if (LOAN_ID != null)
            {
                // Display the updated loan status
                GetLoanLimitsByBusiId();
                GetLoanStatusByBusiId();

                Lbl_err.Text = "Your loan application " + LOAN_ID + " has been submitted successfully!";
                PanelErrorResult.Visible = true;              
            }
            else
            {
                Lbl_err.Text = "Sorry, an error occurred while saving your loan application. Please inform System Administrator.";
                PanelErrorResult.Visible = true;
            } // if (LOAN_ID)
        } // submitLoanCalculation_WhenSelectedIndexChanged()

        protected void BtnViewLoanSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PpBizLoanSummary.aspx");
        } // BtnViewLoanSummary_Click()

        protected void Popout_Alert_No_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }
    }
}