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
            lstRoles = roles.Split('|').Select(el => new Role(el)).ToList();
        }
        public void Add(Role obj) => lstRoles.Add(obj);

        public void Delete(Role obj) => lstRoles.Remove(obj);

        public Role Get(Guid id) => lstRoles.FirstOrDefault(el => el.id == id);

        public IEnumerable<Role> GetAll() => lstRoles;

        public void Update(Role obj)
        {
            var index = lstRoles.FindIndex(el => el.id == obj.id);
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