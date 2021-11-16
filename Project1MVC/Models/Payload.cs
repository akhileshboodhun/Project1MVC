using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Payload<T>
    {
        public Payload(bool state, T data, string message)
        {
            this.state = state;
            this.data = data;
            this.message = message;
        }

        public bool state { get; set; }
        public T data { get; set; }
        public string message { get; set; }

    }
}