using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace Project1MVC.Services
{
    public static class Logger
    {
        public static void Log(string message)
        {
            //    Debug.WriteLine(message);

            //    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            //    string filename = path + @"\log.txt";

            //    string text = $"[{DateTime.Now.ToString()}]: {message + Environment.NewLine}";

            //    File.AppendAllText(filename, text);
        }
    }
}