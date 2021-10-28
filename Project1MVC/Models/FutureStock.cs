using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class FutureStock
    {
        public FutureStock() { }

        public FutureStock(int id, string brand, string model, DateTime orderDate, bool isOrderComplete, double unitPrice, int qty, double netPrice, string supplierName)
        {
            this.Id = id;
            this.Brand = brand;
            this.Model = model;
            this.OrderDate = orderDate;
            this.IsOrderComplete = isOrderComplete;
            this.UnitPrice = unitPrice;
            this.Qty = qty;
            this.NetPrice = netPrice;
            this.SupplierName = supplierName;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Completed")]
        public bool IsOrderComplete { get; set; }
        [Display(Name = "Unit Price")]
        public double  UnitPrice { get; set; }
        [Display(Name = "Quantity")]
        public int Qty { get; set; }
        [Display(Name = "Net Price (Rs)")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public double NetPrice { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
    }
}