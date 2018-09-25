using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrestoPay.Entity
{
    public class User
    {
         public bool auth()
         {
            if(HttpContext.Current.Session["UserName"] != null &&
                 HttpContext.Current.Session["UserType"] != null &&
                     HttpContext.Current.Session["UserEmail"] != null)
            {
                return true;
            }
              
            return false;
         }
        
        

               
           
        
    }
}