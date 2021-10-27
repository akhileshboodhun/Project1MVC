using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace Project1MVC.Services
{
    public static class Logger
    {
        //static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
        //static string filename = path + @"\log.txt";

        public static void Log(string message)
        {
            //Debug.WriteLine(message);

            //string text = $"[{DateTime.Now.ToString()}]: {message + Environment.NewLine}";
            //Task.Run(() => File.AppendAllText(filename, text));
        }
    }
}