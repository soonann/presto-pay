using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class Transfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(new User().auth()))
                Response.Redirect("~/Login.aspx");


        }

        protected void Button_pay_Click(object sender, EventArgs e)
        {
            
            AccountDAO aDAO = new AccountDAO();
            TransactionDAO tDAO = new TransactionDAO();

            //string tb_purpose = TextBox_purpose.Text;
            string tb_recepient = TextBox_paymentTo.Text;
            double tb_amount = Convert.ToDouble(TextBox_amount.Text);          
            string description = TextBox_description.Text;
            
            

            if(aDAO.RetrieveWalletBalanceByEmail(Session["UserEmail"].ToString()) >= tb_amount)
            {
                bool resultDeduct = aDAO.DeductAmountFromWallet(Session["UserEmail"].ToString(), tb_amount);
                if (resultDeduct)
                {
                    String reference = tDAO.InsertTransaction(tb_amount, description, "Transfer", Session["UserEmail"].ToString(), tb_recepient);
                    LoanRepaymentDAO loanRepaymentDAO = new LoanRepaymentDAO();
                    double dblRemainingAmount = loanRepaymentDAO.PerformLoanRepaymentByAccountEmail(tb_recepient, tb_amount, reference);
                    bool resultAdd = aDAO.AddAmountToWallet(tb_recepient, dblRemainingAmount);


                    Response.Write("<script>alert('" + reference + " ')</script>");
                    TextBox_paymentTo.Text = String.Empty;
                    TextBox_description.Text = String.Empty;
                    TextBox_amount.Text = String.Empty;
                }
                else
                {
                    // alert error has occured
                    Response.Write("<script>alert('An error has occured. Please try again later !')</script>");
                }
                   

            }
            else
            {
                // alert not enough balance
                Response.Write("<script>alert('You do not have sufficient balance to make this transaction !')</script>");
            }

            



        }
    }
}