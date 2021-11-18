using Newtonsoft.Json;
using Project1MVC.DAL;
using Project1MVC.Models;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project1MVC.Controllers
{
    [AuthorizeUser(Roles = "Admin,Technician")]
    public class EquipmentAssignmentController : Controller
    {
        // GET: EquipmentAssignment
        public ActionResult Index(int Id = 10)
        {
            var assignEquipmentService = AssignEquipmentService.Instance;
            var assignedEquipments = assignEquipmentService.ViewAssigned(Id);
            List<int> assignedEquipmentsSerials = assignedEquipments.Select<AssignedEquipment, int>(el => el.SerialNo.ToInt()).ToList();


            var equipDB = EquipmentDAL.Instance;
            var equipmentsList = equipDB.GetAllEquipmentsInStock();
            ViewBag.EquipmentsList = equipmentsList.Select(element => new { EquipId = element.EquipId, DisplayName = element.DisplayName() });
            //List<Equipment> equipments = equipmentsList.Where(el1 => assignedEquipmentsIds.Contains(el1.EquipId)).ToList();

            var query = from assignedEquipment in assignedEquipments
                        join equipment in equipmentsList
                        on assignedEquipment.EquipmentId equals equipment.EquipId
                        select JsonConvert.SerializeObject(new { SerialNo = assignedEquipment.SerialNo, DisplayName = equipment.DisplayName(), DateAssigned = assignedEquipment.DateAssigned.ToShortDateString()});

            ViewBag.AssignedEquipmentList = query.ToList();

            var empDB = EmployeeDAL.Instance;
            var employee = empDB.Get(Id);
            ViewBag.UserId = employee.UserId;
            ViewBag.FName = employee.FName;
            ViewBag.LName = employee.LName;

            return View();
        }



        // POST: EquipmentAssignment/Assign
        [HttpPost]
        public ActionResult Assign(int UserId, int EquipmentId)
        {
            bool state = false;
            try
            {
                // TODO: Add insert logic here
                System.Diagnostics.Debug.WriteLine(UserId.ToString());
                System.Diagnostics.Debug.WriteLine(EquipmentId.ToString());

                int assignorId = Request.Cookies["UserIdCookie"].Value.ToInt();


                var assignEquipmentService = AssignEquipmentService.Instance;
                var assignedEquipment = new AssignedEquipment(employeeId: UserId,
                                          equipmentId: EquipmentId,
                                          assignorId: assignorId);
                state = assignEquipmentService.Assign(assignedEquipment);


                if (state) return Json(assignedEquipment);
                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();
            }
        }
        // POST: EquipmentAssignment/Return
        [HttpPost]
        public ActionResult Return(int UserId, int SerialNo)
        {
            bool state = false;
            try
            {
                // TODO: Add insert logic here
                System.Diagnostics.Debug.WriteLine(UserId.ToString());
                System.Diagnostics.Debug.WriteLine(SerialNo.ToString());

                int assignorId = Request.Cookies["UserIdCookie"].Value.ToInt();

                var equipment = new AssignedEquipment(employeeId: UserId,
                                                        serialNo: SerialNo,
                                                        assignorId: assignorId);

                var assignEquipmentService = AssignEquipmentService.Instance;
                state = assignEquipmentService.Return(equipment);


                if (state) return Json(equipment);
                return HttpNotFound();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult ViewAssignedEquipments()
        {
            var assignEquipmentService = AssignEquipmentService.Instance;
            var assignedEquipments = assignEquipmentService.ViewAllAssignedEquipments();
            var empDB = EmployeeDAL.Instance;
            var employees = empDB.GetAll();
            var equipDB = EquipmentDAL.Instance;
            var equipmentsList = equipDB.GetAll();

            //u.[FName], u.[LName], es.[SerialNo], eq.[Type], eq.[Brand], eq.[Model], eq.[Description]
            var query = from assignedEquipment in assignedEquipments
                        join employee in employees
                        on assignedEquipment.EmployeeId equals employee.UserId
                        join equipment in equipmentsList
                        on assignedEquipment.EquipmentId equals equipment.EquipId
                        select JsonConvert.SerializeObject(new { FirstName = employee.FName, LastName = employee.LName, SerialNo = assignedEquipment.SerialNo, Type = equipment.Type, Brand = equipment.Brand, Model = equipment.Model, Description = equipment.Description });

            ViewBag.AssignedEquipmentList = query.ToList();

            return View();
        }
    }

}
