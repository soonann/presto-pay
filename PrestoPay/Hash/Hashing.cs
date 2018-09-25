using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PrestoPay.Hash
{
    public class Hashing
    {
        

        public static string GetHash(string toBeHashed)
        {

            using (MD5 md5Hash = MD5.Create())
            {

                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }



        }

        // Verify a hash against a string.
        public static bool IsValueOfHash(string input, string hash)
        {         
                string hashOfInput = GetHash(input);
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                    return true;
                else
                    return false;
            

        }

    }


}

