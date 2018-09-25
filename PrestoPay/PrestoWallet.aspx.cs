using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrestoPay.Entity;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace PrestoPay
{
    public partial class PrestoWallet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(new User().auth()))
                Response.Redirect("~/Login.aspx");

            
           
            
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object FillTransactionTable()
        {

            if (HttpContext.Current.Session["UserEmail"] == null)
            {
                return false;
            }
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            var list = new List<TransactionDetailTable>();
            TransactionDetailTable t1 = new TransactionDetailTable();

            var tDAO = new TransactionDAO();
            var aDAO = new AccountDAO();
            List<Transaction> tList = tDAO.RetrieveTransactionsOfUser(HttpContext.Current.Session["UserEmail"].ToString());
            List<TransactionDetailTable> tdlList = new List<TransactionDetailTable>();


            foreach (Transaction t in tList)
            {
                var tdt = new TransactionDetailTable();
                tdt.Email = t.trans_from;
                tdt.Date = t.trans_date.ToString("dd-MM-yyyy");
                if (t.trans_to == HttpContext.Current.Session["UserEmail"].ToString())
                {
                    tdt.Receipt = t.trans_amt;
                    tdt.Payment = 0;
                }

                else
                {
                    tdt.Receipt = 0;
                    tdt.Payment = t.trans_amt;
                }


                tdt.Description = t.trans_description;
                tdlList.Add(tdt);

            }


            return new { data = tdlList };

        }

        protected void SendDDL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CreditCardBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCreditCard.aspx");
        }
        protected void  TopUpAmtBtn_Click (object sender, EventArgs e)
        {
            double preswallet = 0;
            preswallet = new AccountDAO().RetrieveWalletBalanceByEmail(Session["UserEmail"].ToString());
            double amount = 0;
            amount = Convert.ToDouble(Amttb.Text);



            var accDao = new AccountDAO();
            //sender
            String emailAmt = Session["UserEmail"].ToString();

            String creditcardnum = CreditCardDDL.SelectedValue.ToString();


            //recepient
            



            
                this.Master.ShowAlertPopout("Payment", "Are you sure you want to top up SGD " + amount.ToString("$#,###,###.00") + " from " +creditcardnum +"", "confirm");
                ViewState["AlertPopoutState"] = "ConfirmPayment";
                ViewState["AlertPopoutAmount"] = amount; 
                
          

           
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
                    var user = Session["UserEmail"].ToString();
                    
                    double walletbal = acc.RetrieveWalletBalanceByEmail(user);
                    double amtToPay = Convert.ToDouble(ViewState["AlertPopoutAmount"]);
                   

                        
                        acc.AddAmountToWallet(user, amtToPay);
                       

                        this.Master.HideAlertPopout();
                        // Response.Redirect(Request.RawUrl);
                        this.Master.ShowAlertPopout("Success !", "Top Up Successful ! Thank you for using PrestoPay ", "success");
                        ViewState.Clear();
                        this.Master.UpdateWalletValue();

                   

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

        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
            Response.Redirect("PrestoWallet.aspx");
        }
    }
    

        

        
    }
