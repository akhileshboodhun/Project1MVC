using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class Tile
    {
        public Tile(string label, string icon, string desc, string location)
        {
            this.label = label;
            this.icon = icon;
            this.desc = desc;
            this.location = location;
        }

        public string label { get; set; }
        public string icon { get; set; }
        public string desc { get; set; }
        public string location { get; set; }
    }
}