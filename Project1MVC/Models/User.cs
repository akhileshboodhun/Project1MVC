using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class User
    {
        public User()
        {

        }
        public User(string fName, string lName, string email, string hashedPassword, int userRoleId)
        {
            FName = fName;
            LName = lName;
            Email = email;
            HashedPassword = hashedPassword;
            UserRoleId = userRoleId;
        }
        public User(int? userId, string fName, string lName, string email, string hashedPassword, int userRoleId)
        {
            UserId = userId;
            FName = fName;
            LName = lName;
            Email = email;
            HashedPassword = hashedPassword;
            UserRoleId = userRoleId;
        }

        public int? UserId { get; set; }
        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public int UserRoleId { get; set; }

    }
}