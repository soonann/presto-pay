using PrestoPay.Entity;
using PrestoPay.Entity.Api_Entites;
using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class Login : System.Web.UI.Page
    {
        static string temp = "DashBoard.aspx";
        //Transactions.aspx
        public static string Homepage = "~/"+temp;

        protected void Page_Load(object sender, EventArgs e)
        {
            
           
            if (new User().auth())
                Response.Redirect(Homepage);
            if (Session["Admin"] != null && Session["AdminName"] != null)
                Response.Redirect("~/PrestoPayAnalysis.aspx");

            /*

            Session["UserEmail"] = "walker@gmail.com";
            Session["UserName"] = "Walker lee";
            AccountDAO da = new AccountDAO();
            Account ac = da.RetrieveAccountByEmail("walker@gmail.com");
            if (ac.busi_id == null)
            {
                Session["UserType"] = "Personal";
            }
            else
            {
                var dao = new BusinessApiKeyDAO();
                if (String.IsNullOrEmpty(dao.RetrieveLatestKeyOfBusiness(ac.busi_id)))
                    dao.GenerateNewApiKeyForBusiness(ac.busi_id);
                Session["UserType"] = "Enterprise";
            }
            Response.Redirect(Homepage);

            //Response.Redirect(Homepage);
            */
            

        }

        protected void Button_login_Click(object sender, EventArgs e)
        {
        
              /*
            Session["UserEmail"] = "john@gmail.com";
            Session["UserName"] = "John Tan";
      
            Session["UserType"] = "Personal";
            Response.Redirect("~/Transactions.aspx");

            
            Session["UserEmail"] = "Walker@gmail.com";
            Session["UserName"] = "Walker Lee";
          
      
          */

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
                Admin ad = new AdminDAO().RetrieveAdminById(tb_email);
                if (ad == null)
                {

                    if (ac != null)
                    {

                        if (ac.password == tb_password)
                        {



                            Session["UserEmail"] = tb_email.First().ToString().ToUpper() + tb_email.Substring(1);


                            if (ac.busi_id == null)
                            {
                                Session["UserType"] = "Personal";
                                Session["UserName"] = ac.name;

                            }
                            else
                            {
                                var dao = new BusinessApiKeyDAO();
                                if (String.IsNullOrEmpty(dao.RetrieveLatestKeyOfBusiness(ac.busi_id)))
                                    dao.GenerateNewApiKeyForBusiness(ac.busi_id);
                                Session["UserType"] = "Enterprise";
                                string companyname = new BusinessDAO().getBusinessById(ac.busi_id).busi_companyName;
                                Session["UserName"] = companyname;
                            }


                            Response.Redirect(Homepage);
                            // login success
                            // redirect
                        }
                        else
                        {
                            this.Master.ShowAlertPopout("Error !", "Invalid Login !", "error");
                            // invalid login
                        }

                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "Invalid Login !", "error");
                        // show error for invalid login
                    }
                }
                else { 

                    if(ad.AdminPassword == tb_password)
                    {

                        Session["Admin"] = ad.AdminLogin;
                        Session["AdminName"] = ad.AdminName;
                        Response.Redirect("~/PrestoPayAnalysis.aspx");

                    }
                    else
                    {
                        this.Master.ShowAlertPopout("Error !", "Invalid Login !", "error");
                    }

                }
               
            }
        }

        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }
    }
}