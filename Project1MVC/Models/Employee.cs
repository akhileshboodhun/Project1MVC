using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Project1MVC.Models
{
    public class Employee : User
    {
        public Employee()
        {
        }

        public Employee(string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName, DateTime dateOfBirth, string address, string phoneNo, bool isActive, [Optional] int? mgrId) : base(fName, lName, email, salt, hashedPassword, userRoleId, roleName)
        {
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.IsActive = isActive;
            this.MgrId = mgrId;
        }

        public Employee(int? userId, string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName, DateTime dateOfBirth, string address, string phoneNo, bool isActive, [Optional] int? mgrId) : this( fName,  lName,  email,  salt,  hashedPassword,  userRoleId, roleName, dateOfBirth,  address,  phoneNo,  isActive, mgrId)
        {
            this.UserId = userId;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(250), MinLength(5)]
        public string Address { get; set; }
        [Display(Name = "Phone No")]
        [MaxLength(15), MinLength(3)]
        [RegularExpression(@"[0-9]{3,15}", ErrorMessage = "Invalid phone number")]
        public string PhoneNo { get; set; }
        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        public int? MgrId { get; set; }

        public int Age { get => Convert.ToInt32(DateTime.Now.Subtract(this.DateOfBirth).TotalDays / 365.2425); }

    }
}