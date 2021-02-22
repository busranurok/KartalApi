using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class CreateUserResponseModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}