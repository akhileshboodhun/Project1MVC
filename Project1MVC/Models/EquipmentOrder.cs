using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class EquipmentOrder
    {
        public EquipmentOrder() { }

        public EquipmentOrder(int equipmentId, int qty, double unitPrice)
        {
            this.EquipmentId = equipmentId;
            this.Qty = qty;
            this.UnitPrice = unitPrice;
        }

        public EquipmentOrder(int qty, double unitPrice, string equipmentBrand, string equipmentModel)
        {
            this.Qty = qty;
            this.UnitPrice = unitPrice;
            this.EquipmentBrand = equipmentBrand;
            this.EquipmentModel = equipmentModel;
        }

        public int EquipmentId { get; set; }

        [Display(Name = "Quantity")]
        public int Qty { get; set; }

        [Display(Name = "Unit Price (Rs)")]
        public double UnitPrice { get; set; }

        public double NetPrice { get { return this.UnitPrice * this.Qty; } }

        [Display(Name = "Brand")]
        public string EquipmentBrand { get; set; }

        [Display(Name = "Model")]
        public string EquipmentModel { get; set; }
    }
}