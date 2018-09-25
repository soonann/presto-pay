using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrestoPay.Encrypt
{
    public class Crypter
    {
        //to make a tripledes object with a decrypting/encrypting 'pattern'
        public static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            des.IV = new byte[des.BlockSize / 8];
            return des;

        }

        //to encrypt a string
        public CryptResult EncryptString(string text , string pattern) {
            //custom object to return
            CryptResult cr = new CryptResult();
            try
            {
                //converting the text to a byte array
                byte[] textBytes = Encoding.Unicode.GetBytes(text);
                //create temporary memory stream
                MemoryStream mStream = new MemoryStream();
                //
                TripleDES des = CreateDES(pattern);
                CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                //
                cStream.Write(textBytes, 0, textBytes.Length);
                //clear buffer
                cStream.FlushFinalBlock();
                cr.result = Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                cr.errMsg = ex.Message.ToString();
                cr.success = false;
            }

            return cr;

        }


        //To decrypt a string 
        public CryptResult DecryptString(string text, string pattern)
        {

            CryptResult cr = new CryptResult();
            try
            {
                byte[] textBytes = Convert.FromBase64String(text);
                MemoryStream mStream = new MemoryStream();
                TripleDES des = CreateDES(pattern);
                CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cStream.Write(textBytes,0,textBytes.Length);
                cStream.FlushFinalBlock();
                cr.result = Encoding.Unicode.GetString(mStream.ToArray());

            }
            catch (Exception ex)
            {
                cr.errMsg = ex.Message.ToString();
                cr.success = false;

            }

            return cr;
        }
        

    }
}
