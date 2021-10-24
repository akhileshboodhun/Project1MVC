using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class FutureStock
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsOrderComplete { get; set; }
        public double  UnitPrice { get; set; }
        public int Qty { get; set; }
        public double NetPrice { get; set; }
        public string SupplierName { get; set; }

    }
}