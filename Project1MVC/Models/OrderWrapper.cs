using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class OrderWrapper
    {
        public Order OrderProp { get; set; }
        public EquipmentOrder[] EquipmentOrderProp {get; set;}
    }
}