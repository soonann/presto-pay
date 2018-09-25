using PrestoPay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class SummaryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RequestPanel.Visible = false;
            SendPanel.Visible = true;

        }

        protected void SendBtn_Click(object sender, EventArgs e)
        {
            RequestPanel.Visible = false;
            SendPanel.Visible = true;
        }

        protected void RequestBtn_Click(object sender, EventArgs e)
        {
            
            RequestPanel.Visible = true;
            SendPanel.Visible = false;
            Response.Redirect("RequestPage.aspx");
        }

        protected void RequestAmtBtn_Click(object sender, EventArgs e)
        {
            string amount = requestAmttb.Text;
            double amountConverted = 0;
            bool isValidAmt = true;
            String recepient = RequestDDL.SelectedValue.ToString();
            if (amount != null)
            {

                try
                {
                    amountConverted = Convert.ToDouble(amount);
                    if (amountConverted <= 0)
                        isValidAmt = false;
                }
                catch (Exception ex)
                {
                    isValidAmt = false;
                }

                if (isValidAmt)
                {
                    this.Master.ShowAlertPopout("Payment", "Are you sure you want to request " + amountConverted.ToString("$#,###,###.00") + " from " + recepient + " ?", "confirm");
                    ViewState["AlertPopoutState"] = "ConfirmPayment";
                    ViewState["AlertPopoutAmount"] = amount;
                    ViewState["AlertPopouRecipient"] = recepient;



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

        protected void SendDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void SendAmtBtn_Click(object sender, EventArgs e)
        {

          


            double preswallet = 0;
            preswallet = new AccountDAO().RetrieveWalletBalanceByEmail(Session["UserEmail"].ToString());
            double amount = 0;
            amount = Convert.ToDouble(SentAmttb.Text);


            
                var accDao = new AccountDAO();
            //sender
            String emailAmt = Session["UserEmail"].ToString();

            //recepient
            String recepient = SendDDL.SelectedValue.ToString();



            if (preswallet > amount)
            {
                this.Master.ShowAlertPopout("Payment", "Are you sure you want to pay SGD " + amount.ToString("$#,###,###.00") + " to " + recepient + " ?", "confirm");
                ViewState["AlertPopoutState"] = "ConfirmPayment";
                ViewState["AlertPopoutAmount"] = amount;
                ViewState["AlertPopoutRecipient"] = recepient;
            }

            else
            {


                this.Master.ShowAlertPopout("Error !", "You do not have required amount!", "error");
            }



        }

        

        protected void Popout_Alert_Yes_Click(object sender, EventArgs e)
        {
            if (ViewState["AlertPopoutState"] != null)
            {
                string stateOfPopout = ViewState["AlertPopoutState"].ToString();


                if (stateOfPopout == "ConfirmPayment")
                {


                    //check if sufficient amt
                    // user pay to others 
                    var acc = new AccountDAO();
                    var payee = Session["UserEmail"].ToString();
                    var recipient = ViewState["AlertPopoutRecipient"].ToString();
                    double walletbal = acc.RetrieveWalletBalanceByEmail(payee);
                    double amtToPay = Convert.ToDouble(ViewState["AlertPopoutAmount"]);
                    if (walletbal >= amtToPay)
                    {

                        var trans = new TransactionDAO();
                        var trans_no = trans.InsertTransaction(amtToPay,"","Transfer",payee,recipient);
                        acc.AddAmountToWallet(recipient, amtToPay);
                        acc.DeductAmountFromWallet(payee, amtToPay);
                        
                        this.Master.HideAlertPopout();
                        // Response.Redirect(Request.RawUrl);
                        this.Master.ShowAlertPopout("Success !", "Payment Successful !<br/>Transaction No:"+  trans_no+"<br/>Thank you for paying with PrestoPay ", "success");
                        ViewState.Clear();
                        this.Master.UpdateWalletValue();
                        Response.Redirect("FundTransfer.aspx");

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

        protected void Popout_Alert_No_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {
            
        }

        protected void Unnamed6_Click(object sender, EventArgs e)
        {
            
        }

        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }

        protected void RequestDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPayeePage.aspx");
        }
    }
    }
