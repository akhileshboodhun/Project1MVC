using System;

namespace Project1MVC.Models
{
    public class Role
    {

        public Role(string name)
        {
            this.id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid id { get; set; }
        public string Name { get; set; }
    }
}