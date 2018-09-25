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
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using PrestoPay.Entity.DB_Entities;

namespace PrestoPay
{
    
    public partial class BudgetCenterDashBoard : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateBudgetExpenditureChart();
                PopulateBudgetFixedCostChart();
                PopulateBudgetFlexSpendingChart();
                PopulateBudgetDebtRepaymentChart();
                PopulateBudgetPriorityGoalsChart();
            } // if(!IsPostBack)
        } // Page_Load()


        public void PopulateBudgetExpenditureChart()
        {
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

            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                setBudgetSummary_GridView_Budget_IdByEmail(ACC_EMAIL);
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            lblbudgetId.Text = BUDGET_ID;

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            // Populate infomation on labels
            lblTotalExpenditureAllocated.Text = "";
            lblTotalExpenditureSpent.Text = "";
            lblTotalExpenditureLeftOver.Text = "";

            Series series = BudgetExpenditureChart.Series["BudgetExpenditureSeries"];
            BudgetExpenditureChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                string strTotalExpenditureAllocated = "Total Expenditure Allocated ";
                double dblTotalExpenditureAllocated = budgetDashBoardObj.budget_totalExpenditureAmountAllocated;
                if (dblTotalExpenditureAllocated < 0.0)
                {
                    dblTotalExpenditureAllocated = 0.0;
                } // if (dblTotalExpenditureAllocated)

                string strTotalExpenditureSpent = "Total Expenditure Spent";
                double dblTotalExpenditureSpent = budgetDashBoardObj.budget_totalExpenditureAmountSpent;
                if (dblTotalExpenditureSpent < 0.0)
                {
                    dblTotalExpenditureSpent = 0.0;
                } // if (dblTotalExpenditureSpent)

                string strTotalExpenditureAllocatedLeftToSpend = "Total Expenditure Left To Spend ";
                double dblTotalExpenditureAllocatedLeftToSpend = budgetDashBoardObj.budget_totalExpenditureAmountAllocated - budgetDashBoardObj.budget_totalExpenditureAmountSpent;

                if (dblTotalExpenditureAllocatedLeftToSpend < 0.0)
                {
                    dblTotalExpenditureAllocatedLeftToSpend = 0.0;
                } // if (dblTotalExpenditureAllocatedLeftToSpend)

                // Populate Donut Chart
                series.Points.AddXY(strTotalExpenditureAllocatedLeftToSpend, dblTotalExpenditureAllocatedLeftToSpend);
                series.Points.AddXY(strTotalExpenditureSpent, dblTotalExpenditureSpent);

                double dblBudget_totalExpenditureAmountAllocated = budgetDashBoardObj.budget_totalExpenditureAmountAllocated;
                double dblBudget_totalExpenditureAmountSpent = budgetDashBoardObj.budget_totalExpenditureAmountSpent;
                double dblBudget_totalExpenditureAmountLeftOver = budgetDashBoardObj.budget_totalExpenditureAmountLeftOver;

                // Populate infomations on labels
                lblTotalExpenditureAllocated.Text = Math.Round(dblBudget_totalExpenditureAmountAllocated, 2).ToString("$,###,##0.00");  
                lblTotalExpenditureSpent.Text = Math.Round(dblBudget_totalExpenditureAmountSpent, 2).ToString("$,###,##0.00");
                lblTotalExpenditureLeftOver.Text = Math.Round(dblBudget_totalExpenditureAmountLeftOver, 2).ToString("$,###,##0.00");


                double dblTotalExpenditureSpentPercentage = 0.0;
                if (dblTotalExpenditureAllocated != 0.0)
                {
                    dblTotalExpenditureSpentPercentage = (dblTotalExpenditureSpent / dblTotalExpenditureAllocated) * 100.0;
                }
                else
                {
                    dblTotalExpenditureSpentPercentage = 0.0;
                } // if (dblTotalExpenditureAllocated)


                double dblBudget_totalExpenditureAmountLeftOverPercentage = 0.0;
                if (dblBudget_totalExpenditureAmountAllocated != 0.0)
                {
                    dblBudget_totalExpenditureAmountLeftOverPercentage = (dblBudget_totalExpenditureAmountLeftOver / dblBudget_totalExpenditureAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_totalExpenditureAmountLeftOverPercentage = 0.0;
                } // if (dblBudget_totalExpenditureAmountAllocated)

                if (dblBudget_totalExpenditureAmountLeftOverPercentage < 33.3)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblTotalExpenditureSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Total Expenditure Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Red;
                    lblPercentageSpent2.ForeColor = Color.Red;

                    lblYouAreInThe.Text = "You Are In The Red!";
                    lblYouAreInThe.ForeColor = Color.Red;
                    // BudgetExpenditureChart.Series[0].Color = Color.Red;
                }
                else if (dblBudget_totalExpenditureAmountLeftOverPercentage < 66.6)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblTotalExpenditureSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Total Expenditure Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Orange;
                    lblPercentageSpent2.ForeColor = Color.Orange;

                    lblYouAreInThe.Text = "You Are In The Orange!";
                    lblYouAreInThe.ForeColor = Color.Orange;
                    // BudgetExpenditureChart.Series[0].Color = Color.Orange;
                }
                else
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblTotalExpenditureSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Total Expenditure Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Green;
                    lblPercentageSpent2.ForeColor = Color.Green;

                    lblYouAreInThe.Text = "You Are In The Green!";
                    lblYouAreInThe.ForeColor = Color.Green;
                    // BudgetExpenditureChart.Series[0].Color = Color.Green;
                } // if (dblBudget_totalExpenditureAmountLeftOverPercentage)
            } // if(budgetDashBoardObj)
        } // PopulateBudgetExpenditureChart()
        

        public void PopulateBudgetFixedCostChart()
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetFixedCostChart.Series["BudgetFixedCostSeries"];
            BudgetFixedCostChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                string strFixedCostAllocated = "Fixed Cost Allocated";
                double dblFixedCostAllocated = budgetDashBoardObj.budget_fixedCostAmountAllocated;
                if (dblFixedCostAllocated < 0.0)
                {
                    dblFixedCostAllocated = 0.0;
                } // if (dblFixedCostAllocated)

                string strFixedCostSpent = "Fixed Cost Spent";
                double dblFixedCostSpent = budgetDashBoardObj.budget_fixedCostAmountSpent;
                if (dblFixedCostSpent < 0.0)
                {
                    dblFixedCostSpent = 0.0;
                } // if (dblFixedCostSpent)

                string strFixedCostAllocatedLeftToSpend = "Fixed Cost Left To Spend";
                double dblFixedCostAllocatedLeftToSpend = budgetDashBoardObj.budget_fixedCostAmountAllocated - budgetDashBoardObj.budget_fixedCostAmountSpent;
                if (dblFixedCostAllocatedLeftToSpend < 0.0)
                {
                    dblFixedCostAllocatedLeftToSpend = 0.0;
                } // if (dblFixedCostAllocatedLeftToSpend)

                // Populate Donut Chart
                series.Points.AddXY(strFixedCostAllocatedLeftToSpend, dblFixedCostAllocatedLeftToSpend);
                series.Points.AddXY(strFixedCostSpent, dblFixedCostSpent);
            } // if(budgetDashBoardObj)
        } // PopulateBudgetFixedCostChart()


        public void PopulateBudgetFlexSpendingChart()
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetFlexSpendingChart.Series["BudgetFlexSpendingSeries"];
            BudgetFlexSpendingChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                string strFlexSpendingAllocated = "Flex Spending Allocated";
                double dblFlexSpendingAllocated = budgetDashBoardObj.budget_flexSpendingAmountAllocated;
                if (dblFlexSpendingAllocated < 0.0)
                {
                    dblFlexSpendingAllocated = 0.0;
                } // if (dblFlexSpendingAllocated)

                string strFlexSpendingSpent = "Flex Spending Spent";
                double dblFlexSpendingSpent = budgetDashBoardObj.budget_flexSpendingAmountSpent;
                if (dblFlexSpendingSpent < 0.0)
                {
                    dblFlexSpendingSpent = 0.0;
                } // if (dblFlexSpendingSpent)

                string strFlexSpendingAllocatedLeftToSpend = "Flex Spending Left To Spend";
                double dblFlexSpendingAllocatedLeftToSpend = budgetDashBoardObj.budget_flexSpendingAmountAllocated - budgetDashBoardObj.budget_flexSpendingAmountSpent;
                if (dblFlexSpendingAllocatedLeftToSpend < 0.0)
                {
                    dblFlexSpendingAllocatedLeftToSpend = 0.0;
                } // if (dblFlexSpendingAllocatedLeftToSpend)

                // Populate Donut Chart
                series.Points.AddXY(strFlexSpendingAllocatedLeftToSpend, dblFlexSpendingAllocatedLeftToSpend);
                series.Points.AddXY(strFlexSpendingSpent, dblFlexSpendingSpent);
            } // if(budgetDashBoardObj)
        } // PopulateBudgetFlexSpendingChart()


        public void PopulateBudgetDebtRepaymentChart()
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetDebtRepaymentChart.Series["BudgetDebtRepaymentSeries"];
            BudgetDebtRepaymentChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                string strDebtRepaymentAllocated = "Debt Repayment Allocated";
                double dblDebtRepaymentAllocated = budgetDashBoardObj.budget_debtRepaymentAmountAllocated;
                if (dblDebtRepaymentAllocated < 0.0)
                {
                    dblDebtRepaymentAllocated = 0.0;
                } // if (dblDebtRepaymentAllocated)

                string strDebtRepaymentSpent = "Debt Repayment Spent";
                double dblDebtRepaymentSpent = budgetDashBoardObj.budget_debtRepaymentAmountSpent;
                if (dblDebtRepaymentSpent < 0.0)
                {
                    dblDebtRepaymentSpent = 0.0;
                } // if (dblDebtRepaymentSpent)

                string strDebtRepaymentAllocatedLeftToSpend = "Debt Repayment Left To Spend";
                double dblDebtRepaymentAllocatedLeftToSpend = budgetDashBoardObj.budget_debtRepaymentAmountAllocated - budgetDashBoardObj.budget_debtRepaymentAmountSpent;
                if (dblDebtRepaymentAllocatedLeftToSpend < 0.0)
                {
                    dblDebtRepaymentAllocatedLeftToSpend = 0.0;
                } // if (dblDebtRepaymentAllocatedLeftToSpend)

                // Populate Donut Chart
                series.Points.AddXY(strDebtRepaymentAllocatedLeftToSpend, dblDebtRepaymentAllocatedLeftToSpend);
                series.Points.AddXY(strDebtRepaymentSpent, dblDebtRepaymentSpent);
            } // if(budgetDashBoardObj)
        } // PopulateBudgetDebtRepaymentChart()


        public void PopulateBudgetPriorityGoalsChart()
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetPriorityGoalsChart.Series["BudgetPriorityGoalsSeries"];
            BudgetPriorityGoalsChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                string strPriorityGoalsAllocated = "Priority Goals Allocated";
                double dblPriorityGoalsAllocated = budgetDashBoardObj.budget_priorityGoalsAmountAllocated;
                if (dblPriorityGoalsAllocated < 0.0)
                {
                    dblPriorityGoalsAllocated = 0.0;
                } // if (dblPriorityGoalsAllocated)

                string strPriorityGoalsSpent = "Priority Goals Spent";
                double dblPriorityGoalsSpent = budgetDashBoardObj.budget_priorityGoalsAmountSpent;
                if (dblPriorityGoalsSpent < 0.0)
                {
                    dblPriorityGoalsSpent = 0.0;
                } // if (dblPriorityGoalsSpent)

                string strPriorityGoalsAllocatedLeftToSpend = "Priority Goals Left To Spend";
                double dblPriorityGoalsAllocatedLeftToSpend = budgetDashBoardObj.budget_priorityGoalsAmountAllocated - budgetDashBoardObj.budget_priorityGoalsAmountSpent;
                if (dblPriorityGoalsAllocatedLeftToSpend < 0.0)
                {
                    dblPriorityGoalsAllocatedLeftToSpend = 0.0;
                } // if (dblPriorityGoalsAllocatedLeftToSpend)

                // Populate Donut Chart
                series.Points.AddXY(strPriorityGoalsAllocatedLeftToSpend, dblPriorityGoalsAllocatedLeftToSpend);
                series.Points.AddXY(strPriorityGoalsSpent, dblPriorityGoalsSpent);
            } // if(budgetDashBoardObj)
        } // PopulateBudgetPriorityGoalsChart()


        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetSummary.aspx");
        } // BtnBack_Click()


        protected void BudgetExpenditureChart_Click(object sender, ImageMapEventArgs e)
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            Session["BudgetCenterDashBoard_Expenditure_PostBackValue"] = e.PostBackValue;

            Response.Redirect("BudgetSummary.aspx");
        } // BudgetExpenditureChart_Click()


        protected void BudgetFixedCostChart_Click(object sender, ImageMapEventArgs e)
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            Session["BudgetCenterDashBoard_FixedCost_PostBackValue"] = e.PostBackValue;

            Response.Redirect("BudgetFixedCostDetails.aspx");
        } // BudgetFixedCostChart_Click()


        protected void BudgetFlexSpendingChart_Click(object sender, ImageMapEventArgs e)
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            Session["BudgetCenterDashBoard_FlexSpending_PostBackValue"] = e.PostBackValue;

            Response.Redirect("BudgetFlexSpendingDetails.aspx");
        } // BudgetFlexSpendingChart_Click()


        protected void BudgetDebtRepaymentChart_Click(object sender, ImageMapEventArgs e)
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            Session["BudgetCenterDashBoard_DebtRepayment_PostBackValue"] = e.PostBackValue;

            Response.Redirect("BudgetDebtRepaymentDetails.aspx");
        } // BudgetDebtRepaymentChart_Click()


        protected void BudgetPriorityGoalsChart_Click(object sender, ImageMapEventArgs e)
        {
            // Check whether Session["BudgetSummary_GridView_Budget_Id"] is valid
            if (Session["BudgetSummary_GridView_Budget_Id"] == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            Session["BudgetCenterDashBoard_PriorityGoals_PostBackValue"] = e.PostBackValue;

            Response.Redirect("BudgetPriorityGoalsDetails.aspx");
        } // BudgetPriorityGoalsChart_Click()


        protected void setBudgetSummary_GridView_Budget_IdByEmail(string ACC_EMAIL)
        {
            string BUDGET_ID = "";

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByEmail(ACC_EMAIL);

            List<BudgetDashBoard> budgetDashBoardList = new List<BudgetDashBoard>();
            budgetDashBoardList = budgetDashBoardDAO.CheckBudgetDashBoardByEmail(ACC_EMAIL);

            if (budgetDashBoardList != null)
            {
                int rec_cnt = budgetDashBoardList.Count;

                if (rec_cnt > 0)
                {
                    int i = rec_cnt - 1;

                    BUDGET_ID = budgetDashBoardList[i].budget_id;
                } // if(rec_cnt)
            } // if(budgetDashBoardList)

            // Check whether the BUDGET_ID is valid
            if (BUDGET_ID != "")
            {
                // Assign the ID to the session variable, BudgetCenterDashBoard.aspx will pick up from PageLoad
                Session["BudgetSummary_GridView_Budget_Id"] = BUDGET_ID;
            } // if (BUDGET_ID)
        } // setBudgetSummary_GridView_Budget_IdByEmail()


        protected string GetLatestBudgetIdByEmail(string ACC_EMAIL)
        {
            string BUDGET_ID = "";

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByEmail(ACC_EMAIL);

            List<BudgetDashBoard> budgetDashBoardList = new List<BudgetDashBoard>();
            budgetDashBoardList = budgetDashBoardDAO.CheckBudgetDashBoardByEmail(ACC_EMAIL);

            if (budgetDashBoardList != null)
            {
                int rec_cnt = budgetDashBoardList.Count;

                if (rec_cnt > 0)
                {
                    int i = rec_cnt - 1;

                    BUDGET_ID = budgetDashBoardList[i].budget_id;
                } // if(rec_cnt)
            } // if(budgetDashBoardList)

            // Check whether the BUDGET_ID is valid
            if (String.IsNullOrWhiteSpace(BUDGET_ID))
            {
                BUDGET_ID = "";
            } // if (BUDGET_ID)

            return BUDGET_ID;
        } // GetLatestBudgetIdByEmail()

    } // BudgetCenterDashBoard
} // PrestoPay