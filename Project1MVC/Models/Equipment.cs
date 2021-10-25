using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment
    {
        public Equipment(int id, string type, string brand, string model, string description, int quantity, int reorderThreshold)
        {
            this.Id = id;
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.Quantity = quantity;
            this.ReorderThreshold = reorderThreshold;
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int ReorderThreshold { get; set; }
    }
}