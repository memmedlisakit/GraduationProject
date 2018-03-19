using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ISM_Final_Lahiye.Areas.Adminpanel.Public
{
    public class Settings
    {
        public static string Hash_Password(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] result = md5.Hash;
            string hashed_password = "";
            foreach (byte item in result)
            {
                hashed_password += item;
            }
            return hashed_password;
        }
    }
}