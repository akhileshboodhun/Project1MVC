using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Project1MVC.Models
{
    public class Employee
    {
        public Employee(int id, string firstName, string lastName, DateTime dateOfBirth, string email, string role, string employeeStatus, [Optional] ICollection<Equipment> equipments, string password="")
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth =  dateOfBirth;
            Email = email;
            Role = role;
            EmployeeStatus = employeeStatus;
            Equipments = equipments ?? new List<Equipment>();
            Password = password;
        }

        public int Id { get; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("Developer|TE|BA|PO|QA|Admin|Technician", ErrorMessage = "Invalid Role")]
        public string Role { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("Active|Inactive", ErrorMessage = "Invalid Status")]
        public string EmployeeStatus { get; set; }
        
        public int Age { get => Convert.ToInt32(DateTime.Now.Subtract(this.DateOfBirth).TotalDays / 365.2425); }

        public virtual ICollection<Equipment> Equipments { get; set; }
        
        public string Password { get; set; }
    }
}