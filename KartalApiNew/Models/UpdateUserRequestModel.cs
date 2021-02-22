using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class UpdateUserRequestModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
    }
}