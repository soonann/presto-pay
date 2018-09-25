using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using PrestoPay.Entity.DB_Entities;
using PrestoPay.Entity;

namespace PrestoPay
{
    public partial class CategorisePrestoPayTransactionIntoBudgetCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        private void populateTransactionByDate()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            DataTable dt = new DataTable();
            dt.Columns.Add("Transaction Id");
            dt.Columns.Add("Transaction Amount");
            dt.Columns.Add("Transaction Description");
            dt.Columns.Add("Transaction Type");
            dt.Columns.Add("Transaction From");
            dt.Columns.Add("Transaction To");
            dt.Columns.Add("Transaction Date");

            dt.Columns.Add("Budget Category");
            dt.Columns.Add("Budget SubCategory");

            // Check whether Session["UserEmail"] is valid
            if (Session["UserEmail"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (ACC_EMAIL)

            string ACC_EMAIL = (string)Session["UserEmail"];

            // Check whether ACC_EMAIL is valid
            if (ACC_EMAIL == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (ACC_EMAIL)

            DateTime dtmStartDate = StartDateCalendar.SelectedDate;
            if (dtmStartDate.Year == 1)
            {
                Lbl_err.Text = "Error: Please select a valid Transaction Start Date.";
                PanelErrorResult.Visible = true;

                TransactionTableGridView.DataSource = dt;
                TransactionTableGridView.AutoGenerateColumns = false;
                TransactionTableGridView.DataBind();

                return;
            } // if (dtmStartDate.Year)

            DateTime dtmEndDate = EndDateCalendar.SelectedDate;
            if (dtmEndDate.Year == 1)
            {
                Lbl_err.Text = "Error: Please select a valid Transaction End Date.";
                PanelErrorResult.Visible = true;

                TransactionTableGridView.DataSource = dt;
                TransactionTableGridView.AutoGenerateColumns = false;
                TransactionTableGridView.DataBind();

                return;
            } // if (dtmEndDate.Year)

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
                Lbl_err.Text = "Error: Your Transaction Start Date " + dtmStartDate.ToString() + " is later than End Date " + dtmEndDate.ToString() + ".";
                PanelErrorResult.Visible = true;

                TransactionTableGridView.DataSource = dt;
                TransactionTableGridView.AutoGenerateColumns = false;
                TransactionTableGridView.DataBind();

                return;
            } // if (intDateTimeComparisonResult)

            List<CategorisedTransaction> transactionList = new List<CategorisedTransaction>();
            BudgetExpenditurePrestopayTransactionDAO budgetExpenditurePrestopayTransactionDAO = new BudgetExpenditurePrestopayTransactionDAO();

            transactionList = budgetExpenditurePrestopayTransactionDAO.RetrieveTransactionsOfUserByDate(ACC_EMAIL, dtmStartDate, dtmEndDate);

            if (transactionList != null)
            {
                int rec_cnt = transactionList.Count;

                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        dt.Rows.Add();

                        dt.Rows[i]["Transaction Id"] = transactionList[i].trans_id;
                        dt.Rows[i]["Transaction Amount"] = Convert.ToDouble(transactionList[i].trans_amt);
                        dt.Rows[i]["Transaction Description"] = transactionList[i].trans_description;
                        dt.Rows[i]["Transaction Type"] = transactionList[i].trans_type;
                        dt.Rows[i]["Transaction From"] = transactionList[i].trans_from;
                        dt.Rows[i]["Transaction To"] = transactionList[i].trans_to;
                        dt.Rows[i]["Transaction Date"] = Convert.ToDateTime(transactionList[i].trans_date);

                        dt.Rows[i]["Budget Category"] = transactionList[i].budgetCategory;
                        dt.Rows[i]["Budget SubCategory"] = transactionList[i].budgetSubCategory;
                    } // for (i)
                }
                else
                {
                    Lbl_err.Text = "Sorry, no Transactions between " + dtmStartDate.ToString() + " and " + dtmEndDate.ToString() + " have been found.";
                    PanelErrorResult.Visible = true;
                } // if(rec_cnt)
            }
            else
            {
                Lbl_err.Text = "Sorry, no Transactions between " + dtmStartDate.ToString() + " and " + dtmEndDate.ToString() + " have been found.";
                PanelErrorResult.Visible = true;
            } // if(transactionList)

            TransactionTableGridView.DataSource = dt;
            TransactionTableGridView.AutoGenerateColumns = false;
            TransactionTableGridView.DataBind();
        } // populateTransactionByDate()

        protected void TransactionTableGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the CATEGORISE command is clicked
            if (command == "CATEGORISE")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid
                string TRANS_ID = row.Cells[0].Text; // the TRANS_ID is stored on the 1st column
                double TRANS_AMT = Convert.ToDouble(row.Cells[1].Text);
                string TRANS_DESCRIPTION = row.Cells[2].Text;
                string TRANS_TYPE = row.Cells[3].Text;
                string TRANS_FROM = row.Cells[4].Text;
                string TRANS_TO = row.Cells[5].Text;
                DateTime TRANS_DATE = Convert.ToDateTime(row.Cells[6].Text);

                string BUDGET_CATEGORY = row.Cells[7].Text;
                string BUDGET_SUBCATEGORY = row.Cells[8].Text;


                // Assign the ID to the session variable, PpBizLoanDetails.aspx will pick up from PageLoad
                Session["PP_Trans_Id"] = TRANS_ID;
                Session["PP_Trans_Amt"] = TRANS_AMT;
                Session["PP_Trans_Description"] = TRANS_DESCRIPTION;
                Session["PP_Trans_Type"] = TRANS_TYPE;
                Session["PP_Trans_From"] = TRANS_FROM;
                Session["PP_Trans_To"] = TRANS_TO;
                Session["PP_Trans_Date"] = TRANS_DATE;

                Session["PP_BUDGET_CATEGORY"] = BUDGET_CATEGORY;
                Session["PP_BUDGET_SUBCATEGORY"] = BUDGET_SUBCATEGORY;
                Response.Redirect("CategorisePrestoPayTransactionIntoBudgetCenterDetails.aspx");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            populateTransactionByDate();
        }

        protected void StartDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year != 1) && (EndDateCalendar.SelectedDate.Year == 1))
            {
                EndDateCalendar.SelectedDate = StartDateCalendar.SelectedDate.AddDays(30);

                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            } // if (EndDateCalendar.SelectedDate.Year)

            btnSubmit_Click(null, null);
        } // StartDateCalendar_SelectionChanged()


        protected void EndDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year == 1) && (EndDateCalendar.SelectedDate.Year != 1))
            {
                StartDateCalendar.SelectedDate = EndDateCalendar.SelectedDate.AddDays(-30);

                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            } // if (StartDateCalendar.SelectedDate.Year)

            btnSubmit_Click(null, null);
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

    }
}