using PrestoPay.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(new User().auth()))
                Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                // Call Get ChartData() method in the PageLoad event
                GetChartData();

            }

        }




        private void GetChartData()
        {
            
            Series series = Chart1.Series["Series1"];
            var list = new TransactionDAO().RetrieveTransactionsOfUser(Session["UserEmail"].ToString());
            foreach (Transaction t in list)
            {
                if (t.trans_from.ToUpper() == Session["UserEmail"].ToString().ToUpper()  )
                     series.Points.AddXY(t.trans_date, t.trans_amt);
               
 
            }

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
    }
}