using System;
using System.Runtime.InteropServices;

namespace Project1MVC.Models
{
    public class AssignedEquipment
    {
        public AssignedEquipment(int? employeeId, [Optional] int? equipmentId, [Optional] DateTime dateAssigned, [Optional] DateTime dateReturned, [Optional] int? assignorId, [Optional] int? serialNo)
        {
            EmployeeId = employeeId;
            EquipmentId = equipmentId;
            DateAssigned = dateAssigned;
            DateReturned = dateReturned;
            AssignorId = assignorId;
            SerialNo = serialNo;
        }

        public int? EmployeeId { get; }
        public int? EquipmentId { get; }
        public DateTime DateAssigned {get;}
        public DateTime DateReturned { get;}
        public int? AssignorId { get;}
        public int? SerialNo { get; }
    }
}