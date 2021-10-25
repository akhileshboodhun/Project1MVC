﻿using Project1MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Services
{
    public class InMemoryEquipments
    {
        List<Equipment> lstEquipments;
        private InMemoryEquipments()
        {
            lstEquipments = new List<Equipment> {
            //new Equipment("Desktop 520"),
            //new Equipment("Laptop P1"),
            //new Equipment("Laptop Standard"),
            //new Equipment("Monitor"),
            //new Equipment("Mouse"),
            //new Equipment("Keyboard"),
            //new Equipment("Docking Station"),
            //new Equipment("headset")
        };
        }

        private static InMemoryEquipments _instance;

        public static InMemoryEquipments GetInstance()
        {
            if(_instance is null) _instance = new InMemoryEquipments();
            return _instance;
        }
        public void Add(Equipment obj) => lstEquipments.Add(obj);

        public void Delete(Equipment obj) => lstEquipments.Remove(obj);

        public Equipment Get(int id) => lstEquipments.FirstOrDefault(el => el.Id == id);

        public IEnumerable<Equipment> GetAll() => lstEquipments;


        public void Update(Equipment obj)
        {
            var index = lstEquipments.FindIndex(el => el.Id == obj.Id);
            lstEquipments[index] = obj;
        }
    }
}