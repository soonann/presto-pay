using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class PrestoQrPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(new User().auth()))
                Response.Redirect("~/Login.aspx");

            AccountDAO ad = new AccountDAO();
            var user = ad.RetrieveAccountByEmail(Session["UserEmail"].ToString());
            if(user.busi_id == null)
            {
                //personal
                Panel_NavbarOne.Visible = false;
                Panel_NavbarBoth.Visible = true;
                if (Panel_RequestAmount.Visible)
                    Panel_Receive.Visible = false;

            }
            else
            {
                // enterprise
                Panel_NavbarOne.Visible = true;
                Panel_NavbarBoth.Visible = false;
                Panel_Pay.Visible = false;
                Panel_Receive.Visible = true;


            }



            QrPayKey qr = new QrPayKeyDAO().RetrieveLatestQrPayKeyByUserId(Session["UserEmail"].ToString());
            if (qr != null) {

                setImageVal(qr.Qr_Key);
               
            }
            else
            {
                string newKey = new QrPayKeyDAO().GenerateKeyForUser(Session["UserEmail"].ToString());
                setImageVal(newKey); ;
            }

            if(!Page.IsPostBack)
                Button_ScanTab.CssClass += " tabActive";
        }

        
        private void setImageVal(string value)
        {
            Image_QrCode.ImageUrl = "~/Images/Rolling.gif";
            Image_QrCode.ImageUrl = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + value;
            //Image_QrCode.ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=400x400&data=" + value;
            staticurl.HRef = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + value;
        }


     

        protected void Button_RequestAmount_Click(object sender, EventArgs e)
        {

            string amount = TextBox_Request.Text;
            double amountConverted = 0;
            bool isValidAmt = true ;
            if (amount!= null)
            {

                try
                {
                   amountConverted = Convert.ToDouble(amount);
                    if (amountConverted <= 0)
                       isValidAmt = false;
                }
                catch (Exception ex) {
                    isValidAmt = false;
                }

                if (isValidAmt)
                {
                    // generate and show user qr code
                    Panel_Receive.Visible = false;
                    string newKey = new QrPayAmtDAO().GenerateNewKeyForUser(Session["UserEmail"].ToString(), amountConverted);
                    Image_Request.ImageUrl = "~/Images/Rolling.gif";
                    Image_Request.ImageUrl = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + newKey;
                    requestUrl.HRef = "https://chart.googleapis.com/chart?cht=qr&chs=400x400&chl=" + newKey;

                }
                else
                {

                    this.Master.ShowAlertPopout("Error !", "Please enter a valid amount !", "error");
                    // not valid amt 
                }


            }
            else
            {
                this.Master.ShowAlertPopout("Error !", "Please enter an amount !", "error");
                //please enter an amount
            }



        }

        protected void HiddenField_UserScannedKey_ValueChanged(object sender, EventArgs e)
        {

            string key = HiddenField_UserScannedKey.Value.ToString();
            var result = new JsonResult();
            var qrDAO = new QrPayKeyDAO();
            var qrAmtDAO = new QrPayAmtDAO();

            string email = qrDAO.IndentifyUserByHash(key);
            string emailAmt = qrAmtDAO.IndentifyUserByHash(key);


            if (email == null && emailAmt == null)
            {

                // show error panel
                this.Master.ShowAlertPopout("Error !","Invalid QR Code !","error");

            }
            else
            {
              

                if (email == null)
                {
                    if (emailAmt.ToUpper() != Session["UserEmail"].ToString().ToUpper() ) {
                        // payment with amount email 
                        var amount = qrAmtDAO.RetrieveAmountByKey(key);
                        if (amount > 0)
                        {
                            //are you sure you want to pay

                            this.Master.ShowAlertPopout("Payment", "Are you sure you want to pay SGD " + amount.ToString("$#,###,###.00") + " to " + emailAmt + " ?", "confirm");
                            ViewState["AlertPopoutState"] = "ConfirmPayment";
                            ViewState["AlertPopoutAmount"] = amount;
                            ViewState["AlertPopoutRecipient"] = emailAmt;



                        }
                        else
                        {
                            //error ! amount requested is 0 
                            this.Master.ShowAlertPopout("Error !", "Amount requested was lesser than 0 !", "error");
                        }
                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "You are making a payment to yourself !", "error");
                    }
                    
                    
                }

                else
                {
                    // pay without amount
                    if (email.ToUpper() != Session["UserEmail"].ToString().ToUpper())
                    {


                        var qr = qrDAO.RetrieveLatestQrPayKeyByUserId(email);
                        if (qr.Qr_Key == key)
                        {
                            // are you sure you want to transact with this person?
                            this.Master.ShowPromptPopout("Payment", "Please enter the amount you wish to pay " + email + " : ");
                            ViewState["PromptPopoutState"] = "QrPayEnterAmt";
                            ViewState["PromptPopoutRecipient"] = email;

                        }
                        else
                        {
                            // An error occured !

                            this.Master.ShowAlertPopout("Error !", "This key is not valid !", "error");

                        }
                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "You are making a payment to yourself !", "error");
                    }

                }

            }


            HiddenField_UserScannedKey.Value = null;




        }

        protected void Popout_Alert_Yes_Click(object sender, EventArgs e)
        {
            if(ViewState["AlertPopoutState"] != null)
            {
                string stateOfPopout = ViewState["AlertPopoutState"].ToString();


                if ( stateOfPopout == "ConfirmPayment")
                {


                    //check if sufficient amt
                    // user pay to others 
                    var acc = new AccountDAO();
                    var payee = Session["UserEmail"].ToString();
                    var recipient = ViewState["AlertPopoutRecipient"].ToString();
                    double walletbal = acc.RetrieveWalletBalanceByEmail(payee);
                    double amtToPay =Convert.ToDouble(ViewState["AlertPopoutAmount"]);
                    if (walletbal >= amtToPay)
                    {

                        var trans = new TransactionDAO();
                        var Busi = new BusinessDAO().GetBusinessIdByEmail(recipient);
                  

                        string TransCode = "";

                        // deduct start
                        if (Busi!= null)
                        {
                            string bizID = Busi.busi_id;
                            string defaultType = new BusinessDAO().getBusinessById(bizID).busi_defaultItem;
                            TransCode = trans.InsertTransaction(amtToPay, "QRPay - " + defaultType, "QR Pay", payee, recipient);
                            new CategorisedTransactionDAO().WriteCategorisedTransactionByTransID(TransCode, "Flex Spending", defaultType);
                            LoanRepaymentDAO loanRepaymentDAO = new LoanRepaymentDAO();
                            double dblRemainingAmount = loanRepaymentDAO.PerformLoanRepaymentByAccountEmail(recipient, amtToPay, TransCode);
                            acc.AddAmountToWallet(recipient, dblRemainingAmount);
                        }
                        else
                        {
                           TransCode = trans.InsertTransaction(amtToPay, "QRPay - Transfer" , "QR Pay", payee, recipient);
                            acc.AddAmountToWallet(recipient,amtToPay);
                        }
                       
                        acc.DeductAmountFromWallet(payee, amtToPay);



                        /// deduct end 
                        /// 



                        this.Master.HideAlertPopout();
                       // Response.Redirect(Request.RawUrl);
                        this.Master.ShowAlertPopout("Success !", "Payment Successful !<br/>Transaction No:" + TransCode+ "<br/>Thank you for paying with PrestoPay ", "success");
                        ViewState.Clear();
                        this.Master.UpdateWalletValue();
                        ViewState["AlertState"] = "Refresh";
                    }
                    else
                    {
                        this.Master.HideAlertPopout();
                        this.Master.ShowAlertPopout("Error !", "You do not have enough balance in your wallet !", "error");
                    }

                    //

                }
                else
                {
                    this.Master.HideAlertPopout();
                    this.Master.ShowAlertPopout("Error !", "Please try again later !", "error");
                }

            }
            else
            {
                this.Master.HideAlertPopout();
                this.Master.ShowAlertPopout("Error !", "Please try again later !", "error");
            }

           


            


        }

        protected void Popout_Prompt_Submit_Click(object sender, EventArgs e)
        {
            if(ViewState["PromptPopoutState"] != null)
            {
                // valid state
                string promptPopoutState = ViewState["PromptPopoutState"].ToString();

                if(promptPopoutState == "QrPayEnterAmt")
                {
                    bool isNotANumber = false;
                    double amount = 0 ;
                    try
                    {
                        amount = Convert.ToDouble(Popout_Prompt_Textbox.Text);
                    }
                    catch (Exception ex)
                    {
                        isNotANumber = true;
                    }

 
                    if(isNotANumber || amount == 0){

                        this.Master.ShowAlertPopout("Error !", "Please enter a valid amount !", "error");
                    }
                    else
                    {

                      
                        string recipient = ViewState["PromptPopoutRecipient"].ToString();
                        this.Master.HideAlertPopout();
                        this.Master.ShowAlertPopout("Confirm", "Are you sure you want to pay SGD "+amount.ToString("$#,###,###.00") +" to "+recipient +" ?", "confirm");
                        ViewState["AlertPopoutState"] = "ConfirmPayment";
                        ViewState["AlertPopoutRecipient"] = recipient;
                        ViewState["AlertPopoutAmount"] = amount;



                    }



                }


            }
            else
            {
                this.Master.ShowAlertPopout("Error !", "Please try again later !", "error");
            }
        }


        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
            if(ViewState["AlertState"] != null)
            {
                if(ViewState["AlertState"].ToString() == "Refresh")
                    Response.Redirect(Request.RawUrl);

            }
           

           // Response.Redirect(Request.RawUrl);
        }

        protected void Popout_Alert_No_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }

        protected void Button_RequestTab_Click(object sender, EventArgs e)
        {
            Panel_Pay.Visible = false;
            Panel_Receive.Visible = false;
            Panel_RequestAmount.Visible = true;
            Button_PayTab.CssClass += " tabActive";
            Button_ScanTab.CssClass = Button_ScanTab.CssClass.Replace("tabActive", "");
        }

        protected void Button_ScanTab_Click(object sender, EventArgs e)
        {

            Panel_Pay.Visible = true;
            Panel_Receive.Visible = false;
            
            Panel_RequestAmount.Visible = false;
            Button_ScanTab.CssClass += " tabActive";
            Button_PayTab.CssClass = Button_PayTab.CssClass.Replace("tabActive","");

        }

        protected void Button_PayTab_Click(object sender, EventArgs e)
        {
            Panel_Pay.Visible = false;
            Panel_Receive.Visible = true;
            Panel_RequestAmount.Visible = false;
            Button_PayTab.CssClass += " tabActive";
            Button_ScanTab.CssClass = Button_ScanTab.CssClass.Replace("tabActive", "");

        }

        protected void Popout_Prompt_CancelBtn_Click(object sender, EventArgs e)
        {
            this.Master.HidePromptPopout();
        }

        protected void Button_Back_Click(object sender, EventArgs e)
        {
            Panel_Pay.Visible = false;
            Panel_Receive.Visible = true;
            Panel_RequestAmount.Visible = false;
            Button_PayTab.CssClass += " tabActive";
            Button_ScanTab.CssClass = Button_ScanTab.CssClass.Replace("tabActive", "");
        }
    }
}