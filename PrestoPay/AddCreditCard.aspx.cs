using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class AddCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PrestoConn"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into creditcard(creditcardnum, fname, lname, dob, cvc,dateofexpiry, acc_email,creditcardtype) values ('" + TextBox_ccnum.Text + "','" + TextBox_fname.Text + "', '" + TextBox_lname.Text + "','" + TextBox_dob.Text + "','" + TextBox_cvc.Text + "','" + TextBox_doe.Text + "','" +Session["UserEmail"].ToString()+ "','"+TextBox1.Text+"')", con);
            
            cmd.ExecuteNonQuery();

          
            this.Master.ShowAlertPopout("Successs", "You have successfuly link your credit cards!", "success");
            ViewState["AlertPopoutState"] = "ConfirmPayment";
        }


        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrestoWallet.aspx");
        }
    }
}