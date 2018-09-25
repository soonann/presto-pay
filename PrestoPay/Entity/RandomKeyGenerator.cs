using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PrestoPay.Entity
{
    public class RandomKeyGenerator
    {
        public string key { get; set; }

        public RandomKeyGenerator()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[24];
            rng.GetBytes(randomBytes);
            this.key = Convert.ToBase64String(randomBytes);
        }

    }
}