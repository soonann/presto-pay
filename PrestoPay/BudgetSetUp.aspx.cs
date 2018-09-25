using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using PrestoPay.Entity.DB_Entities;

namespace PrestoPay
{
    public partial class BudgetSetUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            if (!Page.IsPostBack)
            {
                SetInitialIncomeRow();
                SetInitialFixedCostRow();
                SetInitialFlexSpendingRow();
                SetInitialDebtRepaymentRow();
                SetInitialPriorityGoalsRow();
            } // if (!Page.IsPostBack)
        } // Page_Load()


        private void SetInitialIncomeRow()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            Session["BudgetSetUp_tbCurrentIncomeCategory"] = "Income";
            string INCOME_CATEGORY = "Income";

            Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = 0;
            int intCurrentIncomeCategoryProtectedRowCount = 0;

            DataTable dtIncomeFullTable = new DataTable();
            dtIncomeFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtIncomeFullTable.Columns.Add(new DataColumn("Income Category", typeof(string)));
            dtIncomeFullTable.Columns.Add(new DataColumn("Income SubCategory", typeof(string)));
            dtIncomeFullTable.Columns.Add(new DataColumn("Income Amount Allocated", typeof(string)));

            DataTable dtIncome = new DataTable();
            dtIncome.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            // dtIncome.Columns.Add(new DataColumn("Income Category", typeof(string)));
            // dtIncome.Columns.Add(new DataColumn("Income SubCategory", typeof(string)));
            // dtIncome.Columns.Add(new DataColumn("Income Amount Allocated", typeof(string)));

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

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetIncomeCategory budgetIncomeCategoryObj = budgetIncomeCategoryList[i];

                    DataRow drIncomeFullTable = dtIncomeFullTable.NewRow();
                    drIncomeFullTable["RowNumber"] = i + 1;
                    drIncomeFullTable["Income Category"] = budgetIncomeCategoryObj.budget_incomeCategory;
                    drIncomeFullTable["Income SubCategory"] = budgetIncomeCategoryObj.budget_incomeSubCategory;
                    drIncomeFullTable["Income Amount Allocated"] = string.Empty;
                    dtIncomeFullTable.Rows.Add(drIncomeFullTable);

                    DataRow drIncome = dtIncome.NewRow();
                    drIncome["RowNumber"] = i + 1;
                    // drIncome["Income Category"] = budgetIncomeCategoryObj.budget_incomeCategory;
                    // drIncome["Income SubCategory"] = budgetIncomeCategoryObj.budget_incomeSubCategory;
                    // drIncome["Income Amount Allocated"] = string.Empty;
                    dtIncome.Rows.Add(drIncome);

                    Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = i + 1;
                    intCurrentIncomeCategoryProtectedRowCount = i + 1;
                } //  for (i)
            } // if (rec_cnt)

            // Populate the DataTable
            if (dtIncomeFullTable.Rows.Count == 0)
            {
                DataRow drIncomeFullTable = dtIncomeFullTable.NewRow();
                drIncomeFullTable["RowNumber"] = 1;
                drIncomeFullTable["Income Category"] = INCOME_CATEGORY;
                drIncomeFullTable["Income SubCategory"] = string.Empty;
                drIncomeFullTable["Income Amount Allocated"] = string.Empty;
                dtIncomeFullTable.Rows.Add(drIncomeFullTable);

                DataRow drIncome = dtIncome.NewRow();
                drIncome["RowNumber"] = 1;
                // drIncome["Income Category"] = INCOME_CATEGORY;
                // drIncome["Income SubCategory"] = string.Empty;
                // drIncome["Income Amount Allocated"] = string.Empty;
                dtIncome.Rows.Add(drIncome);

                intCurrentIncomeCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = intCurrentIncomeCategoryProtectedRowCount;
            } //if (dtIncomeFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] = dtIncomeFullTable;
            ViewState["BudgetSetUp_dtCurrentIncome"] = dtIncome;

            GridviewBudgetSetUpIncome.DataSource = dtIncome;
            GridviewBudgetSetUpIncome.AutoGenerateColumns = false;
            GridviewBudgetSetUpIncome.DataBind();

            // Populate the TextBoxes
            if ((dtIncomeFullTable.Rows.Count > 0) && (GridviewBudgetSetUpIncome.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtIncomeFullTable.Rows.Count) && (i < GridviewBudgetSetUpIncome.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtIncomeFullTable.Rows[i]["Income Category"].ToString();
                    box2.Text = dtIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                    box3.Text = dtIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                    if (i > (intCurrentIncomeCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtIncomeFullTable.Rows.Count)
        } // SetInitialIncomeRow()


        private void AddNewRowToGridviewBudgetSetUpIncome()
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

            string strIncomeCategory = "Income";
            if (Session["BudgetSetUp_tbCurrentIncomeCategory"] != null)
            {
                strIncomeCategory = (string)Session["BudgetSetUp_tbCurrentIncomeCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentIncomeCategory"] = "Income";
                strIncomeCategory = "Income";
            } // if (Session["BudgetSetUp_tbCurrentIncomeCategory"])

            int intCurrentIncomeCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] != null)
            {
                intCurrentIncomeCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = 0;
                intCurrentIncomeCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"])

            DataTable dtCurrentIncomeFullTable = null;
            DataTable dtCurrentIncome = null;

            DataRow drCurrentIncomeFullTable = null;
            DataRow drCurrentIncome = null;

            if (ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentIncome"] != null)
                {
                    dtCurrentIncomeFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentIncomeFullTable"];
                    dtCurrentIncome = (DataTable)ViewState["BudgetSetUp_dtCurrentIncome"];

                    drCurrentIncomeFullTable = null;
                    drCurrentIncome = null;

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentIncomeFullTable.Rows.Count > 0) && (GridviewBudgetSetUpIncome.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentIncomeFullTable.Rows.Count) && (i < GridviewBudgetSetUpIncome.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentIncomeFullTable.Rows[i]["Income Category"].ToString();
                            // box2.Text = dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                            // box3.Text = dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentIncomeFullTable.Rows[i]["Income Category"] = box1.Text;
                            dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"] = box2.Text;
                            dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = box3.Text;

                            string strIncomeAmountAllocated = box3.Text;
                            double dblIncomeAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strIncomeAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strIncomeAmountAllocated, out dblIncomeAmountAllocated);
                                if (result == true)
                                {
                                    dblIncomeAmountAllocated = double.Parse(strIncomeAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strIncomeAmountAllocated = strIncomeAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strIncomeAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblIncomeAmountAllocated = Math.Round(dblIncomeAmountAllocated, 2);
                            if (dblIncomeAmountAllocated == 0.0)
                            {
                                dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = Convert.ToString(dblIncomeAmountAllocated);
                            } // if (dblIncomeAmountAllocated)      

                            // Read values from TextBoxes
                            string INCOME_CATEGORY = box1.Text;
                            string INCOME_SUBCATEGORY = box2.Text;

                            // Check whether Category and SubCategory already exist in the DB
                            BudgetIncomeCategory budgetIncomeCategoryObj = new BudgetIncomeCategory();
                            BudgetIncomeCategoryDAO budgetIncomeCategoryDAO = new BudgetIncomeCategoryDAO();

                            if ((ACC_EMAIL != "") && (INCOME_CATEGORY != "") && (INCOME_SUBCATEGORY != ""))
                            {
                                budgetIncomeCategoryObj = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                                int budgetIncomeCategoryResult = 0;
                                if (budgetIncomeCategoryObj == null)
                                {
                                    budgetIncomeCategoryResult = budgetIncomeCategoryDAO.InsertBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                                    if (budgetIncomeCategoryResult > 0)
                                    {
                                        Lbl_err.Text = "Your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + " have been saved successfully!";
                                        PanelErrorResult.Visible = true;
                                    }
                                    else
                                    {
                                        Lbl_err.Text = "Sorry, an error occurred while saving your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + ". Please inform System Administrator.";
                                        PanelErrorResult.Visible = true;
                                    } // if (budgetIncomeCategoryResult)
                                } // if(budgetIncomeCategoryObj)
                            } // if((ACC_EMAIL)

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentIncomeFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] = dtCurrentIncomeFullTable;
                    ViewState["BudgetSetUp_dtCurrentIncome"] = dtCurrentIncome;

                    GridviewBudgetSetUpIncome.DataSource = dtCurrentIncome;
                    GridviewBudgetSetUpIncome.AutoGenerateColumns = false;
                    GridviewBudgetSetUpIncome.DataBind();
                }
                else
                {
                    Response.Write("AddNewRowToGridviewBudgetSetUpIncome Error: ViewState[dtCurrentIncome] is null");

                    dtCurrentIncomeFullTable = new DataTable();
                    dtCurrentIncome = new DataTable();

                    drCurrentIncomeFullTable = null;
                    drCurrentIncome = null;
                } // if (ViewState["BudgetSetUp_dtCurrentIncome"])
            }
            else
            {
                Response.Write("AddNewRowToGridviewBudgetSetUpIncome Error: ViewState[dtCurrentIncomeFullTable] is null");

                dtCurrentIncomeFullTable = new DataTable();
                dtCurrentIncome = new DataTable();

                drCurrentIncomeFullTable = null;
                drCurrentIncome = null;
            } // if (ViewState["BudgetSetUp_dtCurrentIncomeFullTable"])

            // Add new row to DataTable
            drCurrentIncomeFullTable = dtCurrentIncomeFullTable.NewRow();
            drCurrentIncomeFullTable["RowNumber"] = dtCurrentIncomeFullTable.Rows.Count + 1;
            drCurrentIncomeFullTable["Income Category"] = strIncomeCategory;
            drCurrentIncomeFullTable["Income SubCategory"] = string.Empty;
            drCurrentIncomeFullTable["Income Amount Allocated"] = string.Empty;
            dtCurrentIncomeFullTable.Rows.Add(drCurrentIncomeFullTable);

            drCurrentIncome = dtCurrentIncome.NewRow();
            drCurrentIncome["RowNumber"] = dtCurrentIncome.Rows.Count + 1;
            // drCurrentIncome["Income Category"] = strIncomeCategory;
            // drCurrentIncome["Income SubCategory"] = string.Empty;
            // drCurrentIncome["Income Amount Allocated"] = string.Empty;
            dtCurrentIncome.Rows.Add(drCurrentIncome);

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] = dtCurrentIncomeFullTable;
            ViewState["BudgetSetUp_dtCurrentIncome"] = dtCurrentIncome;

            GridviewBudgetSetUpIncome.DataSource = dtCurrentIncome;
            GridviewBudgetSetUpIncome.AutoGenerateColumns = false;
            GridviewBudgetSetUpIncome.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentIncomeFullTable.Rows.Count > 0) && (GridviewBudgetSetUpIncome.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentIncomeFullTable.Rows.Count) && (i < GridviewBudgetSetUpIncome.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentIncomeFullTable.Rows[i]["Income Category"].ToString();
                    box2.Text = dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                    box3.Text = dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentIncomeFullTable.Rows[i]["Income Category"] = box1.Text;
                    // dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"] = box2.Text;
                    // dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = box3.Text;

                    if (i > (intCurrentIncomeCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentIncomeFullTable.Rows.Count)  
        } // AddNewRowToGridviewBudgetSetUpIncome()


        protected void ButtonAddNewRowToGridviewBudgetSetUpIncome_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            AddNewRowToGridviewBudgetSetUpIncome();
        } // ButtonAddNewRowToGridviewBudgetSetUpIncome_Click()

        protected void DeleteRowFromGridviewBudgetSetUpIncome_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int DEL_INDEX = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "Delete SubCategory and Amount Allocated")
            {
                DEL_INDEX = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[DEL_INDEX]; // grab the row object from the grid

                // This is the way to get a string from a row of the GridView
                // string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Get the addresses of the TextBoxes
                TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[DEL_INDEX].Cells[1].FindControl("tbIncomeCategory");
                TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[DEL_INDEX].Cells[2].FindControl("tbIncomeSubCategory");
                TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[DEL_INDEX].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                // Write values to TextBoxes
                //box1.Text = dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income Category"].ToString();
                //box2.Text = dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income SubCategory"].ToString();
                //box3.Text = dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income Amount Allocated"].ToString();

                // Read values from TextBoxes
                //dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income Category"] = box1.Text;
                //dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income SubCategory"] = box2.Text;
                //dtCurrentIncomeFullTable.Rows[DEL_INDEX]["Income Amount Allocated"] = box3.Text;

                // Read values from TextBoxes
                string INCOME_CATEGORY = box1.Text;
                string INCOME_SUBCATEGORY = box2.Text;

                string strIncomeAmountAllocated = box3.Text;
                double dblIncomeAmountAllocated = 0.0;

                // get the length of the current string
                int intLength = strIncomeAmountAllocated.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strIncomeAmountAllocated, out dblIncomeAmountAllocated);
                    if (result == true)
                    {
                        dblIncomeAmountAllocated = double.Parse(strIncomeAmountAllocated);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strIncomeAmountAllocated = strIncomeAmountAllocated.Substring(1);

                        // get the length of the new string
                        intLength = strIncomeAmountAllocated.Length;
                    } // if (result)
                } // while (intLength)

                dblIncomeAmountAllocated = Math.Round(dblIncomeAmountAllocated, 2);
                if (dblIncomeAmountAllocated == 0.0)
                {
                    box3.Text = string.Empty;
                }
                else
                {
                    box3.Text = Convert.ToString(dblIncomeAmountAllocated);
                } // if (dblIncomeAmountAllocated)

                double INCOME_AMOUNT_ALLOCATED = dblIncomeAmountAllocated;

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

                BudgetIncomeCategoryDAO budgetIncomeCategoryDAO = new BudgetIncomeCategoryDAO();

                int budgetIncomeCategoryResult = 0;
                budgetIncomeCategoryResult = budgetIncomeCategoryDAO.DeleteBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                if (budgetIncomeCategoryResult > 0)
                {
                    Lbl_err.Text = "Your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + " have been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Lbl_err.Text = "Sorry, an error occurred while deleting your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + ". Please inform System Administrator.";
                    // PanelErrorResult.Visible = true;
                } // if (budgetIncomeCategoryResult)

                // BudgetSetUpIncomeDAO budgetSetUpIncomeDAO = new BudgetSetUpIncomeDAO();

                // int budgetSetUpIncomeResult = 0;
                // budgetSetUpIncomeResult = budgetSetUpIncomeDAO.DeleteBudgetSetUpIncomeByBudgetId(BUDGET_ID, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                // if (budgetSetUpIncomeResult > 0)
                // {
                //     Lbl_err.Text = "Your Budget " + BUDGET_ID + " Income Category and SubCategory " + INCOME_SUBCATEGORY + " have been deleted successfully!";
                //     PanelErrorResult.Visible = true;
                // }
                // else
                // {
                //     Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " Income Category and SubCategory " + INCOME_SUBCATEGORY + ". Please inform System Administrator.";
                //     PanelErrorResult.Visible = true;
                // } // if (budgetSetUpIncomeResult)

                // if (budgetIncomeCategoryResult > 0)
                // {
                // Re-populate the GridView
                int deleteRowResult = DeleteRowFromGridviewBudgetSetUpIncome(DEL_INDEX);
                // } // if (budgetIncomeCategoryResult)
            } // if (command)
        } // DeleteRowFromGridviewBudgetSetUpIncome_RowCommand()


        private int DeleteRowFromGridviewBudgetSetUpIncome(int DEL_INDEX)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int deleteRowResult = 0;

            string strIncomeCategory = "Income";
            if (Session["BudgetSetUp_tbCurrentIncomeCategory"] != null)
            {
                strIncomeCategory = (string)Session["BudgetSetUp_tbCurrentIncomeCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentIncomeCategory"] = "Income";
                strIncomeCategory = "Income";
            } // if (Session["BudgetSetUp_tbCurrentIncomeCategory"])

            int intCurrentIncomeCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] != null)
            {
                intCurrentIncomeCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = 0;
                intCurrentIncomeCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"]) 

            DataTable dtCurrentIncomeFullTable = null;
            DataTable dtCurrentIncome = null;

            if (ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentIncome"] != null)
                {
                    dtCurrentIncomeFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentIncomeFullTable"];
                    dtCurrentIncome = (DataTable)ViewState["BudgetSetUp_dtCurrentIncome"];

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentIncomeFullTable.Rows.Count > 0) && (GridviewBudgetSetUpIncome.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentIncomeFullTable.Rows.Count) && (i < GridviewBudgetSetUpIncome.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentIncomeFullTable.Rows[i]["Income Category"].ToString();
                            // box2.Text = dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                            // box3.Text = dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentIncomeFullTable.Rows[i]["Income Category"] = box1.Text;
                            dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"] = box2.Text;
                            dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = box3.Text;

                            string strIncomeAmountAllocated = box3.Text;
                            double dblIncomeAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strIncomeAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strIncomeAmountAllocated, out dblIncomeAmountAllocated);
                                if (result == true)
                                {
                                    dblIncomeAmountAllocated = double.Parse(strIncomeAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strIncomeAmountAllocated = strIncomeAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strIncomeAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblIncomeAmountAllocated = Math.Round(dblIncomeAmountAllocated, 2);
                            if (dblIncomeAmountAllocated == 0.0)
                            {
                                dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = Convert.ToString(dblIncomeAmountAllocated);
                            } // if (dblIncomeAmountAllocated)      

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentIncomeFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] = dtCurrentIncomeFullTable;
                    ViewState["BudgetSetUp_dtCurrentIncome"] = dtCurrentIncome;

                    GridviewBudgetSetUpIncome.DataSource = dtCurrentIncome;
                    GridviewBudgetSetUpIncome.AutoGenerateColumns = false;
                    GridviewBudgetSetUpIncome.DataBind();
                }
                else
                {
                    Response.Write("DeleteRowFromGridviewBudgetSetUpIncome Error: ViewState[dtCurrentIncome] is null");

                    dtCurrentIncomeFullTable = new DataTable();
                    dtCurrentIncome = new DataTable();
                } // if (ViewState["BudgetSetUp_dtCurrentIncome"])
            }
            else
            {
                Response.Write("DeleteRowFromGridviewBudgetSetUpIncome Error: ViewState[dtCurrentIncomeFullTable] is null");

                dtCurrentIncomeFullTable = new DataTable();
                dtCurrentIncome = new DataTable();
            } // if (ViewState["BudgetSetUp_dtCurrentIncomeFullTable"])

            if (dtCurrentIncomeFullTable == null)
            {
                dtCurrentIncomeFullTable = new DataTable();
            } // if (dtCurrentIncomeFullTable)

            if (dtCurrentIncome == null)
            {
                dtCurrentIncome = new DataTable();
            } // if (dtCurrentIncome)

            if (DEL_INDEX < dtCurrentIncomeFullTable.Rows.Count)
            {
                // Delete row from DataTable
                DataRow drCurrentIncomeFullTable = dtCurrentIncomeFullTable.Rows[DEL_INDEX];
                dtCurrentIncomeFullTable.Rows.Remove(drCurrentIncomeFullTable);

                DataRow drCurrentIncome = dtCurrentIncome.Rows[DEL_INDEX];
                dtCurrentIncome.Rows.Remove(drCurrentIncome);

                deleteRowResult += 1;

                if (DEL_INDEX < intCurrentIncomeCategoryProtectedRowCount)
                {
                    intCurrentIncomeCategoryProtectedRowCount -= 1;
                } // if (DEL_INDEX)

                if (intCurrentIncomeCategoryProtectedRowCount < 0)
                {
                    intCurrentIncomeCategoryProtectedRowCount = 0;
                } // if (intCurrentIncomeCategoryProtectedRowCount)
            } // if (DEL_INDEX)

            // Populate the DataTable
            if (dtCurrentIncomeFullTable.Rows.Count == 0)
            {
                DataRow drIncomeFullTable = dtCurrentIncomeFullTable.NewRow();
                drIncomeFullTable["RowNumber"] = 1;
                drIncomeFullTable["Income Category"] = strIncomeCategory;
                drIncomeFullTable["Income SubCategory"] = string.Empty;
                drIncomeFullTable["Income Amount Allocated"] = string.Empty;
                dtCurrentIncomeFullTable.Rows.Add(drIncomeFullTable);

                DataRow drIncome = dtCurrentIncome.NewRow();
                drIncome["RowNumber"] = 1;
                // drIncome["Income Category"] = strIncomeCategory;
                // drIncome["Income SubCategory"] = string.Empty;
                // drIncome["Income Amount Allocated"] = string.Empty;
                dtCurrentIncome.Rows.Add(drIncome);

                intCurrentIncomeCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = intCurrentIncomeCategoryProtectedRowCount;
            } //if (dtCurrentIncomeFullTable.Rows.Count)

            // Check whether the number of rows in the DataTables
            if ((dtCurrentIncomeFullTable.Rows.Count > 0) && (dtCurrentIncome.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentIncomeFullTable.Rows.Count) && (i < dtCurrentIncome.Rows.Count)); i++)
                {
                    // Re-write the Row Numbers into the DataTables
                    dtCurrentIncomeFullTable.Rows[i]["RowNumber"] = i + 1;
                    dtCurrentIncome.Rows[i]["RowNumber"] = i + 1;
                } // for (i)
            } //if (dtCurrentIncomeFullTable.Rows.Count)      

            Session["BudgetSetUp_tbCurrentIncomeCategoryProtectedRowCount"] = intCurrentIncomeCategoryProtectedRowCount;

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentIncomeFullTable"] = dtCurrentIncomeFullTable;
            ViewState["BudgetSetUp_dtCurrentIncome"] = dtCurrentIncome;

            GridviewBudgetSetUpIncome.DataSource = dtCurrentIncome;
            GridviewBudgetSetUpIncome.AutoGenerateColumns = false;
            GridviewBudgetSetUpIncome.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentIncomeFullTable.Rows.Count > 0) && (GridviewBudgetSetUpIncome.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentIncomeFullTable.Rows.Count) && (i < GridviewBudgetSetUpIncome.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentIncomeFullTable.Rows[i]["Income Category"].ToString();
                    box2.Text = dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                    box3.Text = dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentIncomeFullTable.Rows[i]["Income Category"] = box1.Text;
                    // dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"] = box2.Text;
                    // dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = box3.Text;

                    if (i > (intCurrentIncomeCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentIncomeFullTable.Rows.Count)

            return deleteRowResult;
        } // DeleteRowFromGridviewBudgetSetUpIncome()


        private void SetInitialFixedCostRow()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            Session["BudgetSetUp_tbCurrentFixedCostCategory"] = "Fixed Cost";
            string FIXED_COST_CATEGORY = "Fixed Cost";

            Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = 0;
            int intCurrentFixedCostCategoryProtectedRowCount = 0;

            DataTable dtFixedCostFullTable = new DataTable();
            dtFixedCostFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            dtFixedCostFullTable.Columns.Add(new DataColumn("Fixed Cost Amount Allocated", typeof(string)));

            DataTable dtFixedCost = new DataTable();
            dtFixedCost.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            // dtFixedCost.Columns.Add(new DataColumn("Fixed Cost Category", typeof(string)));
            // dtFixedCost.Columns.Add(new DataColumn("Fixed Cost SubCategory", typeof(string)));
            // dtFixedCost.Columns.Add(new DataColumn("Fixed Cost Amount Allocated", typeof(string)));

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
                // Read the default category and subCategory from the BudgetExpenditureCategory DB Table

                string strAcc_email = "";
                budgetFixedCostCategoryList = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FIXED_COST_CATEGORY);

                rec_cnt = 0;
                if (budgetFixedCostCategoryList != null)
                {
                    rec_cnt = budgetFixedCostCategoryList.Count;
                } // if (budgetFixedCostCategoryList)
            } // if (rec_cnt)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetFixedCostCategoryObj = budgetFixedCostCategoryList[i];

                    DataRow drFixedCostFullTable = dtFixedCostFullTable.NewRow();
                    drFixedCostFullTable["RowNumber"] = i + 1;
                    drFixedCostFullTable["Fixed Cost Category"] = budgetFixedCostCategoryObj.budget_expenditureCategory;
                    drFixedCostFullTable["Fixed Cost SubCategory"] = budgetFixedCostCategoryObj.budget_expenditureSubCategory;
                    drFixedCostFullTable["Fixed Cost Amount Allocated"] = string.Empty;
                    dtFixedCostFullTable.Rows.Add(drFixedCostFullTable);

                    DataRow drFixedCost = dtFixedCost.NewRow();
                    drFixedCost["RowNumber"] = i + 1;
                    // drFixedCost["Fixed Cost Category"] = budgetFixedCostCategoryObj.budget_expenditureCategory;
                    // drFixedCost["Fixed Cost SubCategory"] = budgetFixedCostCategoryObj.budget_expenditureSubCategory;
                    // drFixedCost["Fixed Cost Amount Allocated"] = string.Empty;
                    dtFixedCost.Rows.Add(drFixedCost);

                    Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = i + 1;
                    intCurrentFixedCostCategoryProtectedRowCount = i + 1;
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

                DataRow drFixedCost = dtFixedCost.NewRow();
                drFixedCost["RowNumber"] = 1;
                // drFixedCost["Fixed Cost Category"] = FIXED_COST_CATEGORY;
                // drFixedCost["Fixed Cost SubCategory"] = string.Empty;
                // drFixedCost["Fixed Cost Amount Allocated"] = string.Empty;
                dtFixedCost.Rows.Add(drFixedCost);

                intCurrentFixedCostCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = intCurrentFixedCostCategoryProtectedRowCount;
            } //if (dtFixedCostFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] = dtFixedCostFullTable;
            ViewState["BudgetSetUp_dtCurrentFixedCost"] = dtFixedCost;

            GridviewBudgetSetUpFixedCost.DataSource = dtFixedCost;
            GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
            GridviewBudgetSetUpFixedCost.DataBind();

            // Populate the TextBoxes
            if ((dtFixedCostFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFixedCost.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtFixedCostFullTable.Rows.Count) && (i < GridviewBudgetSetUpFixedCost.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                    box2.Text = dtFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                    box3.Text = dtFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                    if (i > (intCurrentFixedCostCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtFixedCostFullTable.Rows.Count)
        } // SetInitialFixedCostRow()


        private void AddNewRowToGridviewBudgetSetUpFixedCost()
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

            string strFixedCostCategory = "Fixed Cost";
            if (Session["BudgetSetUp_tbCurrentFixedCostCategory"] != null)
            {
                strFixedCostCategory = (string)Session["BudgetSetUp_tbCurrentFixedCostCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFixedCostCategory"] = "Fixed Cost";
                strFixedCostCategory = "Fixed Cost";
            } // if (Session["BudgetSetUp_tbCurrentFixedCostCategory"])

            int intCurrentFixedCostCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] != null)
            {
                intCurrentFixedCostCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = 0;
                intCurrentFixedCostCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"])

            DataTable dtCurrentFixedCostFullTable = null;
            DataTable dtCurrentFixedCost = null;

            DataRow drCurrentFixedCostFullTable = null;
            DataRow drCurrentFixedCost = null;

            if (ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentFixedCost"] != null)
                {
                    dtCurrentFixedCostFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"];
                    dtCurrentFixedCost = (DataTable)ViewState["BudgetSetUp_dtCurrentFixedCost"];

                    drCurrentFixedCostFullTable = null;
                    drCurrentFixedCost = null;

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentFixedCostFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFixedCost.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentFixedCostFullTable.Rows.Count) && (i < GridviewBudgetSetUpFixedCost.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                            // box2.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                            // box3.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"] = box1.Text;
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"] = box2.Text;
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = box3.Text;

                            string strFixedCostAmountAllocated = box3.Text;
                            double dblFixedCostAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strFixedCostAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strFixedCostAmountAllocated, out dblFixedCostAmountAllocated);
                                if (result == true)
                                {
                                    dblFixedCostAmountAllocated = double.Parse(strFixedCostAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strFixedCostAmountAllocated = strFixedCostAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strFixedCostAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblFixedCostAmountAllocated = Math.Round(dblFixedCostAmountAllocated, 2);
                            if (dblFixedCostAmountAllocated == 0.0)
                            {
                                dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = Convert.ToString(dblFixedCostAmountAllocated);
                            } // if (dblFixedCostAmountAllocated)

                            // Read values from TextBoxes
                            string FIXED_COST_CATEGORY = box1.Text;
                            string FIXED_COST_SUBCATEGORY = box2.Text;

                            // Check whether Category and SubCategory already exist in the DB
                            BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();
                            BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

                            if ((ACC_EMAIL != "") && (FIXED_COST_CATEGORY != "") && (FIXED_COST_SUBCATEGORY != ""))
                            {
                                budgetFixedCostCategoryObj = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                                int budgetFixedCostCategoryResult = 0;
                                if (budgetFixedCostCategoryObj == null)
                                {
                                    budgetFixedCostCategoryResult = budgetFixedCostCategoryDAO.InsertBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                                    if (budgetFixedCostCategoryResult > 0)
                                    {
                                        Lbl_err.Text = "Your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + " have been saved successfully!";
                                        PanelErrorResult.Visible = true;
                                    }
                                    else
                                    {
                                        Lbl_err.Text = "Sorry, an error occurred while saving your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + ". Please inform System Administrator.";
                                        PanelErrorResult.Visible = true;
                                    } // if (budgetFixedCostCategoryResult)
                                } // if(budgetFixedCostCategoryObj)
                            } // if((ACC_EMAIL)

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentFixedCostFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] = dtCurrentFixedCostFullTable;
                    ViewState["BudgetSetUp_dtCurrentFixedCost"] = dtCurrentFixedCost;

                    GridviewBudgetSetUpFixedCost.DataSource = dtCurrentFixedCost;
                    GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
                    GridviewBudgetSetUpFixedCost.DataBind();
                }
                else
                {
                    Response.Write("AddNewRowToGridviewBudgetSetUpFixedCost Error: ViewState[dtCurrentFixedCost] is null");

                    dtCurrentFixedCostFullTable = new DataTable();
                    dtCurrentFixedCost = new DataTable();

                    drCurrentFixedCostFullTable = null;
                    drCurrentFixedCost = null;
                } // if (ViewState["BudgetSetUp_dtCurrentFixedCost"])
            }
            else
            {
                Response.Write("AddNewRowToGridviewBudgetSetUpFixedCost Error: ViewState[dtCurrentFixedCostFullTable] is null");

                dtCurrentFixedCostFullTable = new DataTable();
                dtCurrentFixedCost = new DataTable();

                drCurrentFixedCostFullTable = null;
                drCurrentFixedCost = null;
            } // if (ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"])

            // Add new row to DataTable
            drCurrentFixedCostFullTable = dtCurrentFixedCostFullTable.NewRow();
            drCurrentFixedCostFullTable["RowNumber"] = dtCurrentFixedCostFullTable.Rows.Count + 1;
            drCurrentFixedCostFullTable["Fixed Cost Category"] = strFixedCostCategory;
            drCurrentFixedCostFullTable["Fixed Cost SubCategory"] = string.Empty;
            drCurrentFixedCostFullTable["Fixed Cost Amount Allocated"] = string.Empty;
            dtCurrentFixedCostFullTable.Rows.Add(drCurrentFixedCostFullTable);

            drCurrentFixedCost = dtCurrentFixedCost.NewRow();
            drCurrentFixedCost["RowNumber"] = dtCurrentFixedCost.Rows.Count + 1;
            // drCurrentFixedCost["Fixed Cost Category"] = strFixedCostCategory;
            // drCurrentFixedCost["Fixed Cost SubCategory"] = string.Empty;
            // drCurrentFixedCost["Fixed Cost Amount Allocated"] = string.Empty;
            dtCurrentFixedCost.Rows.Add(drCurrentFixedCost);

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] = dtCurrentFixedCostFullTable;
            ViewState["BudgetSetUp_dtCurrentFixedCost"] = dtCurrentFixedCost;

            GridviewBudgetSetUpFixedCost.DataSource = dtCurrentFixedCost;
            GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
            GridviewBudgetSetUpFixedCost.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentFixedCostFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFixedCost.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFixedCostFullTable.Rows.Count) && (i < GridviewBudgetSetUpFixedCost.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                    box2.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                    box3.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"] = box1.Text;
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"] = box2.Text;
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = box3.Text;

                    if (i > (intCurrentFixedCostCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentFixedCostFullTable.Rows.Count)  
        } // AddNewRowToGridviewBudgetSetUpFixedCost()


        protected void ButtonAddNewRowToGridviewBudgetSetUpFixedCost_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            AddNewRowToGridviewBudgetSetUpFixedCost();
        } // ButtonAddNewRowToGridviewBudgetSetUpFixedCost_Click()

        protected void DeleteRowFromGridviewBudgetSetUpFixedCost_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int DEL_INDEX = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "Delete SubCategory and Amount Allocated")
            {
                DEL_INDEX = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[DEL_INDEX]; // grab the row object from the grid

                // This is the way to get a string from a row of the GridView
                // string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Get the addresses of the TextBoxes
                TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[DEL_INDEX].Cells[1].FindControl("tbFixedCostCategory");
                TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[DEL_INDEX].Cells[2].FindControl("tbFixedCostSubCategory");
                TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[DEL_INDEX].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                // Write values to TextBoxes
                //box1.Text = dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost Category"].ToString();
                //box2.Text = dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost SubCategory"].ToString();
                //box3.Text = dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost Amount Allocated"].ToString();

                // Read values from TextBoxes
                //dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost Category"] = box1.Text;
                //dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost SubCategory"] = box2.Text;
                //dtCurrentFixedCostFullTable.Rows[DEL_INDEX]["Fixed Cost Amount Allocated"] = box3.Text;

                // Read values from TextBoxes
                string FIXED_COST_CATEGORY = box1.Text;
                string FIXED_COST_SUBCATEGORY = box2.Text;

                string strFixedCostAmountAllocated = box3.Text;
                double dblFixedCostAmountAllocated = 0.0;

                // get the length of the current string
                int intLength = strFixedCostAmountAllocated.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strFixedCostAmountAllocated, out dblFixedCostAmountAllocated);
                    if (result == true)
                    {
                        dblFixedCostAmountAllocated = double.Parse(strFixedCostAmountAllocated);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strFixedCostAmountAllocated = strFixedCostAmountAllocated.Substring(1);

                        // get the length of the new string
                        intLength = strFixedCostAmountAllocated.Length;
                    } // if (result)
                } // while (intLength)

                dblFixedCostAmountAllocated = Math.Round(dblFixedCostAmountAllocated, 2);
                if (dblFixedCostAmountAllocated == 0.0)
                {
                    box3.Text = string.Empty;
                }
                else
                {
                    box3.Text = Convert.ToString(dblFixedCostAmountAllocated);
                } // if (dblFixedCostAmountAllocated)

                double FIXED_COST_AMOUNT_ALLOCATED = dblFixedCostAmountAllocated;

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

                BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

                int budgetFixedCostCategoryResult = 0;
                budgetFixedCostCategoryResult = budgetFixedCostCategoryDAO.DeleteBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                if (budgetFixedCostCategoryResult > 0)
                {
                    Lbl_err.Text = "Your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + " have been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Lbl_err.Text = "Sorry, an error occurred while deleting your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + ". Please inform System Administrator.";
                    // PanelErrorResult.Visible = true;
                } // if (budgetFixedCostCategoryResult)

                // BudgetSetUpExpenditureDAO budgetSetUpFixedCostDAO = new BudgetSetUpExpenditureDAO();

                // int budgetSetUpFixedCostResult = 0;
                // budgetSetUpFixedCostResult = budgetSetUpFixedCostDAO.DeleteBudgetSetUpFixedCostByBudgetId(BUDGET_ID, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                // if (budgetSetUpFixedCostResult > 0)
                // {
                //     Lbl_err.Text = "Your Budget " + BUDGET_ID + " Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + " have been deleted successfully!";
                //     PanelErrorResult.Visible = true;
                // }
                // else
                // {
                //     Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + ". Please inform System Administrator.";
                //     PanelErrorResult.Visible = true;
                // } // if (budgetSetUpFixedCostResult)

                // if (budgetFixedCostCategoryResult > 0)
                // {
                // Re-populate the GridView
                int deleteRowResult = DeleteRowFromGridviewBudgetSetUpFixedCost(DEL_INDEX);
                // } // if (budgetFixedCostCategoryResult)
            } // if (command)
        } // DeleteRowFromGridviewBudgetSetUpFixedCost_RowCommand()


        private int DeleteRowFromGridviewBudgetSetUpFixedCost(int DEL_INDEX)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int deleteRowResult = 0;

            string strFixedCostCategory = "Fixed Cost";
            if (Session["BudgetSetUp_tbCurrentFixedCostCategory"] != null)
            {
                strFixedCostCategory = (string)Session["BudgetSetUp_tbCurrentFixedCostCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFixedCostCategory"] = "Fixed Cost";
                strFixedCostCategory = "Fixed Cost";
            } // if (Session["BudgetSetUp_tbCurrentFixedCostCategory"])

            int intCurrentFixedCostCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] != null)
            {
                intCurrentFixedCostCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = 0;
                intCurrentFixedCostCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"]) 

            DataTable dtCurrentFixedCostFullTable = null;
            DataTable dtCurrentFixedCost = null;

            if (ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentFixedCost"] != null)
                {
                    dtCurrentFixedCostFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"];
                    dtCurrentFixedCost = (DataTable)ViewState["BudgetSetUp_dtCurrentFixedCost"];

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentFixedCostFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFixedCost.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentFixedCostFullTable.Rows.Count) && (i < GridviewBudgetSetUpFixedCost.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                            // box2.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                            // box3.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"] = box1.Text;
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"] = box2.Text;
                            dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = box3.Text;

                            string strFixedCostAmountAllocated = box3.Text;
                            double dblFixedCostAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strFixedCostAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strFixedCostAmountAllocated, out dblFixedCostAmountAllocated);
                                if (result == true)
                                {
                                    dblFixedCostAmountAllocated = double.Parse(strFixedCostAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strFixedCostAmountAllocated = strFixedCostAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strFixedCostAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblFixedCostAmountAllocated = Math.Round(dblFixedCostAmountAllocated, 2);
                            if (dblFixedCostAmountAllocated == 0.0)
                            {
                                dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = Convert.ToString(dblFixedCostAmountAllocated);
                            } // if (dblFixedCostAmountAllocated)      

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentFixedCostFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] = dtCurrentFixedCostFullTable;
                    ViewState["BudgetSetUp_dtCurrentFixedCost"] = dtCurrentFixedCost;

                    GridviewBudgetSetUpFixedCost.DataSource = dtCurrentFixedCost;
                    GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
                    GridviewBudgetSetUpFixedCost.DataBind();
                }
                else
                {
                    Response.Write("DeleteRowFromGridviewBudgetSetUpFixedCost Error: ViewState[dtCurrentFixedCost] is null");

                    dtCurrentFixedCostFullTable = new DataTable();
                    dtCurrentFixedCost = new DataTable();
                } // if (ViewState["BudgetSetUp_dtCurrentFixedCost"])
            }
            else
            {
                Response.Write("DeleteRowFromGridviewBudgetSetUpFixedCost Error: ViewState[dtCurrentFixedCostFullTable] is null");

                dtCurrentFixedCostFullTable = new DataTable();
                dtCurrentFixedCost = new DataTable();
            } // if (ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"])

            if (dtCurrentFixedCostFullTable == null)
            {
                dtCurrentFixedCostFullTable = new DataTable();
            } // if (dtCurrentFixedCostFullTable)

            if (dtCurrentFixedCost == null)
            {
                dtCurrentFixedCost = new DataTable();
            } // if (dtCurrentFixedCost)

            if (DEL_INDEX < dtCurrentFixedCostFullTable.Rows.Count)
            {
                // Delete row from DataTable
                DataRow drCurrentFixedCostFullTable = dtCurrentFixedCostFullTable.Rows[DEL_INDEX];
                dtCurrentFixedCostFullTable.Rows.Remove(drCurrentFixedCostFullTable);

                DataRow drCurrentFixedCost = dtCurrentFixedCost.Rows[DEL_INDEX];
                dtCurrentFixedCost.Rows.Remove(drCurrentFixedCost);

                deleteRowResult += 1;

                if (DEL_INDEX < intCurrentFixedCostCategoryProtectedRowCount)
                {
                    intCurrentFixedCostCategoryProtectedRowCount -= 1;
                } // if (DEL_INDEX)

                if (intCurrentFixedCostCategoryProtectedRowCount < 0)
                {
                    intCurrentFixedCostCategoryProtectedRowCount = 0;
                } // if (intCurrentFixedCostCategoryProtectedRowCount)
            } // if (DEL_INDEX)

            // Populate the DataTable
            if (dtCurrentFixedCostFullTable.Rows.Count == 0)
            {
                DataRow drFixedCostFullTable = dtCurrentFixedCostFullTable.NewRow();
                drFixedCostFullTable["RowNumber"] = 1;
                drFixedCostFullTable["Fixed Cost Category"] = strFixedCostCategory;
                drFixedCostFullTable["Fixed Cost SubCategory"] = string.Empty;
                drFixedCostFullTable["Fixed Cost Amount Allocated"] = string.Empty;
                dtCurrentFixedCostFullTable.Rows.Add(drFixedCostFullTable);

                DataRow drFixedCost = dtCurrentFixedCost.NewRow();
                drFixedCost["RowNumber"] = 1;
                // drFixedCost["Fixed Cost Category"] = strFixedCostCategory;
                // drFixedCost["Fixed Cost SubCategory"] = string.Empty;
                // drFixedCost["Fixed Cost Amount Allocated"] = string.Empty;
                dtCurrentFixedCost.Rows.Add(drFixedCost);

                intCurrentFixedCostCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = intCurrentFixedCostCategoryProtectedRowCount;
            } //if (dtCurrentFixedCostFullTable.Rows.Count)

            // Check whether the number of rows in the DataTables
            if ((dtCurrentFixedCostFullTable.Rows.Count > 0) && (dtCurrentFixedCost.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFixedCostFullTable.Rows.Count) && (i < dtCurrentFixedCost.Rows.Count)); i++)
                {
                    // Re-write the Row Numbers into the DataTables
                    dtCurrentFixedCostFullTable.Rows[i]["RowNumber"] = i + 1;
                    dtCurrentFixedCost.Rows[i]["RowNumber"] = i + 1;
                } // for (i)
            } //if (dtCurrentFixedCostFullTable.Rows.Count)      

            Session["BudgetSetUp_tbCurrentFixedCostCategoryProtectedRowCount"] = intCurrentFixedCostCategoryProtectedRowCount;

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFixedCostFullTable"] = dtCurrentFixedCostFullTable;
            ViewState["BudgetSetUp_dtCurrentFixedCost"] = dtCurrentFixedCost;

            GridviewBudgetSetUpFixedCost.DataSource = dtCurrentFixedCost;
            GridviewBudgetSetUpFixedCost.AutoGenerateColumns = false;
            GridviewBudgetSetUpFixedCost.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentFixedCostFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFixedCost.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFixedCostFullTable.Rows.Count) && (i < GridviewBudgetSetUpFixedCost.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                    box2.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                    box3.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"] = box1.Text;
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"] = box2.Text;
                    // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = box3.Text;

                    if (i > (intCurrentFixedCostCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentFixedCostFullTable.Rows.Count)

            return deleteRowResult;
        } // DeleteRowFromGridviewBudgetSetUpFixedCost()


        private void SetInitialFlexSpendingRow()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            Session["BudgetSetUp_tbCurrentFlexSpendingCategory"] = "Flex Spending";
            string FLEX_SPENDING_CATEGORY = "Flex Spending";

            Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = 0;
            int intCurrentFlexSpendingCategoryProtectedRowCount = 0;

            DataTable dtFlexSpendingFullTable = new DataTable();
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            dtFlexSpendingFullTable.Columns.Add(new DataColumn("Flex Spending Amount Allocated", typeof(string)));

            DataTable dtFlexSpending = new DataTable();
            dtFlexSpending.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            // dtFlexSpending.Columns.Add(new DataColumn("Flex Spending Category", typeof(string)));
            // dtFlexSpending.Columns.Add(new DataColumn("Flex Spending SubCategory", typeof(string)));
            // dtFlexSpending.Columns.Add(new DataColumn("Flex Spending Amount Allocated", typeof(string)));

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
                // Read the default category and subCategory from the BudgetExpenditureCategory DB Table

                string strAcc_email = "";
                budgetFlexSpendingCategoryList = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailAndCategory(strAcc_email, FLEX_SPENDING_CATEGORY);

                rec_cnt = 0;
                if (budgetFlexSpendingCategoryList != null)
                {
                    rec_cnt = budgetFlexSpendingCategoryList.Count;
                } // if (budgetFlexSpendingCategoryList)
            } // if (rec_cnt)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryList[i];

                    DataRow drFlexSpendingFullTable = dtFlexSpendingFullTable.NewRow();
                    drFlexSpendingFullTable["RowNumber"] = i + 1;
                    drFlexSpendingFullTable["Flex Spending Category"] = budgetFlexSpendingCategoryObj.budget_expenditureCategory;
                    drFlexSpendingFullTable["Flex Spending SubCategory"] = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;
                    drFlexSpendingFullTable["Flex Spending Amount Allocated"] = string.Empty;
                    dtFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);

                    DataRow drFlexSpending = dtFlexSpending.NewRow();
                    drFlexSpending["RowNumber"] = i + 1;
                    // drFlexSpending["Flex Spending Category"] = budgetFlexSpendingCategoryObj.budget_expenditureCategory;
                    // drFlexSpending["Flex Spending SubCategory"] = budgetFlexSpendingCategoryObj.budget_expenditureSubCategory;
                    // drFlexSpending["Flex Spending Amount Allocated"] = string.Empty;
                    dtFlexSpending.Rows.Add(drFlexSpending);

                    Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = i + 1;
                    intCurrentFlexSpendingCategoryProtectedRowCount = i + 1;
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

                DataRow drFlexSpending = dtFlexSpending.NewRow();
                drFlexSpending["RowNumber"] = 1;
                // drFlexSpending["Flex Spending Category"] = FLEX_SPENDING_CATEGORY;
                // drFlexSpending["Flex Spending SubCategory"] = string.Empty;
                // drFlexSpending["Flex Spending Amount Allocated"] = string.Empty;
                dtFlexSpending.Rows.Add(drFlexSpending);

                intCurrentFlexSpendingCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = intCurrentFlexSpendingCategoryProtectedRowCount;
            } //if (dtFlexSpendingFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] = dtFlexSpendingFullTable;
            ViewState["BudgetSetUp_dtCurrentFlexSpending"] = dtFlexSpending;

            GridviewBudgetSetUpFlexSpending.DataSource = dtFlexSpending;
            GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetSetUpFlexSpending.DataBind();

            // Populate the TextBoxes
            if ((dtFlexSpendingFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFlexSpending.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtFlexSpendingFullTable.Rows.Count) && (i < GridviewBudgetSetUpFlexSpending.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                    box2.Text = dtFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                    box3.Text = dtFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                    if (i > (intCurrentFlexSpendingCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtFlexSpendingFullTable.Rows.Count)
        } // SetInitialFlexSpendingRow()


        private void AddNewRowToGridviewBudgetSetUpFlexSpending()
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

            string strFlexSpendingCategory = "Flex Spending";
            if (Session["BudgetSetUp_tbCurrentFlexSpendingCategory"] != null)
            {
                strFlexSpendingCategory = (string)Session["BudgetSetUp_tbCurrentFlexSpendingCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFlexSpendingCategory"] = "Flex Spending";
                strFlexSpendingCategory = "Flex Spending";
            } // if (Session["BudgetSetUp_tbCurrentFlexSpendingCategory"])

            int intCurrentFlexSpendingCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] != null)
            {
                intCurrentFlexSpendingCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = 0;
                intCurrentFlexSpendingCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"])

            DataTable dtCurrentFlexSpendingFullTable = null;
            DataTable dtCurrentFlexSpending = null;

            DataRow drCurrentFlexSpendingFullTable = null;
            DataRow drCurrentFlexSpending = null;

            if (ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentFlexSpending"] != null)
                {
                    dtCurrentFlexSpendingFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"];
                    dtCurrentFlexSpending = (DataTable)ViewState["BudgetSetUp_dtCurrentFlexSpending"];

                    drCurrentFlexSpendingFullTable = null;
                    drCurrentFlexSpending = null;

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentFlexSpendingFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFlexSpending.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentFlexSpendingFullTable.Rows.Count) && (i < GridviewBudgetSetUpFlexSpending.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                            // box2.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                            // box3.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"] = box1.Text;
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"] = box2.Text;
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = box3.Text;

                            string strFlexSpendingAmountAllocated = box3.Text;
                            double dblFlexSpendingAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strFlexSpendingAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strFlexSpendingAmountAllocated, out dblFlexSpendingAmountAllocated);
                                if (result == true)
                                {
                                    dblFlexSpendingAmountAllocated = double.Parse(strFlexSpendingAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strFlexSpendingAmountAllocated = strFlexSpendingAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strFlexSpendingAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblFlexSpendingAmountAllocated = Math.Round(dblFlexSpendingAmountAllocated, 2);
                            if (dblFlexSpendingAmountAllocated == 0.0)
                            {
                                dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = Convert.ToString(dblFlexSpendingAmountAllocated);
                            } // if (dblFlexSpendingAmountAllocated)      

                            // Read values from TextBoxes
                            string FLEX_SPENDING_CATEGORY = box1.Text;
                            string FLEX_SPENDING_SUBCATEGORY = box2.Text;

                            // Check whether Category and SubCategory already exist in the DB
                            BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();
                            BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

                            if ((ACC_EMAIL != "") && (FLEX_SPENDING_CATEGORY != "") && (FLEX_SPENDING_SUBCATEGORY != ""))
                            {
                                budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                                int budgetFlexSpendingCategoryResult = 0;
                                if (budgetFlexSpendingCategoryObj == null)
                                {
                                    budgetFlexSpendingCategoryResult = budgetFlexSpendingCategoryDAO.InsertBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                                    if (budgetFlexSpendingCategoryResult > 0)
                                    {
                                        Lbl_err.Text = "Your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + " have been saved successfully!";
                                        PanelErrorResult.Visible = true;
                                    }
                                    else
                                    {
                                        Lbl_err.Text = "Sorry, an error occurred while saving your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + ". Please inform System Administrator.";
                                        PanelErrorResult.Visible = true;
                                    } // if (budgetFlexSpendingCategoryResult)
                                } // if(budgetFlexSpendingCategoryObj)
                            } // if((ACC_EMAIL)

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentFlexSpendingFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] = dtCurrentFlexSpendingFullTable;
                    ViewState["BudgetSetUp_dtCurrentFlexSpending"] = dtCurrentFlexSpending;

                    GridviewBudgetSetUpFlexSpending.DataSource = dtCurrentFlexSpending;
                    GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
                    GridviewBudgetSetUpFlexSpending.DataBind();
                }
                else
                {
                    Response.Write("AddNewRowToGridviewBudgetSetUpFlexSpending Error: ViewState[dtCurrentFlexSpending] is null");

                    dtCurrentFlexSpendingFullTable = new DataTable();
                    dtCurrentFlexSpending = new DataTable();

                    drCurrentFlexSpendingFullTable = null;
                    drCurrentFlexSpending = null;
                } // if (ViewState["BudgetSetUp_dtCurrentFlexSpending"])
            }
            else
            {
                Response.Write("AddNewRowToGridviewBudgetSetUpFlexSpending Error: ViewState[dtCurrentFlexSpendingFullTable] is null");

                dtCurrentFlexSpendingFullTable = new DataTable();
                dtCurrentFlexSpending = new DataTable();

                drCurrentFlexSpendingFullTable = null;
                drCurrentFlexSpending = null;
            } // if (ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"])

            // Add new row to DataTable
            drCurrentFlexSpendingFullTable = dtCurrentFlexSpendingFullTable.NewRow();
            drCurrentFlexSpendingFullTable["RowNumber"] = dtCurrentFlexSpendingFullTable.Rows.Count + 1;
            drCurrentFlexSpendingFullTable["Flex Spending Category"] = strFlexSpendingCategory;
            drCurrentFlexSpendingFullTable["Flex Spending SubCategory"] = string.Empty;
            drCurrentFlexSpendingFullTable["Flex Spending Amount Allocated"] = string.Empty;
            dtCurrentFlexSpendingFullTable.Rows.Add(drCurrentFlexSpendingFullTable);

            drCurrentFlexSpending = dtCurrentFlexSpending.NewRow();
            drCurrentFlexSpending["RowNumber"] = dtCurrentFlexSpending.Rows.Count + 1;
            // drCurrentFlexSpending["Flex Spending Category"] = strFlexSpendingCategory;
            // drCurrentFlexSpending["Flex Spending SubCategory"] = string.Empty;
            // drCurrentFlexSpending["Flex Spending Amount Allocated"] = string.Empty;
            dtCurrentFlexSpending.Rows.Add(drCurrentFlexSpending);

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] = dtCurrentFlexSpendingFullTable;
            ViewState["BudgetSetUp_dtCurrentFlexSpending"] = dtCurrentFlexSpending;

            GridviewBudgetSetUpFlexSpending.DataSource = dtCurrentFlexSpending;
            GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetSetUpFlexSpending.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentFlexSpendingFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFlexSpending.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFlexSpendingFullTable.Rows.Count) && (i < GridviewBudgetSetUpFlexSpending.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                    box2.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                    box3.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"] = box1.Text;
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"] = box2.Text;
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = box3.Text;

                    if (i > (intCurrentFlexSpendingCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentFlexSpendingFullTable.Rows.Count)  
        } // AddNewRowToGridviewBudgetSetUpFlexSpending()


        protected void ButtonAddNewRowToGridviewBudgetSetUpFlexSpending_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            AddNewRowToGridviewBudgetSetUpFlexSpending();
        } // ButtonAddNewRowToGridviewBudgetSetUpFlexSpending_Click()

        protected void DeleteRowFromGridviewBudgetSetUpFlexSpending_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int DEL_INDEX = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "Delete SubCategory and Amount Allocated")
            {
                DEL_INDEX = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[DEL_INDEX]; // grab the row object from the grid

                // This is the way to get a string from a row of the GridView
                // string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Get the addresses of the TextBoxes
                TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[DEL_INDEX].Cells[1].FindControl("tbFlexSpendingCategory");
                TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[DEL_INDEX].Cells[2].FindControl("tbFlexSpendingSubCategory");
                TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[DEL_INDEX].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                // Write values to TextBoxes
                //box1.Text = dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending Category"].ToString();
                //box2.Text = dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending SubCategory"].ToString();
                //box3.Text = dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending Amount Allocated"].ToString();

                // Read values from TextBoxes
                //dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending Category"] = box1.Text;
                //dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending SubCategory"] = box2.Text;
                //dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX]["Flex Spending Amount Allocated"] = box3.Text;

                // Read values from TextBoxes
                string FLEX_SPENDING_CATEGORY = box1.Text;
                string FLEX_SPENDING_SUBCATEGORY = box2.Text;

                string strFlexSpendingAmountAllocated = box3.Text;
                double dblFlexSpendingAmountAllocated = 0.0;

                // get the length of the current string
                int intLength = strFlexSpendingAmountAllocated.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strFlexSpendingAmountAllocated, out dblFlexSpendingAmountAllocated);
                    if (result == true)
                    {
                        dblFlexSpendingAmountAllocated = double.Parse(strFlexSpendingAmountAllocated);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strFlexSpendingAmountAllocated = strFlexSpendingAmountAllocated.Substring(1);

                        // get the length of the new string
                        intLength = strFlexSpendingAmountAllocated.Length;
                    } // if (result)
                } // while (intLength)

                dblFlexSpendingAmountAllocated = Math.Round(dblFlexSpendingAmountAllocated, 2);
                if (dblFlexSpendingAmountAllocated == 0.0)
                {
                    box3.Text = string.Empty;
                }
                else
                {
                    box3.Text = Convert.ToString(dblFlexSpendingAmountAllocated);
                } // if (dblFlexSpendingAmountAllocated)

                double FLEX_SPENDING_AMOUNT_ALLOCATED = dblFlexSpendingAmountAllocated;

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

                BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

                int budgetFlexSpendingCategoryResult = 0;
                budgetFlexSpendingCategoryResult = budgetFlexSpendingCategoryDAO.DeleteBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                if (budgetFlexSpendingCategoryResult > 0)
                {
                    Lbl_err.Text = "Your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + " have been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Lbl_err.Text = "Sorry, an error occurred while deleting your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + ". Please inform System Administrator.";
                    // PanelErrorResult.Visible = true;
                } // if (budgetFlexSpendingCategoryResult)

                // BudgetSetUpExpenditureDAO budgetSetUpFlexSpendingDAO = new BudgetSetUpExpenditureDAO();

                // int budgetSetUpFlexSpendingResult = 0;
                // budgetSetUpFlexSpendingResult = budgetSetUpFlexSpendingDAO.DeleteBudgetSetUpFlexSpendingByBudgetId(BUDGET_ID, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                // if (budgetSetUpFlexSpendingResult > 0)
                // {
                //     Lbl_err.Text = "Your Budget " + BUDGET_ID + " Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + " have been deleted successfully!";
                //     PanelErrorResult.Visible = true;
                // }
                // else
                // {
                //     Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + ". Please inform System Administrator.";
                //     PanelErrorResult.Visible = true;
                // } // if (budgetSetUpFlexSpendingResult)

                // if (budgetFlexSpendingCategoryResult > 0)
                // {
                // Re-populate the GridView
                int deleteRowResult = DeleteRowFromGridviewBudgetSetUpFlexSpending(DEL_INDEX);
                // } // if (budgetFlexSpendingCategoryResult)
            } // if (command)
        } // DeleteRowFromGridviewBudgetSetUpFlexSpending_RowCommand()


        private int DeleteRowFromGridviewBudgetSetUpFlexSpending(int DEL_INDEX)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int deleteRowResult = 0;

            string strFlexSpendingCategory = "Flex Spending";
            if (Session["BudgetSetUp_tbCurrentFlexSpendingCategory"] != null)
            {
                strFlexSpendingCategory = (string)Session["BudgetSetUp_tbCurrentFlexSpendingCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFlexSpendingCategory"] = "Flex Spending";
                strFlexSpendingCategory = "Flex Spending";
            } // if (Session["BudgetSetUp_tbCurrentFlexSpendingCategory"])

            int intCurrentFlexSpendingCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] != null)
            {
                intCurrentFlexSpendingCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = 0;
                intCurrentFlexSpendingCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"]) 

            DataTable dtCurrentFlexSpendingFullTable = null;
            DataTable dtCurrentFlexSpending = null;

            if (ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentFlexSpending"] != null)
                {
                    dtCurrentFlexSpendingFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"];
                    dtCurrentFlexSpending = (DataTable)ViewState["BudgetSetUp_dtCurrentFlexSpending"];

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentFlexSpendingFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFlexSpending.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentFlexSpendingFullTable.Rows.Count) && (i < GridviewBudgetSetUpFlexSpending.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                            // box2.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                            // box3.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"] = box1.Text;
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"] = box2.Text;
                            dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = box3.Text;

                            string strFlexSpendingAmountAllocated = box3.Text;
                            double dblFlexSpendingAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strFlexSpendingAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strFlexSpendingAmountAllocated, out dblFlexSpendingAmountAllocated);
                                if (result == true)
                                {
                                    dblFlexSpendingAmountAllocated = double.Parse(strFlexSpendingAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strFlexSpendingAmountAllocated = strFlexSpendingAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strFlexSpendingAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblFlexSpendingAmountAllocated = Math.Round(dblFlexSpendingAmountAllocated, 2);
                            if (dblFlexSpendingAmountAllocated == 0.0)
                            {
                                dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = Convert.ToString(dblFlexSpendingAmountAllocated);
                            } // if (dblFlexSpendingAmountAllocated)      

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentFlexSpendingFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] = dtCurrentFlexSpendingFullTable;
                    ViewState["BudgetSetUp_dtCurrentFlexSpending"] = dtCurrentFlexSpending;

                    GridviewBudgetSetUpFlexSpending.DataSource = dtCurrentFlexSpending;
                    GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
                    GridviewBudgetSetUpFlexSpending.DataBind();
                }
                else
                {
                    Response.Write("DeleteRowFromGridviewBudgetSetUpFlexSpending Error: ViewState[dtCurrentFlexSpending] is null");

                    dtCurrentFlexSpendingFullTable = new DataTable();
                    dtCurrentFlexSpending = new DataTable();
                } // if (ViewState["BudgetSetUp_dtCurrentFlexSpending"])
            }
            else
            {
                Response.Write("DeleteRowFromGridviewBudgetSetUpFlexSpending Error: ViewState[dtCurrentFlexSpendingFullTable] is null");

                dtCurrentFlexSpendingFullTable = new DataTable();
                dtCurrentFlexSpending = new DataTable();
            } // if (ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"])

            if (dtCurrentFlexSpendingFullTable == null)
            {
                dtCurrentFlexSpendingFullTable = new DataTable();
            } // if (dtCurrentFlexSpendingFullTable)

            if (dtCurrentFlexSpending == null)
            {
                dtCurrentFlexSpending = new DataTable();
            } // if (dtCurrentFlexSpending)

            if (DEL_INDEX < dtCurrentFlexSpendingFullTable.Rows.Count)
            {
                // Delete row from DataTable
                DataRow drCurrentFlexSpendingFullTable = dtCurrentFlexSpendingFullTable.Rows[DEL_INDEX];
                dtCurrentFlexSpendingFullTable.Rows.Remove(drCurrentFlexSpendingFullTable);

                DataRow drCurrentFlexSpending = dtCurrentFlexSpending.Rows[DEL_INDEX];
                dtCurrentFlexSpending.Rows.Remove(drCurrentFlexSpending);

                deleteRowResult += 1;

                if (DEL_INDEX < intCurrentFlexSpendingCategoryProtectedRowCount)
                {
                    intCurrentFlexSpendingCategoryProtectedRowCount -= 1;
                } // if (DEL_INDEX)

                if (intCurrentFlexSpendingCategoryProtectedRowCount < 0)
                {
                    intCurrentFlexSpendingCategoryProtectedRowCount = 0;
                } // if (intCurrentFlexSpendingCategoryProtectedRowCount)
            } // if (DEL_INDEX)

            // Populate the DataTable
            if (dtCurrentFlexSpendingFullTable.Rows.Count == 0)
            {
                DataRow drFlexSpendingFullTable = dtCurrentFlexSpendingFullTable.NewRow();
                drFlexSpendingFullTable["RowNumber"] = 1;
                drFlexSpendingFullTable["Flex Spending Category"] = strFlexSpendingCategory;
                drFlexSpendingFullTable["Flex Spending SubCategory"] = string.Empty;
                drFlexSpendingFullTable["Flex Spending Amount Allocated"] = string.Empty;
                dtCurrentFlexSpendingFullTable.Rows.Add(drFlexSpendingFullTable);

                DataRow drFlexSpending = dtCurrentFlexSpending.NewRow();
                drFlexSpending["RowNumber"] = 1;
                // drFlexSpending["Flex Spending Category"] = strFlexSpendingCategory;
                // drFlexSpending["Flex Spending SubCategory"] = string.Empty;
                // drFlexSpending["Flex Spending Amount Allocated"] = string.Empty;
                dtCurrentFlexSpending.Rows.Add(drFlexSpending);

                intCurrentFlexSpendingCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = intCurrentFlexSpendingCategoryProtectedRowCount;
            } //if (dtCurrentFlexSpendingFullTable.Rows.Count)

            // Check whether the number of rows in the DataTables
            if ((dtCurrentFlexSpendingFullTable.Rows.Count > 0) && (dtCurrentFlexSpending.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFlexSpendingFullTable.Rows.Count) && (i < dtCurrentFlexSpending.Rows.Count)); i++)
                {
                    // Re-write the Row Numbers into the DataTables
                    dtCurrentFlexSpendingFullTable.Rows[i]["RowNumber"] = i + 1;
                    dtCurrentFlexSpending.Rows[i]["RowNumber"] = i + 1;
                } // for (i)
            } //if (dtCurrentFlexSpendingFullTable.Rows.Count)      

            Session["BudgetSetUp_tbCurrentFlexSpendingCategoryProtectedRowCount"] = intCurrentFlexSpendingCategoryProtectedRowCount;

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentFlexSpendingFullTable"] = dtCurrentFlexSpendingFullTable;
            ViewState["BudgetSetUp_dtCurrentFlexSpending"] = dtCurrentFlexSpending;

            GridviewBudgetSetUpFlexSpending.DataSource = dtCurrentFlexSpending;
            GridviewBudgetSetUpFlexSpending.AutoGenerateColumns = false;
            GridviewBudgetSetUpFlexSpending.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentFlexSpendingFullTable.Rows.Count > 0) && (GridviewBudgetSetUpFlexSpending.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentFlexSpendingFullTable.Rows.Count) && (i < GridviewBudgetSetUpFlexSpending.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                    box2.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                    box3.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"] = box1.Text;
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"] = box2.Text;
                    // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = box3.Text;

                    if (i > (intCurrentFlexSpendingCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentFlexSpendingFullTable.Rows.Count)

            return deleteRowResult;
        } // DeleteRowFromGridviewBudgetSetUpFlexSpending()


        private void SetInitialDebtRepaymentRow()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"] = "Debt Repayment";
            string DEBT_REPAYMENT_CATEGORY = "Debt Repayment";

            Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = 0;
            int intCurrentDebtRepaymentCategoryProtectedRowCount = 0;

            DataTable dtDebtRepaymentFullTable = new DataTable();
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            dtDebtRepaymentFullTable.Columns.Add(new DataColumn("Debt Repayment Amount Allocated", typeof(string)));

            DataTable dtDebtRepayment = new DataTable();
            dtDebtRepayment.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            // dtDebtRepayment.Columns.Add(new DataColumn("Debt Repayment Category", typeof(string)));
            // dtDebtRepayment.Columns.Add(new DataColumn("Debt Repayment SubCategory", typeof(string)));
            // dtDebtRepayment.Columns.Add(new DataColumn("Debt Repayment Amount Allocated", typeof(string)));

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
                // Read the default category and subCategory from the BudgetExpenditureCategory DB Table

                string strAcc_email = "";
                budgetDebtRepaymentCategoryList = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailAndCategory(strAcc_email, DEBT_REPAYMENT_CATEGORY);

                rec_cnt = 0;
                if (budgetDebtRepaymentCategoryList != null)
                {
                    rec_cnt = budgetDebtRepaymentCategoryList.Count;
                } // if (budgetDebtRepaymentCategoryList)
            } // if (rec_cnt)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryList[i];

                    DataRow drDebtRepaymentFullTable = dtDebtRepaymentFullTable.NewRow();
                    drDebtRepaymentFullTable["RowNumber"] = i + 1;
                    drDebtRepaymentFullTable["Debt Repayment Category"] = budgetDebtRepaymentCategoryObj.budget_expenditureCategory;
                    drDebtRepaymentFullTable["Debt Repayment SubCategory"] = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;
                    drDebtRepaymentFullTable["Debt Repayment Amount Allocated"] = string.Empty;
                    dtDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);

                    DataRow drDebtRepayment = dtDebtRepayment.NewRow();
                    drDebtRepayment["RowNumber"] = i + 1;
                    // drDebtRepayment["Debt Repayment Category"] = budgetDebtRepaymentCategoryObj.budget_expenditureCategory;
                    // drDebtRepayment["Debt Repayment SubCategory"] = budgetDebtRepaymentCategoryObj.budget_expenditureSubCategory;
                    // drDebtRepayment["Debt Repayment Amount Allocated"] = string.Empty;
                    dtDebtRepayment.Rows.Add(drDebtRepayment);

                    Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = i + 1;
                    intCurrentDebtRepaymentCategoryProtectedRowCount = i + 1;
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

                DataRow drDebtRepayment = dtDebtRepayment.NewRow();
                drDebtRepayment["RowNumber"] = 1;
                // drDebtRepayment["Debt Repayment Category"] = DEBT_REPAYMENT_CATEGORY;
                // drDebtRepayment["Debt Repayment SubCategory"] = string.Empty;
                // drDebtRepayment["Debt Repayment Amount Allocated"] = string.Empty;
                dtDebtRepayment.Rows.Add(drDebtRepayment);

                intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = intCurrentDebtRepaymentCategoryProtectedRowCount;
            } //if (dtDebtRepaymentFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] = dtDebtRepaymentFullTable;
            ViewState["BudgetSetUp_dtCurrentDebtRepayment"] = dtDebtRepayment;

            GridviewBudgetSetUpDebtRepayment.DataSource = dtDebtRepayment;
            GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetSetUpDebtRepayment.DataBind();

            // Populate the TextBoxes
            if ((dtDebtRepaymentFullTable.Rows.Count > 0) && (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtDebtRepaymentFullTable.Rows.Count) && (i < GridviewBudgetSetUpDebtRepayment.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                    box2.Text = dtDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                    box3.Text = dtDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                    if (i > (intCurrentDebtRepaymentCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtDebtRepaymentFullTable.Rows.Count)
        } // SetInitialDebtRepaymentRow()


        private void AddNewRowToGridviewBudgetSetUpDebtRepayment()
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

            string strDebtRepaymentCategory = "Debt Repayment";
            if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"] != null)
            {
                strDebtRepaymentCategory = (string)Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"] = "Debt Repayment";
                strDebtRepaymentCategory = "Debt Repayment";
            } // if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"])

            int intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] != null)
            {
                intCurrentDebtRepaymentCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = 0;
                intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"])

            DataTable dtCurrentDebtRepaymentFullTable = null;
            DataTable dtCurrentDebtRepayment = null;

            DataRow drCurrentDebtRepaymentFullTable = null;
            DataRow drCurrentDebtRepayment = null;

            if (ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentDebtRepayment"] != null)
                {
                    dtCurrentDebtRepaymentFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"];
                    dtCurrentDebtRepayment = (DataTable)ViewState["BudgetSetUp_dtCurrentDebtRepayment"];

                    drCurrentDebtRepaymentFullTable = null;
                    drCurrentDebtRepayment = null;

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentDebtRepaymentFullTable.Rows.Count > 0) && (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentDebtRepaymentFullTable.Rows.Count) && (i < GridviewBudgetSetUpDebtRepayment.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                            // box2.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                            // box3.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"] = box1.Text;
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"] = box2.Text;
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = box3.Text;

                            string strDebtRepaymentAmountAllocated = box3.Text;
                            double dblDebtRepaymentAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strDebtRepaymentAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strDebtRepaymentAmountAllocated, out dblDebtRepaymentAmountAllocated);
                                if (result == true)
                                {
                                    dblDebtRepaymentAmountAllocated = double.Parse(strDebtRepaymentAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strDebtRepaymentAmountAllocated = strDebtRepaymentAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strDebtRepaymentAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblDebtRepaymentAmountAllocated = Math.Round(dblDebtRepaymentAmountAllocated, 2);
                            if (dblDebtRepaymentAmountAllocated == 0.0)
                            {
                                dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = Convert.ToString(dblDebtRepaymentAmountAllocated);
                            } // if (dblDebtRepaymentAmountAllocated)      

                            // Read values from TextBoxes
                            string DEBT_REPAYMENT_CATEGORY = box1.Text;
                            string DEBT_REPAYMENT_SUBCATEGORY = box2.Text;

                            // Check whether Category and SubCategory already exist in the DB
                            BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();
                            BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

                            if ((ACC_EMAIL != "") && (DEBT_REPAYMENT_CATEGORY != "") && (DEBT_REPAYMENT_SUBCATEGORY != ""))
                            {
                                budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                                int budgetDebtRepaymentCategoryResult = 0;
                                if (budgetDebtRepaymentCategoryObj == null)
                                {
                                    budgetDebtRepaymentCategoryResult = budgetDebtRepaymentCategoryDAO.InsertBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                                    if (budgetDebtRepaymentCategoryResult > 0)
                                    {
                                        Lbl_err.Text = "Your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + " have been saved successfully!";
                                        PanelErrorResult.Visible = true;
                                    }
                                    else
                                    {
                                        Lbl_err.Text = "Sorry, an error occurred while saving your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + ". Please inform System Administrator.";
                                        PanelErrorResult.Visible = true;
                                    } // if (budgetDebtRepaymentCategoryResult)
                                } // if(budgetDebtRepaymentCategoryObj)
                            } // if((ACC_EMAIL)

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] = dtCurrentDebtRepaymentFullTable;
                    ViewState["BudgetSetUp_dtCurrentDebtRepayment"] = dtCurrentDebtRepayment;

                    GridviewBudgetSetUpDebtRepayment.DataSource = dtCurrentDebtRepayment;
                    GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
                    GridviewBudgetSetUpDebtRepayment.DataBind();
                }
                else
                {
                    Response.Write("AddNewRowToGridviewBudgetSetUpDebtRepayment Error: ViewState[dtCurrentDebtRepayment] is null");

                    dtCurrentDebtRepaymentFullTable = new DataTable();
                    dtCurrentDebtRepayment = new DataTable();

                    drCurrentDebtRepaymentFullTable = null;
                    drCurrentDebtRepayment = null;
                } // if (ViewState["BudgetSetUp_dtCurrentDebtRepayment"])
            }
            else
            {
                Response.Write("AddNewRowToGridviewBudgetSetUpDebtRepayment Error: ViewState[dtCurrentDebtRepaymentFullTable] is null");

                dtCurrentDebtRepaymentFullTable = new DataTable();
                dtCurrentDebtRepayment = new DataTable();

                drCurrentDebtRepaymentFullTable = null;
                drCurrentDebtRepayment = null;
            } // if (ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"])

            // Add new row to DataTable
            drCurrentDebtRepaymentFullTable = dtCurrentDebtRepaymentFullTable.NewRow();
            drCurrentDebtRepaymentFullTable["RowNumber"] = dtCurrentDebtRepaymentFullTable.Rows.Count + 1;
            drCurrentDebtRepaymentFullTable["Debt Repayment Category"] = strDebtRepaymentCategory;
            drCurrentDebtRepaymentFullTable["Debt Repayment SubCategory"] = string.Empty;
            drCurrentDebtRepaymentFullTable["Debt Repayment Amount Allocated"] = string.Empty;
            dtCurrentDebtRepaymentFullTable.Rows.Add(drCurrentDebtRepaymentFullTable);

            drCurrentDebtRepayment = dtCurrentDebtRepayment.NewRow();
            drCurrentDebtRepayment["RowNumber"] = dtCurrentDebtRepayment.Rows.Count + 1;
            // drCurrentDebtRepayment["Debt Repayment Category"] = strDebtRepaymentCategory;
            // drCurrentDebtRepayment["Debt Repayment SubCategory"] = string.Empty;
            // drCurrentDebtRepayment["Debt Repayment Amount Allocated"] = string.Empty;
            dtCurrentDebtRepayment.Rows.Add(drCurrentDebtRepayment);

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] = dtCurrentDebtRepaymentFullTable;
            ViewState["BudgetSetUp_dtCurrentDebtRepayment"] = dtCurrentDebtRepayment;

            GridviewBudgetSetUpDebtRepayment.DataSource = dtCurrentDebtRepayment;
            GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetSetUpDebtRepayment.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentDebtRepaymentFullTable.Rows.Count > 0) && (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentDebtRepaymentFullTable.Rows.Count) && (i < GridviewBudgetSetUpDebtRepayment.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                    box2.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                    box3.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"] = box1.Text;
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"] = box2.Text;
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = box3.Text;

                    if (i > (intCurrentDebtRepaymentCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)  
        } // AddNewRowToGridviewBudgetSetUpDebtRepayment()


        protected void ButtonAddNewRowToGridviewBudgetSetUpDebtRepayment_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            AddNewRowToGridviewBudgetSetUpDebtRepayment();
        } // ButtonAddNewRowToGridviewBudgetSetUpDebtRepayment_Click()

        protected void DeleteRowFromGridviewBudgetSetUpDebtRepayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int DEL_INDEX = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "Delete SubCategory and Amount Allocated")
            {
                DEL_INDEX = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[DEL_INDEX]; // grab the row object from the grid

                // This is the way to get a string from a row of the GridView
                // string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Get the addresses of the TextBoxes
                TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[DEL_INDEX].Cells[1].FindControl("tbDebtRepaymentCategory");
                TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[DEL_INDEX].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[DEL_INDEX].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                // Write values to TextBoxes
                //box1.Text = dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment Category"].ToString();
                //box2.Text = dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment SubCategory"].ToString();
                //box3.Text = dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment Amount Allocated"].ToString();

                // Read values from TextBoxes
                //dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment Category"] = box1.Text;
                //dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment SubCategory"] = box2.Text;
                //dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX]["Debt Repayment Amount Allocated"] = box3.Text;

                // Read values from TextBoxes
                string DEBT_REPAYMENT_CATEGORY = box1.Text;
                string DEBT_REPAYMENT_SUBCATEGORY = box2.Text;

                string strDebtRepaymentAmountAllocated = box3.Text;
                double dblDebtRepaymentAmountAllocated = 0.0;

                // get the length of the current string
                int intLength = strDebtRepaymentAmountAllocated.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strDebtRepaymentAmountAllocated, out dblDebtRepaymentAmountAllocated);
                    if (result == true)
                    {
                        dblDebtRepaymentAmountAllocated = double.Parse(strDebtRepaymentAmountAllocated);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strDebtRepaymentAmountAllocated = strDebtRepaymentAmountAllocated.Substring(1);

                        // get the length of the new string
                        intLength = strDebtRepaymentAmountAllocated.Length;
                    } // if (result)
                } // while (intLength)

                dblDebtRepaymentAmountAllocated = Math.Round(dblDebtRepaymentAmountAllocated, 2);
                if (dblDebtRepaymentAmountAllocated == 0.0)
                {
                    box3.Text = string.Empty;
                }
                else
                {
                    box3.Text = Convert.ToString(dblDebtRepaymentAmountAllocated);
                } // if (dblDebtRepaymentAmountAllocated)

                double DEBT_REPAYMENT_AMOUNT_ALLOCATED = dblDebtRepaymentAmountAllocated;

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

                BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

                int budgetDebtRepaymentCategoryResult = 0;
                budgetDebtRepaymentCategoryResult = budgetDebtRepaymentCategoryDAO.DeleteBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                if (budgetDebtRepaymentCategoryResult > 0)
                {
                    Lbl_err.Text = "Your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + " have been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Lbl_err.Text = "Sorry, an error occurred while deleting your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + ". Please inform System Administrator.";
                    // PanelErrorResult.Visible = true;
                } // if (budgetDebtRepaymentCategoryResult)

                // BudgetSetUpExpenditureDAO budgetSetUpDebtRepaymentDAO = new BudgetSetUpExpenditureDAO();

                // int budgetSetUpDebtRepaymentResult = 0;
                // budgetSetUpDebtRepaymentResult = budgetSetUpDebtRepaymentDAO.DeleteBudgetSetUpDebtRepaymentByBudgetId(BUDGET_ID, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                // if (budgetSetUpDebtRepaymentResult > 0)
                // {
                //     Lbl_err.Text = "Your Budget " + BUDGET_ID + " Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + " have been deleted successfully!";
                //     PanelErrorResult.Visible = true;
                // }
                // else
                // {
                //     Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + ". Please inform System Administrator.";
                //     PanelErrorResult.Visible = true;
                // } // if (budgetSetUpDebtRepaymentResult)

                // if (budgetDebtRepaymentCategoryResult > 0)
                // {
                // Re-populate the GridView
                int deleteRowResult = DeleteRowFromGridviewBudgetSetUpDebtRepayment(DEL_INDEX);
                // } // if (budgetDebtRepaymentCategoryResult)
            } // if (command)
        } // DeleteRowFromGridviewBudgetSetUpDebtRepayment_RowCommand()


        private int DeleteRowFromGridviewBudgetSetUpDebtRepayment(int DEL_INDEX)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int deleteRowResult = 0;

            string strDebtRepaymentCategory = "Debt Repayment";
            if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"] != null)
            {
                strDebtRepaymentCategory = (string)Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"] = "Debt Repayment";
                strDebtRepaymentCategory = "Debt Repayment";
            } // if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategory"])

            int intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] != null)
            {
                intCurrentDebtRepaymentCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = 0;
                intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"]) 

            DataTable dtCurrentDebtRepaymentFullTable = null;
            DataTable dtCurrentDebtRepayment = null;

            if (ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentDebtRepayment"] != null)
                {
                    dtCurrentDebtRepaymentFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"];
                    dtCurrentDebtRepayment = (DataTable)ViewState["BudgetSetUp_dtCurrentDebtRepayment"];

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentDebtRepaymentFullTable.Rows.Count > 0) && (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentDebtRepaymentFullTable.Rows.Count) && (i < GridviewBudgetSetUpDebtRepayment.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                            // box2.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                            // box3.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"] = box1.Text;
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"] = box2.Text;
                            dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = box3.Text;

                            string strDebtRepaymentAmountAllocated = box3.Text;
                            double dblDebtRepaymentAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strDebtRepaymentAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strDebtRepaymentAmountAllocated, out dblDebtRepaymentAmountAllocated);
                                if (result == true)
                                {
                                    dblDebtRepaymentAmountAllocated = double.Parse(strDebtRepaymentAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strDebtRepaymentAmountAllocated = strDebtRepaymentAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strDebtRepaymentAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblDebtRepaymentAmountAllocated = Math.Round(dblDebtRepaymentAmountAllocated, 2);
                            if (dblDebtRepaymentAmountAllocated == 0.0)
                            {
                                dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = Convert.ToString(dblDebtRepaymentAmountAllocated);
                            } // if (dblDebtRepaymentAmountAllocated)      

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] = dtCurrentDebtRepaymentFullTable;
                    ViewState["BudgetSetUp_dtCurrentDebtRepayment"] = dtCurrentDebtRepayment;

                    GridviewBudgetSetUpDebtRepayment.DataSource = dtCurrentDebtRepayment;
                    GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
                    GridviewBudgetSetUpDebtRepayment.DataBind();
                }
                else
                {
                    Response.Write("DeleteRowFromGridviewBudgetSetUpDebtRepayment Error: ViewState[dtCurrentDebtRepayment] is null");

                    dtCurrentDebtRepaymentFullTable = new DataTable();
                    dtCurrentDebtRepayment = new DataTable();
                } // if (ViewState["BudgetSetUp_dtCurrentDebtRepayment"])
            }
            else
            {
                Response.Write("DeleteRowFromGridviewBudgetSetUpDebtRepayment Error: ViewState[dtCurrentDebtRepaymentFullTable] is null");

                dtCurrentDebtRepaymentFullTable = new DataTable();
                dtCurrentDebtRepayment = new DataTable();
            } // if (ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"])

            if (dtCurrentDebtRepaymentFullTable == null)
            {
                dtCurrentDebtRepaymentFullTable = new DataTable();
            } // if (dtCurrentDebtRepaymentFullTable)

            if (dtCurrentDebtRepayment == null)
            {
                dtCurrentDebtRepayment = new DataTable();
            } // if (dtCurrentDebtRepayment)

            if (DEL_INDEX < dtCurrentDebtRepaymentFullTable.Rows.Count)
            {
                // Delete row from DataTable
                DataRow drCurrentDebtRepaymentFullTable = dtCurrentDebtRepaymentFullTable.Rows[DEL_INDEX];
                dtCurrentDebtRepaymentFullTable.Rows.Remove(drCurrentDebtRepaymentFullTable);

                DataRow drCurrentDebtRepayment = dtCurrentDebtRepayment.Rows[DEL_INDEX];
                dtCurrentDebtRepayment.Rows.Remove(drCurrentDebtRepayment);

                deleteRowResult += 1;

                if (DEL_INDEX < intCurrentDebtRepaymentCategoryProtectedRowCount)
                {
                    intCurrentDebtRepaymentCategoryProtectedRowCount -= 1;
                } // if (DEL_INDEX)

                if (intCurrentDebtRepaymentCategoryProtectedRowCount < 0)
                {
                    intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
                } // if (intCurrentDebtRepaymentCategoryProtectedRowCount)
            } // if (DEL_INDEX)

            // Populate the DataTable
            if (dtCurrentDebtRepaymentFullTable.Rows.Count == 0)
            {
                DataRow drDebtRepaymentFullTable = dtCurrentDebtRepaymentFullTable.NewRow();
                drDebtRepaymentFullTable["RowNumber"] = 1;
                drDebtRepaymentFullTable["Debt Repayment Category"] = strDebtRepaymentCategory;
                drDebtRepaymentFullTable["Debt Repayment SubCategory"] = string.Empty;
                drDebtRepaymentFullTable["Debt Repayment Amount Allocated"] = string.Empty;
                dtCurrentDebtRepaymentFullTable.Rows.Add(drDebtRepaymentFullTable);

                DataRow drDebtRepayment = dtCurrentDebtRepayment.NewRow();
                drDebtRepayment["RowNumber"] = 1;
                // drDebtRepayment["Debt Repayment Category"] = strDebtRepaymentCategory;
                // drDebtRepayment["Debt Repayment SubCategory"] = string.Empty;
                // drDebtRepayment["Debt Repayment Amount Allocated"] = string.Empty;
                dtCurrentDebtRepayment.Rows.Add(drDebtRepayment);

                intCurrentDebtRepaymentCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = intCurrentDebtRepaymentCategoryProtectedRowCount;
            } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)

            // Check whether the number of rows in the DataTables
            if ((dtCurrentDebtRepaymentFullTable.Rows.Count > 0) && (dtCurrentDebtRepayment.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentDebtRepaymentFullTable.Rows.Count) && (i < dtCurrentDebtRepayment.Rows.Count)); i++)
                {
                    // Re-write the Row Numbers into the DataTables
                    dtCurrentDebtRepaymentFullTable.Rows[i]["RowNumber"] = i + 1;
                    dtCurrentDebtRepayment.Rows[i]["RowNumber"] = i + 1;
                } // for (i)
            } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)      

            Session["BudgetSetUp_tbCurrentDebtRepaymentCategoryProtectedRowCount"] = intCurrentDebtRepaymentCategoryProtectedRowCount;

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentDebtRepaymentFullTable"] = dtCurrentDebtRepaymentFullTable;
            ViewState["BudgetSetUp_dtCurrentDebtRepayment"] = dtCurrentDebtRepayment;

            GridviewBudgetSetUpDebtRepayment.DataSource = dtCurrentDebtRepayment;
            GridviewBudgetSetUpDebtRepayment.AutoGenerateColumns = false;
            GridviewBudgetSetUpDebtRepayment.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentDebtRepaymentFullTable.Rows.Count > 0) && (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentDebtRepaymentFullTable.Rows.Count) && (i < GridviewBudgetSetUpDebtRepayment.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                    box2.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                    box3.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"] = box1.Text;
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"] = box2.Text;
                    // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = box3.Text;

                    if (i > (intCurrentDebtRepaymentCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)

            return deleteRowResult;
        } // DeleteRowFromGridviewBudgetSetUpDebtRepayment()


        private void SetInitialPriorityGoalsRow()
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"] = "Priority Goals";
            string PRIORITY_GOALS_CATEGORY = "Priority Goals";

            Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = 0;
            int intCurrentPriorityGoalsCategoryProtectedRowCount = 0;

            DataTable dtPriorityGoalsFullTable = new DataTable();
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            dtPriorityGoalsFullTable.Columns.Add(new DataColumn("Priority Goals Amount Allocated", typeof(string)));

            DataTable dtPriorityGoals = new DataTable();
            dtPriorityGoals.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            // dtPriorityGoals.Columns.Add(new DataColumn("Priority Goals Category", typeof(string)));
            // dtPriorityGoals.Columns.Add(new DataColumn("Priority Goals SubCategory", typeof(string)));
            // dtPriorityGoals.Columns.Add(new DataColumn("Priority Goals Amount Allocated", typeof(string)));

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
                // Read the default category and subCategory from the BudgetExpenditureCategory DB Table

                string strAcc_email = "";
                budgetPriorityGoalsCategoryList = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailAndCategory(strAcc_email, PRIORITY_GOALS_CATEGORY);

                rec_cnt = 0;
                if (budgetPriorityGoalsCategoryList != null)
                {
                    rec_cnt = budgetPriorityGoalsCategoryList.Count;
                } // if (budgetPriorityGoalsCategoryList)
            } // if (rec_cnt)

            // Check whether the number of Category and SubCategory rows is valid
            if (rec_cnt > 0)
            {
                // Populate the DataTable
                for (int i = 0; i < rec_cnt; i++)
                {
                    BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryList[i];

                    DataRow drPriorityGoalsFullTable = dtPriorityGoalsFullTable.NewRow();
                    drPriorityGoalsFullTable["RowNumber"] = i + 1;
                    drPriorityGoalsFullTable["Priority Goals Category"] = budgetPriorityGoalsCategoryObj.budget_expenditureCategory;
                    drPriorityGoalsFullTable["Priority Goals SubCategory"] = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;
                    drPriorityGoalsFullTable["Priority Goals Amount Allocated"] = string.Empty;
                    dtPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);

                    DataRow drPriorityGoals = dtPriorityGoals.NewRow();
                    drPriorityGoals["RowNumber"] = i + 1;
                    // drPriorityGoals["Priority Goals Category"] = budgetPriorityGoalsCategoryObj.budget_expenditureCategory;
                    // drPriorityGoals["Priority Goals SubCategory"] = budgetPriorityGoalsCategoryObj.budget_expenditureSubCategory;
                    // drPriorityGoals["Priority Goals Amount Allocated"] = string.Empty;
                    dtPriorityGoals.Rows.Add(drPriorityGoals);

                    Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = i + 1;
                    intCurrentPriorityGoalsCategoryProtectedRowCount = i + 1;
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

                DataRow drPriorityGoals = dtPriorityGoals.NewRow();
                drPriorityGoals["RowNumber"] = 1;
                // drPriorityGoals["Priority Goals Category"] = PRIORITY_GOALS_CATEGORY;
                // drPriorityGoals["Priority Goals SubCategory"] = string.Empty;
                // drPriorityGoals["Priority Goals Amount Allocated"] = string.Empty;
                dtPriorityGoals.Rows.Add(drPriorityGoals);

                intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = intCurrentPriorityGoalsCategoryProtectedRowCount;
            } //if (dtPriorityGoalsFullTable.Rows.Count)

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] = dtPriorityGoalsFullTable;
            ViewState["BudgetSetUp_dtCurrentPriorityGoals"] = dtPriorityGoals;

            GridviewBudgetSetUpPriorityGoals.DataSource = dtPriorityGoals;
            GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetSetUpPriorityGoals.DataBind();

            // Populate the TextBoxes
            if ((dtPriorityGoalsFullTable.Rows.Count > 0) && (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtPriorityGoalsFullTable.Rows.Count) && (i < GridviewBudgetSetUpPriorityGoals.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                    box2.Text = dtPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                    box3.Text = dtPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                    if (i > (intCurrentPriorityGoalsCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtPriorityGoalsFullTable.Rows.Count)
        } // SetInitialPriorityGoalsRow()


        private void AddNewRowToGridviewBudgetSetUpPriorityGoals()
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

            string strPriorityGoalsCategory = "Priority Goals";
            if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"] != null)
            {
                strPriorityGoalsCategory = (string)Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"] = "Priority Goals";
                strPriorityGoalsCategory = "Priority Goals";
            } // if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"])

            int intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] != null)
            {
                intCurrentPriorityGoalsCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = 0;
                intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"])

            DataTable dtCurrentPriorityGoalsFullTable = null;
            DataTable dtCurrentPriorityGoals = null;

            DataRow drCurrentPriorityGoalsFullTable = null;
            DataRow drCurrentPriorityGoals = null;

            if (ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentPriorityGoals"] != null)
                {
                    dtCurrentPriorityGoalsFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"];
                    dtCurrentPriorityGoals = (DataTable)ViewState["BudgetSetUp_dtCurrentPriorityGoals"];

                    drCurrentPriorityGoalsFullTable = null;
                    drCurrentPriorityGoals = null;

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentPriorityGoalsFullTable.Rows.Count > 0) && (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentPriorityGoalsFullTable.Rows.Count) && (i < GridviewBudgetSetUpPriorityGoals.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                            // box2.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                            // box3.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"] = box1.Text;
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"] = box2.Text;
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = box3.Text;

                            string strPriorityGoalsAmountAllocated = box3.Text;
                            double dblPriorityGoalsAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strPriorityGoalsAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strPriorityGoalsAmountAllocated, out dblPriorityGoalsAmountAllocated);
                                if (result == true)
                                {
                                    dblPriorityGoalsAmountAllocated = double.Parse(strPriorityGoalsAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strPriorityGoalsAmountAllocated = strPriorityGoalsAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strPriorityGoalsAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblPriorityGoalsAmountAllocated = Math.Round(dblPriorityGoalsAmountAllocated, 2);
                            if (dblPriorityGoalsAmountAllocated == 0.0)
                            {
                                dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = Convert.ToString(dblPriorityGoalsAmountAllocated);
                            } // if (dblPriorityGoalsAmountAllocated)      

                            // Read values from TextBoxes
                            string PRIORITY_GOALS_CATEGORY = box1.Text;
                            string PRIORITY_GOALS_SUBCATEGORY = box2.Text;

                            // Check whether Category and SubCategory already exist in the DB
                            BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();
                            BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

                            if ((ACC_EMAIL != "") && (PRIORITY_GOALS_CATEGORY != "") && (PRIORITY_GOALS_SUBCATEGORY != ""))
                            {
                                budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                                int budgetPriorityGoalsCategoryResult = 0;
                                if (budgetPriorityGoalsCategoryObj == null)
                                {
                                    budgetPriorityGoalsCategoryResult = budgetPriorityGoalsCategoryDAO.InsertBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                                    if (budgetPriorityGoalsCategoryResult > 0)
                                    {
                                        Lbl_err.Text = "Your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + " have been saved successfully!";
                                        PanelErrorResult.Visible = true;
                                    }
                                    else
                                    {
                                        Lbl_err.Text = "Sorry, an error occurred while saving your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + ". Please inform System Administrator.";
                                        PanelErrorResult.Visible = true;
                                    } // if (budgetPriorityGoalsCategoryResult)
                                } // if(budgetPriorityGoalsCategoryObj)
                            } // if((ACC_EMAIL)

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] = dtCurrentPriorityGoalsFullTable;
                    ViewState["BudgetSetUp_dtCurrentPriorityGoals"] = dtCurrentPriorityGoals;

                    GridviewBudgetSetUpPriorityGoals.DataSource = dtCurrentPriorityGoals;
                    GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
                    GridviewBudgetSetUpPriorityGoals.DataBind();
                }
                else
                {
                    Response.Write("AddNewRowToGridviewBudgetSetUpPriorityGoals Error: ViewState[dtCurrentPriorityGoals] is null");

                    dtCurrentPriorityGoalsFullTable = new DataTable();
                    dtCurrentPriorityGoals = new DataTable();

                    drCurrentPriorityGoalsFullTable = null;
                    drCurrentPriorityGoals = null;
                } // if (ViewState["BudgetSetUp_dtCurrentPriorityGoals"])
            }
            else
            {
                Response.Write("AddNewRowToGridviewBudgetSetUpPriorityGoals Error: ViewState[dtCurrentPriorityGoalsFullTable] is null");

                dtCurrentPriorityGoalsFullTable = new DataTable();
                dtCurrentPriorityGoals = new DataTable();

                drCurrentPriorityGoalsFullTable = null;
                drCurrentPriorityGoals = null;
            } // if (ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"])

            // Add new row to DataTable
            drCurrentPriorityGoalsFullTable = dtCurrentPriorityGoalsFullTable.NewRow();
            drCurrentPriorityGoalsFullTable["RowNumber"] = dtCurrentPriorityGoalsFullTable.Rows.Count + 1;
            drCurrentPriorityGoalsFullTable["Priority Goals Category"] = strPriorityGoalsCategory;
            drCurrentPriorityGoalsFullTable["Priority Goals SubCategory"] = string.Empty;
            drCurrentPriorityGoalsFullTable["Priority Goals Amount Allocated"] = string.Empty;
            dtCurrentPriorityGoalsFullTable.Rows.Add(drCurrentPriorityGoalsFullTable);

            drCurrentPriorityGoals = dtCurrentPriorityGoals.NewRow();
            drCurrentPriorityGoals["RowNumber"] = dtCurrentPriorityGoals.Rows.Count + 1;
            // drCurrentPriorityGoals["Priority Goals Category"] = strPriorityGoalsCategory;
            // drCurrentPriorityGoals["Priority Goals SubCategory"] = string.Empty;
            // drCurrentPriorityGoals["Priority Goals Amount Allocated"] = string.Empty;
            dtCurrentPriorityGoals.Rows.Add(drCurrentPriorityGoals);

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] = dtCurrentPriorityGoalsFullTable;
            ViewState["BudgetSetUp_dtCurrentPriorityGoals"] = dtCurrentPriorityGoals;

            GridviewBudgetSetUpPriorityGoals.DataSource = dtCurrentPriorityGoals;
            GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetSetUpPriorityGoals.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentPriorityGoalsFullTable.Rows.Count > 0) && (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentPriorityGoalsFullTable.Rows.Count) && (i < GridviewBudgetSetUpPriorityGoals.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                    box2.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                    box3.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"] = box1.Text;
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"] = box2.Text;
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = box3.Text;

                    if (i > (intCurrentPriorityGoalsCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)  
        } // AddNewRowToGridviewBudgetSetUpPriorityGoals()


        protected void ButtonAddNewRowToGridviewBudgetSetUpPriorityGoals_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            AddNewRowToGridviewBudgetSetUpPriorityGoals();
        } // ButtonAddNewRowToGridviewBudgetSetUpPriorityGoals_Click()

        protected void DeleteRowFromGridviewBudgetSetUpPriorityGoals_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int DEL_INDEX = 0;
            GridViewRow row;
            GridView grid = sender as GridView;
            string command = e.CommandName;

            // this segment handles what happens when the SELECT command is clicked
            if (command == "Delete SubCategory and Amount Allocated")
            {
                DEL_INDEX = Convert.ToInt32(e.CommandArgument);  // compute which row was clicked
                row = grid.Rows[DEL_INDEX]; // grab the row object from the grid

                // This is the way to get a string from a row of the GridView
                // string LOAN_ID = row.Cells[0].Text; // the LOAN_ID is stored on the 1st column

                // Get the addresses of the TextBoxes
                TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[DEL_INDEX].Cells[1].FindControl("tbPriorityGoalsCategory");
                TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[DEL_INDEX].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[DEL_INDEX].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                // Write values to TextBoxes
                //box1.Text = dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals Category"].ToString();
                //box2.Text = dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals SubCategory"].ToString();
                //box3.Text = dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals Amount Allocated"].ToString();

                // Read values from TextBoxes
                //dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals Category"] = box1.Text;
                //dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals SubCategory"] = box2.Text;
                //dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX]["Priority Goals Amount Allocated"] = box3.Text;

                // Read values from TextBoxes
                string PRIORITY_GOALS_CATEGORY = box1.Text;
                string PRIORITY_GOALS_SUBCATEGORY = box2.Text;

                string strPriorityGoalsAmountAllocated = box3.Text;
                double dblPriorityGoalsAmountAllocated = 0.0;

                // get the length of the current string
                int intLength = strPriorityGoalsAmountAllocated.Length;

                while (intLength > 0)
                {
                    // Check whether user input is numeric '3200'
                    bool result = double.TryParse(strPriorityGoalsAmountAllocated, out dblPriorityGoalsAmountAllocated);
                    if (result == true)
                    {
                        dblPriorityGoalsAmountAllocated = double.Parse(strPriorityGoalsAmountAllocated);
                        break;
                    }
                    else
                    {
                        // user input is non numeric '$3200'

                        // user input is numeric '3200'
                        strPriorityGoalsAmountAllocated = strPriorityGoalsAmountAllocated.Substring(1);

                        // get the length of the new string
                        intLength = strPriorityGoalsAmountAllocated.Length;
                    } // if (result)
                } // while (intLength)

                dblPriorityGoalsAmountAllocated = Math.Round(dblPriorityGoalsAmountAllocated, 2);
                if (dblPriorityGoalsAmountAllocated == 0.0)
                {
                    box3.Text = string.Empty;
                }
                else
                {
                    box3.Text = Convert.ToString(dblPriorityGoalsAmountAllocated);
                } // if (dblPriorityGoalsAmountAllocated)

                double PRIORITY_GOALS_AMOUNT_ALLOCATED = dblPriorityGoalsAmountAllocated;

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

                BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

                int budgetPriorityGoalsCategoryResult = 0;
                budgetPriorityGoalsCategoryResult = budgetPriorityGoalsCategoryDAO.DeleteBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                if (budgetPriorityGoalsCategoryResult > 0)
                {
                    Lbl_err.Text = "Your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + " have been deleted successfully!";
                    PanelErrorResult.Visible = true;
                }
                else
                {
                    // Lbl_err.Text = "Sorry, an error occurred while deleting your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + ". Please inform System Administrator.";
                    // PanelErrorResult.Visible = true;
                } // if (budgetPriorityGoalsCategoryResult)

                // BudgetSetUpExpenditureDAO budgetSetUpPriorityGoalsDAO = new BudgetSetUpExpenditureDAO();

                // int budgetSetUpPriorityGoalsResult = 0;
                // budgetSetUpPriorityGoalsResult = budgetSetUpPriorityGoalsDAO.DeleteBudgetSetUpPriorityGoalsByBudgetId(BUDGET_ID, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                // if (budgetSetUpPriorityGoalsResult > 0)
                // {
                //     Lbl_err.Text = "Your Budget " + BUDGET_ID + " Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + " have been deleted successfully!";
                //     PanelErrorResult.Visible = true;
                // }
                // else
                // {
                //     Lbl_err.Text = "Sorry, an error occurred while deleting your Budget " + BUDGET_ID + " Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + ". Please inform System Administrator.";
                //     PanelErrorResult.Visible = true;
                // } // if (budgetSetUpPriorityGoalsResult)

                // if (budgetPriorityGoalsCategoryResult > 0)
                // {
                // Re-populate the GridView
                int deleteRowResult = DeleteRowFromGridviewBudgetSetUpPriorityGoals(DEL_INDEX);
                // } // if (budgetPriorityGoalsCategoryResult)
            } // if (command)
        } // DeleteRowFromGridviewBudgetSetUpPriorityGoals_RowCommand()


        private int DeleteRowFromGridviewBudgetSetUpPriorityGoals(int DEL_INDEX)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            int deleteRowResult = 0;

            string strPriorityGoalsCategory = "Priority Goals";
            if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"] != null)
            {
                strPriorityGoalsCategory = (string)Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"] = "Priority Goals";
                strPriorityGoalsCategory = "Priority Goals";
            } // if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategory"])

            int intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
            if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] != null)
            {
                intCurrentPriorityGoalsCategoryProtectedRowCount = (int)Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"];
            }
            else
            {
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = 0;
                intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
            } // if (Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"]) 

            DataTable dtCurrentPriorityGoalsFullTable = null;
            DataTable dtCurrentPriorityGoals = null;

            if (ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] != null)
            {
                if (ViewState["BudgetSetUp_dtCurrentPriorityGoals"] != null)
                {
                    dtCurrentPriorityGoalsFullTable = (DataTable)ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"];
                    dtCurrentPriorityGoals = (DataTable)ViewState["BudgetSetUp_dtCurrentPriorityGoals"];

                    // Copy values from TextBoxes and populate the DataTable
                    if ((dtCurrentPriorityGoalsFullTable.Rows.Count > 0) && (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0))
                    {
                        for (int i = 0; ((i < dtCurrentPriorityGoalsFullTable.Rows.Count) && (i < GridviewBudgetSetUpPriorityGoals.Rows.Count)); i++)
                        {
                            // Get the addresses of the TextBoxes
                            TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                            TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                            TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                            // Write values to TextBoxes
                            // box1.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                            // box2.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                            // box3.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                            // Read values from TextBoxes
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"] = box1.Text;
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"] = box2.Text;
                            dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = box3.Text;

                            string strPriorityGoalsAmountAllocated = box3.Text;
                            double dblPriorityGoalsAmountAllocated = 0.0;

                            // get the length of the current string
                            int intLength = strPriorityGoalsAmountAllocated.Length;

                            while (intLength > 0)
                            {
                                // Check whether user input is numeric '3200'
                                bool result = double.TryParse(strPriorityGoalsAmountAllocated, out dblPriorityGoalsAmountAllocated);
                                if (result == true)
                                {
                                    dblPriorityGoalsAmountAllocated = double.Parse(strPriorityGoalsAmountAllocated);
                                    break;
                                }
                                else
                                {
                                    // user input is non numeric '$3200'

                                    // user input is numeric '3200'
                                    strPriorityGoalsAmountAllocated = strPriorityGoalsAmountAllocated.Substring(1);

                                    // get the length of the new string
                                    intLength = strPriorityGoalsAmountAllocated.Length;
                                } // if (result)
                            } // while (intLength)

                            dblPriorityGoalsAmountAllocated = Math.Round(dblPriorityGoalsAmountAllocated, 2);
                            if (dblPriorityGoalsAmountAllocated == 0.0)
                            {
                                dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = string.Empty;
                            }
                            else
                            {
                                dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = Convert.ToString(dblPriorityGoalsAmountAllocated);
                            } // if (dblPriorityGoalsAmountAllocated)      

                            // box1.ReadOnly = true;
                            // box2.ReadOnly = true;
                            // box3.ReadOnly = false;
                        } // for (i)
                    } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)

                    // Store the DataTable in ViewState
                    ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] = dtCurrentPriorityGoalsFullTable;
                    ViewState["BudgetSetUp_dtCurrentPriorityGoals"] = dtCurrentPriorityGoals;

                    GridviewBudgetSetUpPriorityGoals.DataSource = dtCurrentPriorityGoals;
                    GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
                    GridviewBudgetSetUpPriorityGoals.DataBind();
                }
                else
                {
                    Response.Write("DeleteRowFromGridviewBudgetSetUpPriorityGoals Error: ViewState[dtCurrentPriorityGoals] is null");

                    dtCurrentPriorityGoalsFullTable = new DataTable();
                    dtCurrentPriorityGoals = new DataTable();
                } // if (ViewState["BudgetSetUp_dtCurrentPriorityGoals"])
            }
            else
            {
                Response.Write("DeleteRowFromGridviewBudgetSetUpPriorityGoals Error: ViewState[dtCurrentPriorityGoalsFullTable] is null");

                dtCurrentPriorityGoalsFullTable = new DataTable();
                dtCurrentPriorityGoals = new DataTable();
            } // if (ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"])

            if (dtCurrentPriorityGoalsFullTable == null)
            {
                dtCurrentPriorityGoalsFullTable = new DataTable();
            } // if (dtCurrentPriorityGoalsFullTable)

            if (dtCurrentPriorityGoals == null)
            {
                dtCurrentPriorityGoals = new DataTable();
            } // if (dtCurrentPriorityGoals)

            if (DEL_INDEX < dtCurrentPriorityGoalsFullTable.Rows.Count)
            {
                // Delete row from DataTable
                DataRow drCurrentPriorityGoalsFullTable = dtCurrentPriorityGoalsFullTable.Rows[DEL_INDEX];
                dtCurrentPriorityGoalsFullTable.Rows.Remove(drCurrentPriorityGoalsFullTable);

                DataRow drCurrentPriorityGoals = dtCurrentPriorityGoals.Rows[DEL_INDEX];
                dtCurrentPriorityGoals.Rows.Remove(drCurrentPriorityGoals);

                deleteRowResult += 1;

                if (DEL_INDEX < intCurrentPriorityGoalsCategoryProtectedRowCount)
                {
                    intCurrentPriorityGoalsCategoryProtectedRowCount -= 1;
                } // if (DEL_INDEX)

                if (intCurrentPriorityGoalsCategoryProtectedRowCount < 0)
                {
                    intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
                } // if (intCurrentPriorityGoalsCategoryProtectedRowCount)
            } // if (DEL_INDEX)

            // Populate the DataTable
            if (dtCurrentPriorityGoalsFullTable.Rows.Count == 0)
            {
                DataRow drPriorityGoalsFullTable = dtCurrentPriorityGoalsFullTable.NewRow();
                drPriorityGoalsFullTable["RowNumber"] = 1;
                drPriorityGoalsFullTable["Priority Goals Category"] = strPriorityGoalsCategory;
                drPriorityGoalsFullTable["Priority Goals SubCategory"] = string.Empty;
                drPriorityGoalsFullTable["Priority Goals Amount Allocated"] = string.Empty;
                dtCurrentPriorityGoalsFullTable.Rows.Add(drPriorityGoalsFullTable);

                DataRow drPriorityGoals = dtCurrentPriorityGoals.NewRow();
                drPriorityGoals["RowNumber"] = 1;
                // drPriorityGoals["Priority Goals Category"] = strPriorityGoalsCategory;
                // drPriorityGoals["Priority Goals SubCategory"] = string.Empty;
                // drPriorityGoals["Priority Goals Amount Allocated"] = string.Empty;
                dtCurrentPriorityGoals.Rows.Add(drPriorityGoals);

                intCurrentPriorityGoalsCategoryProtectedRowCount = 0;
                Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = intCurrentPriorityGoalsCategoryProtectedRowCount;
            } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)

            // Check whether the number of rows in the DataTables
            if ((dtCurrentPriorityGoalsFullTable.Rows.Count > 0) && (dtCurrentPriorityGoals.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentPriorityGoalsFullTable.Rows.Count) && (i < dtCurrentPriorityGoals.Rows.Count)); i++)
                {
                    // Re-write the Row Numbers into the DataTables
                    dtCurrentPriorityGoalsFullTable.Rows[i]["RowNumber"] = i + 1;
                    dtCurrentPriorityGoals.Rows[i]["RowNumber"] = i + 1;
                } // for (i)
            } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)      

            Session["BudgetSetUp_tbCurrentPriorityGoalsCategoryProtectedRowCount"] = intCurrentPriorityGoalsCategoryProtectedRowCount;

            // Store the DataTable in ViewState
            ViewState["BudgetSetUp_dtCurrentPriorityGoalsFullTable"] = dtCurrentPriorityGoalsFullTable;
            ViewState["BudgetSetUp_dtCurrentPriorityGoals"] = dtCurrentPriorityGoals;

            GridviewBudgetSetUpPriorityGoals.DataSource = dtCurrentPriorityGoals;
            GridviewBudgetSetUpPriorityGoals.AutoGenerateColumns = false;
            GridviewBudgetSetUpPriorityGoals.DataBind();

            // Populate the TextBoxes
            if ((dtCurrentPriorityGoalsFullTable.Rows.Count > 0) && (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0))
            {
                for (int i = 0; ((i < dtCurrentPriorityGoalsFullTable.Rows.Count) && (i < GridviewBudgetSetUpPriorityGoals.Rows.Count)); i++)
                {
                    // Get the addresses of the TextBoxes
                    TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                    TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                    TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                    // Write values to TextBoxes
                    box1.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                    box2.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                    box3.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                    // Read values from TextBoxes
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"] = box1.Text;
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"] = box2.Text;
                    // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = box3.Text;

                    if (i > (intCurrentPriorityGoalsCategoryProtectedRowCount - 1))
                    {
                        // The values in the TextBoxes in these rows have been added by the user
                        // Do NOT allow the user to modify the Category TextBox
                        // Allow the user to modify the SubCategory and  Amount Allocated TextBoxes
                        box1.ReadOnly = true;
                        box2.ReadOnly = false;
                        box3.ReadOnly = false;
                    }
                    else
                    {
                        // The values in the TextBoxes in these rows have been retrieved from the Database Tables
                        // Do NOT allow the user to modify the Category and SubCategory TextBoxes
                        // Allow the user to modify the Amount Allocated TextBox
                        box1.ReadOnly = true;
                        box2.ReadOnly = true;
                        box3.ReadOnly = false;
                    } // if (i)
                } // for (i)
            } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)

            return deleteRowResult;
        } // DeleteRowFromGridviewBudgetSetUpPriorityGoals()


        protected void btnSaveBudgetSetUp_Click(object sender, EventArgs e)
        {
            Lbl_err.Text = String.Empty;
            PanelErrorResult.Visible = false;

            DateTime dtmStartDate = StartDateCalendar.SelectedDate;
            if (dtmStartDate.Year == 1)
            {
                Lbl_err.Text = "Error: Please select a valid Budget Start Date.";
                PanelErrorResult.Visible = true;

                return;
            } // if (dtmStartDate.Year)

            DateTime dtmEndDate = EndDateCalendar.SelectedDate;
            if (dtmEndDate.Year == 1)
            {
                Lbl_err.Text = "Error: Please select a valid Budget End Date.";
                PanelErrorResult.Visible = true;

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
                Lbl_err.Text = "Error: Your Budget Set Up Start Date " + dtmStartDate.ToString() + " is later than End Date " + dtmEndDate.ToString() + ".";
                PanelErrorResult.Visible = true;

                return;
            } // if (intDateTimeComparisonResult)

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

            DateTime BUDGET_STARTDATE = dtmStartDate;
            DateTime BUDGET_ENDDATE = dtmEndDate;

            double BUDGET_INCOMEAMOUNTALLOCATED = 0.0;
            int BUDGET_INCOMEAMOUNTCOUNT = 0;
            double BUDGET_INCOMEAMOUNTRECEIVED = 0.0;

            double BUDGET_FIXEDCOSTAMOUNTALLOCATED = 0.0;
            int BUDGET_FIXEDCOSTAMOUNTCOUNT = 0;
            double BUDGET_FIXEDCOSTAMOUNTSPENT = 0.0;

            double BUDGET_FLEXSPENDINGAMOUNTALLOCATED = 0.0;
            int BUDGET_FLEXSPENDINGAMOUNTCOUNT = 0;
            double BUDGET_FLEXSPENDINGAMOUNTSPENT = 0.0;

            double BUDGET_DEBTREPAYMENTAMOUNTALLOCATED = 0.0;
            int BUDGET_DEBTREPAYMENTAMOUNTCOUNT = 0;
            double BUDGET_DEBTREPAYMENTAMOUNTSPENT = 0.0;

            double BUDGET_PRIORITYGOALSAMOUNTALLOCATED = 0.0;
            int BUDGET_PRIORITYGOALSAMOUNTCOUNT = 0;
            double BUDGET_PRIORITYGOALSAMOUNTSPENT = 0.0;

            double BUDGET_TOTALEXPENDITUREAMOUNTALLOCATED = 0.0;
            int BUDGET_TOTALEXPENDITUREAMOUNTCOUNT = 0;
            double BUDGET_TOTALEXPENDITUREAMOUNTSPENT = 0.0;

            double BUDGET_TOTALEXPENDITUREAMOUNTLEFTOVER = 0.0;

            // Save BudgetDashBoard in the DB
            BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
            BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

            string BUDGET_ID = budgetDashBoardDAO.InsertBudgetDashBoardByEmail(ACC_EMAIL,
                                                                                BUDGET_STARTDATE,
                                                                                BUDGET_ENDDATE,

                                                                                BUDGET_INCOMEAMOUNTALLOCATED,
                                                                                BUDGET_INCOMEAMOUNTCOUNT,
                                                                                BUDGET_INCOMEAMOUNTRECEIVED,

                                                                                BUDGET_FIXEDCOSTAMOUNTALLOCATED,
                                                                                BUDGET_FIXEDCOSTAMOUNTCOUNT,
                                                                                BUDGET_FIXEDCOSTAMOUNTSPENT,

                                                                                BUDGET_FLEXSPENDINGAMOUNTALLOCATED,
                                                                                BUDGET_FLEXSPENDINGAMOUNTCOUNT,
                                                                                BUDGET_FLEXSPENDINGAMOUNTSPENT,

                                                                                BUDGET_DEBTREPAYMENTAMOUNTALLOCATED,
                                                                                BUDGET_DEBTREPAYMENTAMOUNTCOUNT,
                                                                                BUDGET_DEBTREPAYMENTAMOUNTSPENT,

                                                                                BUDGET_PRIORITYGOALSAMOUNTALLOCATED,
                                                                                BUDGET_PRIORITYGOALSAMOUNTCOUNT,
                                                                                BUDGET_PRIORITYGOALSAMOUNTSPENT,

                                                                                BUDGET_TOTALEXPENDITUREAMOUNTALLOCATED,
                                                                                BUDGET_TOTALEXPENDITUREAMOUNTCOUNT,
                                                                                BUDGET_TOTALEXPENDITUREAMOUNTSPENT,

                                                                                BUDGET_TOTALEXPENDITUREAMOUNTLEFTOVER);

            if (BUDGET_ID != null)
            {
                Lbl_err.Text = "Your Budget " + BUDGET_ID + " has been saved successfully!";
                PanelErrorResult.Visible = true;
            }
            else
            {
                Lbl_err.Text = "Sorry, an error occurred while saving your Budget. Please inform System Administrator.";
                PanelErrorResult.Visible = true;
            } // if (BUDGET_ID)

            if (BUDGET_ID != null)
            {
                if (GridviewBudgetSetUpIncome.Rows.Count > 0)
                {
                    for (int i = 0; i < GridviewBudgetSetUpIncome.Rows.Count; i++)
                    {
                        // Get the addresses of the TextBoxes
                        TextBox box1 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[1].FindControl("tbIncomeCategory");
                        TextBox box2 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[2].FindControl("tbIncomeSubCategory");
                        TextBox box3 = (TextBox)GridviewBudgetSetUpIncome.Rows[i].Cells[3].FindControl("tbIncomeSubCategoryAmountAllocated");

                        // Write values to TextBoxes
                        // box1.Text = dtCurrentIncomeFullTable.Rows[i]["Income Category"].ToString();
                        // box2.Text = dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"].ToString();
                        // box3.Text = dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"].ToString();

                        // Read values from TextBoxes
                        // dtCurrentIncomeFullTable.Rows[i]["Income Category"] = box1.Text;
                        // dtCurrentIncomeFullTable.Rows[i]["Income SubCategory"] = box2.Text;
                        // dtCurrentIncomeFullTable.Rows[i]["Income Amount Allocated"] = box3.Text;

                        // Read values from TextBoxes
                        string INCOME_CATEGORY = box1.Text;
                        string INCOME_SUBCATEGORY = box2.Text;

                        string strIncomeAmountAllocated = box3.Text;
                        double dblIncomeAmountAllocated = 0.0;

                        // get the length of the current string
                        int intLength = strIncomeAmountAllocated.Length;

                        while (intLength > 0)
                        {
                            // Check whether user input is numeric '3200'
                            bool result = double.TryParse(strIncomeAmountAllocated, out dblIncomeAmountAllocated);
                            if (result == true)
                            {
                                dblIncomeAmountAllocated = double.Parse(strIncomeAmountAllocated);
                                break;
                            }
                            else
                            {
                                // user input is non numeric '$3200'

                                // user input is numeric '3200'
                                strIncomeAmountAllocated = strIncomeAmountAllocated.Substring(1);

                                // get the length of the new string
                                intLength = strIncomeAmountAllocated.Length;
                            } // if (result)
                        } // while (intLength)

                        dblIncomeAmountAllocated = Math.Round(dblIncomeAmountAllocated, 2);
                        if (dblIncomeAmountAllocated == 0.0)
                        {
                            box3.Text = string.Empty;
                        }
                        else
                        {
                            box3.Text = Convert.ToString(dblIncomeAmountAllocated);
                        } // if (dblIncomeAmountAllocated)

                        double INCOME_AMOUNT_ALLOCATED = dblIncomeAmountAllocated;

                        // box1.ReadOnly = true;
                        // box2.ReadOnly = true;
                        // box3.ReadOnly = false;

                        // Check whether Category and SubCategory already exist in the DB
                        BudgetIncomeCategory budgetIncomeCategoryObj = new BudgetIncomeCategory();
                        BudgetIncomeCategoryDAO budgetIncomeCategoryDAO = new BudgetIncomeCategoryDAO();

                        if ((ACC_EMAIL != "") && (INCOME_CATEGORY != "") && (INCOME_SUBCATEGORY != ""))
                        {
                            budgetIncomeCategoryObj = budgetIncomeCategoryDAO.ReadBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                            int budgetIncomeCategoryResult = 0;
                            if (budgetIncomeCategoryObj == null)
                            {
                                budgetIncomeCategoryResult = budgetIncomeCategoryDAO.InsertBudgetIncomeCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, INCOME_CATEGORY, INCOME_SUBCATEGORY);

                                if (budgetIncomeCategoryResult > 0)
                                {
                                    Lbl_err.Text = "Your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + " have been saved successfully!";
                                    PanelErrorResult.Visible = true;
                                }
                                else
                                {
                                    Lbl_err.Text = "Sorry, an error occurred while saving your Budget Income Category and SubCategory " + INCOME_SUBCATEGORY + ". Please inform System Administrator.";
                                    PanelErrorResult.Visible = true;
                                } // if (budgetIncomeCategoryResult)
                            } // if(budgetIncomeCategoryObj)
                        } // if((ACC_EMAIL)

                        if ((BUDGET_ID != "") && (INCOME_CATEGORY != "") && (INCOME_SUBCATEGORY != "") && (INCOME_AMOUNT_ALLOCATED > 0.0))
                        {
                            BudgetSetUpIncomeDAO budgetSetUpIncomeDAO = new BudgetSetUpIncomeDAO();

                            int budgetSetUpIncomeResult = 0;
                            budgetSetUpIncomeResult = budgetSetUpIncomeDAO.InsertBudgetSetUpIncomeByBudgetId(BUDGET_ID, INCOME_CATEGORY, INCOME_SUBCATEGORY, INCOME_AMOUNT_ALLOCATED);

                            if (budgetSetUpIncomeResult > 0)
                            {
                                Lbl_err.Text = "Your Budget " + BUDGET_ID + " Income Category and SubCategory " + INCOME_SUBCATEGORY + " have been saved successfully!";
                                PanelErrorResult.Visible = true;
                            }
                            else
                            {
                                Lbl_err.Text = "Sorry, an error occurred while saving your Budget " + BUDGET_ID + " Income Category and SubCategory " + INCOME_SUBCATEGORY + ". Please inform System Administrator.";
                                PanelErrorResult.Visible = true;
                            } // if (budgetSetUpIncomeResult)
                        } // if((BUDGET_ID)
                    } // for (i)
                } //if (dtCurrentIncomeFullTable.Rows.Count)


                if (GridviewBudgetSetUpFixedCost.Rows.Count > 0)
                {
                    for (int i = 0; i < GridviewBudgetSetUpFixedCost.Rows.Count; i++)
                    {
                        // Get the addresses of the TextBoxes
                        TextBox box1 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[1].FindControl("tbFixedCostCategory");
                        TextBox box2 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[2].FindControl("tbFixedCostSubCategory");
                        TextBox box3 = (TextBox)GridviewBudgetSetUpFixedCost.Rows[i].Cells[3].FindControl("tbFixedCostSubCategoryAmountAllocated");

                        // Write values to TextBoxes
                        // box1.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"].ToString();
                        // box2.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"].ToString();
                        // box3.Text = dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"].ToString();

                        // Read values from TextBoxes
                        // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Category"] = box1.Text;
                        // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost SubCategory"] = box2.Text;
                        // dtCurrentFixedCostFullTable.Rows[i]["Fixed Cost Amount Allocated"] = box3.Text;

                        // Read values from TextBoxes
                        string FIXED_COST_CATEGORY = box1.Text;
                        string FIXED_COST_SUBCATEGORY = box2.Text;

                        string strFixedCostAmountAllocated = box3.Text;
                        double dblFixedCostAmountAllocated = 0.0;

                        // get the length of the current string
                        int intLength = strFixedCostAmountAllocated.Length;

                        while (intLength > 0)
                        {
                            // Check whether user input is numeric '3200'
                            bool result = double.TryParse(strFixedCostAmountAllocated, out dblFixedCostAmountAllocated);
                            if (result == true)
                            {
                                dblFixedCostAmountAllocated = double.Parse(strFixedCostAmountAllocated);
                                break;
                            }
                            else
                            {
                                // user input is non numeric '$3200'

                                // user input is numeric '3200'
                                strFixedCostAmountAllocated = strFixedCostAmountAllocated.Substring(1);

                                // get the length of the new string
                                intLength = strFixedCostAmountAllocated.Length;
                            } // if (result)
                        } // while (intLength)

                        dblFixedCostAmountAllocated = Math.Round(dblFixedCostAmountAllocated, 2);
                        if (dblFixedCostAmountAllocated == 0.0)
                        {
                            box3.Text = string.Empty;
                        }
                        else
                        {
                            box3.Text = Convert.ToString(dblFixedCostAmountAllocated);
                        } // if (dblFixedCostAmountAllocated)

                        double FIXED_COST_AMOUNT_ALLOCATED = dblFixedCostAmountAllocated;

                        // box1.ReadOnly = true;
                        // box2.ReadOnly = true;
                        // box3.ReadOnly = false;

                        // Check whether Category and SubCategory already exist in the DB
                        BudgetExpenditureCategory budgetFixedCostCategoryObj = new BudgetExpenditureCategory();
                        BudgetExpenditureCategoryDAO budgetFixedCostCategoryDAO = new BudgetExpenditureCategoryDAO();

                        if ((ACC_EMAIL != "") && (FIXED_COST_CATEGORY != "") && (FIXED_COST_SUBCATEGORY != ""))
                        {
                            budgetFixedCostCategoryObj = budgetFixedCostCategoryDAO.ReadBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                            int budgetFixedCostCategoryResult = 0;
                            if (budgetFixedCostCategoryObj == null)
                            {
                                budgetFixedCostCategoryResult = budgetFixedCostCategoryDAO.InsertBudgetFixedCostCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY);

                                if (budgetFixedCostCategoryResult > 0)
                                {
                                    Lbl_err.Text = "Your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + " have been saved successfully!";
                                    PanelErrorResult.Visible = true;
                                }
                                else
                                {
                                    Lbl_err.Text = "Sorry, an error occurred while saving your Budget Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + ". Please inform System Administrator.";
                                    PanelErrorResult.Visible = true;
                                } // if (budgetFixedCostCategoryResult)
                            } // if(budgetFixedCostCategoryObj)
                        } // if((ACC_EMAIL)

                        if ((BUDGET_ID != "") && (FIXED_COST_CATEGORY != "") && (FIXED_COST_SUBCATEGORY != "") && (FIXED_COST_AMOUNT_ALLOCATED > 0.0))
                        {
                            BudgetSetUpExpenditureDAO budgetSetUpFixedCostDAO = new BudgetSetUpExpenditureDAO();

                            int budgetSetUpFixedCostResult = 0;
                            budgetSetUpFixedCostResult = budgetSetUpFixedCostDAO.InsertBudgetSetUpFixedCostByBudgetId(BUDGET_ID, FIXED_COST_CATEGORY, FIXED_COST_SUBCATEGORY, FIXED_COST_AMOUNT_ALLOCATED);

                            if (budgetSetUpFixedCostResult > 0)
                            {
                                Lbl_err.Text = "Your Budget " + BUDGET_ID + " Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + " have been saved successfully!";
                                PanelErrorResult.Visible = true;
                            }
                            else
                            {
                                Lbl_err.Text = "Sorry, an error occurred while saving your Budget " + BUDGET_ID + " Fixed Cost Category and SubCategory " + FIXED_COST_SUBCATEGORY + ". Please inform System Administrator.";
                                PanelErrorResult.Visible = true;
                            } // if (budgetSetUpFixedCostResult)
                        } // if((BUDGET_ID)
                    } // for (i)
                } //if (dtCurrentFixedCostFullTable.Rows.Count)


                if (GridviewBudgetSetUpFlexSpending.Rows.Count > 0)
                {
                    for (int i = 0; i < GridviewBudgetSetUpFlexSpending.Rows.Count; i++)
                    {
                        // Get the addresses of the TextBoxes
                        TextBox box1 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[1].FindControl("tbFlexSpendingCategory");
                        TextBox box2 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[2].FindControl("tbFlexSpendingSubCategory");
                        TextBox box3 = (TextBox)GridviewBudgetSetUpFlexSpending.Rows[i].Cells[3].FindControl("tbFlexSpendingSubCategoryAmountAllocated");

                        // Write values to TextBoxes
                        // box1.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"].ToString();
                        // box2.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"].ToString();
                        // box3.Text = dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"].ToString();

                        // Read values from TextBoxes
                        // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Category"] = box1.Text;
                        // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending SubCategory"] = box2.Text;
                        // dtCurrentFlexSpendingFullTable.Rows[i]["Flex Spending Amount Allocated"] = box3.Text;

                        // Read values from TextBoxes
                        string FLEX_SPENDING_CATEGORY = box1.Text;
                        string FLEX_SPENDING_SUBCATEGORY = box2.Text;

                        string strFlexSpendingAmountAllocated = box3.Text;
                        double dblFlexSpendingAmountAllocated = 0.0;

                        // get the length of the current string
                        int intLength = strFlexSpendingAmountAllocated.Length;

                        while (intLength > 0)
                        {
                            // Check whether user input is numeric '3200'
                            bool result = double.TryParse(strFlexSpendingAmountAllocated, out dblFlexSpendingAmountAllocated);
                            if (result == true)
                            {
                                dblFlexSpendingAmountAllocated = double.Parse(strFlexSpendingAmountAllocated);
                                break;
                            }
                            else
                            {
                                // user input is non numeric '$3200'

                                // user input is numeric '3200'
                                strFlexSpendingAmountAllocated = strFlexSpendingAmountAllocated.Substring(1);

                                // get the length of the new string
                                intLength = strFlexSpendingAmountAllocated.Length;
                            } // if (result)
                        } // while (intLength)

                        dblFlexSpendingAmountAllocated = Math.Round(dblFlexSpendingAmountAllocated, 2);
                        if (dblFlexSpendingAmountAllocated == 0.0)
                        {
                            box3.Text = string.Empty;
                        }
                        else
                        {
                            box3.Text = Convert.ToString(dblFlexSpendingAmountAllocated);
                        } // if (dblFlexSpendingAmountAllocated)

                        double FLEX_SPENDING_AMOUNT_ALLOCATED = dblFlexSpendingAmountAllocated;

                        // box1.ReadOnly = true;
                        // box2.ReadOnly = true;
                        // box3.ReadOnly = false;

                        // Check whether Category and SubCategory already exist in the DB
                        BudgetExpenditureCategory budgetFlexSpendingCategoryObj = new BudgetExpenditureCategory();
                        BudgetExpenditureCategoryDAO budgetFlexSpendingCategoryDAO = new BudgetExpenditureCategoryDAO();

                        if ((ACC_EMAIL != "") && (FLEX_SPENDING_CATEGORY != "") && (FLEX_SPENDING_SUBCATEGORY != ""))
                        {
                            budgetFlexSpendingCategoryObj = budgetFlexSpendingCategoryDAO.ReadBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                            int budgetFlexSpendingCategoryResult = 0;
                            if (budgetFlexSpendingCategoryObj == null)
                            {
                                budgetFlexSpendingCategoryResult = budgetFlexSpendingCategoryDAO.InsertBudgetFlexSpendingCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY);

                                if (budgetFlexSpendingCategoryResult > 0)
                                {
                                    Lbl_err.Text = "Your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + " have been saved successfully!";
                                    PanelErrorResult.Visible = true;
                                }
                                else
                                {
                                    Lbl_err.Text = "Sorry, an error occurred while saving your Budget Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + ". Please inform System Administrator.";
                                    PanelErrorResult.Visible = true;
                                } // if (budgetFlexSpendingCategoryResult)
                            } // if(budgetFlexSpendingCategoryObj)
                        } // if((ACC_EMAIL)

                        if ((BUDGET_ID != "") && (FLEX_SPENDING_CATEGORY != "") && (FLEX_SPENDING_SUBCATEGORY != "") && (FLEX_SPENDING_AMOUNT_ALLOCATED > 0.0))
                        {
                            BudgetSetUpExpenditureDAO budgetSetUpFlexSpendingDAO = new BudgetSetUpExpenditureDAO();

                            int budgetSetUpFlexSpendingResult = 0;
                            budgetSetUpFlexSpendingResult = budgetSetUpFlexSpendingDAO.InsertBudgetSetUpFlexSpendingByBudgetId(BUDGET_ID, FLEX_SPENDING_CATEGORY, FLEX_SPENDING_SUBCATEGORY, FLEX_SPENDING_AMOUNT_ALLOCATED);

                            if (budgetSetUpFlexSpendingResult > 0)
                            {
                                Lbl_err.Text = "Your Budget " + BUDGET_ID + " Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + " have been saved successfully!";
                                PanelErrorResult.Visible = true;
                            }
                            else
                            {
                                Lbl_err.Text = "Sorry, an error occurred while saving your Budget " + BUDGET_ID + " Flex Spending Category and SubCategory " + FLEX_SPENDING_SUBCATEGORY + ". Please inform System Administrator.";
                                PanelErrorResult.Visible = true;
                            } // if (budgetSetUpFlexSpendingResult)
                        } // if((BUDGET_ID)
                    } // for (i)
                } //if (dtCurrentFlexSpendingFullTable.Rows.Count)


                if (GridviewBudgetSetUpDebtRepayment.Rows.Count > 0)
                {
                    for (int i = 0; i < GridviewBudgetSetUpDebtRepayment.Rows.Count; i++)
                    {
                        // Get the addresses of the TextBoxes
                        TextBox box1 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[1].FindControl("tbDebtRepaymentCategory");
                        TextBox box2 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[2].FindControl("tbDebtRepaymentSubCategory");
                        TextBox box3 = (TextBox)GridviewBudgetSetUpDebtRepayment.Rows[i].Cells[3].FindControl("tbDebtRepaymentSubCategoryAmountAllocated");

                        // Write values to TextBoxes
                        // box1.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"].ToString();
                        // box2.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"].ToString();
                        // box3.Text = dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"].ToString();

                        // Read values from TextBoxes
                        // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Category"] = box1.Text;
                        // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment SubCategory"] = box2.Text;
                        // dtCurrentDebtRepaymentFullTable.Rows[i]["Debt Repayment Amount Allocated"] = box3.Text;

                        // Read values from TextBoxes
                        string DEBT_REPAYMENT_CATEGORY = box1.Text;
                        string DEBT_REPAYMENT_SUBCATEGORY = box2.Text;

                        string strDebtRepaymentAmountAllocated = box3.Text;
                        double dblDebtRepaymentAmountAllocated = 0.0;

                        // get the length of the current string
                        int intLength = strDebtRepaymentAmountAllocated.Length;

                        while (intLength > 0)
                        {
                            // Check whether user input is numeric '3200'
                            bool result = double.TryParse(strDebtRepaymentAmountAllocated, out dblDebtRepaymentAmountAllocated);
                            if (result == true)
                            {
                                dblDebtRepaymentAmountAllocated = double.Parse(strDebtRepaymentAmountAllocated);
                                break;
                            }
                            else
                            {
                                // user input is non numeric '$3200'

                                // user input is numeric '3200'
                                strDebtRepaymentAmountAllocated = strDebtRepaymentAmountAllocated.Substring(1);

                                // get the length of the new string
                                intLength = strDebtRepaymentAmountAllocated.Length;
                            } // if (result)
                        } // while (intLength)

                        dblDebtRepaymentAmountAllocated = Math.Round(dblDebtRepaymentAmountAllocated, 2);
                        if (dblDebtRepaymentAmountAllocated == 0.0)
                        {
                            box3.Text = string.Empty;
                        }
                        else
                        {
                            box3.Text = Convert.ToString(dblDebtRepaymentAmountAllocated);
                        } // if (dblDebtRepaymentAmountAllocated)

                        double DEBT_REPAYMENT_AMOUNT_ALLOCATED = dblDebtRepaymentAmountAllocated;

                        // box1.ReadOnly = true;
                        // box2.ReadOnly = true;
                        // box3.ReadOnly = false;

                        // Check whether Category and SubCategory already exist in the DB
                        BudgetExpenditureCategory budgetDebtRepaymentCategoryObj = new BudgetExpenditureCategory();
                        BudgetExpenditureCategoryDAO budgetDebtRepaymentCategoryDAO = new BudgetExpenditureCategoryDAO();

                        if ((ACC_EMAIL != "") && (DEBT_REPAYMENT_CATEGORY != "") && (DEBT_REPAYMENT_SUBCATEGORY != ""))
                        {
                            budgetDebtRepaymentCategoryObj = budgetDebtRepaymentCategoryDAO.ReadBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                            int budgetDebtRepaymentCategoryResult = 0;
                            if (budgetDebtRepaymentCategoryObj == null)
                            {
                                budgetDebtRepaymentCategoryResult = budgetDebtRepaymentCategoryDAO.InsertBudgetDebtRepaymentCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY);

                                if (budgetDebtRepaymentCategoryResult > 0)
                                {
                                    Lbl_err.Text = "Your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + " have been saved successfully!";
                                    PanelErrorResult.Visible = true;
                                }
                                else
                                {
                                    Lbl_err.Text = "Sorry, an error occurred while saving your Budget Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + ". Please inform System Administrator.";
                                    PanelErrorResult.Visible = true;
                                } // if (budgetDebtRepaymentCategoryResult)
                            } // if(budgetDebtRepaymentCategoryObj)
                        } // if((ACC_EMAIL)

                        if ((BUDGET_ID != "") && (DEBT_REPAYMENT_CATEGORY != "") && (DEBT_REPAYMENT_SUBCATEGORY != "") && (DEBT_REPAYMENT_AMOUNT_ALLOCATED > 0.0))
                        {
                            BudgetSetUpExpenditureDAO budgetSetUpDebtRepaymentDAO = new BudgetSetUpExpenditureDAO();

                            int budgetSetUpDebtRepaymentResult = 0;
                            budgetSetUpDebtRepaymentResult = budgetSetUpDebtRepaymentDAO.InsertBudgetSetUpDebtRepaymentByBudgetId(BUDGET_ID, DEBT_REPAYMENT_CATEGORY, DEBT_REPAYMENT_SUBCATEGORY, DEBT_REPAYMENT_AMOUNT_ALLOCATED);

                            if (budgetSetUpDebtRepaymentResult > 0)
                            {
                                Lbl_err.Text = "Your Budget " + BUDGET_ID + " Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + " have been saved successfully!";
                                PanelErrorResult.Visible = true;
                            }
                            else
                            {
                                Lbl_err.Text = "Sorry, an error occurred while saving your Budget " + BUDGET_ID + " Debt Repayment Category and SubCategory " + DEBT_REPAYMENT_SUBCATEGORY + ". Please inform System Administrator.";
                                PanelErrorResult.Visible = true;
                            } // if (budgetSetUpDebtRepaymentResult)
                        } // if((BUDGET_ID)
                    } // for (i)
                } //if (dtCurrentDebtRepaymentFullTable.Rows.Count)                


                if (GridviewBudgetSetUpPriorityGoals.Rows.Count > 0)
                {
                    for (int i = 0; i < GridviewBudgetSetUpPriorityGoals.Rows.Count; i++)
                    {
                        // Get the addresses of the TextBoxes
                        TextBox box1 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[1].FindControl("tbPriorityGoalsCategory");
                        TextBox box2 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[2].FindControl("tbPriorityGoalsSubCategory");
                        TextBox box3 = (TextBox)GridviewBudgetSetUpPriorityGoals.Rows[i].Cells[3].FindControl("tbPriorityGoalsSubCategoryAmountAllocated");

                        // Write values to TextBoxes
                        // box1.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"].ToString();
                        // box2.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"].ToString();
                        // box3.Text = dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"].ToString();

                        // Read values from TextBoxes
                        // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Category"] = box1.Text;
                        // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals SubCategory"] = box2.Text;
                        // dtCurrentPriorityGoalsFullTable.Rows[i]["Priority Goals Amount Allocated"] = box3.Text;

                        // Read values from TextBoxes
                        string PRIORITY_GOALS_CATEGORY = box1.Text;
                        string PRIORITY_GOALS_SUBCATEGORY = box2.Text;

                        string strPriorityGoalsAmountAllocated = box3.Text;
                        double dblPriorityGoalsAmountAllocated = 0.0;

                        // get the length of the current string
                        int intLength = strPriorityGoalsAmountAllocated.Length;

                        while (intLength > 0)
                        {
                            // Check whether user input is numeric '3200'
                            bool result = double.TryParse(strPriorityGoalsAmountAllocated, out dblPriorityGoalsAmountAllocated);
                            if (result == true)
                            {
                                dblPriorityGoalsAmountAllocated = double.Parse(strPriorityGoalsAmountAllocated);
                                break;
                            }
                            else
                            {
                                // user input is non numeric '$3200'

                                // user input is numeric '3200'
                                strPriorityGoalsAmountAllocated = strPriorityGoalsAmountAllocated.Substring(1);

                                // get the length of the new string
                                intLength = strPriorityGoalsAmountAllocated.Length;
                            } // if (result)
                        } // while (intLength)

                        dblPriorityGoalsAmountAllocated = Math.Round(dblPriorityGoalsAmountAllocated, 2);
                        if (dblPriorityGoalsAmountAllocated == 0.0)
                        {
                            box3.Text = string.Empty;
                        }
                        else
                        {
                            box3.Text = Convert.ToString(dblPriorityGoalsAmountAllocated);
                        } // if (dblPriorityGoalsAmountAllocated)

                        double PRIORITY_GOALS_AMOUNT_ALLOCATED = dblPriorityGoalsAmountAllocated;

                        // box1.ReadOnly = true;
                        // box2.ReadOnly = true;
                        // box3.ReadOnly = false;

                        // Check whether Category and SubCategory already exist in the DB
                        BudgetExpenditureCategory budgetPriorityGoalsCategoryObj = new BudgetExpenditureCategory();
                        BudgetExpenditureCategoryDAO budgetPriorityGoalsCategoryDAO = new BudgetExpenditureCategoryDAO();

                        if ((ACC_EMAIL != "") && (PRIORITY_GOALS_CATEGORY != "") && (PRIORITY_GOALS_SUBCATEGORY != ""))
                        {
                            budgetPriorityGoalsCategoryObj = budgetPriorityGoalsCategoryDAO.ReadBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                            int budgetPriorityGoalsCategoryResult = 0;
                            if (budgetPriorityGoalsCategoryObj == null)
                            {
                                budgetPriorityGoalsCategoryResult = budgetPriorityGoalsCategoryDAO.InsertBudgetPriorityGoalsCategoryAndSubCategoryByEmailCategoryAndSubCategory(ACC_EMAIL, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY);

                                if (budgetPriorityGoalsCategoryResult > 0)
                                {
                                    Lbl_err.Text = "Your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + " have been saved successfully!";
                                    PanelErrorResult.Visible = true;
                                }
                                else
                                {
                                    Lbl_err.Text = "Sorry, an error occurred while saving your Budget Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + ". Please inform System Administrator.";
                                    PanelErrorResult.Visible = true;
                                } // if (budgetPriorityGoalsCategoryResult)
                            } // if(budgetPriorityGoalsCategoryObj)
                        } // if((ACC_EMAIL)

                        if ((BUDGET_ID != "") && (PRIORITY_GOALS_CATEGORY != "") && (PRIORITY_GOALS_SUBCATEGORY != "") && (PRIORITY_GOALS_AMOUNT_ALLOCATED > 0.0))
                        {
                            BudgetSetUpExpenditureDAO budgetSetUpPriorityGoalsDAO = new BudgetSetUpExpenditureDAO();

                            int budgetSetUpPriorityGoalsResult = 0;
                            budgetSetUpPriorityGoalsResult = budgetSetUpPriorityGoalsDAO.InsertBudgetSetUpPriorityGoalsByBudgetId(BUDGET_ID, PRIORITY_GOALS_CATEGORY, PRIORITY_GOALS_SUBCATEGORY, PRIORITY_GOALS_AMOUNT_ALLOCATED);

                            if (budgetSetUpPriorityGoalsResult > 0)
                            {
                                Lbl_err.Text = "Your Budget " + BUDGET_ID + " Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + " have been saved successfully!";
                                PanelErrorResult.Visible = true;
                            }
                            else
                            {
                                Lbl_err.Text = "Sorry, an error occurred while saving your Budget " + BUDGET_ID + " Priority Goals Category and SubCategory " + PRIORITY_GOALS_SUBCATEGORY + ". Please inform System Administrator.";
                                PanelErrorResult.Visible = true;
                            } // if (budgetSetUpPriorityGoalsResult)
                        } // if((BUDGET_ID)
                    } // for (i)
                } //if (dtCurrentPriorityGoalsFullTable.Rows.Count)

                // Update BudgetDashBoard in the DB
                // BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                // BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();

                budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(BUDGET_ID);
            } // if (BUDGET_ID)

            if (BUDGET_ID != null)
            {
                Lbl_err.Text = "Your Budget " + BUDGET_ID + " has been saved successfully!";
                PanelErrorResult.Visible = true;
            }
            else
            {
                Lbl_err.Text = "Sorry, an error occurred while saving your Budget. Please inform System Administrator.";
                PanelErrorResult.Visible = true;
            } // if (BUDGET_ID)
        } // btnSaveBudgetSetUp_Click()


        protected void StartDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year != 1) && (EndDateCalendar.SelectedDate.Year == 1))
            {
                EndDateCalendar.SelectedDate = StartDateCalendar.SelectedDate.AddDays(30);

                lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());
            } // if (EndDateCalendar.SelectedDate.Year)
        } // StartDateCalendar_SelectionChanged()


        protected void EndDateCalendar_SelectionChanged(object sender, EventArgs e)
        {
            lblEndDate.Text = GetDateString(EndDateCalendar.SelectedDate.ToString());

            if ((StartDateCalendar.SelectedDate.Year == 1) && (EndDateCalendar.SelectedDate.Year != 1))
            {
                StartDateCalendar.SelectedDate = EndDateCalendar.SelectedDate.AddDays(-30);

                lblStartDate.Text = GetDateString(StartDateCalendar.SelectedDate.ToString());
            } // if (StartDateCalendar.SelectedDate.Year)
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

    } // BudgetSetUp
} // PrestoPay