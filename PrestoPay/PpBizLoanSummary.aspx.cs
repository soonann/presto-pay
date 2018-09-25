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

    public partial class PpBizLoanSummary : System.Web.UI.Page
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

            btnGetLoanSummaryByBizId_Click(null, null);
        } // BtnBizIDSubmit_Click()


        private void GetLoanSummaryByBusiId()
        {
            Lbl_err.Text = String.Empty;
            Session["SSbizID"] = tbBizID.Text.ToString();

            //    Update DB if there is an existing outstanding loan
            Loan updateLoanObj = new Loan();
            LoanApplicationDAO updateLoanApplicationDAO = new LoanApplicationDAO();

            updateLoanObj = updateLoanApplicationDAO.UpdateLoanRepaymentStatusByBusiId(tbBizID.Text);

            //    Check DB if there is any existing outstanding loan
            List<Loan> loanList = new List<Loan>();
            LoanSummaryDAO loanSummaryDAO = new LoanSummaryDAO();

            loanList = loanSummaryDAO.CheckLoanRepaymentSummaryByBusiId(tbBizID.Text);

            double dummy_1 = 0;


            DataTable dt = new DataTable();
            dt.Columns.Add("Loan Id");
            dt.Columns.Add("Application Date");
            dt.Columns.Add("Application Status");
            dt.Columns.Add("Repayment Rate");
            dt.Columns.Add("Total Amount To Be Repaid");
            dt.Columns.Add("Total Amount Repaid");
            dt.Columns.Add("Repayment Status");

            if (loanList != null)
            {
                int rec_cnt = loanList.Count;

                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        dt.Rows.Add();

                        dt.Rows[i]["Loan Id"] = loanList[i].loan_id;
                        dt.Rows[i]["Application Date"] = Convert.ToDateTime(loanList[i].loan_applicationDate);
                        dt.Rows[i]["Application Status"] = loanList[i].loan_applicationStatus;
                        dt.Rows[i]["Repayment Rate"] = loanList[i].loan_repaymentRate;
                        dt.Rows[i]["Total Amount To Be Repaid"] = Convert.ToDouble(loanList[i].loan_totalAmountToBeRepaid);
                        dt.Rows[i]["Total Amount Repaid"] = Convert.ToDouble(loanList[i].loan_totalAmountRepaid);
                        dt.Rows[i]["Repayment Status"] = loanList[i].loan_repaymentStatus;
                    } // for (i)
                } // if(rec_cnt)
            } // if(loanList)

            LoanRepaymentSummaryGridView.DataSource = dt;
            LoanRepaymentSummaryGridView.AutoGenerateColumns = false;
            LoanRepaymentSummaryGridView.DataBind();
    } // GetLoanSummaryByBusiId()


        protected void btnGetLoanSummaryByBizId_Click(object sender, EventArgs e)
        {
            GetLoanSummaryByBusiId();
        }



        protected void LoanRepaymentSummaryGridView_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "SELECT_LOAN_DETAILS")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid

                string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Assign the ID to the session variable, PpBizLoanDetails.aspx will pick up from PageLoad
                Session["LoanRepaymentSummaryGridView_Loan_Id"] = LOAN_ID;
                Response.Redirect("PpBizLoanDetails.aspx");
            }
            else if (command == "SELECT_LOAN_REPAYMENT")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid

                string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Assign the ID to the session variable, PpBizLoanDetails.aspx will pick up from PageLoad
                Session["LoanRepaymentSummaryGridView_Loan_Id"] = LOAN_ID;
                Response.Redirect("PpBizLoanRepaymentDetails.aspx");
            }

        }

        //protected void LoanRepaymentSummaryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    // this segment changes colour of a row based on status value = PENDING
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string statusS = e.Row.Cells[3].Text;
        //        if (statusS == "PENDING")
        //            e.Row.BackColor = Color.FromName("#82E0AA");
        //    }
        //}


        //protected void LoanRepaymentSummaryGridView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    GridViewRow row = LoanRepaymentSummaryGridView.SelectedRow;
        //    Response.Redirect("PpBizLoanDetails.aspx");

        //     string tempBizID = tbBizID.Text;
        //     Session["SSbizID"] = tempBizID;



        //    LoanRepaymentSummaryGridView.Rows[e.RowIndex].Cells[0].Value.ToString();



        //    int selectedCellCount = LoanRepaymentSummaryGridView.Rows.GetRowCount(DataGridViewElementStates.Selected);


        //}

    } //PpBizLoanSummary
} // PrestoPay
