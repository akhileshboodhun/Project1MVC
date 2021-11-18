using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MaxLength(100), MinLength(3)]
        public string Name { get; set; }
        [MaxLength(15), MinLength(3)]
        public string PhoneNo { get; set; }
        [MaxLength(250), MinLength(3)]
        public string Address { get; set; }
    }
}