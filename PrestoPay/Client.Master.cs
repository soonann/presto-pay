using PrestoPay.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class Client : System.Web.UI.MasterPage
    {
    


        protected void Page_Load(object sender, EventArgs e)
        {




            if (new User().auth())
            {
                if (Session["UserType"].ToString() == "Enterprise")
                {

                    Nav_presqr.Visible = true;
                    Nav_budget.Visible = false;
                    Nav_budget1.Visible = false;
                    Nav_budget2.Visible = false;
                    Nav_budget3.Visible = false;
                    Nav_CategoriseTransaction.Visible = false;
                    Nav_CategoriseTransaction1.Visible = false;
                    Nav_CategoriseTransaction2.Visible = false;
                    //Nav_billing.Visible = false;

                    // Start of modification by OSL
                    Nav_loans.Visible = true;

                    Nav_loans1.Visible = true;
                    Nav_loans2.Visible = true;
                    Nav_loans3.Visible = true;

                    Nav_personal.Visible = false;
                    Nav_business.Visible = true;
                    // End of modification by OSL
                }
                else if (Session["UserType"].ToString() == "Personal")
                {
                    // Start of modification by OSL
                    Nav_presqr.Visible = true;
                    Nav_budget.Visible = true;
                    Nav_budget1.Visible = true;
                    Nav_budget2.Visible = true;
                    Nav_budget3.Visible = true;
                    Nav_CategoriseTransaction.Visible = true;
                    Nav_CategoriseTransaction1.Visible = true;
                    Nav_CategoriseTransaction2.Visible = true;
                    //Nav_billing.Visible = true;
                    Nav_personal.Visible = true;
                    Nav_business.Visible = false;
                    Nav_loans.Visible = false;

                    Nav_loans1.Visible = false;
                    Nav_loans2.Visible = false;
                    Nav_loans3.Visible = false;
                    // End of modification by OSL
                }


                Nav_userName.InnerText = Session["UserName"].ToString();


                UpdateWalletValue();
                if (IsPostBack)
                {

                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

            Nav_signout.ServerClick += Nav_signout_ServerClick;


        }


        public void UpdateWalletValue()
        {
            if (new User().auth())
                Nav_Money.InnerText = new AccountDAO().RetrieveWalletBalanceByEmail(Session["UserEmail"].ToString()).ToString("$#,###,##0.00");
        }



        private void Nav_signout_ServerClick(object sender, EventArgs e)
        {


            Session["UserEmail"] = null;
            Session["UserType"] = null;
            Session["UserName"] = null;
            Server.Transfer("~/Login.aspx");


        }

        protected void CloseMsg(object sender, EventArgs e)
        {

           
            HideAlertPopout();


        }

        protected void ClosePrompt(object sender, EventArgs e)
        {

            
            HidePromptPopout();
        }
        
        public void ShowAlertPopout(string headerText, string message, string type)
        {


            Popout_Alert_Panel.Visible = true;
            Popout_Alert_Title.Text = headerText;
            Popout_Alert_Message.Text = message;

            switch (type)
            {
                case "error":
                    Popout_Alert_OK.Visible = true;
                    Popout_Alert_YN.Visible = false;
                    Popout_Alert_Image.ImageUrl = "~/Images/error.png";
                    break;
                case "success":
                    Popout_Alert_OK.Visible = true;
                    Popout_Alert_YN.Visible = false;
                    Popout_Alert_Image.ImageUrl = "~/Images/success.png";
                    break;
                case "confirm":
                    Popout_Alert_YN.Visible = true;
                    Popout_Alert_OK.Visible = false;
                    Popout_Alert_Image.ImageUrl = "~/Images/qn.png";
                    break;
                default:
                    break;


            }
       
            

        }

        public void HideAlertPopout()
        {
            Popout_Alert_Panel.Visible = false;
            Popout_Alert_Title.Text = "";
            Popout_Alert_Message.Text = "";
            Popout_Alert_YN.Visible = false;
            Popout_Alert_OK.Visible = false;

        }

        public void ShowPromptPopout(string headerText,string message)
        {

  
            Popout_Prompt_TextboxDiv.Visible = true;           
            Popout_Prompt_Panel.Visible = true;
            Popout_Prompt_Title.Text = headerText;
            Popout_Prompt_Message.Text = message;
        

            // wait for value from event handler
        }

        public void HidePromptPopout()
        {
            Popout_Prompt_Panel.Visible = false;
            Popout_Prompt_Title.Text = "";
            Popout_Prompt_Message.Text = "";
            Popout_Prompt_TextboxDiv.Visible = false;

        }

  
    }
}