using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class EquipmentInStock : Equipment
    {
        public EquipmentInStock(int noAssigned, int id = 0, string type = "", string brand = "", string model = "", string description = "", int currentStockCount = 0, int reStockThreshold = 0, int grandTotal = 0) : base(id, type, brand, model, description, currentStockCount, reStockThreshold, grandTotal)
        {
            this.NoAssigned = noAssigned;
        }

        [Display(Name = "Number Assigned")]
        public int NoAssigned { get; set; }
    }
}