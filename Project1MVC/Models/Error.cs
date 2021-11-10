using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Error
    {
        public readonly int StatusCode;
        public readonly string Message;

        public Error(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}