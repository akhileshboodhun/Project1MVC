using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment
    {
        public Equipment(int id, string type, string brand, string model, string description, int currentStockCount = 11, int reStockThreshold = 10)
        {
            this.EquipId = id;
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.CurrentStockCount = currentStockCount;
            this.ReStockThreshold = reStockThreshold;
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

        [Display(Name = "Current Stock Count")]
        public int CurrentStockCount { get; set; }

        [Display(Name = "ReStock Threshold")]
        public int ReStockThreshold { get; set; }

        public string DisplayName()
        {
            return $"{this.Type} - {this.Brand} - {this.Model}";
        }
    }
}