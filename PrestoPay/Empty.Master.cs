using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class Empty : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        public void ShowPromptPopout(string headerText, string message)
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