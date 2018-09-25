using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class Settings : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            //Session["UserEmail"].ToString();
            populateRequests();
            populateEmployees();
        }

        protected void Button_SendRequest_Click(object sender, EventArgs e)
        {
            String requestEmail = TextBox_Request.Text;
            AccountDAO ac = new AccountDAO();
            if (String.IsNullOrEmpty(requestEmail))
            {
                this.Master.ShowAlertPopout("Error !","Please enter a valid email !","error");

            }
            else
            {

                var acc = ac.RetrieveAccountByEmail(requestEmail);

                if(acc != null)
                {

                    String send = Session["UserEmail"].ToString();                   
                    RequestDAO rq = new RequestDAO();
                    // check if the user is already requested.
                    // check if user is a business
                    // check if user is himself
                    if(rq.MakeNewRequest(send, acc.email))
                    {
                        this.Master.ShowAlertPopout("Success !", "Request Sent !", "error");
                        TextBox_Request.Text = "";
                    }
                    else
                        this.Master.ShowAlertPopout("Error !", "Please enter a valid email !", "error");

                }
                else
                {
                    this.Master.ShowAlertPopout("Error !", "Please enter a valid email !", "error");
                }

                // refresh gv
                populateRequests();
                populateEmployees();
            }
           
        
                
        }


        private void populateRequests()
        {

            RequestDAO rd = new RequestDAO();
            String send = Session["UserEmail"].ToString();
           // String bizid = new BusinessDAO().GetBusinessIdByEmail(send).busi_id;
            var data = rd.RetrieveAllRequestOfUser(send,true);
            GridView_Request.DataSource = data ;
            GridView_Request.DataBind();

        }

        private void populateEmployees()
        {

            EmployeesDAO ed = new EmployeesDAO();
           // ed.RetrieveAllEmployeesOfUser(,true);

            //GridView_Employee.DataSource =;

        }



        protected void Popout_Alert_OkButton_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }

        protected void GridView_Request_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "DeleteRequest")
            {
                ViewState["GVState"] = "DeleteRequest";
                ViewState["Id"] = e.CommandArgument;
                this.Master.ShowAlertPopout("Delete","Are you sure you want to delete the request ?","confirm");
            }


        }


        protected void GridView_Request_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[2];
                if (statusCell.Text == "0")
                {
                    statusCell.Text = "Pending";
                }
                if (statusCell.Text == "1")
                {
                    statusCell.Text = "Accepted";
                }
                if(statusCell.Text == "2")
                {
                    statusCell.Text = "Declined";
                }

            }



        }

        protected void Popout_Alert_Yes_Click(object sender, EventArgs e)
        {

            if(ViewState["GVState"] != null)
            {
                if(ViewState["GVState"].ToString() == "DeleteRequest")
                {
                    int id = Convert.ToInt32(ViewState["Id"]);
                    new RequestDAO().UpdateRequestStateById(id, 2);
                    populateRequests();
                    this.Master.ShowAlertPopout("Success !", "Successfully deleted request !", "success");
                }
                else if(ViewState["GVState"].ToString() == "AcceptRequest")
                {

                }

            }
        

        }

        protected void Popout_Alert_No_Click(object sender, EventArgs e)
        {
            this.Master.HideAlertPopout();
        }
    }
}