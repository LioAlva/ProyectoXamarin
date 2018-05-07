using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PModelo.Util
{
    public class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }
        public static bool IsValidPassword(string password)
        {
            return Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}").Success;
        }
        public static DateTime dateMilliToDate(string date)
        {
            //  return (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(date.ToString())).ToString("dd/MM/yyyy");
            return (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(date.ToString()));

        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
