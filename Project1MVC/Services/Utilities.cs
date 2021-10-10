using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class Utilities
    {
        public DateTime getDate(string s, string format = "dd/MM/yyyy") => DateTime.ParseExact(s, format, System.Globalization.CultureInfo.InvariantCulture);
        public string getFormDate(DateTime d) => d.ToString("yyyy-MM-dd");

    }
}