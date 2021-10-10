using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Equipment
    {
        public Equipment(string equipmentName)
        {
            EquipmentId = Guid.NewGuid();
            EquipmentName = equipmentName;
        }

        public Guid EquipmentId { get;}
        [Required(AllowEmptyStrings =false, ErrorMessage ="Equipment Name is required")]
        public string EquipmentName { get; set; }
    }
}