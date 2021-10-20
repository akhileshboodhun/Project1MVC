using System;
using System.Collections.Generic;
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

        public string RoleName { get; set; }
    }
}