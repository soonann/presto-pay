using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity.DB_Entities
{
    public class QrPayKey
    {
    

        public int Qr_Id { get; set; }
        public string Qr_Key { get; set; }
        public DateTime Qr_DateUpdate { get; set; }
       
        public string acc_email { get; set; }

  

        public QrPayKey(int qr_Id, string qr_Key, DateTime qr_DateUpdate, string acc_email)
        {
            Qr_Id = qr_Id;
            Qr_Key = qr_Key;
            Qr_DateUpdate = qr_DateUpdate;
            this.acc_email = acc_email;
          
        }
    }
}