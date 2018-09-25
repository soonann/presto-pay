using PrestoPay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace PrestoPay
{
    public partial class RequestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RequestPanel.Visible = true;
            SendPanel.Visible = false;



        }
        protected void SendBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("FundTransfer.aspx");
            RequestPanel.Visible = false;
            SendPanel.Visible = true;
        }

        protected void RequestBtn_Click(object sender, EventArgs e)
        {
            RequestPanel.Visible = true;
            SendPanel.Visible = false;
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
            String useremail = Session["UserEmail"].ToString();
            String subject = "Request for amount";
            String body1 = "" + Session["UserEmail"].ToString() + " have requested money from you";
            String recepient = SendDDL.SelectedValue.ToString();

            if (ViewState["AlertPopoutState"] != null)
            {
                string stateOfPopout = ViewState["AlertPopoutState"].ToString();


                if (stateOfPopout == "ConfirmPayment")
                {


                    try
                    {
                        
                       

                        SmtpClient client = new SmtpClient( "smtp.gmail.com ", 587);
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("alextbf97@gmail.com", "freeitem1");
                        MailMessage msg = new MailMessage();
                        msg.To.Add("tongbf97@gmail.com");
                        msg.From = new MailAddress("alextbf97@gmail.com");
                        msg.Subject = subject;
                        msg.Body = body1;
                        client.Send(msg);


                           
                      
                        this.Master.ShowAlertPopout("Success !", "Email was Sent", "success");
                        ViewState.Clear();
                    }
                    catch (Exception ex)
                    {

                        this.Master.ShowAlertPopout("Error !", "Email was not Sent", "error");
                    }





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

