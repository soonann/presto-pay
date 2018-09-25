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
    public partial class BudgetDebtRepaymentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string DEBT_REPAYMENT_SUBCATEGORY = "";

                PopulateBudgetDebtRepaymentChart();
                PopulateBudgetDebtRepaymentSubCategoryChart();
                PopulateBudgetDebtRepaymentAmountAllocatedGridView(DEBT_REPAYMENT_SUBCATEGORY);
                PopulateBudgetExpenditureDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
                PopulateBudgetTransactionDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
            } // if(!IsPostBack)
        } // Page_Load()


        public void PopulateBudgetDebtRepaymentChart()
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

            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";


            int intDebtRepaymentSubCategoryRowCount = 0;

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
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                series.Points.AddXY(strDebtRepaymentAllocatedLeftToSpend, dblDebtRepaymentAllocatedLeftToSpend);

                double dblBudget_debtRepaymentAmountAllocated = budgetDashBoardObj.budget_debtRepaymentAmountAllocated;
                double dblBudget_debtRepaymentAmountSpent = budgetDashBoardObj.budget_debtRepaymentAmountSpent;
                double dblBudget_debtRepaymentAmountLeftOver = budgetDashBoardObj.budget_debtRepaymentAmountAllocated - budgetDashBoardObj.budget_debtRepaymentAmountSpent;

                // Populate infomations on labels
                lblTotalDebtRepaymentAllocated.Text = Math.Round(dblBudget_debtRepaymentAmountAllocated, 2).ToString("$,###,##0.00");
                lblTotalDebtRepaymentSpent.Text = Math.Round(dblBudget_debtRepaymentAmountSpent, 2).ToString("$,###,##0.00");
                lblTotalDebtRepaymentLeftOver.Text = Math.Round(dblBudget_debtRepaymentAmountLeftOver, 2).ToString("$,###,##0.00");


                double dblBudget_debtRepaymentAmountSpentPercentage = 0.0;
                if (dblBudget_debtRepaymentAmountAllocated != 0.0)
                {
                    dblBudget_debtRepaymentAmountSpentPercentage = (dblBudget_debtRepaymentAmountSpent / dblBudget_debtRepaymentAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_debtRepaymentAmountSpentPercentage = 0.0;
                } // if (dblBudget_debtRepaymentAmountAllocated)


                double dblBudget_debtRepaymentAmountLeftOverPercentage = 0.0;
                if (dblBudget_debtRepaymentAmountAllocated != 0.0)
                {
                    dblBudget_debtRepaymentAmountLeftOverPercentage = (dblBudget_debtRepaymentAmountLeftOver / dblBudget_debtRepaymentAmountAllocated) * 100.0;
                }
                else
                {
                    dblBudget_debtRepaymentAmountLeftOverPercentage = 0.0;
                } // if (dblBudget_debtRepaymentAmountAllocated)


                if (dblBudget_debtRepaymentAmountLeftOverPercentage < 33.3)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_debtRepaymentAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Debt Repayment Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Red;
                    lblPercentageSpent2.ForeColor = Color.Red;

                    lblYouAreInThe.Text = "You Are In The Red!";
                    lblYouAreInThe.ForeColor = Color.Red;
                    // BudgetExpenditureChart.Series[0].Color = Color.Red;
                }
                else if (dblBudget_debtRepaymentAmountLeftOverPercentage < 66.6)
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_debtRepaymentAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Debt Repayment Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Orange;
                    lblPercentageSpent2.ForeColor = Color.Orange;

                    lblYouAreInThe.Text = "You Are In The Orange!";
                    lblYouAreInThe.ForeColor = Color.Orange;
                    // BudgetExpenditureChart.Series[0].Color = Color.Orange;
                }
                else
                {
                    lblPercentageSpent1.Text = "You have spent " + Math.Round(dblBudget_debtRepaymentAmountSpentPercentage, 2).ToString() + "%";
                    lblPercentageSpent2.Text = "of Your Debt Repayment Amount Allocated.";
                    lblPercentageSpent1.ForeColor = Color.Green;
                    lblPercentageSpent2.ForeColor = Color.Green;

                    lblYouAreInThe.Text = "You Are In The Green!";
                    lblYouAreInThe.ForeColor = Color.Green;
                    // BudgetExpenditureChart.Series[0].Color = Color.Green;
                } // if (dblBudget_debtRepaymentAmountLeftOverPercentage)

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.CheckBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY);

                int rec_cnt = 0;
                if (budgetDebtRepaymentCategoryList != null)
                {
                    rec_cnt = budgetDebtRepaymentCategoryList.Count;
                } // if (budgetDebtRepaymentCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetDebtRepaymentCategory DB Table

                    string strAcc_email = "";
                    budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.CheckBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(strAcc_email, DEBT_REPAYMENT_CATEGORY);

                    rec_cnt = 0;
                    if (budgetDebtRepaymentCategoryList != null)
                    {
                        rec_cnt = budgetDebtRepaymentCategoryList.Count;
                    } // if (budgetDebtRepaymentCategoryList)
                } // if (rec_cnt)

                string strDebtRepaymentSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryList[i];

                        if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                        {
                            strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Debt Repayment SubCategory Amount Allocated and Total Budget Debt Repayment SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardDebtRepaymentSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                            budgetDashBoardDebtRepaymentSubCategoryObj = budgetDebtRepaymentDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, DEBT_REPAYMENT_CATEGORY, strDebtRepaymentSubCategory, START_DATE, END_DATE);

                            string strDebtRepaymentSubCategoryAllocated = strDebtRepaymentSubCategory + " Allocated";
                            double dblDebtRepaymentSubCategoryAllocated = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountAllocated;
                            if (dblDebtRepaymentSubCategoryAllocated < 0.0)
                            {
                                dblDebtRepaymentSubCategoryAllocated = 0.0;
                            } // if (dblDebtRepaymentSubCategoryAllocated)

                            string strDebtRepaymentSubCategorySpent = strDebtRepaymentSubCategory + " Spent";
                            double dblDebtRepaymentSubCategorySpent = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountSpent;
                            if (dblDebtRepaymentSubCategorySpent < 0.0)
                            {
                                dblDebtRepaymentSubCategorySpent = 0.0;
                            } // if (dblDebtRepaymentSubCategorySpent)

                            string strDebtRepaymentSubCategoryAllocatedLeftToSpend = strDebtRepaymentSubCategory + " Left To Spend";
                            double dblDebtRepaymentSubCategoryAllocatedLeftToSpend = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountAllocated - budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountSpent;
                            if (dblDebtRepaymentSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblDebtRepaymentSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblDebtRepaymentSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            if ((dblDebtRepaymentSubCategoryAllocated > 0.0) || (dblDebtRepaymentSubCategorySpent > 0.0))
                            {
                                // Add Total Budget Debt Repayment SubCategory Amount Spent to Pie Chart
                                series.Points.AddXY(strDebtRepaymentSubCategorySpent, dblDebtRepaymentSubCategorySpent);

                                intDebtRepaymentSubCategoryRowCount += 1;
                            } // if(dblDebtRepaymentSubCategoryAllocated)
                        } // if(strDebtRepaymentSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)
        } // PopulateBudgetDebtRepaymentChart()


        public void PopulateBudgetDebtRepaymentSubCategoryChart()
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


            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            int intDebtRepaymentSubCategoryRowCount = 0;

            DataTable dtDebtRepaymentFullTable = new DataTable();
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory Amount Allocated", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory Amount Spent", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory Amount Left To Spend", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory PostBackValue", typeof(string)));

            // Update BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);

            // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
            budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(BUDGET_ID);

            double dummy_1 = 0;

            Series series = BudgetDebtRepaymentChart.Series["BudgetDebtRepaymentSeries"];
            BudgetDebtRepaymentSubCategoryChart.Legends["Legend1"].Docking = Docking.Bottom;

            if (budgetDashBoardObj != null)
            {
                DateTime START_DATE = budgetDashBoardObj.budget_startDate;
                DateTime END_DATE = budgetDashBoardObj.budget_endDate;

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

                // Populate Pie Chart
                // series.Points.AddXY(strDebtRepaymentAllocatedLeftToSpend, dblDebtRepaymentAllocatedLeftToSpend);

                // Check whether Category and SubCategory already exist in the DB
                List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
                BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

                // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.CheckBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY);

                int rec_cnt = 0;
                if (budgetDebtRepaymentCategoryList != null)
                {
                    rec_cnt = budgetDebtRepaymentCategoryList.Count;
                } // if (budgetDebtRepaymentCategoryList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt <= 0)
                {
                    // Read the default category and subCategory from the BudgetDebtRepaymentCategory DB Table

                    string strAcc_email = "";
                    budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.CheckBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(strAcc_email, DEBT_REPAYMENT_CATEGORY);

                    rec_cnt = 0;
                    if (budgetDebtRepaymentCategoryList != null)
                    {
                        rec_cnt = budgetDebtRepaymentCategoryList.Count;
                    } // if (budgetDebtRepaymentCategoryList)
                } // if (rec_cnt)

                string strDebtRepaymentSubCategory = "";

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryList[i];

                        if ((budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory != "") && (strDebtRepaymentSubCategory != budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory))
                        {
                            strDebtRepaymentSubCategory = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;

                            // Read Total Budget SetUp Debt Repayment SubCategory Amount Allocated and Total Budget Debt Repayment SubCategory Amount Spent
                            BudgetDashBoard budgetDashBoardDebtRepaymentSubCategoryObj = new BudgetDashBoard();
                            BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();
                            budgetDashBoardDebtRepaymentSubCategoryObj = budgetDebtRepaymentDAO.CheckTotalBudgetExpenditureSubCategoryAmountAllocatedAndSpentByEmailBudgetIdCategoryAndSubCategory(ACC_EMAIL, BUDGET_ID, DEBT_REPAYMENT_CATEGORY, strDebtRepaymentSubCategory, START_DATE, END_DATE);

                            string strDebtRepaymentSubCategoryAllocated = strDebtRepaymentSubCategory + " Allocated";
                            double dblDebtRepaymentSubCategoryAllocated = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountAllocated;
                            if (dblDebtRepaymentSubCategoryAllocated < 0.0)
                            {
                                dblDebtRepaymentSubCategoryAllocated = 0.0;
                            } // if (dblDebtRepaymentSubCategoryAllocated)

                            string strDebtRepaymentSubCategorySpent = strDebtRepaymentSubCategory + " Spent";
                            double dblDebtRepaymentSubCategorySpent = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountSpent;
                            if (dblDebtRepaymentSubCategorySpent < 0.0)
                            {
                                dblDebtRepaymentSubCategorySpent = 0.0;
                            } // if (dblDebtRepaymentSubCategorySpent)

                            string strDebtRepaymentSubCategoryAllocatedLeftToSpend = strDebtRepaymentSubCategory + " Left To Spend";
                            double dblDebtRepaymentSubCategoryAllocatedLeftToSpend = budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountAllocated - budgetDashBoardDebtRepaymentSubCategoryObj.budget_debtRepaymentSubCategoryAmountSpent;
                            if (dblDebtRepaymentSubCategoryAllocatedLeftToSpend < 0.0)
                            {
                                dblDebtRepaymentSubCategoryAllocatedLeftToSpend = 0.0;
                            } // if (dblDebtRepaymentSubCategoryAllocatedLeftToSpend)

                            // Populate Pie Chart
                            // if ((dblDebtRepaymentSubCategoryAllocated > 0.0) || (dblDebtRepaymentSubCategorySpent > 0.0))
                            // {
                            //     // Add Total Budget Debt Repayment SubCategory Amount Spent to Pie Chart
                            //     series.Points.AddXY(strDebtRepaymentSubCategorySpent, dblDebtRepaymentSubCategorySpent);

                            //     intDebtRepaymentSubCategoryRowCount += 1;
                            // } // if(dblDebtRepaymentSubCategoryAllocated)

                            // Populate Stacked Column Chart
                            if ((dblDebtRepaymentSubCategoryAllocated > 0.0) || (dblDebtRepaymentSubCategorySpent > 0.0))
                            {
                                BudgetDebtRepaymentSubCategoryChart.Series["BudgetDebtRepaymentSubCategorySeriesLeftToSpend"].Points.Add(new DataPoint(intDebtRepaymentSubCategoryRowCount, dblDebtRepaymentSubCategoryAllocatedLeftToSpend));
                                BudgetDebtRepaymentSubCategoryChart.Series["BudgetDebtRepaymentSubCategorySeriesSpent"].Points.Add(new DataPoint(intDebtRepaymentSubCategoryRowCount, dblDebtRepaymentSubCategorySpent));

                                BudgetDebtRepaymentSubCategoryChart.Series[0].Points[intDebtRepaymentSubCategoryRowCount].AxisLabel = strDebtRepaymentSubCategory;

                                DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                                drDebtRepaymentFullTable["RowNumber"] = intDebtRepaymentSubCategoryRowCount + 1;
                                drDebtRepaymentFullTable["Debt Repayment Category"] = DEBT_REPAYMENT_CATEGORY;
                                drDebtRepaymentFullTable["Debt Repayment SubCategory"] = strDebtRepaymentSubCategory;
                                drDebtRepaymentFullTable["Debt Repayment SubCategory Amount Allocated"] = Convert.ToString(dblDebtRepaymentSubCategoryAllocated);
                                drDebtRepaymentFullTable["Debt Repayment SubCategory Amount Spent"] = Convert.ToString(dblDebtRepaymentSubCategorySpent);
                                drDebtRepaymentFullTable["Debt Repayment SubCategory Amount Left To Spend"] = Convert.ToString(dblDebtRepaymentSubCategoryAllocatedLeftToSpend);
                                drDebtRepaymentFullTable["Debt Repayment SubCategory PostBackValue"] = Convert.ToString(intDebtRepaymentSubCategoryRowCount);
                                dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);

                                intDebtRepaymentSubCategoryRowCount += 1;
                            } // if(dblDebtRepaymentSubCategoryAllocated)
                        } // if(strDebtRepaymentSubCategory)
                    } //  for (i)
                } // if (rec_cnt)
            } // if(budgetDashBoardObj)

            ViewState["BudgetDebtRepaymentDetails_DebtRepaymentSubCategory"] = dtDebtRepaymentFullTable;
        } // PopulateBudgetDebtRepaymentSubCategoryChart()


        private void PopulateBudgetDebtRepaymentAmountAllocatedGridView(string DEBT_REPAYMENT_SUBCATEGORY)
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

            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            DataTable dtDebtRepaymentFullTable = new DataTable();
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Amount Allocated", typeof(string)));

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetSetUpExpenditure> budgetDebtRepaymentCategoryList = new List<BudgetSetUpExpenditure>();
            BudgetSetUpExpenditureDAO budgetDebtRepaymentCategoryDAO = new BudgetSetUpExpenditureDAO();

            // Check whether the DEBT_REPAYMENT_SUBCATEGORY is valid
            if (String.IsNullOrWhiteSpace(DEBT_REPAYMENT_SUBCATEGORY) == false)
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdCategoryAndSubCategory(BUDGET_ID, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);
            }
            else
            {
                // Read the user's Category, SubCategory and Amount Allocated from the BudgetSetUpExpenditure DB Table
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetSetUpExpenditureSubCategoryAmountAllocatedByBudgetIdAndCategory(BUDGET_ID, DEBT_REPAYMENT_CATEGORY);
            } // if (DEBT_REPAYMENT_SUBCATEGORY)

            int rec_cnt = 0;
            if (budgetDebtRepaymentCategoryList != null)
            {
                rec_cnt = budgetDebtRepaymentCategoryList.Count;
            } // if (budgetDebtRepaymentCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetSetUpExpenditure budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryList[i];

                    DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                    drDebtRepaymentFullTable["RowNumber"] = i + 1;
                    drDebtRepaymentFullTable["Debt Repayment Category"] = budgetDebtRepaymentCategoryObj.budget_expenditureCategory;
                    drDebtRepaymentFullTable["Debt Repayment SubCategory"] = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;
                    drDebtRepaymentFullTable["Debt Repayment Amount Allocated"] = Convert.ToString(budgetDebtRepaymentCategoryObj.budget_expenditureSubCategoryAmountAllocated);
                    dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (dtDebtRepaymentFullTable.Rows.Count == 0)
            {
                DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                drDebtRepaymentFullTable["RowNumber"] = 1;
                drDebtRepaymentFullTable["Debt Repayment Category"] = DEBT_REPAYMENT_CATEGORY;
                drDebtRepaymentFullTable["Debt Repayment SubCategory"] = string.Empty;
                drDebtRepaymentFullTable["Debt Repayment Amount Allocated"] = string.Empty;
                dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
            } //if (dtDebtRepaymentFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetDebtRepaymentDetails_dtCurrentDebtRepaymentFullTable"] = dtDebtRepaymentFullTable;

            GridviewBudgetSetUpDebtRepayment.DataSource = dtDebtRepaymentFullTable;
            GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetSetUpDebtRepayment.DataBind();
        } // PopulateBudgetDebtRepaymentAmountAllocatedGridView()


        private void PopulateBudgetExpenditureDebtRepaymentGridView(string DEBT_REPAYMENT_SUBCATEGORY)
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


            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtDebtRepaymentFullTable = new DataTable();
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Expenditure ID", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Amount Spent", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Expenditure Date", typeof(DateTime)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Expenditure Remarks", typeof(string)));

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

                // Read Budget Debt Repayment Amount Spent
                List<BudgetExpenditure> budgetDebtRepaymentAmountSpentList = new List<BudgetExpenditure>();
                BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();

                // Check whether the DEBT_REPAYMENT_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(DEBT_REPAYMENT_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetDebtRepaymentAmountSpentList = budgetDebtRepaymentDAO.ReadBudgetExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the BudgetExpenditure DB Table
                    budgetDebtRepaymentAmountSpentList = budgetDebtRepaymentDAO.ReadBudgetExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, START_DATE, END_DATE);
                } // if (DEBT_REPAYMENT_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetDebtRepaymentAmountSpentList != null)
                {
                    rec_cnt = budgetDebtRepaymentAmountSpentList.Count;
                } // if (budgetDebtRepaymentAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        BudgetExpenditure budgetDebtRepaymentAmountSpentObj = budgetDebtRepaymentAmountSpentList[i];

                        DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                        drDebtRepaymentFullTable["Expenditure ID"] = budgetDebtRepaymentAmountSpentObj.budget_expenditureId;
                        drDebtRepaymentFullTable["Debt Repayment Category"] = budgetDebtRepaymentAmountSpentObj.budget_expenditureCategory;
                        drDebtRepaymentFullTable["Debt Repayment SubCategory"] = budgetDebtRepaymentAmountSpentObj.budget_expenditureSubCategory;
                        drDebtRepaymentFullTable["Debt Repayment Amount Spent"] = Convert.ToString(budgetDebtRepaymentAmountSpentObj.budget_expenditureAmountSpent);
                        drDebtRepaymentFullTable["Expenditure Date"] = budgetDebtRepaymentAmountSpentObj.budget_expenditureDate;
                        drDebtRepaymentFullTable["Expenditure Remarks"] = budgetDebtRepaymentAmountSpentObj.budget_expenditureRemarks;

                        dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtDebtRepaymentFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                drDebtRepaymentFullTable["Expenditure ID"] = string.Empty;
                drDebtRepaymentFullTable["Debt Repayment Category"] = string.Empty;
                drDebtRepaymentFullTable["Debt Repayment SubCategory"] = string.Empty;
                drDebtRepaymentFullTable["Debt Repayment Amount Spent"] = string.Empty;
                drDebtRepaymentFullTable["Expenditure Date"] = strBudget_expenditureDate;
                drDebtRepaymentFullTable["Expenditure Remarks"] = string.Empty;

                dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
            } //if (dtDebtRepaymentFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetDebtRepaymentDetails_dtCurrentDebtRepaymentFullTable"] = dtDebtRepaymentFullTable;

            GridviewBudgetExpenditureDebtRepayment.DataSource = dtDebtRepaymentFullTable;
            GridviewBudgetExpenditureDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetExpenditureDebtRepayment.DataBind();
        } // PopulateBudgetExpenditureDebtRepaymentGridView()


        private void PopulateBudgetTransactionDebtRepaymentGridView(string DEBT_REPAYMENT_SUBCATEGORY)
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


            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            DateTime START_DATE = new DateTime();
            DateTime END_DATE = new DateTime();

            DataTable dtDebtRepaymentFullTable = new DataTable();
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction ID", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Amount Spent", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction Date", typeof(DateTime)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction Type", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction From", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction To", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Transaction Remarks", typeof(string)));

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

                // Read Budget Debt Repayment Amount Spent
                List<CategorisedTransaction> budgetDebtRepaymentAmountSpentList = new List<CategorisedTransaction>();
                BudgetExpenditureDAO budgetDebtRepaymentDAO = new BudgetExpenditureDAO();

                // Check whether the DEBT_REPAYMENT_SUBCATEGORY is valid
                if (String.IsNullOrWhiteSpace(DEBT_REPAYMENT_SUBCATEGORY) == false)
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetDebtRepaymentAmountSpentList = budgetDebtRepaymentDAO.ReadTransactionExpenditureAmountSpentByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY, START_DATE, END_DATE);
                }
                else
                {
                    // Read the user's Category, SubCategory and Amount Spent from the CategorisedTransaction DB Table
                    budgetDebtRepaymentAmountSpentList = budgetDebtRepaymentDAO.ReadTransactionExpenditureAmountSpentByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, START_DATE, END_DATE);
                } // if (DEBT_REPAYMENT_SUBCATEGORY)

                int rec_cnt = 0;
                if (budgetDebtRepaymentAmountSpentList != null)
                {
                    rec_cnt = budgetDebtRepaymentAmountSpentList.Count;
                } // if (budgetDebtRepaymentAmountSpentList)

                // Check whether the number of Category and SubCategory rows is valid
                if (rec_cnt > 0)
                {
                    // Populate the DataTable
                    for (int i = 0; i < rec_cnt; i++)
                    {
                        CategorisedTransaction budgetDebtRepaymentAmountSpentObj = budgetDebtRepaymentAmountSpentList[i];

                        DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                        drDebtRepaymentFullTable["Transaction ID"] = budgetDebtRepaymentAmountSpentObj.trans_id;
                        drDebtRepaymentFullTable["Debt Repayment Category"] = budgetDebtRepaymentAmountSpentObj.budgetCategory;
                        drDebtRepaymentFullTable["Debt Repayment SubCategory"] = budgetDebtRepaymentAmountSpentObj.budgetSubCategory;
                        drDebtRepaymentFullTable["Debt Repayment Amount Spent"] = Convert.ToString(budgetDebtRepaymentAmountSpentObj.trans_amt);
                        drDebtRepaymentFullTable["Transaction Date"] = budgetDebtRepaymentAmountSpentObj.trans_date;
                        drDebtRepaymentFullTable["Transaction Type"] = budgetDebtRepaymentAmountSpentObj.trans_type;
                        drDebtRepaymentFullTable["Transaction From"] = budgetDebtRepaymentAmountSpentObj.trans_from;
                        drDebtRepaymentFullTable["Transaction To"] = budgetDebtRepaymentAmountSpentObj.trans_to;
                        drDebtRepaymentFullTable["Transaction Remarks"] = budgetDebtRepaymentAmountSpentObj.trans_description;

                        dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
                    } //  for (i)
                } // if (rec_cnt)
            } // if (budgetDashBoardObj)

            // Populate the DataTable
            if (dtDebtRepaymentFullTable.Rows.Count == 0)
            {
                DateTime strBudget_expenditureDate = new DateTime();

                DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                drDebtRepaymentFullTable["Transaction ID"] = String.Empty;
                drDebtRepaymentFullTable["Debt Repayment Category"] = String.Empty;
                drDebtRepaymentFullTable["Debt Repayment SubCategory"] = String.Empty;
                drDebtRepaymentFullTable["Debt Repayment Amount Spent"] = String.Empty;
                drDebtRepaymentFullTable["Transaction Date"] = strBudget_expenditureDate;
                drDebtRepaymentFullTable["Transaction Type"] = String.Empty;
                drDebtRepaymentFullTable["Transaction From"] = String.Empty;
                drDebtRepaymentFullTable["Transaction To"] = String.Empty;
                drDebtRepaymentFullTable["Transaction Remarks"] = String.Empty;

                dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);
            } //if (dtDebtRepaymentFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetDebtRepaymentDetails_dtCurrentDebtRepaymentFullTable"] = dtDebtRepaymentFullTable;

            GridviewBudgetTransactionDebtRepayment.DataSource = dtDebtRepaymentFullTable;
            GridviewBudgetTransactionDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetTransactionDebtRepayment.DataBind();
        } // PopulateBudgetTransactionDebtRepaymentGridView()


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

            Session["BudgetDebtRepaymentDetails_DebtRepaymentCategory_PostBackValue"] = e.PostBackValue;

            string budgetDebtRepaymentDetails_DebtRepaymentCategory_PostBackValue = e.PostBackValue;

            string DEBT_REPAYMENT_SUBCATEGORY = "";

            PopulateBudgetDebtRepaymentChart();
            PopulateBudgetDebtRepaymentSubCategoryChart();
            PopulateBudgetDebtRepaymentAmountAllocatedGridView(DEBT_REPAYMENT_SUBCATEGORY);
            PopulateBudgetExpenditureDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
            PopulateBudgetTransactionDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
        } // BudgetDebtRepaymentChart_Click()


        protected void BudgetDebtRepaymentSubCategoryChart_Click(object sender, ImageMapEventArgs e)
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

            Session["BudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue"] = e.PostBackValue;

            string budgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue = e.PostBackValue;

            string DEBT_REPAYMENT_SUBCATEGORY = GetBudgetDebtRepaymentDetails_DebtRepaymentSubCategoryByPostBackValue(budgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue);

            PopulateBudgetDebtRepaymentChart();
            PopulateBudgetDebtRepaymentSubCategoryChart();
            PopulateBudgetDebtRepaymentAmountAllocatedGridView(DEBT_REPAYMENT_SUBCATEGORY);
            PopulateBudgetExpenditureDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
            PopulateBudgetTransactionDebtRepaymentGridView(DEBT_REPAYMENT_SUBCATEGORY);
        } // BudgetDebtRepaymentSubCategoryChart_Click()


        public string GetBudgetDebtRepaymentDetails_DebtRepaymentSubCategoryByPostBackValue(string strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue)
        {
            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            string DEBT_REPAYMENT_SUBCATEGORY = "";

            // Check whether the strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue is valid
            if (String.IsNullOrWhiteSpace(strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue) == false)
            {
                DataTable dtDebtRepaymentSubCategory = null;

                if (ViewState["BudgetDebtRepaymentDetails_DebtRepaymentSubCategory"] != null)
                {
                    dtDebtRepaymentSubCategory = (DataTable)ViewState["BudgetDebtRepaymentDetails_DebtRepaymentSubCategory"];

                    if (dtDebtRepaymentSubCategory != null)
                    {
                        if (dtDebtRepaymentSubCategory.Rows.Count > 0)
                        {
                            for (int i = 0; (i < dtDebtRepaymentSubCategory.Rows.Count); i++)
                            {
                                // Check whether the Debt Repayment SubCategory PostBackValue is valid
                                if (strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue == dtDebtRepaymentSubCategory.Rows[i]["Debt Repayment SubCategory PostBackValue"].ToString())
                                {
                                    // Check whether the Debt Repayment Category is valid
                                    if (DEBT_REPAYMENT_CATEGORY == dtDebtRepaymentSubCategory.Rows[i]["Debt Repayment Category"].ToString())
                                    {
                                        // Retrieve the Debt Repayment SubCategory from the DataTable
                                        DEBT_REPAYMENT_SUBCATEGORY = dtDebtRepaymentSubCategory.Rows[i]["Debt Repayment SubCategory"].ToString();
                                        break;
                                    } // if (strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue)

                                } // if (strBudgetDebtRepaymentDetails_DebtRepaymentSubCategory_PostBackValue)
                            } // for (i)
                        } // if (dtDebtRepaymentSubCategory.Rows.Count)
                    } // if (dtDebtRepaymentSubCategory)
                } // if (ViewState["BudgetDebtRepaymentDetails_DebtRepaymentSubCategory"])
            }
            else
            {
                DEBT_REPAYMENT_SUBCATEGORY = "";
            } // if (DEBT_REPAYMENT_SUBCATEGORY)

            return DEBT_REPAYMENT_SUBCATEGORY;
        } // GetBudgetDebtRepaymentDetails_DebtRepaymentSubCategoryByPostBackValue()


        protected void BtnBackToBudgetSummary_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetSummary.aspx");
        } // BtnBackToBudgetSummary_Click()


        protected void BtnBackToBudgetDashBoard_Click(object sender, EventArgs e)
        {
            Response.Redirect("BudgetCenterDashBoard.aspx");
        } // BtnBackToBudgetDashBoard_Click()

    } // BudgetDebtRepaymentDetails
} // PrestoPay