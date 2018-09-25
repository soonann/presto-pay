using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrestoPay.Entity
{
    public class JsonResult
    {
        public bool success { get; set; }
        public object value { get; set; }

        public JsonResult()
        {
            this.success = true;
            
        }

        public JsonResult(bool success, object value)
        {
            this.success = success;
            this.value = value;
        }
    }
}