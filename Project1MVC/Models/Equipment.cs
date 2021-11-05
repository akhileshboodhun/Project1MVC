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
            this.Id = id;
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.CurrentStockCount = currentStockCount;
            this.ReStockThreshold = reStockThreshold;
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public int CurrentStockCount { get; set; }

        public int ReStockThreshold { get; set; }

        public string DisplayName()
        {
            return $"{this.Type} - {this.Brand} - {this.Model}";
        }
    }
}