using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1MVC.Models
{
    public class User : UserRole
    {
        public User()
        {

        }
        public User(string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName) : base(userRoleId, roleName)
        {
            FName = fName;
            LName = lName;
            Email = email;
            Salt = salt;
            HashedPassword = hashedPassword;
            UserRoleId = userRoleId;
        }
        public User(int? userId, string fName, string lName, string email, string salt, string hashedPassword, int userRoleId, string roleName): this(fName, lName, email, salt, hashedPassword, userRoleId, roleName)
        {
            UserId = userId;
        }

        public int? UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [MaxLength(50), MinLength(3)]
        public string FName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [MaxLength(50), MinLength(3)]
        public string LName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(16)]
        public string Salt { get; set; }

        [StringLength(44)]
        public string HashedPassword { get; set; }

        public string FullName { get => (FName + " " + LName); }


    }
}