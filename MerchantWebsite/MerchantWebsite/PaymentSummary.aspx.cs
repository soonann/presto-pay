using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using MerchantWebsite.Db;

namespace MerchantWebsite
{
 

    public partial class PaymentSummary : System.Web.UI.Page
    {

        List<Item> globIList;
        MerchantOrders globOrder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] == null)
            {
                Response.Redirect("~/PageNotFound.aspx");
            }
            else
            {

                string refno = Request.QueryString["id"].ToString();
                MerchantOrders mo = new MerchantOrders();
                var iList = mo.RetrieveMerchantOrderByReferenceNo(refno);
                globOrder = mo;
                globIList = iList;
                populatePaymentTable(globIList);
                if(mo.Payment_Status == 1)
                {
                    Button_Pay.Visible = false;

                }
                else
                {

                }

            }
        }
        private void populatePaymentTable(List<Item> psList)
        {
            if (psList == null)
                Response.Redirect("~/PageNotFound.aspx");
            
            string statusSpan = "";
            if (globOrder.Payment_Status == 0)
                statusSpan = "<span class='yellow'>Pending Payment</span>";
            else
                statusSpan = "<span class='green'>Payment Confirmed</span>";
            string TransactionNum = globOrder.Order_id;


            var trans = new TableRow() { CssClass = "subTotalRow" };

            trans.Cells.Add(new TableCell() { Text = "Transaction No: ", CssClass = "boldOnly leftAlignText" });
            trans.Cells.Add(new TableCell() { Text = TransactionNum ,CssClass="rightAlignText"} );
            Table_Items.Rows.Add(trans);

            var status = new TableRow() { CssClass = "subTotalRow" };
            status.Cells.Add(new TableCell() { Text = "Payment status: ", CssClass = "boldOnly leftAlignText" });
            status.Cells.Add(new TableCell() { Text = statusSpan, CssClass = " rightAlignText" });
            Table_Items.Rows.Add(status);

            var space= new TableRow();
            space.Cells.Add(new TableCell() { Text = "" });
            space.Cells.Add(new TableCell() { Text = "" });
            Table_Items.Rows.Add(space);

            double subtotalPrice = 0;


            var header = new TableRow() { CssClass = "subTotalRow" };
            header.Cells.Add(new TableCell() { Text = "Description", CssClass = "boldOnly leftAlignText" });
            header.Cells.Add(new TableCell() { Text = "Amount", CssClass = "boldOnly rightAlignText" });

            Table_Items.Rows.Add(header);



            for (int i = 0;i < psList.Count; i++)
            {

                var tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = psList[i].ItemDescription, CssClass = "leftAlignText" });
                tr.Cells.Add(new TableCell() { Text = psList[i].ItemPrice.ToString("SGD $,###,##0.00"), CssClass = "rightAlignText" });
                subtotalPrice += psList[i].ItemPrice;
                Table_Items.Rows.Add(tr);

            }

            var subTotalRow = new TableRow();
            var subTotalCell = new TableCell();
            subTotalRow.Attributes["class"] = "subTotalRow";
            subTotalRow.Cells.Add(new TableCell() { Text = "Total Amount:", CssClass = "boldOnly leftAlignText" });
            subTotalRow.Cells.Add(new TableCell() { Text = subtotalPrice.ToString("SGD $,###,##0.00"), CssClass = "boldOnly rightAlignText" });
            Table_Items.Rows.Add(subTotalRow);



        }
        protected void Button_Click(object sender, EventArgs e)
        {
            //http://PrestoMerchant.azurewebsites.net/PaymentSummary.aspx?id=
            var myurl = "http://localhost:7000/PaymentSummary.aspx?id=";

            var data = new PaymentInformation();
            var client = new HttpClient();
            data.ApiKey = new Business().GetPaymentKey();          
            data.MerchantReferenceNo = globOrder.Reference_No;
            data.OnCompleteUrl = myurl+globOrder.Reference_No;
            data.ItemList = globIList;

            


            var converted = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(converted);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("http://localhost:3000/api/Payment/", byteContent ).Result;
            //
           // var response = client.GetAsync("http://localhost:3000/api/Payment?apiKey="+data.ApiKey+"&referenceNo="+data.MerchantReferenceNo).Result;
            if (response.IsSuccessStatusCode)
            {

                string newUrl = JsonConvert.DeserializeObject<String>(response.Content.ReadAsStringAsync().Result).ToString();
                Response.Redirect(newUrl);

            }
            else
            {
                Response.Write(JsonConvert.DeserializeObject<String>(response.Content.ReadAsStringAsync().Result).ToString());
            }

            
            

        }
    }
}