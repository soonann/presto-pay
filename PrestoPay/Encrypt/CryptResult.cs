using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestoPay.Encrypt
{
    public class CryptResult
    {
        public bool success { get; set; }
        public string result { get; set; }
        public string errMsg { get; set; }

        public CryptResult()
        {
            this.success = true;
            this.result = "";
            this.errMsg = "";
        }
    }
}
