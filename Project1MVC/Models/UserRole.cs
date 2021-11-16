using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class UserRole
    {
        public UserRole() { }

        public UserRole(string roleName) 
        {
            RoleName = roleName;
        }

        public UserRole(int? userRoleId, string roleName)
        {
            UserRoleId = userRoleId;
            RoleName = roleName;

        }

        public int? UserRoleId { get; set; }

        [Display(Name="Role Name")]
        public string RoleName { get; set; }
    }
}