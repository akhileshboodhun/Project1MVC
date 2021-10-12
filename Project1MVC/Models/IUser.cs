using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1MVC.Models
{
    interface IUser
    {
        string Email {get; set;}
        string Password { get; set; }

    }
}
