﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1MVC.Models
{
    public class UserCredentials
    {
        public UserCredentials()
        {
            this.Email = "";
            this.Password = "";
        }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email")]
        [EmailAddress]
        public string Email {get; set;}
        [Required(AllowEmptyStrings = false)]
        [MaxLength(50), MinLength(5)]
        public string Password { get; set; }

    }
}
