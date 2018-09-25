using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;
using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;

namespace PrestoPay
{
    
    public partial class CategorisePersonalTransactionIntoBudgetCenter : System.Web.UI.Page
    {
        // string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            if (!IsPostBack)
            {
                ddlCategory.Items.Clear();

                setInitialIncomeCategory();
                setInitialFixedCostCategory();
                setInitialFlexSpendingCategory();
                setInitialDebtRepaymentCategory();
                setInitialPriorityGoalsCategory();
            } // if(!IsPostBack)
        } // Page_Load()


        private void setInitialIncomeCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // ddlCategory.Items.Clear();

            string INCOME_CATEGORY = "Income";

            int intIncomeCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetIncomeCategory> budgetIncomeCategoryList = new List<BudgetIncomeCategory>();
            BudgetIncomeCategoryDAO budgetIncomeCategoryDAO = new BudgetIncomeCategoryDAO();

            // Read the user's category and subCategory from the BudgetIncomeCategory DB Table
            budgetIncomeCategoryList = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, INCOME_CATEGORY);

            int rec_cnt = 0;
            if (budgetIncomeCategoryList != null)
            {
                rec_cnt = budgetIncomeCategoryList.Count;
            } // if (budgetIncomeCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt <= 0)
            {
                // Read the default category and subCategory from the BudgetIncomeCategory DB Table

                string strAcc_email = "";
                budgetIncomeCategoryList = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailAndCategory(strAcc_email, INCOME_CATEGORY);

                rec_cnt = 0;
                if (budgetIncomeCategoryList != null)
                {
                    rec_cnt = budgetIncomeCategoryList.Count;
                } // if (budgetIncomeCategoryList)
            } // if (rec_cnt)

            string strIncomeCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetIncomeCategory budgetIncomeCategoryObj = budgetIncomeCategoryList[i];

                    if((budgetIncomeCategoryObj.budget_incomeCategory != "") && (strIncomeCategory != budgetIncomeCategoryObj.budget_incomeCategory))
                    {
                        strIncomeCategory = budgetIncomeCategoryObj.budget_incomeCategory;
                        ddlCategory.Items.Add(strIncomeCategory);

                        intIncomeCategoryRowCount += 1;
                    } // if(strIncomeCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intIncomeCategoryRowCount == 0)
            {
                strIncomeCategory = INCOME_CATEGORY;
                ddlCategory.Items.Add(strIncomeCategory);

                intIncomeCategoryRowCount += 1;
            } //if (intIncomeCategoryRowCount)

            ddlCategory_SelectedIndexChanged(null, null);
        } // setInitialIncomeCategory()


        private void setInitialFixedCostCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // ddlCategory.Items.Clear();

            string FIXED_COST_CATEGORY = "Fixed Cost";

            int intFixedCostCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY);

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
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FIXED_COST_CATEGORY);

                rec_cnt = 0;
                if (budgetFixedCostCategoryList != null)
                {
                    rec_cnt = budgetFixedCostCategoryList.Count;
                } // if (budgetFixedCostCategoryList)
            } // if (rec_cnt)

            string strFixedCostCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetFixedCostCategoryObj = budgetFixedCostCategoryList[i];

                    if ((budgetFixedCostCategoryObj.budget_expenditureCategory != "") && (strFixedCostCategory != budgetFixedCostCategoryObj.budget_expenditureCategory))
                    {
                        strFixedCostCategory = budgetFixedCostCategoryObj.budget_expenditureCategory;
                        ddlCategory.Items.Add(strFixedCostCategory);

                        intFixedCostCategoryRowCount += 1;
                    } // if(strFixedCostCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intFixedCostCategoryRowCount == 0)
            {
                strFixedCostCategory = FIXED_COST_CATEGORY;
                ddlCategory.Items.Add(strFixedCostCategory);

                intFixedCostCategoryRowCount += 1;
            } //if (intFixedCostCategoryRowCount)

            ddlCategory_SelectedIndexChanged(null, null);
        } // setInitialFixedCostCategory()


        private void setInitialFlexSpendingCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // ddlCategory.Items.Clear();

            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            int intFlexSpendingCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY);

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
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FLEX_SPENDING_CATEGORY);

                rec_cnt = 0;
                if (budgetFlexSpendingCategoryList != null)
                {
                    rec_cnt = budgetFlexSpendingCategoryList.Count;
                } // if (budgetFlexSpendingCategoryList)
            } // if (rec_cnt)

            string strFlexSpendingCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryList[i];

                    if ((budgetFlexSpendingCategoryObj.budget_expenditureCategory != "") && (strFlexSpendingCategory != budgetFlexSpendingCategoryObj.budget_expenditureCategory))
                    {
                        strFlexSpendingCategory = budgetFlexSpendingCategoryObj.budget_expenditureCategory;
                        ddlCategory.Items.Add(strFlexSpendingCategory);

                        intFlexSpendingCategoryRowCount += 1;
                    } // if(strFlexSpendingCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intFlexSpendingCategoryRowCount == 0)
            {
                strFlexSpendingCategory = FLEX_SPENDING_CATEGORY;
                ddlCategory.Items.Add(strFlexSpendingCategory);

                intFlexSpendingCategoryRowCount += 1;
            } //if (intFlexSpendingCategoryRowCount)

            ddlCategory_SelectedIndexChanged(null, null);
        } // setInitialFlexSpendingCategory()


        private void setInitialDebtRepaymentCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // ddlCategory.Items.Clear();

            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            int intDebtRepaymentCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY);

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
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(strAcc_email, DEBT_REPAYMENT_CATEGORY);

                rec_cnt = 0;
                if (budgetDebtRepaymentCategoryList != null)
                {
                    rec_cnt = budgetDebtRepaymentCategoryList.Count;
                } // if (budgetDebtRepaymentCategoryList)
            } // if (rec_cnt)

            string strDebtRepaymentCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryList[i];

                    if ((budgetDebtRepaymentCategoryObj.budget_expenditureCategory != "") && (strDebtRepaymentCategory != budgetDebtRepaymentCategoryObj.budget_expenditureCategory))
                    {
                        strDebtRepaymentCategory = budgetDebtRepaymentCategoryObj.budget_expenditureCategory;
                        ddlCategory.Items.Add(strDebtRepaymentCategory);

                        intDebtRepaymentCategoryRowCount += 1;
                    } // if(strDebtRepaymentCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intDebtRepaymentCategoryRowCount == 0)
            {
                strDebtRepaymentCategory = DEBT_REPAYMENT_CATEGORY;
                ddlCategory.Items.Add(strDebtRepaymentCategory);

                intDebtRepaymentCategoryRowCount += 1;
            } //if (intDebtRepaymentCategoryRowCount)

            ddlCategory_SelectedIndexChanged(null, null);
        } // setInitialDebtRepaymentCategory()


        private void setInitialPriorityGoalsCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            // ddlCategory.Items.Clear();

            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            int intPriorityGoalsCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY);

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
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(strAcc_email, PRIORITY_GOALS_CATEGORY);

                rec_cnt = 0;
                if (budgetPriorityGoalsCategoryList != null)
                {
                    rec_cnt = budgetPriorityGoalsCategoryList.Count;
                } // if (budgetPriorityGoalsCategoryList)
            } // if (rec_cnt)

            string strPriorityGoalsCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryList[i];

                    if ((budgetPriorityGoalsCategoryObj.budget_expenditureCategory != "") && (strPriorityGoalsCategory != budgetPriorityGoalsCategoryObj.budget_expenditureCategory))
                    {
                        strPriorityGoalsCategory = budgetPriorityGoalsCategoryObj.budget_expenditureCategory;
                        ddlCategory.Items.Add(strPriorityGoalsCategory);

                        intPriorityGoalsCategoryRowCount += 1;
                    } // if(strPriorityGoalsCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intPriorityGoalsCategoryRowCount == 0)
            {
                strPriorityGoalsCategory = PRIORITY_GOALS_CATEGORY;
                ddlCategory.Items.Add(strPriorityGoalsCategory);

                intPriorityGoalsCategoryRowCount += 1;
            } //if (intPriorityGoalsCategoryRowCount)

            ddlCategory_SelectedIndexChanged(null, null);
        } // setInitialPriorityGoalsCategory()


        //protected void setInitialExpenditureCategory()
        //{
        //    string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connStr);
        //    SqlCommand cmd = new SqlCommand("SELECT DISTINCT (budget_expenditureCategory) FROM BudgetExpenditureCategory ORDER BY budget_expenditureCategory ASC ", conn);

        //    // ddlCategory.Items.Clear();

        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            ddlCategory.Items.Add(reader.GetSqlString(0).ToString());
        //        } // while (reader)
        //        reader.Close();

        //        ddlCategory_SelectedIndexChanged(null, null);
        //    } // try
        //    catch (Exception ex)
        //    {

        //    } // catch

        //    conn.Close();

        //} // setInitialExpenditureCategory()


        protected void BtnSubmit_Click(object sender, EventArgs e)
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


            if (String.IsNullOrWhiteSpace(tbNameOfTransaction.Text))
            {
                Lbl_err.Text = "Error: Please enter a valid Name Of Transaction. ";
                PanelErrorResult.Visible = true;

                return;
            } // if (String.IsNullOrWhiteSpace(tbNameOfTransaction.Text))

            if (String.IsNullOrWhiteSpace(tbAmountForTransaction.Text))
            {
                Lbl_err.Text = "Error: Please enter a valid Amount For Transaction. ";
                PanelErrorResult.Visible = true;

                return;
            } // if (String.IsNullOrWhiteSpace(tbAmountForTransaction.Text))

            if (CalendarDateOfTransaction.SelectedDate.Year == 1)
            {
                Lbl_err.Text = "Error: Please enter a valid Date of Transaction. ";
                PanelErrorResult.Visible = true;

                return;
            } // if (CalendarDateOfTransaction.SelectedDate.Year)


            string strNameOfTransaction = Convert.ToString(tbNameOfTransaction.Text); // Remarks
            double strAmountForTransaction = Convert.ToDouble(tbAmountForTransaction.Text);
            DateTime dtmDateOfTransaction = Convert.ToDateTime(CalendarDateOfTransaction.SelectedDate);
            string strCategory = ddlCategory.SelectedValue.ToString();
            string strSubCategory = ddlSubCategory.SelectedValue.ToString();

            BudgetExpenditurePersonalTransactionDAO budgetPersonalDao = new BudgetExpenditurePersonalTransactionDAO();

            if (strCategory != "Income")
            {
                string BUDGET_EXPENDITURE_ID = budgetPersonalDao.InsertPersonalExpenseTransactionIntoBudgetExpenditureTableByEmail(ACC_EMAIL, strCategory, strSubCategory, strAmountForTransaction, dtmDateOfTransaction, strNameOfTransaction);

                if (BUDGET_EXPENDITURE_ID != null)
                {
                    // Display the updated loan status
                    //GetLoanLimitsByBusiId();
                    //GetLoanStatusByBusiId();

                    Lbl_err.Text = "Your Expense " + BUDGET_EXPENDITURE_ID + " has been categorised into Budgeting Center successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    Lbl_err.Text = "Sorry, an error occurred while saving your Expense. Please inform System Administrator.";
                    PanelErrorResult.Visible = true;
                } // if (BUDGET_EXPENDITURE_ID)
            }
            else
            {
                string BUDGET_INCOME_ID = budgetPersonalDao.InsertPersonalIncomeTransactionIntoBudgetIncomeTableByEmail(ACC_EMAIL, strCategory, strSubCategory, strAmountForTransaction, dtmDateOfTransaction, strNameOfTransaction);

                if (BUDGET_INCOME_ID != null)
                {
                    // Display the updated loan status
                    //GetLoanLimitsByBusiId();
                    //GetLoanStatusByBusiId();

                    Lbl_err.Text = "Your Income " + BUDGET_INCOME_ID + " has been categorised into Budgetting Center successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    Lbl_err.Text = "Sorry, an error occurred while saving your Income. Please inform System Administrator.";
                    PanelErrorResult.Visible = true;
                } // if (BUDGET_INCOME_ID)
            } // if (strCategory)
        } // BtnSubmit_Click()


        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            string strCategory = ddlCategory.SelectedValue.ToString();

            ddlSubCategory.Items.Clear();

            if (strCategory == "Income")
            {
                setIncomeSubCategory();
            }
            else if (strCategory == "Fixed Cost")
            {
                setFixedCostSubCategory();
            }
            else if (strCategory == "Flex Spending")
            {
                setFlexSpendingSubCategory();
            }
            else if (strCategory == "Debt Repayment")
            {
                setDebtRepaymentSubCategory();
            }
            else if (strCategory == "Priority Goals")
            {
                setPriorityGoalsSubCategory();
            } // if (strCategory)
        } // ddlCategory_SelectedIndexChanged()


        //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Lbl_err.Text = String.Empty;
        //    PanelErrorResult.Visible = false;

        //    string connStr = ConfigurationManager.ConnectionStrings["PrestoConn"].ConnectionString;
        //    SqlConnection conn = new SqlConnection(connStr);
        //    string strCategory = ddlCategory.SelectedValue.ToString();
        //    SqlCommand cmd = new SqlCommand("");

        //    if (strCategory != "Income")
        //    {
        //        cmd = new SqlCommand("SELECT DISTINCT (budget_expenditureSubCategory) FROM BudgetExpenditureCategory WHERE budget_expenditureCategory ='" + strCategory + "' ORDER BY budget_expenditureSubCategory ASC ", conn);
        //    }
        //    else
        //    {
        //        cmd = new SqlCommand("SELECT DISTINCT (budget_incomeSubCategory) FROM BudgetIncomeCategory WHERE budget_incomeCategory ='" + strCategory + "' ORDER BY budget_incomeSubCategory ASC ", conn);
        //    } // if (strCategory)

        //    ddlSubCategory.Items.Clear();

        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            ddlSubCategory.Items.Add(reader.GetSqlString(0).ToString());
        //        } // while (reader)

        //        reader.Close();
        //    } // try
        //    catch (Exception ex)
        //    {

        //    } // catch

        //    conn.Close();
        //} // ddlCategory_SelectedIndexChanged()


        private void setIncomeSubCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            ddlSubCategory.Items.Clear();

            string INCOME_CATEGORY = "Income";

            int intIncomeSubCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetIncomeCategory> budgetIncomeCategoryList = new List<BudgetIncomeCategory>();
            BudgetIncomeCategoryDAO budgetIncomeCategoryDAO = new BudgetIncomeCategoryDAO();

            // Read the user's category and subCategory from the BudgetIncomeCategory DB Table
            budgetIncomeCategoryList = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, INCOME_CATEGORY);

            int rec_cnt = 0;
            if (budgetIncomeCategoryList != null)
            {
                rec_cnt = budgetIncomeCategoryList.Count;
            } // if (budgetIncomeCategoryList)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt <= 0)
            {
                // Read the default category and subCategory from the BudgetIncomeCategory DB Table

                string strAcc_email = "";
                budgetIncomeCategoryList = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailAndCategory(strAcc_email, INCOME_CATEGORY);

                rec_cnt = 0;
                if (budgetIncomeCategoryList != null)
                {
                    rec_cnt = budgetIncomeCategoryList.Count;
                } // if (budgetIncomeCategoryList)
            } // if (rec_cnt)

            string strIncomeSubCategory = "";

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetIncomeCategory budgetIncomeCategoryObj = budgetIncomeCategoryList[i];

                    if ((budgetIncomeCategoryObj.budget_incomeSubCategory != "") && (strIncomeSubCategory != budgetIncomeCategoryObj.budget_incomeSubCategory))
                    {
                        strIncomeSubCategory = budgetIncomeCategoryObj.budget_incomeSubCategory;
                        ddlSubCategory.Items.Add(strIncomeSubCategory);

                        intIncomeSubCategoryRowCount += 1;
                    } // if(strIncomeSubCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intIncomeSubCategoryRowCount == 0)
            {
                strIncomeSubCategory = "Director Fees";
                ddlSubCategory.Items.Add(strIncomeSubCategory);
                intIncomeSubCategoryRowCount += 1;

                strIncomeSubCategory = "Dividend";
                ddlSubCategory.Items.Add(strIncomeSubCategory);
                intIncomeSubCategoryRowCount += 1;

                strIncomeSubCategory = "Part Time Salary";
                ddlSubCategory.Items.Add(strIncomeSubCategory);
                intIncomeSubCategoryRowCount += 1;

                strIncomeSubCategory = "Salary";
                ddlSubCategory.Items.Add(strIncomeSubCategory);
                intIncomeSubCategoryRowCount += 1;
            } //if (intIncomeSubCategoryRowCount)
        } // setIncomeSubCategory()


        private void setFixedCostSubCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            ddlSubCategory.Items.Clear();

            string FIXED_COST_CATEGORY = "Fixed Cost";

            int intFixedCostSubCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetFixedCostCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FIXED_COST_CATEGORY);

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
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FIXED_COST_CATEGORY);

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
                        ddlSubCategory.Items.Add(strFixedCostSubCategory);

                        intFixedCostSubCategoryRowCount += 1;
                    } // if(strFixedCostSubCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intFixedCostSubCategoryRowCount == 0)
            {
                strFixedCostSubCategory = "Insurance";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;

                strFixedCostSubCategory = "Phone Bill";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;

                strFixedCostSubCategory = "Rent";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;

                strFixedCostSubCategory = "Subscription";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;

                strFixedCostSubCategory = "Transportation";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;

                strFixedCostSubCategory = "Utilities";
                ddlSubCategory.Items.Add(strFixedCostSubCategory);
                intFixedCostSubCategoryRowCount += 1;
            } //if (intFixedCostSubCategoryRowCount)
        } // setFixedCostSubCategory()


        private void setFlexSpendingSubCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            ddlSubCategory.Items.Clear();

            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            int intFlexSpendingSubCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetFlexSpendingCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY);

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
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FLEX_SPENDING_CATEGORY);

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
                        ddlSubCategory.Items.Add(strFlexSpendingSubCategory);

                        intFlexSpendingSubCategoryRowCount += 1;
                    } // if(strFlexSpendingSubCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intFlexSpendingSubCategoryRowCount == 0)
            {
                strFlexSpendingSubCategory = "ATM Cash Withdrawal";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Entertainment";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Food";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Gift";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Groceries";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Health";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Household Items";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Overseas Travel";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Personal Care";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Service Charge";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;

                strFlexSpendingSubCategory = "Shopping";
                ddlSubCategory.Items.Add(strFlexSpendingSubCategory);
                intFlexSpendingSubCategoryRowCount += 1;
            } //if (intFlexSpendingSubCategoryRowCount)
        } // setFlexSpendingSubCategory()


        private void setDebtRepaymentSubCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            ddlSubCategory.Items.Clear();

            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            int intDebtRepaymentSubCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetDebtRepaymentCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY);

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
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(strAcc_email, DEBT_REPAYMENT_CATEGORY);

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
                        ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);

                        intDebtRepaymentSubCategoryRowCount += 1;
                    } // if(strDebtRepaymentSubCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intDebtRepaymentSubCategoryRowCount == 0)
            {
                strDebtRepaymentSubCategory = "Credit Card Debt";
                ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);
                intDebtRepaymentSubCategoryRowCount += 1;

                strDebtRepaymentSubCategory = "Housing Loan";
                ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);
                intDebtRepaymentSubCategoryRowCount += 1;

                strDebtRepaymentSubCategory = "Mortgage";
                ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);
                intDebtRepaymentSubCategoryRowCount += 1;

                strDebtRepaymentSubCategory = "Personal Loan";
                ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);
                intDebtRepaymentSubCategoryRowCount += 1;

                strDebtRepaymentSubCategory = "Study Loan";
                ddlSubCategory.Items.Add(strDebtRepaymentSubCategory);
                intDebtRepaymentSubCategoryRowCount += 1;
            } //if (intDebtRepaymentSubCategoryRowCount)
        } // setDebtRepaymentSubCategory()


        private void setPriorityGoalsSubCategory()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            ddlSubCategory.Items.Clear();

            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            int intPriorityGoalsSubCategoryRowCount = 0;

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

            // Check whether Category and SubCategory already exist in the DB
            List<BudgetExpenditureCategory> budgetPriorityGoalsCategoryList = new List<BudgetExpenditureCategory>();
            BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

            // Read the user's category and subCategory from the BudgetExpenditureCategory DB Table
            budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY);

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
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(strAcc_email, PRIORITY_GOALS_CATEGORY);

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
                        ddlSubCategory.Items.Add(strPriorityGoalsSubCategory);

                        intPriorityGoalsSubCategoryRowCount += 1;
                    } // if(strPriorityGoalsSubCategory)
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (intPriorityGoalsSubCategoryRowCount == 0)
            {
                strPriorityGoalsSubCategory = "Educational Savings";
                ddlSubCategory.Items.Add(strPriorityGoalsSubCategory);
                intPriorityGoalsSubCategoryRowCount += 1;

                strPriorityGoalsSubCategory = "General Savings";
                ddlSubCategory.Items.Add(strPriorityGoalsSubCategory);
                intPriorityGoalsSubCategoryRowCount += 1;

                strPriorityGoalsSubCategory = "Retirement Savings";
                ddlSubCategory.Items.Add(strPriorityGoalsSubCategory);
                intPriorityGoalsSubCategoryRowCount += 1;
            } //if (intPriorityGoalsSubCategoryRowCount)
        } // setPriorityGoalsSubCategory()


        protected void CalendarDateOfTransaction_SelectionChanged(object sender, EventArgs e)
        {
            lblDateOfTransaction.Text = GetDateString(CalendarDateOfTransaction.SelectedDate.ToString());
        } // CalendarDateOfTransaction_SelectionChanged()


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

    } // CategoriseTransactionIntoBudgetCenter
} // PrestoPay