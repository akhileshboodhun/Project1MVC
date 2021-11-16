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
            List<int> assignedEquipmentsIds = assignedEquipments.Select<AssignedEquipment, int>(el => el.EquipmentId.ToInt()).ToList();

            var equipDB = EquipmentDAL.Instance;
            var equipmentsList = equipDB.GetAll();
            ViewBag.EquipmentsList = equipmentsList;
            List<Equipment> equipments = equipmentsList.Where(el1 => assignedEquipmentsIds.Contains(el1.EquipId)).ToList();

            var empDB = EmployeeDAL.Instance;
            var employee = empDB.Get(Id);
            ViewBag.UserId = employee.UserId;
            ViewBag.FName = employee.FName;
            ViewBag.LName = employee.LName;

            return View(equipments);
        }



        // POST: EquipmentAssignment/Assign
        [HttpPost]
        public ActionResult Assign(int UserId, List<int> EquipmentId)
        {
            try
            {
                // TODO: Add insert logic here
                System.Diagnostics.Debug.WriteLine(UserId.ToString());
                System.Diagnostics.Debug.WriteLine(EquipmentId.ToString());

                int assignorId = Request.Cookies["UserIdCookie"].Value.ToInt();

                var equipments = new List<AssignedEquipment>();
                EquipmentId.ForEach(equipId =>
                equipments.Add(
                    new AssignedEquipment(employeeId: UserId,
                                          equipmentId: equipId,
                                          assignorId: assignorId))
                );

                var assignEquipmentService = AssignEquipmentService.Instance;
                assignEquipmentService.Assign(equipments);


                return RedirectToAction("Index", "Employees");
            }
            catch
            {
                return View();
            }
        }
        // POST: EquipmentAssignment/Return
        [HttpPost]
        public ActionResult Return(int UserId, int EquipmentId)
        {
            bool state = false;
            try
            {
                // TODO: Add insert logic here
                System.Diagnostics.Debug.WriteLine(UserId.ToString());
                System.Diagnostics.Debug.WriteLine(EquipmentId.ToString());

                int assignorId = Request.Cookies["UserIdCookie"].Value.ToInt();

                var equipment = new AssignedEquipment(employeeId: UserId,
                                                        equipmentId: EquipmentId,
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
    }

}
