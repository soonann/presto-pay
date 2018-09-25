using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using PrestoPay.Entity.DB_Entities;
using System.Drawing;
using System.Data;

namespace PrestoPay
{
    public partial class BudgetPriorityGoalsDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string PRIORITY_GOALS_SUBCATEGORY = "";

                PopulateBudgetPriorityGoalsChart();
                PopulateBudgetPriorityGoalsSubCategoryChart();
                PopulateBudgetPriorityGoalsAmountAllocatedGridView(PRIORITY_GOALS_SUBCATEGORY);
                PopulateBudgetExpenditurePriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
                PopulateBudgetTransactionPriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
            } // if(!IsPostBack)
        } // Page_Load()


        public void PopulateBudgetPriorityGoalsChart()
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
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)

            lblbudgetId.Text = BUDGET_ID;

            string PRIORITY_GOALS_CATEGORY = "Priority Goals";


            int intPriorityGoalsSubCategoryRowCount = 0;

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
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                series.Points.AddXY(strPriorityGoalsAllocatedLeftToSpend, dblPriorityGoalsAllocatedLeftToSpend);

                double dblBudget_priorityGoalsAmountAllocated = budgetDashBoardObj.budget_priorityGoalsAmountAllocated;
                double dblBudget_priorityGoalsAmountSpent = budgetDashBoardObj.budget_priorityGoalsAmountSpent;
                double dblBudget_priorityGoalsAmountLeftOver = budgetDashBoardObj.budget_priorityGoalsAmountAllocated - budgetDashBoardObj.budget_priorityGoalsAmountSpent;

                // Populate infomations on labels
                lblTotalPriorityGoalsAllocated.Text = Math.Round(dblBudget_priorityGoalsAmountAllocated, 2).ToString("$,###,##0.00");
                lblTotalPriorityGoalsSpent.Text = Math.Round(dblBudget_priorityGoalsAmountSpent, 2).ToString("$,###,##0.00");
                lblTotalPriorityGoalsLeftOver.Text = Math.Round(dblBudget_priorityGoalsAmountLeftOver, 2).ToString("$,###,##0.00");


                double dblBudget_priorityGoalsAmountSpentPercentage = 0.0;
                if (dblBudget_priorityGoalsAmountAllocated != 0.0)
                {
                    dblBudget_priorityGoalsAmountSpentPercentage = (dblBudget_priorityGoalsAmountSpent / dblBudget_priorityGoalsAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_priorityGoalsAmountSpentPercentage = 0.0;
                } // if (dblBudget_priorityGoalsAmountAllocated)


                double dblBudget_priorityGoalsAmountLeftOverPercentage = 0.0;
                if (dblBudget_priorityGoalsAmountAllocated != 0.0)
                {
                    dblBudget_priorityGoalsAmountLeftOverPercentage = (dblBudget_priorityGoalsAmountLeftOver / dblBudget_priorityGoalsAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_priorityGoalsAmountLeftOverPercentage = 0.0;
                } // if (dblBudget_priorityGoalsAmountAllocated)


                if (dblBudget_priorityGoalsAmountLeftOverPercentage < 33.3)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_priorityGoalsAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Priority Goals Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Red;
                    lblPercentageSpent2.ForeColor = Color.Red;

                    lblYouAreInThe.Text = "You Are In The Red!";
                    lblYouAreInThe.ForeColor = Color.Red;
                    // BudgetExpenditureChart.Series[0].Color = Color.Red;
                }
                else if (dblBudget_priorityGoalsAmountLeftOverPercentage < 66.6)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_priorityGoalsAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Priority Goals Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Orange;
                    lblPercentageSpent2.ForeColor = Color.Orange;

                    lblYouAreInThe.Text = "You Are In The Orange!";
                    lblYouAreInThe.ForeColor = Color.Orange;
                    // BudgetExpenditureChart.Series[0].Color = Color.Orange;
                }
                else
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_priorityGoalsAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Priority Goals Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Green;
                    lblPercentageSpent2.ForeColor = Color.Green;

                    lblYouAreInThe.Text = "You Are In The Green!";
                    lblYouAreInThe.ForeColor = Color.Green;
                    // BudgetExpenditureChart.Series[0].Color = Color.Green;
                } // if (dblBudget_priorityGoalsAmountLeftOverPercentage)

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.CheckBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY);

                int rec_cnt = 0;
                if (budgetPriorityGoalsCategoryList != null)
                {
                    rec_cnt = budgetPriorityGoalsCategoryList.Count;
                } // if (budgetPriorityGoalsCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetPriorityGoalsCategory DB Table

                    string strAcc_email = "";
                    budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.CheckBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(strAcc_email, PRIORITY_GOALS_CATEGORY);

                    rec_cnt = 0;
                    if (budgetPriorityGoalsCategoryList != null)
                    {
                        rec_cnt = budgetPriorityGoalsCategoryList.Count;
                    } // if (budgetPriorityGoalsCategoryList)
                } // if (rec_cnt)

                string strPriorityGoalsSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryList[i];

                        if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                        {
                            strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Priority Goals SubCategory Amount Allocated and Total Budget Priority Goals SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardPriorityGoalsSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                            budgetDashBoardPriorityGoalsSubCategoryObj = budgetPriorityGoalsDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, PRIORITY_GOALS_CATEGORY, strPriorityGoalsSubCategory, START_DATE, END_DATE);

                            string strPriorityGoalsSubCategoryAllocated = strPriorityGoalsSubCategory + " Allocated";
                            double dblPriorityGoalsSubCategoryAllocated = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountAllocated;
                            if (dblPriorityGoalsSubCategoryAllocated < 0.0)
                            {
                                dblPriorityGoalsSubCategoryAllocated = 0.0;
                            } // if (dblPriorityGoalsSubCategoryAllocated)

                            string strPriorityGoalsSubCategorySpent = strPriorityGoalsSubCategory + " Spent";
                            double dblPriorityGoalsSubCategorySpent = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountSpent;
                            if (dblPriorityGoalsSubCategorySpent < 0.0)
                            {
                                dblPriorityGoalsSubCategorySpent = 0.0;
                            } // if (dblPriorityGoalsSubCategorySpent)

                            string strPriorityGoalsSubCategoryAllocatedLeftToSpend = strPriorityGoalsSubCategory + " Left To Spend";
                            double dblPriorityGoalsSubCategoryAllocatedLeftToSpend = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountAllocated - budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountSpent;
                            if (dblPriorityGoalsSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblPriorityGoalsSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblPriorityGoalsSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            if ((dblPriorityGoalsSubCategoryAllocated > 0.0) || (dblPriorityGoalsSubCategorySpent > 0.0))
                            {
                                // Add Total Budget Priority Goals SubCategory Amount Spent to Pie Chart
                                series.Points.AddXY(strPriorityGoalsSubCategorySpent, dblPriorityGoalsSubCategorySpent);

                                intPriorityGoalsSubCategoryRowCount += 1;
                            } // if(dblPriorityGoalsSubCategoryAllocated)
                        } // if(strPriorityGoalsSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)
        } // PopulateBudgetPriorityGoalsChart()


        public void PopulateBudgetPriorityGoalsSubCategoryChart()
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
                Response.Redirect("~/Login.aspx");
            } // if (Session["BudgetSummary_GridView_Budget_Id"])

            string BUDGET_ID = (string)Session["BudgetSummary_GridView_Budget_Id"];

            // Check whether BUDGET_ID is valid
            if (BUDGET_ID == null)
            {
                Response.Redirect("~/Login.aspx");
            } // if (BUDGET_ID)


            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            int intPriorityGoalsSubCategoryRowCount = 0;

            DataTable dtPriorityGoalsFullTable = new DataTable();
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory Amount Allocated", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory Amount Spent", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory Amount Left To Spend", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory PostBackValue", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetPriorityGoalsChart.Series["BudgetPriorityGoalsSeries"];
            BudgetPriorityGoalsSubCategoryChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                // series.Points.AddXY(strPriorityGoalsAllocatedLeftToSpend, dblPriorityGoalsAllocatedLeftToSpend);

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.CheckBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY);

                int rec_cnt = 0;
                if (budgetPriorityGoalsCategoryList != null)
                {
                    rec_cnt = budgetPriorityGoalsCategoryList.Count;
                } // if (budgetPriorityGoalsCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetPriorityGoalsCategory DB Table

                    string strAcc_email = "";
                    budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.CheckBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(strAcc_email, PRIORITY_GOALS_CATEGORY);

                    rec_cnt = 0;
                    if (budgetPriorityGoalsCategoryList != null)
                    {
                        rec_cnt = budgetPriorityGoalsCategoryList.Count;
                    } // if (budgetPriorityGoalsCategoryList)
                } // if (rec_cnt)

                string strPriorityGoalsSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryList[i];

                        if ((budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory != "") && (strPriorityGoalsSubCategory != budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory))
                        {
                            strPriorityGoalsSubCategory = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Priority Goals SubCategory Amount Allocated and Total Budget Priority Goals SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardPriorityGoalsSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();
                            budgetDashBoardPriorityGoalsSubCategoryObj = budgetPriorityGoalsDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, PRIORITY_GOALS_CATEGORY, strPriorityGoalsSubCategory, START_DATE, END_DATE);

                            string strPriorityGoalsSubCategoryAllocated = strPriorityGoalsSubCategory + " Allocated";
                            double dblPriorityGoalsSubCategoryAllocated = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountAllocated;
                            if (dblPriorityGoalsSubCategoryAllocated < 0.0)
                            {
                                dblPriorityGoalsSubCategoryAllocated = 0.0;
                            } // if (dblPriorityGoalsSubCategoryAllocated)

                            string strPriorityGoalsSubCategorySpent = strPriorityGoalsSubCategory + " Spent";
                            double dblPriorityGoalsSubCategorySpent = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountSpent;
                            if (dblPriorityGoalsSubCategorySpent < 0.0)
                            {
                                dblPriorityGoalsSubCategorySpent = 0.0;
                            } // if (dblPriorityGoalsSubCategorySpent)

                            string strPriorityGoalsSubCategoryAllocatedLeftToSpend = strPriorityGoalsSubCategory + " Left To Spend";
                            double dblPriorityGoalsSubCategoryAllocatedLeftToSpend = budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountAllocated - budgetDashBoardPriorityGoalsSubCategoryObj.budget_priorityGoalsSubCategoryAmountSpent;
                            if (dblPriorityGoalsSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblPriorityGoalsSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblPriorityGoalsSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            // if ((dblPriorityGoalsSubCategoryAllocated > 0.0) || (dblPriorityGoalsSubCategorySpent > 0.0))
                            // {
                            //     // Add Total Budget Priority Goals SubCategory Amount Spent to Pie Chart
                            //     series.Points.AddXY(strPriorityGoalsSubCategorySpent, dblPriorityGoalsSubCategorySpent);

                            //     intPriorityGoalsSubCategoryRowCount += 1;
                            // } // if(dblPriorityGoalsSubCategoryAllocated)

                            // Populate Stacked Column Chart
                            if ((dblPriorityGoalsSubCategoryAllocated > 0.0) || (dblPriorityGoalsSubCategorySpent > 0.0))
                            {
                                BudgetPriorityGoalsSubCategoryChart.Series["BudgetPriorityGoalsSubCategorySeriesLeftToSpend"].Points.Add(new DataPoint(intPriorityGoalsSubCategoryRowCount, dblPriorityGoalsSubCategoryAllocatedLeftToSpend));
                                BudgetPriorityGoalsSubCategoryChart.Series["BudgetPriorityGoalsSubCategorySeriesSpent"].Points.Add(new DataPoint(intPriorityGoalsSubCategoryRowCount, dblPriorityGoalsSubCategorySpent));

                                BudgetPriorityGoalsSubCategoryChart.Series[0].Points[intPriorityGoalsSubCategoryRowCount].AxisLabel = strPriorityGoalsSubCategory;

                                DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                                drPriorityGoalsFullTable["RowNumber"] = intPriorityGoalsSubCategoryRowCount + 1;
                                drPriorityGoalsFullTable["Priority Goals Category"] = PRIORITY_GOALS_CATEGORY;
                                drPriorityGoalsFullTable["Priority Goals SubCategory"] = strPriorityGoalsSubCategory;
                                drPriorityGoalsFullTable["Priority Goals SubCategory Amount Allocated"] = Convert.ToString(dblPriorityGoalsSubCategoryAllocated);
                                drPriorityGoalsFullTable["Priority Goals SubCategory Amount Spent"] = Convert.ToString(dblPriorityGoalsSubCategorySpent);
                                drPriorityGoalsFullTable["Priority Goals SubCategory Amount Left To Spend"] = Convert.ToString(dblPriorityGoalsSubCategoryAllocatedLeftToSpend);
                                drPriorityGoalsFullTable["Priority Goals SubCategory PostBackValue"] = Convert.ToString(intPriorityGoalsSubCategoryRowCount);
                                dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);

                                intPriorityGoalsSubCategoryRowCount += 1;
                            } // if(dblPriorityGoalsSubCategoryAllocated)
                        } // if(strPriorityGoalsSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)

            ViewState["BudgetPriorityGoalsDetails_PriorityGoalsSubCategory"] = dtPriorityGoalsFullTable;
        } // PopulateBudgetPriorityGoalsSubCategoryChart()


        private void PopulateBudgetPriorityGoalsAmountAllocatedGridView(string PRIORITY_GOALS_SUBCATEGORY)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

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

            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            DataTable dtPriorityGoalsFullTable = new DataTable();
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Amount Allocated", typeof(string)));

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetSetUpExpenditure> budgetPriorityGoalsCategoryList = new List<BudgetSetUpExpenditure>();
            BudgetSetUpExpenditureDAO budgetPriorityGoalsCategoryDAO = new BudgetSetUpExpenditureDAO();

            // Check whether the PRIORITY_GOALS_SUBCATEGORY is valid
            if (String.IsNullOrWhiteSpace(PRIORITY_GOALS_SUBCATEGORY) == false)
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);
            }
            else
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory(BUDGET_ID, PRIORITY_GOALS_CATEGORY);
            } // if (PRIORITY_GOALS_SUBCATEGORY)

            int rec_cnt = 0;
            if (budgetPriorityGoalsCategoryList != null)
            {
                rec_cnt = budgetPriorityGoalsCategoryList.Count;
            } // if (budgetPriorityGoalsCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetSetUpExpenditure budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryList[i];

                    DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                    drPriorityGoalsFullTable["RowNumber"] = i + 1;
                    drPriorityGoalsFullTable["Priority Goals Category"] = budgetPriorityGoalsCategoryObj.budget_expenditureCategory;
                    drPriorityGoalsFullTable["Priority Goals SubCategory"] = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;
                    drPriorityGoalsFullTable["Priority Goals Amount Allocated"] = Convert.ToString(budgetPriorityGoalsCategoryObj.budget_expenditureSubCategoryAmountAllocated);
                    dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (dtPriorityGoalsFullTable.Rows.Count == 0)
            {
                DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                drPriorityGoalsFullTable["RowNumber"] = 1;
                drPriorityGoalsFullTable["Priority Goals Category"] = PRIORITY_GOALS_CATEGORY;
                drPriorityGoalsFullTable["Priority Goals SubCategory"] = string.Empty;
                drPriorityGoalsFullTable["Priority Goals Amount Allocated"] = string.Empty;
                dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
            } //if (dtPriorityGoalsFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetPriorityGoalsDetails_dtCurrentPriorityGoalsFullTable"] = dtPriorityGoalsFullTable;

            GridviewBudgetSetUpPriorityGoals.DataSource = dtPriorityGoalsFullTable;
            GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetSetUpPriorityGoals.DataBind();
        } // PopulateBudgetPriorityGoalsAmountAllocatedGridView()


        private void PopulateBudgetExpenditurePriorityGoalsGridView(string PRIORITY_GOALS_SUBCATEGORY)
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


            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtPriorityGoalsFullTable = new DataTable();
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Expenditure ID", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Amount Spent", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Expenditure Date", typeof(DateTime)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Expenditure Remarks", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            if (budgetDashBoardObj != null)
            {
                START_DATE = budgetDashBoardObj.budget_startDate;
                END_DATE = budgetDashBoardObj.budget_endDate;

                // Read Budget Priority Goals Amount Spent
                List<BudgetExpenditure> budgetPriorityGoalsAmountSpentList = new List<BudgetExpenditure>();
                BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();

                // Check whether the PRIORITY_GOALS_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(PRIORITY_GOALS_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetPriorityGoalsAmountSpentList = budgetPriorityGoalsDAO.ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetPriorityGoalsAmountSpentList = budgetPriorityGoalsDAO.ReadBudgetExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, START_DATE, END_DATE);
                } // if (PRIORITY_GOALS_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetPriorityGoalsAmountSpentList != null)
                {
                    rec_cnt = budgetPriorityGoalsAmountSpentList.Count;
                } // if (budgetPriorityGoalsAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditure budgetPriorityGoalsAmountSpentObj = budgetPriorityGoalsAmountSpentList[i];

                        DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                        drPriorityGoalsFullTable["Expenditure ID"] = budgetPriorityGoalsAmountSpentObj.budget_expenditureId;
                        drPriorityGoalsFullTable["Priority Goals Category"] = budgetPriorityGoalsAmountSpentObj.budget_expenditureCategory;
                        drPriorityGoalsFullTable["Priority Goals SubCategory"] = budgetPriorityGoalsAmountSpentObj.budget_expenditureSubCategory;
                        drPriorityGoalsFullTable["Priority Goals Amount Spent"] = Convert.ToString(budgetPriorityGoalsAmountSpentObj.budget_expenditureAmountSpent);
                        drPriorityGoalsFullTable["Expenditure Date"] = budgetPriorityGoalsAmountSpentObj.budget_expenditureDate;
                        drPriorityGoalsFullTable["Expenditure Remarks"] = budgetPriorityGoalsAmountSpentObj.budget_expenditureRemarks;

                        dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtPriorityGoalsFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                drPriorityGoalsFullTable["Expenditure ID"] = string.Empty;
                drPriorityGoalsFullTable["Priority Goals Category"] = string.Empty;
                drPriorityGoalsFullTable["Priority Goals SubCategory"] = string.Empty;
                drPriorityGoalsFullTable["Priority Goals Amount Spent"] = string.Empty;
                drPriorityGoalsFullTable["Expenditure Date"] = strBudget_expenditureDate;
                drPriorityGoalsFullTable["Expenditure Remarks"] = string.Empty;

                dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
            } //if (dtPriorityGoalsFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetPriorityGoalsDetails_dtCurrentPriorityGoalsFullTable"] = dtPriorityGoalsFullTable;

            GridviewBudgetExpenditurePriorityGoals.DataSource = dtPriorityGoalsFullTable;
            GridviewBudgetExpenditurePriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetExpenditurePriorityGoals.DataBind();
        } // PopulateBudgetExpenditurePriorityGoalsGridView()


        private void PopulateBudgetTransactionPriorityGoalsGridView(string PRIORITY_GOALS_SUBCATEGORY)
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


            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtPriorityGoalsFullTable = new DataTable();
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction ID", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Amount Spent", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction Date", typeof(DateTime)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction Type", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction From", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction To", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Transaction Remarks", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            if (budgetDashBoardObj != null)
            {
                START_DATE = budgetDashBoardObj.budget_startDate;
                END_DATE = budgetDashBoardObj.budget_endDate;

                // Read Budget Priority Goals Amount Spent
                List<CategorisedTransaction> budgetPriorityGoalsAmountSpentList = new List<CategorisedTransaction>();
                BudgetExpenditureDAO budgetPriorityGoalsDAO = new BudgetExpenditureDAO();

                // Check whether the PRIORITY_GOALS_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(PRIORITY_GOALS_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetPriorityGoalsAmountSpentList = budgetPriorityGoalsDAO.ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetPriorityGoalsAmountSpentList = budgetPriorityGoalsDAO.ReadTransactionExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, START_DATE, END_DATE);
                } // if (PRIORITY_GOALS_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetPriorityGoalsAmountSpentList != null)
                {
                    rec_cnt = budgetPriorityGoalsAmountSpentList.Count;
                } // if (budgetPriorityGoalsAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        CategorisedTransaction budgetPriorityGoalsAmountSpentObj = budgetPriorityGoalsAmountSpentList[i];

                        DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                        drPriorityGoalsFullTable["Transaction ID"] = budgetPriorityGoalsAmountSpentObj.trans_id;
                        drPriorityGoalsFullTable["Priority Goals Category"] = budgetPriorityGoalsAmountSpentObj.budgetCategory;
                        drPriorityGoalsFullTable["Priority Goals SubCategory"] = budgetPriorityGoalsAmountSpentObj.budgetSubCategory;
                        drPriorityGoalsFullTable["Priority Goals Amount Spent"] = Convert.ToString(budgetPriorityGoalsAmountSpentObj.trans_amt);
                        drPriorityGoalsFullTable["Transaction Date"] = budgetPriorityGoalsAmountSpentObj.trans_date;
                        drPriorityGoalsFullTable["Transaction Type"] = budgetPriorityGoalsAmountSpentObj.trans_type;
                        drPriorityGoalsFullTable["Transaction From"] = budgetPriorityGoalsAmountSpentObj.trans_from;
                        drPriorityGoalsFullTable["Transaction To"] = budgetPriorityGoalsAmountSpentObj.trans_to;
                        drPriorityGoalsFullTable["Transaction Remarks"] = budgetPriorityGoalsAmountSpentObj.trans_description;

                        dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtPriorityGoalsFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                drPriorityGoalsFullTable["Transaction ID"] = String.Empty;
                drPriorityGoalsFullTable["Priority Goals Category"] = String.Empty;
                drPriorityGoalsFullTable["Priority Goals SubCategory"] = String.Empty;
                drPriorityGoalsFullTable["Priority Goals Amount Spent"] = String.Empty;
                drPriorityGoalsFullTable["Transaction Date"] = strBudget_expenditureDate;
                drPriorityGoalsFullTable["Transaction Type"] = String.Empty;
                drPriorityGoalsFullTable["Transaction From"] = String.Empty;
                drPriorityGoalsFullTable["Transaction To"] = String.Empty;
                drPriorityGoalsFullTable["Transaction Remarks"] = String.Empty;

                dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);
            } //if (dtPriorityGoalsFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetPriorityGoalsDetails_dtCurrentPriorityGoalsFullTable"] = dtPriorityGoalsFullTable;

            GridviewBudgetTransactionPriorityGoals.DataSource = dtPriorityGoalsFullTable;
            GridviewBudgetTransactionPriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetTransactionPriorityGoals.DataBind();
        } // PopulateBudgetTransactionPriorityGoalsGridView()


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

            Session["BudgetPriorityGoalsDetails_PriorityGoalsCategory_PostBackValue"] = e.PostBackValue;

            string budgetPriorityGoalsDetails_PriorityGoalsCategory_PostBackValue = e.PostBackValue;

            string PRIORITY_GOALS_SUBCATEGORY = "";

            PopulateBudgetPriorityGoalsChart();
            PopulateBudgetPriorityGoalsSubCategoryChart();
            PopulateBudgetPriorityGoalsAmountAllocatedGridView(PRIORITY_GOALS_SUBCATEGORY);
            PopulateBudgetExpenditurePriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
            PopulateBudgetTransactionPriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
        } // BudgetPriorityGoalsChart_Click()


        protected void BudgetPriorityGoalsSubCategoryChart_Click(object sender, ImageMapEventArgs e)
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

            Session["BudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue"] = e.PostBackValue;

            string budgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue = e.PostBackValue;

            string PRIORITY_GOALS_SUBCATEGORY = GetBudgetPriorityGoalsDetails_PriorityGoalsSubCategoryByPostBackValue(budgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue);

            PopulateBudgetPriorityGoalsChart();
            PopulateBudgetPriorityGoalsSubCategoryChart();
            PopulateBudgetPriorityGoalsAmountAllocatedGridView(PRIORITY_GOALS_SUBCATEGORY);
            PopulateBudgetExpenditurePriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
            PopulateBudgetTransactionPriorityGoalsGridView(PRIORITY_GOALS_SUBCATEGORY);
        } // BudgetPriorityGoalsSubCategoryChart_Click()


        public string GetBudgetPriorityGoalsDetails_PriorityGoalsSubCategoryByPostBackValue(string strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue)
        {
            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            string PRIORITY_GOALS_SUBCATEGORY = "";

            // Check whether the strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue is valid
            if (String.IsNullOrWhiteSpace(strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue) == false)
            {
                DataTable dtPriorityGoalsSubCategory = null;

                if (ViewState["BudgetPriorityGoalsDetails_PriorityGoalsSubCategory"] != null)
                {
                    dtPriorityGoalsSubCategory = (DataTable)ViewState["BudgetPriorityGoalsDetails_PriorityGoalsSubCategory"];

                    if (dtPriorityGoalsSubCategory != null)
                    {
                        if (dtPriorityGoalsSubCategory.Rows.Count > 0)
                        {
                            for (int i = 0; (i < dtPriorityGoalsSubCategory.Rows.Count); i++)
                            {
                                // Check whether the Priority Goals SubCategory PostBackValue is valid
                                if (strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue == dtPriorityGoalsSubCategory.Rows[i]["Priority Goals SubCategory PostBackValue"].ToString())
                                {
                                    // Check whether the Priority Goals Category is valid
                                    if (PRIORITY_GOALS_CATEGORY == dtPriorityGoalsSubCategory.Rows[i]["Priority Goals Category"].ToString())
                                    {
                                        // Retrieve the Priority Goals SubCategory from the DataTable
                                        PRIORITY_GOALS_SUBCATEGORY = dtPriorityGoalsSubCategory.Rows[i]["Priority Goals SubCategory"].ToString();
                                        break;
                                    } // if (strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue)

                                } // if (strBudgetPriorityGoalsDetails_PriorityGoalsSubCategory_PostBackValue)
                            } // for (i)
                        } // if (dtPriorityGoalsSubCategory.Rows.Count)
                    } // if (dtPriorityGoalsSubCategory)
                } // if (ViewState["BudgetPriorityGoalsDetails_PriorityGoalsSubCategory"])
            }
            else
            {
                PRIORITY_GOALS_SUBCATEGORY = "";
            } // if (PRIORITY_GOALS_SUBCATEGORY)

            return PRIORITY_GOALS_SUBCATEGORY;
        } // GetBudgetPriorityGoalsDetails_PriorityGoalsSubCategoryByPostBackValue()


        protected void BtnBackToBudgetSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetSummary.aspx");
        } // BtnBackToBudgetSummary_Click()


        protected void BtnBackToBudgetDashBoard_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetCenterDashBoard.aspx");
        } // BtnBackToBudgetDashBoard_Click()

    } // BudgetPriorityGoalsDetails
} // PrestoPay