using System;

namespace Project1MVC.Models
{
    public class Role
    {
        public Role(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}