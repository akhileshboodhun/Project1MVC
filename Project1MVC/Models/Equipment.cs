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
        //public object this[string propertyName]
        //{
        //    get
        //    {
        //        Type myType = typeof(Equipment);
        //        PropertyInfo myPropInfo = myType.GetProperty(propertyName);
        //        return myPropInfo.GetValue(this, null);
        //    }

        //    set
        //    {
        //        Type myType = typeof(Equipment);
        //        PropertyInfo myPropInfo = myType.GetProperty(propertyName);
        //        myPropInfo.SetValue(this, value, null);
        //    }
        //}

        public Equipment(int id = 0, string type = "", string brand = "", string model = "", string description = "", int currentStockCount = 0, int reStockThreshold = 0)
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