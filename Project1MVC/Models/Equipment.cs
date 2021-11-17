using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment : Model<Equipment>
    {
        public Equipment(int id = 0, string type = "", string brand = "", string model = "", string description = "", int currentStockCount = 0, int reStockThreshold = 0, int grandTotal = 0)
        {
            this.EquipId = id;
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.CurrentStockCount = currentStockCount;
            this.ReStockThreshold = reStockThreshold;
            this.GrandTotal = grandTotal;            
        }

        [Display(Name = "Equipment Id"), Key]
        public int EquipId { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ReStock Threshold")]
        public int ReStockThreshold { get; set; }

        // Is not used
        public int CurrentStockCount { get; private set; }

        [Display(Name = "Grand Total"), DirectCount(ForeignTable = "EquipmentInStock", ForeignColumn = "SerialNo", ForeignKey = "EquipId")]
        public int GrandTotal { get; set; }



        public string DisplayName()
        {
            return $"{this.Type} - {this.Brand} - {this.Model}";
        }
    }
}