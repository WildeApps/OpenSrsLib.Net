using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Helpers
{
    public static class CryptographyHelper
    {
        public static string CalculateMD5Hash(string value)
        {
            var md5 = MD5.Create();

            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(value.Trim()));

            var sb = new StringBuilder();

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
