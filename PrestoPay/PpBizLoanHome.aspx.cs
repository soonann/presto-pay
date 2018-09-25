using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class PpBizLoanHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnApplyNow_Click(object sender, EventArgs e)
        {
            Response.Redirect("PpBizLoanApplicationPage.aspx");
        }
    }
}