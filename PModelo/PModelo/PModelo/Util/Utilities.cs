using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PModelo.Util
{
    public class Utilities
    {
        public static DateTime GetFecha()
        {
            return DateTime.UtcNow.AddHours(-5);
        }

        public static TimeZoneInfo GetTimeZonePeru()
        {
            string displayName = "(GMT-05:00) Peru Time";
            string standardName = "Peru Time";
            TimeSpan offset = new TimeSpan(-5, 0, 0);
            return TimeZoneInfo.CreateCustomTimeZone(standardName, offset, displayName, standardName);
        }

        //public static long ObtenerMillisegundos(DateTime fecha)
        //{
        //    return (long)(fecha - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        //}
        public static long? ObtenerMillisegundos(DateTime? fecha)
        {
            if (fecha != null)
                return (long)(Convert.ToDateTime(fecha) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            else
                return null;
        }

        public static int GetClaveRandom()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return r.Next(100000, 999999);
        }

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
