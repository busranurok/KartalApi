using KartalApiNew.DataAccess;
using KartalApiNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KartalApiNew.Controllers
{
    public class UserProcessController : Controller
    {
        public ActionResult RefreshPassword(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ChangePassword([FromBody]RefreshPasswordModel model)
        {
            var resultMessage = "Başarısız";
            if (model!=null)
            {
                UserManager.UpdatePassword(model.Eposta, model.Sifre);
                resultMessage = "Başarılı";
            }

            return Json(resultMessage);
        }

        public ActionResult ActivateUser(string email)
        {
            var resultMessage = "Üyeliğiniz başarıyla aktif edilmiştir!!";
            try
            {
                UserManager.ActivateUser(email);

            }
            catch (Exception ex)
            {
                resultMessage = ex.Message;
            }
            ViewBag.Message = resultMessage;
            return View();
        }
    }
}