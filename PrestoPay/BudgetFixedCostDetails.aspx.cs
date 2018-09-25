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
    public partial class BudgetFixedCostDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string FIXED_COST_SUBCATEGORY = "";

                PopulateBudgetFixedCostChart();
                PopulateBudgetFixedCostSubCategoryChart();
                PopulateBudgetFixedCostAmountAllocatedGridView(FIXED_COST_SUBCATEGORY);
                PopulateBudgetExpenditureFixedCostGridView(FIXED_COST_SUBCATEGORY);
                PopulateBudgetTransactionFixedCostGridView(FIXED_COST_SUBCATEGORY);
            } // if(!IsPostBack)
        } // Page_Load()


        public void PopulateBudgetFixedCostChart()
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

            string FIXED_COST_CATEGORY = "Fixed Cost";


            int intFixedCostSubCategoryRowCount = 0;

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
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                series.Points.AddXY(strFixedCostAllocatedLeftToSpend, dblFixedCostAllocatedLeftToSpend);

                double dblBudget_fixedCostAmountAllocated = budgetDashBoardObj.budget_fixedCostAmountAllocated;
                double dblBudget_fixedCostAmountSpent = budgetDashBoardObj.budget_fixedCostAmountSpent;
                double dblBudget_fixedCostAmountLeftOver = budgetDashBoardObj.budget_fixedCostAmountAllocated - budgetDashBoardObj.budget_fixedCostAmountSpent;

                // Populate infomations on labels
                lblTotalFixedCostAllocated.Text = Math.Round(dblBudget_fixedCostAmountAllocated, 2).ToString("$,###,##0.00");
                lblTotalFixedCostSpent.Text = Math.Round(dblBudget_fixedCostAmountSpent, 2).ToString("$,###,##0.00");
                lblTotalFixedCostLeftOver.Text = Math.Round(dblBudget_fixedCostAmountLeftOver, 2).ToString("$,###,##0.00");


                double dblBudget_fixedCostAmountSpentPercentage = 0.0;
                if (dblBudget_fixedCostAmountAllocated != 0.0)
                {
                    dblBudget_fixedCostAmountSpentPercentage = (dblBudget_fixedCostAmountSpent / dblBudget_fixedCostAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_fixedCostAmountSpentPercentage = 0.0;
                } // if (dblBudget_fixedCostAmountAllocated)


                double dblBudget_fixedCostAmountLeftOverPercentage = 0.0;
                if (dblBudget_fixedCostAmountAllocated != 0.0)
                {
                    dblBudget_fixedCostAmountLeftOverPercentage = (dblBudget_fixedCostAmountLeftOver / dblBudget_fixedCostAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_fixedCostAmountLeftOverPercentage = 0.0;
                } // if (dblBudget_fixedCostAmountAllocated)


                if (dblBudget_fixedCostAmountLeftOverPercentage < 33.3)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_fixedCostAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Fixed Cost Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Red;
                    lblPercentageSpent2.ForeColor = Color.Red;

                    lblYouAreInThe.Text = "You Are In The Red!";
                    lblYouAreInThe.ForeColor = Color.Red;
                    // BudgetExpenditureChart.Series[0].Color = Color.Red;
                }
                else if (dblBudget_fixedCostAmountLeftOverPercentage < 66.6)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_fixedCostAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Fixed Cost Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Orange;
                    lblPercentageSpent2.ForeColor = Color.Orange;

                    lblYouAreInThe.Text = "You Are In The Orange!";
                    lblYouAreInThe.ForeColor = Color.Orange;
                    // BudgetExpenditureChart.Series[0].Color = Color.Orange;
                }
                else
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_fixedCostAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Fixed Cost Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Green;
                    lblPercentageSpent2.ForeColor = Color.Green;

                    lblYouAreInThe.Text = "You Are In The Green!";
                    lblYouAreInThe.ForeColor = Color.Green;
                    // BudgetExpenditureChart.Series[0].Color = Color.Green;
                } // if (dblBudget_fixedCostAmountLeftOverPercentage)

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY);

                int rec_cnt = 0;
                if (budgetFixedCostCategoryList != null)
                {
                    rec_cnt = budgetFixedCostCategoryList.Count;
                } // if (budgetFixedCostCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetFixedCostCategory DB Table

                    string strAcc_email = "";
                    budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FIXED_COST_CATEGORY);

                    rec_cnt = 0;
                    if (budgetFixedCostCategoryList != null)
                    {
                        rec_cnt = budgetFixedCostCategoryList.Count;
                    } // if (budgetFixedCostCategoryList)
                } // if (rec_cnt)

                string strFixedCostSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetFixedCostCategoryObj = budgetFixedCostCategoryList[i];

                        if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                        {
                            strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Fixed Cost SubCategory Amount Allocated and Total Budget Fixed Cost SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardFixedCostSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                            budgetDashBoardFixedCostSubCategoryObj = budgetFixedCostDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, FIXED_COST_CATEGORY, strFixedCostSubCategory, START_DATE, END_DATE);

                            string strFixedCostSubCategoryAllocated = strFixedCostSubCategory + " Allocated";
                            double dblFixedCostSubCategoryAllocated = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountAllocated;
                            if (dblFixedCostSubCategoryAllocated < 0.0)
                            {
                                dblFixedCostSubCategoryAllocated = 0.0;
                            } // if (dblFixedCostSubCategoryAllocated)

                            string strFixedCostSubCategorySpent = strFixedCostSubCategory + " Spent";
                            double dblFixedCostSubCategorySpent = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountSpent;
                            if (dblFixedCostSubCategorySpent < 0.0)
                            {
                                dblFixedCostSubCategorySpent = 0.0;
                            } // if (dblFixedCostSubCategorySpent)

                            string strFixedCostSubCategoryAllocatedLeftToSpend = strFixedCostSubCategory + " Left To Spend";
                            double dblFixedCostSubCategoryAllocatedLeftToSpend = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountAllocated - budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountSpent;
                            if (dblFixedCostSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblFixedCostSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblFixedCostSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            if ((dblFixedCostSubCategoryAllocated > 0.0) || (dblFixedCostSubCategorySpent > 0.0))
                            {
                                // Add Total Budget Fixed Cost SubCategory Amount Spent to Pie Chart
                                series.Points.AddXY(strFixedCostSubCategorySpent, dblFixedCostSubCategorySpent);

                                intFixedCostSubCategoryRowCount += 1;
                            } // if(dblFixedCostSubCategoryAllocated)
                        } // if(strFixedCostSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)
        } // PopulateBudgetFixedCostChart()


        public void PopulateBudgetFixedCostSubCategoryChart()
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


            string FIXED_COST_CATEGORY = "Fixed Cost";

            int intFixedCostSubCategoryRowCount = 0;

            DataTable dtFixedCostFullTable = new DataTable();
            dtFixedCostFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory Amount Allocated", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory Amount Spent", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory Amount Left To Spend", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory PostBackValue", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetFixedCostChart.Series["BudgetFixedCostSeries"];
            BudgetFixedCostSubCategoryChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                // series.Points.AddXY(strFixedCostAllocatedLeftToSpend, dblFixedCostAllocatedLeftToSpend);

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY);

                int rec_cnt = 0;
                if (budgetFixedCostCategoryList != null)
                {
                    rec_cnt = budgetFixedCostCategoryList.Count;
                } // if (budgetFixedCostCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetFixedCostCategory DB Table

                    string strAcc_email = "";
                    budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.CheckBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FIXED_COST_CATEGORY);

                    rec_cnt = 0;
                    if (budgetFixedCostCategoryList != null)
                    {
                        rec_cnt = budgetFixedCostCategoryList.Count;
                    } // if (budgetFixedCostCategoryList)
                } // if (rec_cnt)

                string strFixedCostSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetFixedCostCategoryObj = budgetFixedCostCategoryList[i];

                        if ((budgetFixedCostCategoryObj.budget_expenditureSubCategory != "") && (strFixedCostSubCategory != budgetFixedCostCategoryObj.budget_expenditureSubCategory))
                        {
                            strFixedCostSubCategory = budgetFixedCostCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Fixed Cost SubCategory Amount Allocated and Total Budget Fixed Cost SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardFixedCostSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();
                            budgetDashBoardFixedCostSubCategoryObj = budgetFixedCostDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, FIXED_COST_CATEGORY, strFixedCostSubCategory, START_DATE, END_DATE);

                            string strFixedCostSubCategoryAllocated = strFixedCostSubCategory + " Allocated";
                            double dblFixedCostSubCategoryAllocated = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountAllocated;
                            if (dblFixedCostSubCategoryAllocated < 0.0)
                            {
                                dblFixedCostSubCategoryAllocated = 0.0;
                            } // if (dblFixedCostSubCategoryAllocated)

                            string strFixedCostSubCategorySpent = strFixedCostSubCategory + " Spent";
                            double dblFixedCostSubCategorySpent = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountSpent;
                            if (dblFixedCostSubCategorySpent < 0.0)
                            {
                                dblFixedCostSubCategorySpent = 0.0;
                            } // if (dblFixedCostSubCategorySpent)

                            string strFixedCostSubCategoryAllocatedLeftToSpend = strFixedCostSubCategory + " Left To Spend";
                            double dblFixedCostSubCategoryAllocatedLeftToSpend = budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountAllocated - budgetDashBoardFixedCostSubCategoryObj.budget_fixedCostSubCategoryAmountSpent;
                            if (dblFixedCostSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblFixedCostSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblFixedCostSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            // if ((dblFixedCostSubCategoryAllocated > 0.0) || (dblFixedCostSubCategorySpent > 0.0))
                            // {
                            //     // Add Total Budget Fixed Cost SubCategory Amount Spent to Pie Chart
                            //     series.Points.AddXY(strFixedCostSubCategorySpent, dblFixedCostSubCategorySpent);

                            //     intFixedCostSubCategoryRowCount += 1;
                            // } // if(dblFixedCostSubCategoryAllocated)

                            // Populate Stacked Column Chart
                            if ((dblFixedCostSubCategoryAllocated > 0.0) || (dblFixedCostSubCategorySpent > 0.0))
                            {
                                BudgetFixedCostSubCategoryChart.Series["BudgetFixedCostSubCategorySeriesLeftToSpend"].Points.Add(new DataPoint(intFixedCostSubCategoryRowCount, dblFixedCostSubCategoryAllocatedLeftToSpend));
                                BudgetFixedCostSubCategoryChart.Series["BudgetFixedCostSubCategorySeriesSpent"].Points.Add(new DataPoint(intFixedCostSubCategoryRowCount, dblFixedCostSubCategorySpent));

                                BudgetFixedCostSubCategoryChart.Series[0].Points[intFixedCostSubCategoryRowCount].AxisLabel = strFixedCostSubCategory;

                                DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                                drFixedCostFullTable["RowNumber"] = intFixedCostSubCategoryRowCount + 1;
                                drFixedCostFullTable["Fixed Cost Category"] = FIXED_COST_CATEGORY;
                                drFixedCostFullTable["Fixed Cost SubCategory"] = strFixedCostSubCategory;
                                drFixedCostFullTable["Fixed Cost SubCategory Amount Allocated"] = Convert.ToString(dblFixedCostSubCategoryAllocated);
                                drFixedCostFullTable["Fixed Cost SubCategory Amount Spent"] = Convert.ToString(dblFixedCostSubCategorySpent);
                                drFixedCostFullTable["Fixed Cost SubCategory Amount Left To Spend"] = Convert.ToString(dblFixedCostSubCategoryAllocatedLeftToSpend);
                                drFixedCostFullTable["Fixed Cost SubCategory PostBackValue"] = Convert.ToString(intFixedCostSubCategoryRowCount);
                                dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);

                                intFixedCostSubCategoryRowCount += 1;
                            } // if(dblFixedCostSubCategoryAllocated)
                        } // if(strFixedCostSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)

            ViewState["BudgetFixedCostDetails_FixedCostSubCategory"] = dtFixedCostFullTable;
        } // PopulateBudgetFixedCostSubCategoryChart()


        private void PopulateBudgetFixedCostAmountAllocatedGridView(string FIXED_COST_SUBCATEGORY)
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

            string FIXED_COST_CATEGORY = "Fixed Cost";

            DataTable dtFixedCostFullTable = new DataTable();
            dtFixedCostFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Amount Allocated", typeof(string)));

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetSetUpExpenditure> budgetFixedCostCategoryList = new List<BudgetSetUpExpenditure>();
            BudgetSetUpExpenditureDAO budgetFixedCostCategoryDAO = new BudgetSetUpExpenditureDAO();

            // Check whether the FIXED_COST_SUBCATEGORY is valid
            if (String.IsNullOrWhiteSpace(FIXED_COST_SUBCATEGORY) == false)
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);
            }
            else
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory(BUDGET_ID, FIXED_COST_CATEGORY);
            } // if (FIXED_COST_SUBCATEGORY)

            int rec_cnt = 0;
            if (budgetFixedCostCategoryList != null)
            {
                rec_cnt = budgetFixedCostCategoryList.Count;
            } // if (budgetFixedCostCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetSetUpExpenditure budgetFixedCostCategoryObj = budgetFixedCostCategoryList[i];

                    DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                    drFixedCostFullTable["RowNumber"] = i + 1;
                    drFixedCostFullTable["Fixed Cost Category"] = budgetFixedCostCategoryObj.budget_expenditureCategory;
                    drFixedCostFullTable["Fixed Cost SubCategory"] = budgetFixedCostCategoryObj.budget_expenditureSubCategory;
                    drFixedCostFullTable["Fixed Cost Amount Allocated"] = Convert.ToString(budgetFixedCostCategoryObj.budget_expenditureSubCategoryAmountAllocated);
                    dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (dtFixedCostFullTable.Rows.Count == 0)
            {
                DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                drFixedCostFullTable["RowNumber"] = 1;
                drFixedCostFullTable["Fixed Cost Category"] = FIXED_COST_CATEGORY;
                drFixedCostFullTable["Fixed Cost SubCategory"] = string.Empty;
                drFixedCostFullTable["Fixed Cost Amount Allocated"] = string.Empty;
                dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
            } //if (dtFixedCostFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFixedCostDetails_dtCurrentFixedCostFullTable"] = dtFixedCostFullTable;

            GridviewBudgetSetUpFixedCost.DataSource = dtFixedCostFullTable;
            GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
            GridviewBudgetSetUpFixedCost.DataBind();
        } // PopulateBudgetFixedCostAmountAllocatedGridView()


        private void PopulateBudgetExpenditureFixedCostGridView(string FIXED_COST_SUBCATEGORY)
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


            string FIXED_COST_CATEGORY = "Fixed Cost";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtFixedCostFullTable = new DataTable();
            dtFixedCostFullTable.Columns.Add(new DataColumn("Expenditure ID", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Amount Spent", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Expenditure Date", typeof(DateTime)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Expenditure Remarks", typeof(string)));

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

                // Read Budget Fixed Cost Amount Spent
                List<BudgetExpenditure> budgetFixedCostAmountSpentList = new List<BudgetExpenditure>();
                BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();

                // Check whether the FIXED_COST_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(FIXED_COST_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetFixedCostAmountSpentList = budgetFixedCostDAO.ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetFixedCostAmountSpentList = budgetFixedCostDAO.ReadBudgetExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY, START_DATE, END_DATE);
                } // if (FIXED_COST_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetFixedCostAmountSpentList != null)
                {
                    rec_cnt = budgetFixedCostAmountSpentList.Count;
                } // if (budgetFixedCostAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditure budgetFixedCostAmountSpentObj = budgetFixedCostAmountSpentList[i];

                        DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                        drFixedCostFullTable["Expenditure ID"] = budgetFixedCostAmountSpentObj.budget_expenditureId;
                        drFixedCostFullTable["Fixed Cost Category"] = budgetFixedCostAmountSpentObj.budget_expenditureCategory;
                        drFixedCostFullTable["Fixed Cost SubCategory"] = budgetFixedCostAmountSpentObj.budget_expenditureSubCategory;
                        drFixedCostFullTable["Fixed Cost Amount Spent"] = Convert.ToString(budgetFixedCostAmountSpentObj.budget_expenditureAmountSpent);
                        drFixedCostFullTable["Expenditure Date"] = budgetFixedCostAmountSpentObj.budget_expenditureDate;
                        drFixedCostFullTable["Expenditure Remarks"] = budgetFixedCostAmountSpentObj.budget_expenditureRemarks;

                        dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtFixedCostFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                drFixedCostFullTable["Expenditure ID"] = string.Empty;
                drFixedCostFullTable["Fixed Cost Category"] = string.Empty;
                drFixedCostFullTable["Fixed Cost SubCategory"] = string.Empty;
                drFixedCostFullTable["Fixed Cost Amount Spent"] = string.Empty;
                drFixedCostFullTable["Expenditure Date"] = strBudget_expenditureDate;
                drFixedCostFullTable["Expenditure Remarks"] = string.Empty;

                dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
            } //if (dtFixedCostFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFixedCostDetails_dtCurrentFixedCostFullTable"] = dtFixedCostFullTable;

            GridviewBudgetExpenditureFixedCost.DataSource = dtFixedCostFullTable;
            GridviewBudgetExpenditureFixedCost.AutoGenerateColumns = false;
            GridviewBudgetExpenditureFixedCost.DataBind();
        } // PopulateBudgetExpenditureFixedCostGridView()


        private void PopulateBudgetTransactionFixedCostGridView(string FIXED_COST_SUBCATEGORY)
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


            string FIXED_COST_CATEGORY = "Fixed Cost";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtFixedCostFullTable = new DataTable();
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction ID", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Amount Spent", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction Date", typeof(DateTime)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction Type", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction From", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction To", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Transaction Remarks", typeof(string)));

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

                // Read Budget Fixed Cost Amount Spent
                List<CategorisedTransaction> budgetFixedCostAmountSpentList = new List<CategorisedTransaction>();
                BudgetExpenditureDAO budgetFixedCostDAO = new BudgetExpenditureDAO();

                // Check whether the FIXED_COST_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(FIXED_COST_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetFixedCostAmountSpentList = budgetFixedCostDAO.ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetFixedCostAmountSpentList = budgetFixedCostDAO.ReadTransactionExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY, START_DATE, END_DATE);
                } // if (FIXED_COST_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetFixedCostAmountSpentList != null)
                {
                    rec_cnt = budgetFixedCostAmountSpentList.Count;
                } // if (budgetFixedCostAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        CategorisedTransaction budgetFixedCostAmountSpentObj = budgetFixedCostAmountSpentList[i];

                        DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                        drFixedCostFullTable["Transaction ID"] = budgetFixedCostAmountSpentObj.trans_id;
                        drFixedCostFullTable["Fixed Cost Category"] = budgetFixedCostAmountSpentObj.budgetCategory;
                        drFixedCostFullTable["Fixed Cost SubCategory"] = budgetFixedCostAmountSpentObj.budgetSubCategory;
                        drFixedCostFullTable["Fixed Cost Amount Spent"] = Convert.ToString(budgetFixedCostAmountSpentObj.trans_amt);
                        drFixedCostFullTable["Transaction Date"] = budgetFixedCostAmountSpentObj.trans_date;
                        drFixedCostFullTable["Transaction Type"] = budgetFixedCostAmountSpentObj.trans_type;
                        drFixedCostFullTable["Transaction From"] = budgetFixedCostAmountSpentObj.trans_from;
                        drFixedCostFullTable["Transaction To"] = budgetFixedCostAmountSpentObj.trans_to;
                        drFixedCostFullTable["Transaction Remarks"] = budgetFixedCostAmountSpentObj.trans_description;

                        dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtFixedCostFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                drFixedCostFullTable["Transaction ID"] = String.Empty;
                drFixedCostFullTable["Fixed Cost Category"] = String.Empty;
                drFixedCostFullTable["Fixed Cost SubCategory"] = String.Empty;
                drFixedCostFullTable["Fixed Cost Amount Spent"] = String.Empty;
                drFixedCostFullTable["Transaction Date"] = strBudget_expenditureDate;
                drFixedCostFullTable["Transaction Type"] = String.Empty;
                drFixedCostFullTable["Transaction From"] = String.Empty;
                drFixedCostFullTable["Transaction To"] = String.Empty;
                drFixedCostFullTable["Transaction Remarks"] = String.Empty;

                dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);
            } //if (dtFixedCostFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetFixedCostDetails_dtCurrentFixedCostFullTable"] = dtFixedCostFullTable;

            GridviewBudgetTransactionFixedCost.DataSource = dtFixedCostFullTable;
            GridviewBudgetTransactionFixedCost.AutoGenerateColumns = false;
            GridviewBudgetTransactionFixedCost.DataBind();
        } // PopulateBudgetTransactionFixedCostGridView()


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

            Session["BudgetFixedCostDetails_FixedCostCategory_PostBackValue"] = e.PostBackValue;

            string budgetFixedCostDetails_FixedCostCategory_PostBackValue = e.PostBackValue;

            string FIXED_COST_SUBCATEGORY = "";

            PopulateBudgetFixedCostChart();
            PopulateBudgetFixedCostSubCategoryChart();
            PopulateBudgetFixedCostAmountAllocatedGridView(FIXED_COST_SUBCATEGORY);
            PopulateBudgetExpenditureFixedCostGridView(FIXED_COST_SUBCATEGORY);
            PopulateBudgetTransactionFixedCostGridView(FIXED_COST_SUBCATEGORY);
        } // BudgetFixedCostChart_Click()


        protected void BudgetFixedCostSubCategoryChart_Click(object sender, ImageMapEventArgs e)
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

            Session["BudgetFixedCostDetails_FixedCostSubCategory_PostBackValue"] = e.PostBackValue;

            string budgetFixedCostDetails_FixedCostSubCategory_PostBackValue = e.PostBackValue;

            string FIXED_COST_SUBCATEGORY = GetBudgetFixedCostDetails_FixedCostSubCategoryByPostBackValue(budgetFixedCostDetails_FixedCostSubCategory_PostBackValue);
            
            PopulateBudgetFixedCostChart();
            PopulateBudgetFixedCostSubCategoryChart();
            PopulateBudgetFixedCostAmountAllocatedGridView(FIXED_COST_SUBCATEGORY);
            PopulateBudgetExpenditureFixedCostGridView(FIXED_COST_SUBCATEGORY);
            PopulateBudgetTransactionFixedCostGridView(FIXED_COST_SUBCATEGORY);
        } // BudgetFixedCostSubCategoryChart_Click()


        public string GetBudgetFixedCostDetails_FixedCostSubCategoryByPostBackValue(string strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue)
        {
            string FIXED_COST_CATEGORY = "Fixed Cost";

            string FIXED_COST_SUBCATEGORY = "";

            // Check whether the strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue is valid
            if (String.IsNullOrWhiteSpace(strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue) == false)
            {
                DataTable dtFixedCostSubCategory = null;

                if (ViewState["BudgetFixedCostDetails_FixedCostSubCategory"] != null)
                {
                    dtFixedCostSubCategory = (DataTable)ViewState["BudgetFixedCostDetails_FixedCostSubCategory"];

                    if (dtFixedCostSubCategory != null)
                    {
                        if (dtFixedCostSubCategory.Rows.Count > 0)
                        {
                            for (int i = 0; (i < dtFixedCostSubCategory.Rows.Count); i++)
                            {
                                // Check whether the Fixed Cost SubCategory PostBackValue is valid
                                if (strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue  == dtFixedCostSubCategory.Rows[i]["Fixed Cost SubCategory PostBackValue"].ToString())
                                {
                                    // Check whether the Fixed Cost Category is valid
                                    if (FIXED_COST_CATEGORY == dtFixedCostSubCategory.Rows[i]["Fixed Cost Category"].ToString())
                                    {
                                        // Retrieve the Fixed Cost SubCategory from the DataTable
                                        FIXED_COST_SUBCATEGORY = dtFixedCostSubCategory.Rows[i]["Fixed Cost SubCategory"].ToString();
                                        break;
                                    } // if (strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue)

                                } // if (strBudgetFixedCostDetails_FixedCostSubCategory_PostBackValue)
                            } // for (i)
                        } // if (dtFixedCostSubCategory.Rows.Count)
                    } // if (dtFixedCostSubCategory)
                } // if (ViewState["BudgetFixedCostDetails_FixedCostSubCategory"])
            }
            else
            {
                FIXED_COST_SUBCATEGORY = "";
            } // if (FIXED_COST_SUBCATEGORY)

            return FIXED_COST_SUBCATEGORY;
        } // GetBudgetFixedCostDetails_FixedCostSubCategoryByPostBackValue()


        protected void BtnBackToBudgetSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetSummary.aspx");
        } // BtnBackToBudgetSummary_Click()


        protected void BtnBackToBudgetDashBoard_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetCenterDashBoard.aspx");
        } // BtnBackToBudgetDashBoard_Click()

    } // BudgetFixedCostDetails
} // PrestoPay