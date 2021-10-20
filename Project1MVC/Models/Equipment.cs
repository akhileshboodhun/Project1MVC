using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment
    {

        public Equipment() { }

        public Equipment(string brand, string model, string description, double currentStockCount, double reStockThreshold)
        {
            Brand = brand;
            Model = model;
            Description = description;
            CurrentStockCount = currentStockCount;
            ReStockThreshold = reStockThreshold;
        }

        public Equipment(int? equipId, string brand, string model, string description, double currentStockCount, double reStockThreshold)
        {
            EquipId = equipId;
            Brand = brand;
            Model = model;
            Description = description;
            CurrentStockCount = currentStockCount;
            ReStockThreshold = reStockThreshold;
        }

        public int? EquipId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public double CurrentStockCount { get; set; }

        public double ReStockThreshold { get; set; }

    }
}