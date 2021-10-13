using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment
    {
        public Equipment(string equipmentName, int quantity=100)
        {
            EquipmentId = Guid.NewGuid();
            EquipmentName = equipmentName;
            Quantity = quantity;
        }

        public Guid EquipmentId { get;}
        [Required(AllowEmptyStrings =false, ErrorMessage ="Equipment Name is required")]
        public string EquipmentName { get; set; }
        [Range(5,int.MaxValue, ErrorMessage ="Quantity in Stock should be at least 5 units")]
        public int Quantity { get; set; }
    }
}