using KartalApiNew.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KartalApiNew.Models
{
    public class kuponmodel
    {
        public List<User> KuponListesi { get; set; }
        public int Toplam { get; set; }
    }
}