using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantWebsite.Db
{
    public class PaymentInformation
    {
        public String ApiKey { get; set; }
        public String MerchantReferenceNo { get; set; }
        public String OnCompleteUrl { get; set; }
        public List<Item> ItemList { get; set; }

    }
}