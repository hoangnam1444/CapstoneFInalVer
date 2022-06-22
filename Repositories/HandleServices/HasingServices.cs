using Contracts.HandleServices;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Repositories.HandleServices
{
    public class HasingServices : IHasingServices
    {
        public string EncriptSHA256(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
