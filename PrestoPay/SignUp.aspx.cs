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
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblcompanyname.Visible = false;
            companynametb.Visible = false;

            TextBox1.Visible = false;
            Label1.Visible = false;

            TextBox2.Visible = false;
            Label2.Visible = false;

            TextBox3.Visible = false;
            Label3.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PrestoConn"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Account(acc_email, acc_password, acc_name, acc_dob, acc_walletBal) values ('" + TextBox_email.Text + "','" + TextBox_Cfmpassword.Text + "', '" + TextBox_name.Text + "','" + TextBox_dob.Text + "',0.00)", con);

            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand("insert into Business(busi_companyName, busi_type, busi_category, busi_countryofReg) values ('" + companynametb.Text + "','" + TextBox2.Text + "','" + TextBox1.Text + "','" + TextBox3.Text + "'", con);



            this.Master.ShowAlertPopout("Success", "Registration Successful !", "success");
            ViewState["AlertPopoutState"] = "ConfirmPayment";


        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedValue == "Business")
            {
                lblcompanyname.Visible = true;
                companynametb.Visible = true;

                TextBox1.Visible = true;
                Label1.Visible = true;

                TextBox2.Visible = true;
                Label2.Visible = true;

                TextBox3.Visible = true;
                Label3.Visible = true;
            }
        }

        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
            Response.Redirect("Login.aspx");
            
        }
    }
}