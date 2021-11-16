using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public static class ExtensionMethods
    {
        public static int ToInt(this string text)
        {
            return int.Parse(text);
        }

        public static int ToInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }
        public static DateTime ToDateTime(this object obj)
        {
            return Convert.ToDateTime(obj);
        }
        public static Boolean ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }
    }
}