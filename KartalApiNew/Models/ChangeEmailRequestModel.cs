using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class ChangeEmailRequestModel
    {
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
    }
}