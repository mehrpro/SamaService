using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SamaService
{
    public static class PublicStaticClass
    {
        public static bool Send10000 { get; set; }
        public static bool Send5000 { get; set; }
        public static bool Send1200 { get; set; }
        public static string SMS_Username = "iaubijar";
        public static string SMS_Password = "Ss987654@";
        public static string SMSLibKey_ = "HfqXpLA5Yi19meKx8lYULgyZ98P6OmUycJvjtuY6byt66OiyiTpkKQzUjqSGKFcZ";
        public static string strConnMySql = @"server=localhost;port=3306;userid=ard;password=Ss987654;database=schooldb;SSL Mode = None";

        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public static string Convert_PersianCalender(this DateTime dt)
        {
            var pc = new PersianCalendar();
            var years = pc.GetYear(dt);
            var month = pc.GetMonth(dt);
            var day = pc.GetDayOfMonth(dt);
            var hou = pc.GetHour(dt);
            var minu = pc.GetMinute(dt);
            var sec = pc.GetSecond(dt);
            return new DateTime(years, month, day, hou, minu, sec).ToString("yyyy/MM/dd hh:mm");
        }
        public static List<T> RemoveDuplicates<T>(this List<T> items)
        {
            return (from s in items select s).Distinct().ToList();
        }

        #region HexToDecimal

        public static string HexToDecimal(this string tagHex)
        {
            var hexValue = tagHex.Remove(0, 2).Remove(8);
            int decValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            var str = decValue.ToString("0000000000");
            return str;
        }

        #endregion
    }
}
