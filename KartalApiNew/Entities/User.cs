using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsApproved { get; set; }
        public string Password { get; set; }
    }
}