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

namespace PrestoPay
{
    public partial class Transactions : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(new User().auth()))
                Response.Redirect("~/Login.aspx");

            if (IsPostBack)
            {
                Session["Filter"] = DropDownList_Filter.SelectedValue;
            }
            else
            {
                Session["Filter"] = "All";
            }
        



        }
        




    
    
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object FillTransactionTable()
        {
            
            if (HttpContext.Current.Session["UserEmail"] == null) {
                return false;
            }
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            var tDAO = new TransactionDAO();
            var aDAO = new AccountDAO();

            List<Transaction> tList = tDAO.RetrieveTransactionsOfUser(HttpContext.Current.Session["UserEmail"].ToString());
            List<TransactionTable> tdlList = new List<TransactionTable>();
            String filter = "";
            if (HttpContext.Current.Session["Filter"] != null)
                filter = HttpContext.Current.Session["Filter"].ToString();
            else
                filter = "All";

            switch (filter)
            {
                case "All":

                    break;
                case "Qr Pay":
                    tList = tList.Where(x => x.trans_type == "QR Pay").ToList();
                    break;
                case "Online Payment":
                    tList = tList.Where(x => x.trans_type == "Online Payment").ToList();
                    break;
                case "Transfer":
                    tList = tList.Where(x => x.trans_type == "Transfer").ToList();
                    break;
                case "default":

                    break;

            }

            foreach (Transaction t in tList)
            {
                
                var tdt = new TransactionTable();
                tdt.TransactionID = t.trans_id;
                tdt.Date = jsSerial.Serialize(t.trans_date);
                
                if(t.trans_from.ToUpper() == HttpContext.Current.Session["UserEmail"].ToString().ToUpper())
                    tdt.Email = t.trans_to;
                if (t.trans_to.ToUpper() == HttpContext.Current.Session["UserEmail"].ToString().ToUpper())
                {
                    tdt.Email = t.trans_from;
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

            tdlList = tdlList.OrderByDescending(x=>x.TransactionID ).ToList();
            return   new {data = tdlList } ;

        }



        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object FillTransactionMin()
        {

            if (HttpContext.Current.Session["UserEmail"] == null)
            {
                return false;
            }
            JavaScriptSerializer jsSerial = new JavaScriptSerializer();

            var tDAO = new TransactionDAO();
            var aDAO = new AccountDAO();

            List<Transaction> tList = tDAO.RetrieveTransactionsOfUser(HttpContext.Current.Session["UserEmail"].ToString());
            List<TransactionTable> tdlList = new List<TransactionTable>();
            List<MiniTable> tmtList = new List<MiniTable>();
            String filter = "";
            if (HttpContext.Current.Session["Filter"] != null)
                filter = HttpContext.Current.Session["Filter"].ToString();
            else
                filter = "All";

            switch (filter)
            {
                case "All":

                    break;
                case "Qr Pay":
                    tList = tList.Where(x => x.trans_type == "QR Pay").ToList();
                    break;
                case "Online Payment":
                    tList = tList.Where(x => x.trans_type == "Online Payment").ToList();
                    break;
                case "Transfer":
                    tList = tList.Where(x => x.trans_type == "Transfer").ToList();
                    break;
                case "default":

                    break;

            }

            foreach (Transaction t in tList)
            {

                var tdt = new TransactionTable();
                var tmt = new MiniTable();
                tmt.TransactionId = t.trans_id;
                tmt.Date = jsSerial.Serialize(t.trans_date);
                if (t.trans_to.ToUpper() == HttpContext.Current.Session["UserEmail"].ToString().ToUpper())
                {
                    tmt.amt = "+ " + t.trans_amt.ToString("$#,###,##0.00");

                    tdt.Receipt = t.trans_amt;
                    tdt.Payment = 0;
                }

                else
                {
                    tmt.amt = "- " + t.trans_amt.ToString("$#,###,##0.00");
                    tdt.Receipt = 0;
                    tdt.Payment = t.trans_amt;
                }
                tmtList.Add(tmt);
                


            }

            tdlList = tdlList.OrderByDescending(x => x.TransactionID).ToList();
            return new { data = tmtList };

        }
    }
   
}