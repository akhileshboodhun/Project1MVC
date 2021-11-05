using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class InMemoryRoles
    {
        private List<Role> lstRoles;
        public InMemoryRoles()
        {
            string roles = "Developer|TE|BA|PO|QA|Admin|Technician";
            lstRoles = roles.Split('|').Select(el => new Role(1, el)).ToList();
        }
        public void Add(Role obj) => lstRoles.Add(obj);

        public void Delete(Role obj) => lstRoles.Remove(obj);

        public Role Get(int id) => lstRoles.FirstOrDefault(el => el.Id == id);

        public IEnumerable<Role> GetAll() => lstRoles;

        public void Update(Role obj)
        {
            var index = lstRoles.FindIndex(el => el.Id == obj.Id);
            lstRoles[index] = obj;
        }

        private static InMemoryRoles _instance;

        public static InMemoryRoles GetInstance()
        {
            if (_instance is null) _instance = new InMemoryRoles();
            return _instance;
        }
    }
}