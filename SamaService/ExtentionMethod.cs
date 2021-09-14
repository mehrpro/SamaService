﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SamaService
{
    public static class ExtentionMethod
    {
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