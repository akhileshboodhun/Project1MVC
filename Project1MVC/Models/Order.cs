using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Order
    {
        public Order() { }

        //public Order(bool isOrderComplete, DateTime orderDate, int supplierId)
        //{
        //    this.IsOrderComplete = isOrderComplete;
        //    this.OrderDate = orderDate;
        //    this.SupplierId = supplierId;
        //}

        //public Order(int? id, bool isOrderComplete, DateTime orderDate, int supplierId) : this(isOrderComplete, orderDate, supplierId)
        //{
        //    this.Id = id;
        //}

        public Order(int? id, bool isOrderComplete, DateTime orderDate, string supplierName, DateTime effectiveDate)
        {
            this.Id = id;
            this.IsOrderComplete = isOrderComplete;
            this.OrderDate = orderDate;
            this.SupplierName = supplierName;
            this.EffectiveDate = effectiveDate;
        }

        public int? Id { get; set; }

        [Display(Name = "Order Completed")]
        [Required]
        public bool IsOrderComplete { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]

        public DateTime OrderDate { get; set; }

        [Display(Name = "Effective Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        //[DateGreaterThan("OrderDate")]
        public DateTime EffectiveDate { get; set; }

        [Required]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        [Required]
        [MaxLength(100), MinLength(3)]
        public string SupplierName { get; set; }

        [Display(Name = "Automatic Dispatch")]
        public bool AutomaticDispatch { get; set; }
    }
}