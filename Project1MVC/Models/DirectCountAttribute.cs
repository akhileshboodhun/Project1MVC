using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class DirectCountAttribute : Attribute, ICalculatedAttribute
    {
        public DirectCountAttribute()
        {

        }

        public string ForeignTable
        {
            get; set;
        }

        public string ForeignColumn
        {
            get; set;
        }

        public string ForeignKey
        {
            get; set;
        }
    }
}