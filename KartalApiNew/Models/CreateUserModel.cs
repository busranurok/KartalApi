using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}