using PrestoPay.Entity;
using PrestoPay.Entity.DB_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay
{
    public partial class PopulateTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var ad = new AccountDAO();
            var adall = ad.retrieveAllAccounts();
            var persad = adall.Where(x => String.IsNullOrEmpty(x.busi_id)).ToList();
            var bizad = adall.Where(x => !String.IsNullOrEmpty(x.busi_id)).ToList();
            bizad.Remove(bizad.Find(x => x.email.ToLower() == "shopfresh@gmail.com".ToLower()));
            persad.Remove(persad.Find(x => x.email.ToLower() == "john@gmail.com".ToLower()));
            string[] cat = { "Flex Spending", "Debt Repayment", "Fixed Cost", "Priority Goals" };
            string[] subcat = { "Food", "Mortgage", "Insurance", "Retirement Savings" };

            string[] mthds = { "QR Pay", "Transfer", "Online Payment" };

            int noofdays = Convert.ToInt32(TextBox1.Text);
            Random rnd = new Random();
            DateTime dt = Calendar1.SelectedDate;
            var td = new TransactionDAO();
            var cattra = new CategorisedTransactionDAO();
            
            for(int i = 0; i < 5; i++)
            {

                int type = rnd.Next(0, 2);
                double amt = Convert.ToDouble(rnd.Next(30, 100));
                var txid = td.InsertDatedTransaction(
                    amt,
                    mthds[type],
                    mthds[type],
                    persad[rnd.Next(0, persad.Count - 1)].email,
                    bizad[rnd.Next(0, bizad.Count - 1)].email,
                    dt.AddDays(rnd.Next(1, noofdays))
                    );
                int cata = rnd.Next(0, 3);
                cattra.WriteCategorisedTransactionByTransID(txid,cat[cata],subcat[cata]);


            }
            







        }
    }
}