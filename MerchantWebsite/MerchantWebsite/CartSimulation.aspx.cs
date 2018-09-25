using MerchantWebsite.Db;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MerchantWebsite
{
    public partial class CartSimulation : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!IsPostBack)
            {

                populatePaymentTable(new List<Item>());
            }
            else
            {
                if (ViewState["ItemList"] != null)
                {
                    var newList = JsonConvert.DeserializeObject<List<Item>>(ViewState["ItemList"].ToString());
                    populatePaymentTable(newList);

                }
                else
                {
                    populatePaymentTable(new List<Item>());
                }
            }

    

        }
        
        private void populatePaymentTable(List<Item> psList)
        {

            
            //currentPayment.OrderDetails. = paymentTo;
            

            double subtotalPrice = 0;

  
             var header = new TableRow() {CssClass="subTotalRow" };
            header.Cells.Add(new TableCell() { Text = "Description", CssClass = "boldOnly leftAlignText" });
            header.Cells.Add(new TableCell() { Text = "Amount", CssClass = "boldOnly rightAlignText" });
          
            Table_Items.Rows.Add(header);



            for (int i = 0; i < psList.Count; i++)
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

        protected void Button_AddNew_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(TextBox_DescriptionNew.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter a description')", true);
            }
            else if (String.IsNullOrEmpty(TextBox_PriceNew.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter a price')", true);
            }
            else
            {
                bool isvalidinput =true;
                double amt=0;

                try
                {
                    amt = Convert.ToDouble(TextBox_PriceNew.Text);
                }
                catch (Exception ex) {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter a valid price')", true);
                    isvalidinput = false;
                }

                if (isvalidinput)
                {
                    if (ViewState["ItemList"] != null)
                    {

                        var newList = JsonConvert.DeserializeObject<List<Item>>(ViewState["ItemList"].ToString());
                        newList.Add(new Item() { ItemDescription = TextBox_DescriptionNew.Text, ItemPrice = amt });
                        ViewState["ItemList"] = JsonConvert.SerializeObject(newList);
                        Table_Items.Rows.Clear();
                        populatePaymentTable(newList);
                        TextBox_DescriptionNew.Text = "";
                        TextBox_PriceNew.Text = "";
                    }
                    else
                    {
                        var newList = new List<Item>();
                        newList.Add(new Item() { ItemDescription = TextBox_DescriptionNew.Text, ItemPrice = amt });
                        ViewState["ItemList"] = JsonConvert.SerializeObject(newList);
                        Table_Items.Rows.Clear();
                      populatePaymentTable(newList);
                        TextBox_DescriptionNew.Text = "";
                        TextBox_PriceNew.Text = "";
                    }
                    
                   
                }
             
               
                    

            }
            

        }

        protected void Button_Checkout_Click(object sender, EventArgs e)
        {
            if (ViewState["ItemList"] != null)
            {
                var newList = JsonConvert.DeserializeObject<List<Item>>(ViewState["ItemList"].ToString());
                MerchantOrders mo = new MerchantOrders();
                if(newList[0].ItemDescription == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No items in the cart !')", true);
                }
                else
                {
                    string refno = mo.createNewOrder(newList);
                    Response.Redirect("~/PaymentSummary.aspx?id=" + refno);
                }
                

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No items in the cart !')", true);
            }
        }
    }


}