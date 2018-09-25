using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrestoPay.Entity.DB_Entities;

namespace PrestoPay
{
    public partial class BudgetSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            btnShowBudgetSummary.Visible = false;

            if (!IsPostBack)
            {
                btnShowBudgetSummary_Click(null, null);
            } // if(!IsPostBack)
        } // Page_Load()


        protected void btnShowBudgetSummary_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;


            DateTime dtmTempStartDate = StartDateCalendar.SelectedDate;
            if (dtmTempStartDate.Year == 1)
            {
                // Check whether Session["BudgetSummaryStartDate"] is valid
                if (Session["BudgetSummaryStartDate"] != null)
                {
                    DateTime dtmBudgetSummaryStartDate = (DateTime)Session["BudgetSummaryStartDate"];

                    // Check whether StartDateCalendar.SelectedDate is valid
                    if (dtmBudgetSummaryStartDate != null)
                    {
                        StartDateCalendar.SelectedDate = dtmBudgetSummaryStartDate;
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
                        if ((sender == null) && (e == null))
                        {
                            // return;
                        } // if(sender)
                    } // if (StartDateCalendar.SelectedDate)
                }
                else
                {
                    if ((sender == null) && (e == null))
                    {
                        // return;
                    } // if(sender)
                } // if (Session["BudgetSummaryStartDate"])
            } // if (dtmTempStartDate.Year)


            DateTime dtmTempEndDate = EndDateCalendar.SelectedDate;
            if (dtmTempEndDate.Year == 1)
            {
                // Check whether Session["BudgetSummaryEndDate"] is valid
                if (Session["BudgetSummaryEndDate"] != null)
                {
                    DateTime dtmBudgetSummaryEndDate = (DateTime)Session["BudgetSummaryEndDate"];

                    // Check whether EndDateCalendar.SelectedDate is valid
                    if (dtmBudgetSummaryEndDate != null)
                    {
                        EndDateCalendar.SelectedDate = dtmBudgetSummaryEndDate;
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
                        if ((sender == null) && (e == null))
                        {
                            // return;
                        } // if(sender)
                    } // if (EndDateCalendar.SelectedDate)
                }
                else
                {
                    if ((sender == null) && (e == null))
                    {
                        // return;
                    } // if(sender)
                } // if (Session["BudgetSummaryEndDate"])
            } // if (dtmTempEndDate.Year)


            if ((StartDateCalendar.SelectedDate.Year != 1) && (EndDateCalendar.SelectedDate.Year == 1))
            {
                EndDateCalendar.SelectedDate = StartDateCalendar.SelectedDate.AddDays(30);

                // Save EndDateCalendar.SelectedDate in Session["BudgetSummaryEndDate"]
                Session["BudgetSummaryEndDate"] = EndDateCalendar.SelectedDate;

                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            } // if (EndDateCalendar.SelectedDate.Year)

            if ((StartDateCalendar.SelectedDate.Year == 1) && (EndDateCalendar.SelectedDate.Year != 1))
            {
                StartDateCalendar.SelectedDate = EndDateCalendar.SelectedDate.AddDays(-30);

                // Save StartDateCalendar.SelectedDate in Session["BudgetSummaryStartDate"]
                Session["BudgetSummaryStartDate"] = StartDateCalendar.SelectedDate;

                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            } // if (StartDateCalendar.SelectedDate.Year)


            if (StartDateCalendar.SelectedDate.Year != 1)
            {
                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            }
            else
            {
                lblStartDate.Text = "";
            } // if (StartDateCalendar.SelectedDate.Year)

            // Save StartDateCalendar.SelectedDate in Session["BudgetSummaryStartDate"]
            Session["BudgetSummaryStartDate"] = StartDateCalendar.SelectedDate;

            if (EndDateCalendar.SelectedDate.Year != 1)
            {
                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            }
            else
            {
                lblEndDate.Text = "";
            } // if (EndDateCalendar.SelectedDate.Year)

            // Save EndDateCalendar.SelectedDate in Session["BudgetSummaryEndDate"]
            Session["BudgetSummaryEndDate"] = EndDateCalendar.SelectedDate;


            DateTime dtmStartDate = StartDateCalendar.SelectedDate;
            DateTime dtmEndDate = EndDateCalendar.SelectedDate;

            if ((dtmStartDate.Year != 1) && (dtmEndDate.Year != 1))
            {
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
                    Lbl_err.Text = "Error: Your Budget Summary Start Date " + GetDateString(dtmStartDate.ToString()) + " is later than End Date " + GetDateString(dtmEndDate.ToString()) + ".";
                    PanelErrorResult.Visible = true;

                    return;
                } // if (intDateTimeComparisonResult)
            } // if (dtmStartDate.Year)


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

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByEmail(ACC_EMAIL);

            List<BudgetDashBoard> budgetDashBoardList = new List<BudgetDashBoard>();
            budgetDashBoardList = budgetDashBoardDAO.CheckBudgetDashBoardByEmail(ACC_EMAIL);

            DataTable dt = new DataTable();
            dt.Columns.Add("budget_id");
            dt.Columns.Add("budget_startDate");
            dt.Columns.Add("budget_endDate");

            dt.Columns.Add("budget_incomeAmountAllocated");
            dt.Columns.Add("budget_incomeAmountReceived");

            dt.Columns.Add("budget_fixedCostAmountAllocated");
            dt.Columns.Add("budget_fixedCostAmountSpent");

            dt.Columns.Add("budget_flexSpendingAmountAllocated");
            dt.Columns.Add("budget_flexSpendingAmountSpent");

            dt.Columns.Add("budget_debtRepaymentAmountAllocated");
            dt.Columns.Add("budget_debtRepaymentAmountSpent");

            dt.Columns.Add("budget_priorityGoalsAmountAllocated");
            dt.Columns.Add("budget_priorityGoalsAmountSpent");

            dt.Columns.Add("budget_totalExpenditureAmountAllocated");
            dt.Columns.Add("budget_totalExpenditureAmountSpent");

            dt.Columns.Add("budget_totalExpenditureAmountLeftOver");

            if (budgetDashBoardList != null)
            {
                int rec_cnt = budgetDashBoardList.Count;
                int row_cnt = 0;

                if (rec_cnt > 0)
                {
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        DateTime dtmBudget_startDate = Convert.ToDateTime(budgetDashBoardList[i].budget_startDate);
                        DateTime dtmBudget_endDate = Convert.ToDateTime(budgetDashBoardList[i].budget_endDate);


                        if ((dtmStartDate.Year != 1) && (dtmEndDate.Year != 1))
                        {
                            int intDateTimeComparisonResult1 = DateTime.Compare(dtmBudget_endDate, dtmStartDate);

                            // string relationship = "";

                            if (intDateTimeComparisonResult1 < 0)
                            {
                                // relationship = "is earlier than";
                                continue;
                            }
                            else if (intDateTimeComparisonResult1 == 0)
                            {
                                // relationship = "is the same time as";
                            }
                            else
                            {
                                // relationship = "is later than";
                            } // if (intDateTimeComparisonResult1)


                            int intDateTimeComparisonResult2 = DateTime.Compare(dtmBudget_startDate, dtmEndDate);

                            // string relationship = "";

                            if (intDateTimeComparisonResult2 < 0)
                            {
                                // relationship = "is earlier than";
                            }
                            else if (intDateTimeComparisonResult2 == 0)
                            {
                                // relationship = "is the same time as";
                            }
                            else
                            {
                                // relationship = "is later than";
                                continue;
                            } // if (intDateTimeComparisonResult2)
                        } // if (dtmStartDate.Year)


                        dt.Rows.Add();

                        dt.Rows[row_cnt]["budget_id"] = budgetDashBoardList[i].budget_id;
                        dt.Rows[row_cnt]["budget_startDate"] = Convert.ToDateTime(budgetDashBoardList[i].budget_startDate);
                        dt.Rows[row_cnt]["budget_endDate"] = Convert.ToDateTime(budgetDashBoardList[i].budget_endDate);

                        dt.Rows[row_cnt]["budget_incomeAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_incomeAmountAllocated);
                        dt.Rows[row_cnt]["budget_incomeAmountReceived"] = Convert.ToDouble(budgetDashBoardList[i].budget_incomeAmountReceived);

                        dt.Rows[row_cnt]["budget_fixedCostAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_fixedCostAmountAllocated);
                        dt.Rows[row_cnt]["budget_fixedCostAmountSpent"] = Convert.ToDouble(budgetDashBoardList[i].budget_fixedCostAmountSpent);

                        dt.Rows[row_cnt]["budget_flexSpendingAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_flexSpendingAmountAllocated);
                        dt.Rows[row_cnt]["budget_flexSpendingAmountSpent"] = Convert.ToDouble(budgetDashBoardList[i].budget_flexSpendingAmountSpent);

                        dt.Rows[row_cnt]["budget_debtRepaymentAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_debtRepaymentAmountAllocated);
                        dt.Rows[row_cnt]["budget_debtRepaymentAmountSpent"] = Convert.ToDouble(budgetDashBoardList[i].budget_debtRepaymentAmountSpent);

                        dt.Rows[row_cnt]["budget_priorityGoalsAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_priorityGoalsAmountAllocated);
                        dt.Rows[row_cnt]["budget_priorityGoalsAmountSpent"] = Convert.ToDouble(budgetDashBoardList[i].budget_priorityGoalsAmountSpent);

                        dt.Rows[row_cnt]["budget_totalExpenditureAmountAllocated"] = Convert.ToDouble(budgetDashBoardList[i].budget_totalExpenditureAmountAllocated);
                        dt.Rows[row_cnt]["budget_totalExpenditureAmountSpent"] = Convert.ToDouble(budgetDashBoardList[i].budget_totalExpenditureAmountSpent);

                        dt.Rows[row_cnt]["budget_totalExpenditureAmountLeftOver"] = Convert.ToDouble(budgetDashBoardList[i].budget_totalExpenditureAmountLeftOver);

                        row_cnt += 1;
                    } // for (i)
                } // if(rec_cnt)
            } // if(budgetDashBoardList)

            BudgetSummaryGridView.DataSource = dt;
            BudgetSummaryGridView.AutoGenerateColumns = false;
            BudgetSummaryGridView.DataBind();
        } // btnShowBudgetSummary_Click()


        protected void BudgetSummaryGridView_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int index = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SHOW_BUDGET_DASHBOARD command is clicked
            if (command == "SHOW_BUDGET_DASHBOARD")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid

                string BUDGET_ID = row.Cells[0].Text; // the BUDGET_ID is stored on the 1st column

                // Assign the ID to the session variable, BudgetCenterDashBoard.aspx will pick up from PageLoad
                Session["BudgetSummary_GridView_Budget_Id"] = BUDGET_ID;
                Response.Redirect("BudgetCenterDashBoard.aspx");
            }
            else if (command == "SHOW_BUDGET_DETAILS")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid

                string BUDGET_ID = row.Cells[0].Text; // the BUDGET_ID is stored on the 1st column

                // Assign the ID to the session variable, BudgetDetails.aspx will pick up from PageLoad
                Session["BudgetSummary_GridView_Budget_Id"] = BUDGET_ID;
                Response.Redirect("BudgetDetails.aspx");
            }
            else if (command == "DELETE_BUDGET_DETAILS")
            {
                index = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[index]; // grab the row object from the grid

                string BUDGET_ID = row.Cells[0].Text; // the BUDGET_ID is stored on the 1st column

                // Assign the ID to the session variable, BudgetAndTransactions.aspx will pick up from PageLoad
                Session["BudgetSummary_GridView_Budget_Id"] = BUDGET_ID;

                Session["BudgetSummaryStartDate"] = StartDateCalendar.SelectedDate;
                Session["BudgetSummaryEndDate"] = EndDateCalendar.SelectedDate;

                int budgetDashBoardDeleteResult = 0;
                BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                budgetDashBoardDeleteResult = budgetDashBoardDAO.DeleteBudgetDashBoardByBudgetId(BUDGET_ID);

                DataTable dt = new DataTable();
                BudgetSummaryGridView.DataSource = dt;
                BudgetSummaryGridView.AutoGenerateColumns = false;
                BudgetSummaryGridView.DataBind();

                btnShowBudgetSummary_Click(null, null);

                if (budgetDashBoardDeleteResult > 0)
                {
                    Lbl_err.Text = "Your Budget " + BUDGET_ID + " has been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " . Please inform System Administrator.";
                    PanelErrorResult.Visible = true;
                } // if (budgetDashBoardDeleteResult)
            } // if (command)
        } // BudgetSummaryGridView_RowCommand1()


        protected void StartDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year != 1) && (EndDateCalendar.SelectedDate.Year == 1))
            {
                EndDateCalendar.SelectedDate = StartDateCalendar.SelectedDate.AddDays(30);

                // Save EndDateCalendar.SelectedDate in Session["BudgetSummaryEndDate"]
                Session["BudgetSummaryEndDate"] = EndDateCalendar.SelectedDate;

                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            } // if (EndDateCalendar.SelectedDate.Year)

            DataTable dt = new DataTable();
            BudgetSummaryGridView.DataSource = dt;
            BudgetSummaryGridView.AutoGenerateColumns = false;
            BudgetSummaryGridView.DataBind();

            btnShowBudgetSummary_Click(null, null);
        } // StartDateCalendar_SelectionChanged()


        protected void EndDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year == 1) && (EndDateCalendar.SelectedDate.Year != 1))
            {
                StartDateCalendar.SelectedDate = EndDateCalendar.SelectedDate.AddDays(-30);

                // Save StartDateCalendar.SelectedDate in Session["BudgetSummaryStartDate"]
                Session["BudgetSummaryStartDate"] = StartDateCalendar.SelectedDate;

                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            } // if (StartDateCalendar.SelectedDate.Year)

            DataTable dt = new DataTable();
            BudgetSummaryGridView.DataSource = dt;
            BudgetSummaryGridView.AutoGenerateColumns = false;
            BudgetSummaryGridView.DataBind();

            btnShowBudgetSummary_Click(null, null);
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

                if(strTempDateString == " ")
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

    } // BudgetSummary
} // PrestoPay