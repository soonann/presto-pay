using Newtonsoft.Json;
using PrestoPay.Entity;
using PrestoPay.Entity.Api_Entites;
using PrestoPay.Entity.DB_Entities;
using PrestoPay.Entity.View_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class PaymentGateway : System.Web.UI.Page
    {
        protected double paymentAmount = 0;
        protected Payment currentPayment = new Payment();
        protected string recipient = "";
        protected string key = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!new User().auth())
            {
                Panel_Payment.Visible = false;
                Panel_Login.Visible = true;
                Label_LoggedInAs.Text = "";
                Label_panelTitle.Text = "Login";
                Button_signout.Visible = false;

            }
            else
            {
                Panel_Payment.Visible = true;
                Panel_Login.Visible = false;
                Label_LoggedInAs.Text = Session["UserEmail"].ToString();
                Label_panelTitle.Text = "Payment";
                Button_signout.Visible = true;

            }
            

            if (Request.QueryString["id"] != null)
            {
                var prestoKey = Request.QueryString["id"].ToString();
                key = prestoKey;
                var paymentDetails = new Payment();
                
                if (paymentDetails.RetrieveOrderAndItemsByPrestoKey(prestoKey))
                {
                    currentPayment = paymentDetails;
                    AccountDAO ac = new AccountDAO();
                    string email = ac.RetrieveUserEmailByPresto(prestoKey);
                    // presto key to business to email 
                    if (paymentDetails.OrderDetails.Order_Paid == 0)
                        populatePaymentTable(email, paymentDetails.ItemList);
                    else
                        Response.Redirect("~/PageNotFound.aspx");
                }
                else
                {
                    Response.Redirect("~/PageNotFound.aspx");
                    //show not found screen
                }

                /*
                var psList = new List<Item>();
                psList.Add(new Item() { ItemDescription = "Arm Chair", ItemPrice = 10 });
                psList.Add(new Item() { ItemDescription = "Mouse", ItemPrice = 10 });
                psList.Add(new Item() { ItemDescription = "Table", ItemPrice = 10 });
                */


            }
            else
            {

                Response.Redirect("~/PageNotFound.aspx");
                //show not found screen
            }
           
            

        }

        protected void Button_logout_Click(object sender, EventArgs e)
        {
            Session["UserEmail"] = null;
            Session["UserName"] = null;
            Session["UserType"] = null;

            Panel_Payment.Visible = false;
            Panel_Login.Visible = true;
            Label_LoggedInAs.Text = "";
            Label_panelTitle.Text = "Login";
            Button_signout.Visible = false;
            TextBox_email.Text = String.Empty;
            TextBox_password.Text = String.Empty;



        }


        protected void Button_login_Click(object sender, EventArgs e)
        {

            string tb_email = TextBox_email.Text;
            string tb_password = TextBox_password.Text;
            bool tbIsEmpty = (String.IsNullOrWhiteSpace(tb_email) || String.IsNullOrWhiteSpace(tb_password));
            if (tbIsEmpty)
            {

                this.Master.ShowAlertPopout("Error !", "Please fill in all fields !", "error");
            }
            else
            {

                AccountDAO da = new AccountDAO();
                Account ac = da.RetrieveAccountByEmail(tb_email);

                if (ac != null)
                {

                    if (ac.password == tb_password)
                    {
                        Session["UserEmail"] = tb_email.First().ToString().ToUpper() + tb_email.Substring(1);
                        Session["UserName"] = ac.name;

                        if (ac.busi_id == null)
                            Session["UserType"] = "Personal";
                        else
                            Session["UserType"] = "Enterprise";

                        Panel_Payment.Visible = true;
                        Panel_Login.Visible = false;
                        Label_LoggedInAs.Text = Session["UserEmail"].ToString();
                        Label_panelTitle.Text = "Payment";
                        Button_signout.Visible = true;

                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "Invalid Login !", "error");

                    }

                }
                else
                {
                    this.Master.ShowAlertPopout("Error !", "Invalid Login !", "error");

                }


            }




        }

        protected void Button_WalletPay_ServerClick(object sender, EventArgs e)
        {
            Panel_WalletChosen.Visible = true;
            Panel_ChoosePaymentMethod.Visible = false;
            string currentUserEmail = Session["UserEmail"].ToString();
            
            AccountDAO ac = new AccountDAO();
            double currentBal = ac.RetrieveWalletBalanceByEmail(currentUserEmail);
            Label_CurrentWalletBal.Text = currentBal.ToString("SGD $,###,##0.00 ");
           // string paymentAmount = currentPayment.


            Label_PaymentAmount.Text = paymentAmount.ToString("- SGD $,###,##0.00");

            if (currentBal < paymentAmount)
                Label_BalanceAftPayment.Text = 0.ToString("SGD $,###,##0.00 ");
            else
                Label_BalanceAftPayment.Text = (currentBal - paymentAmount).ToString("SGD $,###,##0.00");
        }

        protected void Button_CreditCardPay_ServerClick(object sender, EventArgs e)
        {
            // display card panel
            // show ddl of cards to pay with
            // 
            
   
             
    
            string currentUserEmail = Session["UserEmail"].ToString();
        
            var list = new CreditCardDAO().RetrieveCardsOfUser(currentUserEmail);
            if (list != null) {
                DropDownList_CreditCard.Items.Clear();
                foreach (CreditCard c in list)
                {
                    string cardNo = c.creditcardnum;
                    int noofchar = cardNo.Length;
                    string frontpart = cardNo.Substring(0, 4);
                    string midpart = "";
                    string backend = cardNo.Substring(noofchar - 4, 4);
                    int noX = (noofchar - 4 - 4);
                    for (int i = 1; i <= noX; i++)
                        midpart += "X";
                    string final = frontpart + midpart + backend;
                    DropDownList_CreditCard.Items.Add(new ListItem() { Text = final, Value = final });
                }


                Panel_CardChosen.Visible = true;
                Panel_ChoosePaymentMethod.Visible = false;
                Panel_WalletChosen.Visible = false;
            }
            else
            {

                this.Master.ShowAlertPopout("Warning !", "You cannot pay with this method ! <br/>You do not have any credit cards linked to this account", "error");
            }
           
            

            // populate list 
          //  DropDownList_CreditCard.Items.Clear();
         //   DropDownList_CreditCard.Items.Add(){ }
            

        }

        private void populatePaymentTable(String paymentTo, List<Item> psList)
        {
            if(psList == null)           
                Response.Redirect("~/PageNotFound.aspx");
            
            Label_PaymentTo.Text = paymentTo;
            //currentPayment.OrderDetails. = paymentTo;
            recipient = paymentTo;

            double subtotalPrice = 0;

            for (int i = 0; i < psList.Count; i++)
            {

                var tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = psList[i].ItemDescription, CssClass = "leftAlignText" });
                tr.Cells.Add(new TableCell() { Text = psList[i].ItemPrice.ToString("SGD $,###,##0.00"), CssClass = "rightAlignText" });
                subtotalPrice += psList[i].ItemPrice;
                TblPaymentSummary.Rows.Add(tr);

            }

            var subTotalRow = new TableRow();
            var subTotalCell = new TableCell();
            subTotalRow.Attributes["class"] = "subTotalRow";
            subTotalRow.Cells.Add(new TableCell() { Text = "Total Amount:", CssClass = "boldOnly leftAlignText" });
            subTotalRow.Cells.Add(new TableCell() { Text = subtotalPrice.ToString("SGD $,###,##0.00"), CssClass = "boldOnly rightAlignText" });
            TblPaymentSummary.Rows.Add(subTotalRow);
            /*
            var grandTotalRow = new TableRow();
            grandTotalRow.Cells.Add(new TableCell() { Text = "Grand-Total:", CssClass = "boldOnly leftAlignText" });
            grandTotalRow.Cells.Add(new TableCell() { Text = subtotalPrice.ToString("SGD $,###,##0.00"), CssClass = "boldOnly rightAlignText" });
            grandTotalRow.Attributes["class"] = "grandTotalRow";
            TblPaymentSummary.Rows.Add(grandTotalRow);
            */

            paymentAmount = subtotalPrice;
        }

        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }

        protected void Button_MakePayment_ServerClick(object sender, EventArgs e)
        {
            // popout are you sure

            string current = Session["UserEmail"].ToString();
            if(recipient.ToUpper() == current.ToUpper())
             this.Master.ShowAlertPopout("Error !", "You cannot make payment to yourself !", "error");
            else
            {
                BusinessDAO bd = new BusinessDAO();
                Business b = bd.GetBusinessIdByEmail(current);
                if (b == null)
                {

                    string Budget_Id = GetLatestBudgetIdByEmail(current);
                    BudgetDashBoard budgetDashBoardObj = new BudgetDashBoard();
                    BudgetDashBoardDAO budgetDashBoardDAO = new BudgetDashBoardDAO();
                    if(budgetDashBoardObj == null)
                    {
                        this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment with Presto Wallet? ", "confirm");
                        ViewState["PaymentState"] = "Wallet";

                    }
                    else
                    {

                        budgetDashBoardObj = budgetDashBoardDAO.UpdateBudgetDashBoardByBudgetId(Budget_Id);
                        budgetDashBoardObj = budgetDashBoardDAO.CheckBudgetDashBoardByBudgetId(Budget_Id);
                        //-success-warning-danger
                        double totalAmt = budgetDashBoardObj.budget_flexSpendingAmountAllocated;
                        double spentAmt = budgetDashBoardObj.budget_flexSpendingAmountSpent;

                        double percentageLeft = ((totalAmt - spentAmt) / totalAmt) * 100;
                        double percentage = (spentAmt / totalAmt) * 100;
                        if (percentageLeft <= 0 || percentage < 0)
                        {
                            this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment with Presto Wallet? ", "confirm");
                            ViewState["PaymentState"] = "Wallet";
                        }
                        else if (percentageLeft < 33.33)
                        {  // You have used more than half of your budgeted amount ! 
                            string load = "<div class='progress'><div class='progress-bar-danger' role='progressbar' aria-valuenow='" + percentageLeft + "' aria-valuemin='0' aria-valuemax='100' style='width:" + percentageLeft + "%'>&nbsp;</div></div>";
                            this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment?<br/>" + load + "<br/> You are in the red !", "confirm");
                            ViewState["PaymentState"] = "Wallet";
                        }
                        else if (percentageLeft < 66.66)
                        {
                            // exceeded total
                            string load = "<div class='progress'><div class='progress-bar-warning' role='progressbar' aria-valuenow='" + percentageLeft + "' aria-valuemin='0' aria-valuemax='100' style='width:" + percentageLeft + "%'>&nbsp;</div></div>";
                            this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment? <br/>" + load + "<br/> You are in the orange !", "confirm");
                            ViewState["PaymentState"] = "Wallet";
                        }
                        else
                        {
                            string load = "<div class='progress'><div class='progress-bar-success' role='progressbar' aria-valuenow='" + percentageLeft + "' aria-valuemin='0' aria-valuemax='100' style='width:" + percentageLeft + "%'>&nbsp;</div></div>";
                            this.Master.ShowAlertPopout("Payment with Presto Wallet", "Do you want to make this payment ?<br/>" + load + "<br/> You are in the green ", "confirm");
                            ViewState["PaymentState"] = "Wallet";

                        }


                    }

                }
                else
                {

                    this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment with Presto Wallet? ", "confirm");
                    ViewState["PaymentState"] = "Wallet";
                }

                // check if budget over ? 
                // if yes, prompt , else do nothing 
                //create trans with default item and "Flex Spending"

                //this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment?", "confirm");
                //ViewState["PaymentState"] = "Wallet";
            }
           
        }

        protected void Button_MakePaymentCC_ServerClick(object sender, EventArgs e)
        {

            // check if cc balance is available
            var currentuser = Session["UserEmail"].ToString();
            BankDAO bd = new BankDAO();
            double remainingcred = bd.GetCreditRemainingByUser(currentuser);
            double bal = remainingcred - paymentAmount;


            if ( remainingcred>0 && bal >=0)
            {
                // available

                this.Master.ShowAlertPopout("Payment", "Are you sure you want to pay with your Credit Card ?","confirm");
                ViewState["PaymentState"] = "CreditCard";
                ViewState["AmtBal"] = bal;
            }
            else
            {
                this.Master.ShowAlertPopout("Card limit has been exceeded", "Please choose another card or pay with a different method","error");
            }





        }

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

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Panel_ChoosePaymentMethod.Visible = true;
            Panel_WalletChosen.Visible = false;
            Panel_CardChosen.Visible = false;

        }

        


        protected void Popout_Alert_Yes_Click(object sender, EventArgs e)
        {
            if(ViewState["PaymentState"] != null)
            {

                if(ViewState["PaymentState"].ToString() == "Wallet")
                {

                    string redirect = currentPayment.OrderDetails.Order_UrlRedirect;
                    
                    var ac = new AccountDAO();
                    string payee = Session["UserEmail"].ToString();




                    string bizID = new BusinessDAO().GetBusinessIdByEmail(recipient).busi_id;
                    string defaultType = new BusinessDAO().getBusinessById(bizID).busi_defaultItem;

                    string transNo = new TransactionDAO().InsertTransaction( paymentAmount, "Payment - " + defaultType ,"Online Payment",payee,recipient);
                    new CategorisedTransactionDAO().WriteCategorisedTransactionByTransID(transNo, "Flex Spending", defaultType);
                    new OrderDAO().UpdateOrderPaidByPrestoKey(key,1);                  
                  

                    LoanRepaymentDAO loanRepaymentDAO = new LoanRepaymentDAO();
                    double dblRemainingAmount = loanRepaymentDAO.PerformLoanRepaymentByAccountEmail(recipient, paymentAmount, transNo);
                    ac.AddAmountToWallet(recipient, dblRemainingAmount);
                    ac.DeductAmountFromWallet(payee, paymentAmount);



                    // transaction successful ! redirect to merchant 
                    // call merchant api

                    bool apiCallResult = false;
                    string returnkey = new ReturnCall().getReturnKeyByEmail(recipient);
                    if(returnkey != null)
                        apiCallResult = CallMerchantApi(returnkey, currentPayment.OrderDetails.Order_RefNo);
         
                    this.Master.HideAlertPopout();

                    if (apiCallResult)
                    {
                        try
                        {

                            
                            this.Master.ShowAlertPopout("Success !", "<div class='text-center'>Payment succesfully made ! </br>Redirecting you back now !<br/>Click <a href= '" + redirect + "'>here</a> if you are not redirected.<br/> <img src='Images/Rolling.gif' class='center-block' width='50' height ='50'/></div>", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", "setTimeout(function(){ window.location.href ='" + redirect + "' },6000);", true);
                        }
                        catch (Exception ex) {


                            this.Master.ShowAlertPopout("Error !", "Transaction No: " + transNo + "<br/>Your payment was successful but we failed to redirect you back to your merchant !  Click <a href= '" + redirect + "'>here</a> to manually redirect to your merchant", "error");
                        }

                        }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "Transaction No: " + transNo + "<br/>Your payment was successful but we failed to redirect you back to your your merchant !  Click <a href= '" + redirect + "'>here</a> to manually redirect to your merchant", "error");
                    }  


                }
                else if (ViewState["PaymentState"].ToString() == "CreditCard")
                {

                    string redirect = currentPayment.OrderDetails.Order_UrlRedirect;

                    var ac = new AccountDAO();
                    string payee = Session["UserEmail"].ToString();




                    string bizID = new BusinessDAO().GetBusinessIdByEmail(recipient).busi_id;
                    string defaultType = new BusinessDAO().getBusinessById(bizID).busi_defaultItem;

                    string transNo = new TransactionDAO().InsertTransaction(paymentAmount, "CreditCard - " + defaultType, "Online Payment",  payee, recipient);
                    new CategorisedTransactionDAO().WriteCategorisedTransactionByTransID(transNo, "Flex Spending", defaultType);
                    new OrderDAO().UpdateOrderPaidByPrestoKey(key, 1);


                    LoanRepaymentDAO loanRepaymentDAO = new LoanRepaymentDAO();
                    double dblRemainingAmount = loanRepaymentDAO.PerformLoanRepaymentByAccountEmail(recipient, paymentAmount, transNo);
                    ac.AddAmountToWallet(recipient, dblRemainingAmount);
                    //ac.DeductAmountFromWallet(payee, paymentAmount);
                    BankDAO bd = new BankDAO();
                    bd.UpdateUserRemainingCredit(payee, Convert.ToDouble(ViewState["AmtBal"]));


                    // transaction successful ! redirect to merchant 
                    // call merchant api

                    bool apiCallResult = false;
                    string returnkey = new ReturnCall().getReturnKeyByEmail(recipient);
                    if (returnkey != null)
                        apiCallResult = CallMerchantApi(returnkey, currentPayment.OrderDetails.Order_RefNo);

                    this.Master.HideAlertPopout();

                    if (apiCallResult)
                    {
                        try
                        {


                            this.Master.ShowAlertPopout("Success !", "<div class='text-center'>Payment succesfully made ! </br>Redirecting you back now !<br/>Click <a href= '" + redirect + "'>here</a> if you are not redirected.<br/> <img src='Images/Rolling.gif' class='center-block' width='50' height ='50'/></div>", "");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "SomestartupScript", "setTimeout(function(){ window.location.href ='" + redirect + "' },6000);", true);
                        }
                        catch (Exception ex)
                        {


                            this.Master.ShowAlertPopout("Error !", "Transaction No: " + transNo + "<br/>Your payment was successful but we failed to redirect you back to your merchant !  Click <a href= '" + redirect + "'>here</a> to manually redirect to your merchant", "error");
                        }

                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "Transaction No: " + transNo + "<br/>Your payment was successful but we failed to redirect you back to your your merchant !  Click <a href= '" + redirect + "'>here</a> to manually redirect to your merchant", "error");
                    }


                }
                else if(ViewState["PaymentState"].ToString() == "BudgetConfirm")
                {

                    this.Master.ShowAlertPopout("Payment with Presto Wallet", "Are you sure you want to make this payment?", "confirm");
                    ViewState["PaymentState"] = "Wallet";


                }


            }
            else 
            {
                
            }


        }


        private bool CallMerchantApi(string apiKey, string referenceNo)
        {

            var client = new HttpClient();
            var data = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("apiKey",apiKey),
                  new KeyValuePair<string, string>("referenceNo",referenceNo),
            };


            var converted = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(converted);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //var response = client.PostAsync("http://localhost:5000/api/PaymentStatus/", byteContent).Result;
            var response = client.GetAsync("http://localhost:5000/api/PaymentStatus?apiKey=" + apiKey+"&referenceNo="+referenceNo).Result;
            //client.GetAsync("http://localhost:3000/api/Payment/", byteContent).Result;
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;



        }
    }
}