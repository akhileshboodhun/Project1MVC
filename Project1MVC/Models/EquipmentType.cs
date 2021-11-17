using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Project1MVC.Models
{
    public class EquipmentType : Model<EquipmentType>
    {
        public EquipmentType(int id = 0, string type = "", string brand = "", string model = "", string description = "", int reStockThreshold = 0)
        {
            this.EquipTypeId = id;
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.Description = description;
            this.ReStockThreshold = reStockThreshold;
        }

        [Display(Name = "Equipment Id"), Key]
        public int EquipTypeId { get; set; }

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

        [Display(Name = "Grand Total"), DirectCount(ForeignTable = "Equipment", ForeignColumn = "Serial", ForeignKey="EquipTypeId")]
        public int GrandTotal { get; set; }


        public string DisplayName()
        {
            return $"{this.Type} - {this.Brand} - {this.Model}";
        }
    }
}