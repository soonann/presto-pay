using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using PrestoPay.Entity.DB_Entities;
using System.Drawing;
using System.Data;

namespace PrestoPay
{
    public partial class BudgetFlexSpendingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FLEX_SPENDING_SUBCATEGORY = "";

                PopulateBudgetFlexSpendingChart();
                PopulateBudgetFlexSpendingSubCategoryChart();
                PopulateBudgetFlexSpendingAmountAllocatedGridView(FLEX_SPENDING_SUBCATEGORY);
                PopulateBudgetExpenditureFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
                PopulateBudgetTransactionFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
            } // if(!IsPostBack)
        } // Page_Load()


        public void PopulateBudgetFlexSpendingChart()
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

            string FLEX_SPENDING_CATEGORY = "Flex Spending";


            int intFlexSpendingSubCategoryRowCount = 0;

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
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                series.Points.AddXY(strFlexSpendingAllocatedLeftToSpend, dblFlexSpendingAllocatedLeftToSpend);

                double dblBudget_flexSpendingAmountAllocated = budgetDashBoardObj.budget_flexSpendingAmountAllocated;
                double dblBudget_flexSpendingAmountSpent = budgetDashBoardObj.budget_flexSpendingAmountSpent;
                double dblBudget_flexSpendingAmountLeftOver = budgetDashBoardObj.budget_flexSpendingAmountAllocated - budgetDashBoardObj.budget_flexSpendingAmountSpent;

                // Populate infomations on labels
                lblTotalFlexSpendingAllocated.Text = Math.Round(dblBudget_flexSpendingAmountAllocated, 2).ToString("$,###,##0.00");
                lblTotalFlexSpendingSpent.Text = Math.Round(dblBudget_flexSpendingAmountSpent, 2).ToString("$,###,##0.00");
                lblTotalFlexSpendingLeftOver.Text = Math.Round(dblBudget_flexSpendingAmountLeftOver, 2).ToString("$,###,##0.00");


                double dblBudget_flexSpendingAmountSpentPercentage = 0.0;
                if (dblBudget_flexSpendingAmountAllocated != 0.0)
                {
                    dblBudget_flexSpendingAmountSpentPercentage = (dblBudget_flexSpendingAmountSpent / dblBudget_flexSpendingAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_flexSpendingAmountSpentPercentage = 0.0;
                } // if (dblBudget_flexSpendingAmountAllocated)


                double dblBudget_flexSpendingAmountLeftOverPercentage = 0.0;
                if (dblBudget_flexSpendingAmountAllocated != 0.0)
                {
                    dblBudget_flexSpendingAmountLeftOverPercentage = (dblBudget_flexSpendingAmountLeftOver / dblBudget_flexSpendingAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_flexSpendingAmountLeftOverPercentage = 0.0;
                } // if (dblBudget_flexSpendingAmountAllocated)


                if (dblBudget_flexSpendingAmountLeftOverPercentage < 33.3)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_flexSpendingAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Flex Spending Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Red;
                    lblPercentageSpent2.ForeColor = Color.Red;

                    lblYouAreInThe.Text = "You Are In The Red!";
                    lblYouAreInThe.ForeColor = Color.Red;
                    // BudgetExpenditureChart.Series[0].Color = Color.Red;
                }
                else if (dblBudget_flexSpendingAmountLeftOverPercentage < 66.6)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_flexSpendingAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Flex Spending Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Orange;
                    lblPercentageSpent2.ForeColor = Color.Orange;

                    lblYouAreInThe.Text = "You Are In The Orange!";
                    lblYouAreInThe.ForeColor = Color.Orange;
                    // BudgetExpenditureChart.Series[0].Color = Color.Orange;
                }
                else
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_flexSpendingAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Flex Spending Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Green;
                    lblPercentageSpent2.ForeColor = Color.Green;

                    lblYouAreInThe.Text = "You Are In The Green!";
                    lblYouAreInThe.ForeColor = Color.Green;
                    // BudgetExpenditureChart.Series[0].Color = Color.Green;
                } // if (dblBudget_flexSpendingAmountLeftOverPercentage)

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.CheckBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY);

                int rec_cnt = 0;
                if (budgetFlexSpendingCategoryList != null)
                {
                    rec_cnt = budgetFlexSpendingCategoryList.Count;
                } // if (budgetFlexSpendingCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetFlexSpendingCategory DB Table

                    string strAcc_email = "";
                    budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.CheckBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FLEX_SPENDING_CATEGORY);

                    rec_cnt = 0;
                    if (budgetFlexSpendingCategoryList != null)
                    {
                        rec_cnt = budgetFlexSpendingCategoryList.Count;
                    } // if (budgetFlexSpendingCategoryList)
                } // if (rec_cnt)

                string strFlexSpendingSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryList[i];

                        if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                        {
                            strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Flex Spending SubCategory Amount Allocated and Total Budget Flex Spending SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardFlexSpendingSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                            budgetDashBoardFlexSpendingSubCategoryObj = budgetFlexSpendingDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, FLEX_SPENDING_CATEGORY, strFlexSpendingSubCategory, START_DATE, END_DATE);

                            string strFlexSpendingSubCategoryAllocated = strFlexSpendingSubCategory + " Allocated";
                            double dblFlexSpendingSubCategoryAllocated = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountAllocated;
                            if (dblFlexSpendingSubCategoryAllocated < 0.0)
                            {
                                dblFlexSpendingSubCategoryAllocated = 0.0;
                            } // if (dblFlexSpendingSubCategoryAllocated)

                            string strFlexSpendingSubCategorySpent = strFlexSpendingSubCategory + " Spent";
                            double dblFlexSpendingSubCategorySpent = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountSpent;
                            if (dblFlexSpendingSubCategorySpent < 0.0)
                            {
                                dblFlexSpendingSubCategorySpent = 0.0;
                            } // if (dblFlexSpendingSubCategorySpent)

                            string strFlexSpendingSubCategoryAllocatedLeftToSpend = strFlexSpendingSubCategory + " Left To Spend";
                            double dblFlexSpendingSubCategoryAllocatedLeftToSpend = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountAllocated - budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountSpent;
                            if (dblFlexSpendingSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblFlexSpendingSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblFlexSpendingSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            if ((dblFlexSpendingSubCategoryAllocated > 0.0) || (dblFlexSpendingSubCategorySpent > 0.0))
                            {
                                // Add Total Budget Flex Spending SubCategory Amount Spent to Pie Chart
                                series.Points.AddXY(strFlexSpendingSubCategorySpent, dblFlexSpendingSubCategorySpent);

                                intFlexSpendingSubCategoryRowCount += 1;
                            } // if(dblFlexSpendingSubCategoryAllocated)
                        } // if(strFlexSpendingSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)
        } // PopulateBudgetFlexSpendingChart()


        public void PopulateBudgetFlexSpendingSubCategoryChart()
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


            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            int intFlexSpendingSubCategoryRowCount = 0;

            DataTable dtFlexSpendingFullTable = new DataTable();
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory Amount Allocated", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory Amount Spent", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory Amount Left To Spend", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory PostBackValue", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetFlexSpendingChart.Series["BudgetFlexSpendingSeries"];
            BudgetFlexSpendingSubCategoryChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                // series.Points.AddXY(strFlexSpendingAllocatedLeftToSpend, dblFlexSpendingAllocatedLeftToSpend);

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.CheckBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY);

                int rec_cnt = 0;
                if (budgetFlexSpendingCategoryList != null)
                {
                    rec_cnt = budgetFlexSpendingCategoryList.Count;
                } // if (budgetFlexSpendingCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetFlexSpendingCategory DB Table

                    string strAcc_email = "";
                    budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.CheckBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FLEX_SPENDING_CATEGORY);

                    rec_cnt = 0;
                    if (budgetFlexSpendingCategoryList != null)
                    {
                        rec_cnt = budgetFlexSpendingCategoryList.Count;
                    } // if (budgetFlexSpendingCategoryList)
                } // if (rec_cnt)

                string strFlexSpendingSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryList[i];

                        if ((budgetFlexSpendingCategoryObj.budget_expenditureSubCategory != "") && (strFlexSpendingSubCategory != budgetFlexSpendingCategoryObj.budget_expenditureSubCategory))
                        {
                            strFlexSpendingSubCategory = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Flex Spending SubCategory Amount Allocated and Total Budget Flex Spending SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardFlexSpendingSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();
                            budgetDashBoardFlexSpendingSubCategoryObj = budgetFlexSpendingDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, FLEX_SPENDING_CATEGORY, strFlexSpendingSubCategory, START_DATE, END_DATE);

                            string strFlexSpendingSubCategoryAllocated = strFlexSpendingSubCategory + " Allocated";
                            double dblFlexSpendingSubCategoryAllocated = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountAllocated;
                            if (dblFlexSpendingSubCategoryAllocated < 0.0)
                            {
                                dblFlexSpendingSubCategoryAllocated = 0.0;
                            } // if (dblFlexSpendingSubCategoryAllocated)

                            string strFlexSpendingSubCategorySpent = strFlexSpendingSubCategory + " Spent";
                            double dblFlexSpendingSubCategorySpent = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountSpent;
                            if (dblFlexSpendingSubCategorySpent < 0.0)
                            {
                                dblFlexSpendingSubCategorySpent = 0.0;
                            } // if (dblFlexSpendingSubCategorySpent)

                            string strFlexSpendingSubCategoryAllocatedLeftToSpend = strFlexSpendingSubCategory + " Left To Spend";
                            double dblFlexSpendingSubCategoryAllocatedLeftToSpend = budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountAllocated - budgetDashBoardFlexSpendingSubCategoryObj.budget_flexSpendingSubCategoryAmountSpent;
                            if (dblFlexSpendingSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblFlexSpendingSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblFlexSpendingSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            // if ((dblFlexSpendingSubCategoryAllocated > 0.0) || (dblFlexSpendingSubCategorySpent > 0.0))
                            // {
                            //     // Add Total Budget Flex Spending SubCategory Amount Spent to Pie Chart
                            //     series.Points.AddXY(strFlexSpendingSubCategorySpent, dblFlexSpendingSubCategorySpent);

                            //     intFlexSpendingSubCategoryRowCount += 1;
                            // } // if(dblFlexSpendingSubCategoryAllocated)

                            // Populate Stacked Column Chart
                            if ((dblFlexSpendingSubCategoryAllocated > 0.0) || (dblFlexSpendingSubCategorySpent > 0.0))
                            {
                                BudgetFlexSpendingSubCategoryChart.Series["BudgetFlexSpendingSubCategorySeriesLeftToSpend"].Points.Add(new DataPoint(intFlexSpendingSubCategoryRowCount, dblFlexSpendingSubCategoryAllocatedLeftToSpend));
                                BudgetFlexSpendingSubCategoryChart.Series["BudgetFlexSpendingSubCategorySeriesSpent"].Points.Add(new DataPoint(intFlexSpendingSubCategoryRowCount, dblFlexSpendingSubCategorySpent));

                                BudgetFlexSpendingSubCategoryChart.Series[0].Points[intFlexSpendingSubCategoryRowCount].AxisLabel = strFlexSpendingSubCategory;

                                DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                                drFlexSpendingFullTable["RowNumber"] = intFlexSpendingSubCategoryRowCount + 1;
                                drFlexSpendingFullTable["Flex Spending Category"] = FLEX_SPENDING_CATEGORY;
                                drFlexSpendingFullTable["Flex Spending SubCategory"] = strFlexSpendingSubCategory;
                                drFlexSpendingFullTable["Flex Spending SubCategory Amount Allocated"] = Convert.ToString(dblFlexSpendingSubCategoryAllocated);
                                drFlexSpendingFullTable["Flex Spending SubCategory Amount Spent"] = Convert.ToString(dblFlexSpendingSubCategorySpent);
                                drFlexSpendingFullTable["Flex Spending SubCategory Amount Left To Spend"] = Convert.ToString(dblFlexSpendingSubCategoryAllocatedLeftToSpend);
                                drFlexSpendingFullTable["Flex Spending SubCategory PostBackValue"] = Convert.ToString(intFlexSpendingSubCategoryRowCount);
                                dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);

                                intFlexSpendingSubCategoryRowCount += 1;
                            } // if(dblFlexSpendingSubCategoryAllocated)
                        } // if(strFlexSpendingSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)

            ViewState["BudgetFlexSpendingDetails_FlexSpendingSubCategory"] = dtFlexSpendingFullTable;
        } // PopulateBudgetFlexSpendingSubCategoryChart()


        private void PopulateBudgetFlexSpendingAmountAllocatedGridView(string FLEX_SPENDING_SUBCATEGORY)
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

            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            DataTable dtFlexSpendingFullTable = new DataTable();
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Amount Allocated", typeof(string)));

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetSetUpExpenditure> budgetFlexSpendingCategoryList = new List<BudgetSetUpExpenditure>();
            BudgetSetUpExpenditureDAO budgetFlexSpendingCategoryDAO = new BudgetSetUpExpenditureDAO();

            // Check whether the FLEX_SPENDING_SUBCATEGORY is valid
            if (String.IsNullOrWhiteSpace(FLEX_SPENDING_SUBCATEGORY) == false)
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);
            }
            else
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory(BUDGET_ID, FLEX_SPENDING_CATEGORY);
            } // if (FLEX_SPENDING_SUBCATEGORY)

            int rec_cnt = 0;
            if (budgetFlexSpendingCategoryList != null)
            {
                rec_cnt = budgetFlexSpendingCategoryList.Count;
            } // if (budgetFlexSpendingCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetSetUpExpenditure budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryList[i];

                    DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                    drFlexSpendingFullTable["RowNumber"] = i + 1;
                    drFlexSpendingFullTable["Flex Spending Category"] = budgetFlexSpendingCategoryObj.budget_expenditureCategory;
                    drFlexSpendingFullTable["Flex Spending SubCategory"] = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;
                    drFlexSpendingFullTable["Flex Spending Amount Allocated"] = Convert.ToString(budgetFlexSpendingCategoryObj.budget_expenditureSubCategoryAmountAllocated);
                    dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (dtFlexSpendingFullTable.Rows.Count == 0)
            {
                DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                drFlexSpendingFullTable["RowNumber"] = 1;
                drFlexSpendingFullTable["Flex Spending Category"] = FLEX_SPENDING_CATEGORY;
                drFlexSpendingFullTable["Flex Spending SubCategory"] = string.Empty;
                drFlexSpendingFullTable["Flex Spending Amount Allocated"] = string.Empty;
                dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
            } //if (dtFlexSpendingFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFlexSpendingDetails_dtCurrentFlexSpendingFullTable"] = dtFlexSpendingFullTable;

            GridviewBudgetSetUpFlexSpending.DataSource = dtFlexSpendingFullTable;
            GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetSetUpFlexSpending.DataBind();
        } // PopulateBudgetFlexSpendingAmountAllocatedGridView()


        private void PopulateBudgetExpenditureFlexSpendingGridView(string FLEX_SPENDING_SUBCATEGORY)
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


            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtFlexSpendingFullTable = new DataTable();
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Expenditure ID", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Amount Spent", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Expenditure Date", typeof(DateTime)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Expenditure Remarks", typeof(string)));

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

                // Read Budget Flex Spending Amount Spent
                List<BudgetExpenditure> budgetFlexSpendingAmountSpentList = new List<BudgetExpenditure>();
                BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();

                // Check whether the FLEX_SPENDING_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(FLEX_SPENDING_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetFlexSpendingAmountSpentList = budgetFlexSpendingDAO.ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetFlexSpendingAmountSpentList = budgetFlexSpendingDAO.ReadBudgetExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, START_DATE, END_DATE);
                } // if (FLEX_SPENDING_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetFlexSpendingAmountSpentList != null)
                {
                    rec_cnt = budgetFlexSpendingAmountSpentList.Count;
                } // if (budgetFlexSpendingAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditure budgetFlexSpendingAmountSpentObj = budgetFlexSpendingAmountSpentList[i];

                        DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                        drFlexSpendingFullTable["Expenditure ID"] = budgetFlexSpendingAmountSpentObj.budget_expenditureId;
                        drFlexSpendingFullTable["Flex Spending Category"] = budgetFlexSpendingAmountSpentObj.budget_expenditureCategory;
                        drFlexSpendingFullTable["Flex Spending SubCategory"] = budgetFlexSpendingAmountSpentObj.budget_expenditureSubCategory;
                        drFlexSpendingFullTable["Flex Spending Amount Spent"] = Convert.ToString(budgetFlexSpendingAmountSpentObj.budget_expenditureAmountSpent);
                        drFlexSpendingFullTable["Expenditure Date"] = budgetFlexSpendingAmountSpentObj.budget_expenditureDate;
                        drFlexSpendingFullTable["Expenditure Remarks"] = budgetFlexSpendingAmountSpentObj.budget_expenditureRemarks;

                        dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtFlexSpendingFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                drFlexSpendingFullTable["Expenditure ID"] = string.Empty;
                drFlexSpendingFullTable["Flex Spending Category"] = string.Empty;
                drFlexSpendingFullTable["Flex Spending SubCategory"] = string.Empty;
                drFlexSpendingFullTable["Flex Spending Amount Spent"] = string.Empty;
                drFlexSpendingFullTable["Expenditure Date"] = strBudget_expenditureDate;
                drFlexSpendingFullTable["Expenditure Remarks"] = string.Empty;

                dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
            } //if (dtFlexSpendingFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFlexSpendingDetails_dtCurrentFlexSpendingFullTable"] = dtFlexSpendingFullTable;

            GridviewBudgetExpenditureFlexSpending.DataSource = dtFlexSpendingFullTable;
            GridviewBudgetExpenditureFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetExpenditureFlexSpending.DataBind();
        } // PopulateBudgetExpenditureFlexSpendingGridView()


        private void PopulateBudgetTransactionFlexSpendingGridView(string FLEX_SPENDING_SUBCATEGORY)
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


            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtFlexSpendingFullTable = new DataTable();
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction ID", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Amount Spent", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction Date", typeof(DateTime)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction Type", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction From", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction To", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Transaction Remarks", typeof(string)));

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

                // Read Budget Flex Spending Amount Spent
                List<CategorisedTransaction> budgetFlexSpendingAmountSpentList = new List<CategorisedTransaction>();
                BudgetExpenditureDAO budgetFlexSpendingDAO = new BudgetExpenditureDAO();

                // Check whether the FLEX_SPENDING_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(FLEX_SPENDING_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetFlexSpendingAmountSpentList = budgetFlexSpendingDAO.ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetFlexSpendingAmountSpentList = budgetFlexSpendingDAO.ReadTransactionExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, START_DATE, END_DATE);
                } // if (FLEX_SPENDING_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetFlexSpendingAmountSpentList != null)
                {
                    rec_cnt = budgetFlexSpendingAmountSpentList.Count;
                } // if (budgetFlexSpendingAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        CategorisedTransaction budgetFlexSpendingAmountSpentObj = budgetFlexSpendingAmountSpentList[i];

                        DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                        drFlexSpendingFullTable["Transaction ID"] = budgetFlexSpendingAmountSpentObj.trans_id;
                        drFlexSpendingFullTable["Flex Spending Category"] = budgetFlexSpendingAmountSpentObj.budgetCategory;
                        drFlexSpendingFullTable["Flex Spending SubCategory"] = budgetFlexSpendingAmountSpentObj.budgetSubCategory;
                        drFlexSpendingFullTable["Flex Spending Amount Spent"] = Convert.ToString(budgetFlexSpendingAmountSpentObj.trans_amt);
                        drFlexSpendingFullTable["Transaction Date"] = budgetFlexSpendingAmountSpentObj.trans_date;
                        drFlexSpendingFullTable["Transaction Type"] = budgetFlexSpendingAmountSpentObj.trans_type;
                        drFlexSpendingFullTable["Transaction From"] = budgetFlexSpendingAmountSpentObj.trans_from;
                        drFlexSpendingFullTable["Transaction To"] = budgetFlexSpendingAmountSpentObj.trans_to;
                        drFlexSpendingFullTable["Transaction Remarks"] = budgetFlexSpendingAmountSpentObj.trans_description;

                        dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtFlexSpendingFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                drFlexSpendingFullTable["Transaction ID"] = String.Empty;
                drFlexSpendingFullTable["Flex Spending Category"] = String.Empty;
                drFlexSpendingFullTable["Flex Spending SubCategory"] = String.Empty;
                drFlexSpendingFullTable["Flex Spending Amount Spent"] = String.Empty;
                drFlexSpendingFullTable["Transaction Date"] = strBudget_expenditureDate;
                drFlexSpendingFullTable["Transaction Type"] = String.Empty;
                drFlexSpendingFullTable["Transaction From"] = String.Empty;
                drFlexSpendingFullTable["Transaction To"] = String.Empty;
                drFlexSpendingFullTable["Transaction Remarks"] = String.Empty;

                dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);
            } //if (dtFlexSpendingFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFlexSpendingDetails_dtCurrentFlexSpendingFullTable"] = dtFlexSpendingFullTable;

            GridviewBudgetTransactionFlexSpending.DataSource = dtFlexSpendingFullTable;
            GridviewBudgetTransactionFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetTransactionFlexSpending.DataBind();
        } // PopulateBudgetTransactionFlexSpendingGridView()


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

            Session["BudgetFlexSpendingDetails_FlexSpendingCategory_PostBackValue"] = e.PostBackValue;

            string budgetFlexSpendingDetails_FlexSpendingCategory_PostBackValue = e.PostBackValue;

            string FLEX_SPENDING_SUBCATEGORY = "";

            PopulateBudgetFlexSpendingChart();
            PopulateBudgetFlexSpendingSubCategoryChart();
            PopulateBudgetFlexSpendingAmountAllocatedGridView(FLEX_SPENDING_SUBCATEGORY);
            PopulateBudgetExpenditureFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
            PopulateBudgetTransactionFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
        } // BudgetFlexSpendingChart_Click()


        protected void BudgetFlexSpendingSubCategoryChart_Click(object sender, ImageMapEventArgs e)
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

            Session["BudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue"] = e.PostBackValue;

            string budgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue = e.PostBackValue;

            string FLEX_SPENDING_SUBCATEGORY = GetBudgetFlexSpendingDetails_FlexSpendingSubCategoryByPostBackValue(budgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue);

            PopulateBudgetFlexSpendingChart();
            PopulateBudgetFlexSpendingSubCategoryChart();
            PopulateBudgetFlexSpendingAmountAllocatedGridView(FLEX_SPENDING_SUBCATEGORY);
            PopulateBudgetExpenditureFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
            PopulateBudgetTransactionFlexSpendingGridView(FLEX_SPENDING_SUBCATEGORY);
        } // BudgetFlexSpendingSubCategoryChart_Click()


        public string GetBudgetFlexSpendingDetails_FlexSpendingSubCategoryByPostBackValue(string strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue)
        {
            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            string FLEX_SPENDING_SUBCATEGORY = "";

            // Check whether the strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue is valid
            if (String.IsNullOrWhiteSpace(strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue) == false)
            {
                DataTable dtFlexSpendingSubCategory = null;

                if (ViewState["BudgetFlexSpendingDetails_FlexSpendingSubCategory"] != null)
                {
                    dtFlexSpendingSubCategory = (DataTable)ViewState["BudgetFlexSpendingDetails_FlexSpendingSubCategory"];

                    if (dtFlexSpendingSubCategory != null)
                    {
                        if (dtFlexSpendingSubCategory.Rows.Count > 0)
                        {
                            for (int i = 0; (i < dtFlexSpendingSubCategory.Rows.Count); i++)
                            {
                                // Check whether the Flex Spending SubCategory PostBackValue is valid
                                if (strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue == dtFlexSpendingSubCategory.Rows[i]["Flex Spending SubCategory PostBackValue"].ToString())
                                {
                                    // Check whether the Flex Spending Category is valid
                                    if (FLEX_SPENDING_CATEGORY == dtFlexSpendingSubCategory.Rows[i]["Flex Spending Category"].ToString())
                                    {
                                        // Retrieve the Flex Spending SubCategory from the DataTable
                                        FLEX_SPENDING_SUBCATEGORY = dtFlexSpendingSubCategory.Rows[i]["Flex Spending SubCategory"].ToString();
                                        break;
                                    } // if (strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue)

                                } // if (strBudgetFlexSpendingDetails_FlexSpendingSubCategory_PostBackValue)
                            } // for (i)
                        } // if (dtFlexSpendingSubCategory.Rows.Count)
                    } // if (dtFlexSpendingSubCategory)
                } // if (ViewState["BudgetFlexSpendingDetails_FlexSpendingSubCategory"])
            }
            else
            {
                FLEX_SPENDING_SUBCATEGORY = "";
            } // if (FLEX_SPENDING_SUBCATEGORY)

            return FLEX_SPENDING_SUBCATEGORY;
        } // GetBudgetFlexSpendingDetails_FlexSpendingSubCategoryByPostBackValue()


        protected void BtnBackToBudgetSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetSummary.aspx");
        } // BtnBackToBudgetSummary_Click()


        protected void BtnBackToBudgetDashBoard_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetCenterDashBoard.aspx");
        } // BtnBackToBudgetDashBoard_Click()

    } // BudgetFlexSpendingDetails
} // PrestoPay