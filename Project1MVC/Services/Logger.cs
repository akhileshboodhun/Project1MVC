using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Debug.WriteLine(message);

            string logfilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            string text = $"[{DateTime.Now.ToString()}]: {message + Environment.NewLine}";

            System.IO.File.AppendAllText(logfilePath + @"\log.txt", text);
        }
    }
}