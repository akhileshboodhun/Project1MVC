using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public static class Data
    {
        public static List<Employee> LstEmployees = InMemoryEmployees.GetInstance().GetAll().ToList();
        public static List<Equipment> LstEquipments = InMemoryEquipments.GetInstance().GetAll().ToList();
        public static List<Role> LstRoles = InMemoryRoles.GetInstance().GetAll().ToList();
    }
}