using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.View_Entities
{
    public class Payment
    {
        public Payment()
        {
            OrderDetails = new Order();
            ItemList = new List<Item>();
        }

        public Order OrderDetails { get; set; }
        public List<Item> ItemList { get; set; } 



        public bool RetrieveOrderAndItemsByPrestoKey(string PrestoKey)
        {
            bool valid = true;

            try
            {

                this.OrderDetails = new OrderDAO().RetrieveOrderByPrestoKey(PrestoKey);
                this.ItemList = new ItemDAO().RetrieveItemsByOrderID(OrderDetails.Order_id);
                if (OrderDetails.Order_Paid == 1)
                    valid = false;

            }
            catch (Exception ex)
            {
                valid = false;
            }


            return valid;

        }

        public string InsertOrderAndItems()
        {
            int count = 0;

            var itemDAO = new ItemDAO();
            string newOrderId = new OrderDAO().CreateNewOrder(OrderDetails);

            if (newOrderId == null)
                return String.Empty;

            foreach (Item i in ItemList)
            {
                count += itemDAO.InsertItem(i.ItemDescription, i.ItemPrice, newOrderId);
            }

          
                return newOrderId;
          



        }
    }
}