using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class InMemoryEmployees
    {
        List<Employee> lstEmployees;

        private InMemoryEmployees()
        {
            lstEmployees = new List<Employee> {
            new Employee(1, "Akhilesh", "Boodhun",new Utilities().getDate("26/04/1999"),"A.B@gmail.com","Admin","Active"),
            new Employee(2, "Geesham", "Hossanee",new Utilities().getDate("16/03/1999"),"G.H@gmail.com","Developer","Inactive"),
            new Employee(3, "Bousun", "Teeluck",new Utilities().getDate("30/06/1999"),"B.T@gmail.com","Developer","Active"),
            new Employee(4, "Damien", "Gerard",new Utilities().getDate("27/02/1999"),"D.G@gmail.com","QA","Inactive")
        };
        }

        private static InMemoryEmployees _instance;

        public static InMemoryEmployees GetInstance()
        {
            if(_instance is null) _instance = new InMemoryEmployees();
            return _instance;
        }

        public void Add(Employee obj) => lstEmployees.Add(obj);

        public void Delete(Employee obj) => lstEmployees.Remove(obj);

        public Employee Get(int id) => lstEmployees.FirstOrDefault(el => el.Id == id);

        public IEnumerable<Employee> GetAll() => lstEmployees;

        public void Update(Employee obj)
        {
            var index = lstEmployees.FindIndex(el => el.Id == obj.Id);
            lstEmployees[index] = obj;
        }
    }
}