using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class ChangePasswordRequestModel
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}