using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class Bank
    {
        public int id { get; set; }
        public string acc_email { get; set; }
        public double credit_remaining { get; set; }
     


    }
}