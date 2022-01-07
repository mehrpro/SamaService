using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM
{
    public static class Public_Class
    {
        public static bool Send10000 { get; set; }
        public static bool Send5000 { get; set; }
        public static bool Send1200 { get; set; }
        public static string SMS_Username = "bijar_sms";
        public static string SMS_Password = "123456789";
        public static string SMS_LibKey = "HfqXpLA+EVp57O7G8lYRJnHo98OJPxUycJyZseY/ZSp+m+K28j5uWgvUjqSGKFcZ";
        public static bool Between(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive
                ? lower <= num && num <= upper
                : lower < num && num < upper;
        }

        public static int LastCredit { get; set; }
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
    }
}
