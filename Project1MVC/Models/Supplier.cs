using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Supplier
    {
        public Supplier() { }
        public Supplier(string name, string phoneNo, string address)
        {
            Name = name;
            PhoneNo = phoneNo;
            Address = address;
        }

        public Supplier(int? supplierId, string name, string phoneNo, string address)
        {
            SupplierId = supplierId;
            Name = name;
            PhoneNo = phoneNo;
            Address = address;
        }

        public int? SupplierId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
    }
}