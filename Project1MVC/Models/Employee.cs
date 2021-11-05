using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Employee(string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName, DateTime dateOfBirth, string address, string phoneNo, bool isActive) : base(fName, lName, email, salt, hashedPassword, userRoleId, roleName)
        {
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.IsActive = isActive;
        }

        public Employee(int? userId, string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName, DateTime dateOfBirth, string address, string phoneNo, bool isActive) : this( fName,  lName,  email,  salt,  hashedPassword,  userRoleId, roleName, dateOfBirth,  address,  phoneNo,  isActive)
        {
            this.UserId = userId;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public bool IsActive { get; set; }
        public int Age { get => Convert.ToInt32(DateTime.Now.Subtract(this.DateOfBirth).TotalDays / 365.2425); }

    }
}