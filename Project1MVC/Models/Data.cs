using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public static class Data
    {
        public static List<Employee> LstEmployees = new List<Employee> {
            new Employee("Akhilesh", "Boodhun",new Utilities().getDate("26/04/1999"),"A.B@gmail.com","Admin","Active"),
            new Employee("Geesham", "Hossanee",new Utilities().getDate("16/03/1999"),"G.H@gmail.com","Developer","Inactive"),
            new Employee("Bousun", "Teeluck",new Utilities().getDate("30/06/1999"),"B.T@gmail.com","Developer","Active"),
            new Employee("Damien", "Gerard",new Utilities().getDate("27/02/1999"),"D.G@gmail.com","QA","Inactive")
        };
        public static List<Equipment> LstEquipments = new List<Equipment> {
            new Equipment("Desktop 520"),
            new Equipment("Laptop P1"),
            new Equipment("Laptop Standard"),
            new Equipment("Monitor"),
            new Equipment("Mouse"),
            new Equipment("Keyboard"),
            new Equipment("Docking Station"),
            new Equipment("headset") 
        };


    }
}