using KartalApiNew.DataAccess;
using KartalApiNew.Entities;
using KartalApiNew.Helpers;
using KartalApiNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace KartalApiNew.Controllers
{
    public class UserController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("User/Login")]
        public User Login(string email, string password)
        {
            User user = null;

            try
            {
                user= UserManager.GetUserByEmailPassword(email, password);
            }
            catch (Exception ex)
            {
                return user;
            }

            return user;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("User/GetUserById")]
        public User GetUserById(int id)
        {
            User user = null;

            try
            {
                user= UserManager.GetUserById(id);
            }
            catch (Exception ex)
            {
                return user;
            }

            return user;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("User/GetSampleModel")]
        public CreateUserModel GetSampleModel()
        {
            var model = new CreateUserModel();
            model.Email = "okhasanbasri@gmail.com";
            model.Lastname = "OK";
            model.Name = "HBO";
            model.Password = "1234";
            model.PhoneNumber = "5511103993";
            return model;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("User/CreateUser")]
        public CreateUserResponseModel CreateUser([FromBody] CreateUserModel model)
        {

            var responseModel = new CreateUserResponseModel();
            //ilk olarak bu email ile daha önce kayıt yapılmış mı kontrol etmeliyiz

            try
            {
                var isExist = UserManager.IsExistUserByEmail(model.Email);

                if (isExist == true)
                {
                    responseModel.IsSuccess = false;
                    responseModel.ErrorMessage = "Bu email ile daha önce kullanıcı oluşturulmuştur!!";
                    return responseModel;
                }


                //daha önce kayıt yapılmadıysa bu bilgiler ile kullanıcıyı oluşturalım

                UserManager.InsertUser(model);
                responseModel.IsSuccess = true;
                responseModel.ErrorMessage = "";

                string mailSubject = "Üyelik aktivasyonu";
                string mailBody = $"Aşağıdaki linke giderek üyeliğinizi aktif hale getirebilirsiniz!\n www.biga.vaktihazar.com/UserProcess/ActivateUser?email={model.Email}";

                MailHelper.SendSmtpMail(mailSubject, mailBody, model.Email);


                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.IsSuccess = false;
                responseModel.ErrorMessage = ex.Message;
                return responseModel;
            }
        }

        //Mobilden şifremi unuttum dediğinde gelen mail adresine mail atılacak
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("User/ForgotPassword")]
        public string ForgotPassword(string email)
        {
            try
            {
                //Mail Gönderilecek
                string mailSubject = "Şifre Sıfırlama";
                string mailBody = $"Aşağıdaki linke giderek mail şifrenizi sıfırlayabilirsiniz!\n www.biga.vaktihazar.com/UserProcess/RefreshPassword?email={email}";

                MailHelper.SendSmtpMail(mailSubject, mailBody, email);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
           
            return "";
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("User/UpdateUser")]
        public UpdateUserResponseModel UpdateUser(UpdateUserRequestModel model)
        {
            var response = new UpdateUserResponseModel();
            try
            {
                UserManager.UpdateUser(model);
                response.IsSuccess = true;
                response.Message = "Başarılı";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("User/DeactivateUser")]
        public DeactivateUserResponseModel DeactivateUser(int id)
        {
            var resultMessage = "Başarılı";
            var isSuccess = true;
            try
            {
                UserManager.DeactivateUser(id);
            }
            catch (Exception ex)
            {
                resultMessage = "İşlem gerçekleştirilirken hata oluştu:" + ex.Message;
                isSuccess = false;
            }

            return new DeactivateUserResponseModel() { Message=resultMessage,IsSuccess=isSuccess };
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("User/ChangeEmail")]
        public ChangeEmailResponseModel ChangeEmail(ChangeEmailRequestModel model)
        {
            var resultMessage = "Başarılı";
            var isSuccess = true;
            try
            {
                UserManager.ChangeEmail(model.OldEmail, model.NewEmail);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                resultMessage = ex.Message;
            }

            return new ChangeEmailResponseModel() { IsSuccess=isSuccess,Message=resultMessage };
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("User/ChangePassword")]
        public ChangePasswordResponseModel ChangePassword(ChangePasswordRequestModel model)
        {
            var resultMessage = "Başarılı";
            var isSuccess = true;
            try
            {
                UserManager.ChangePassword(model.OldPassword,model.UserId,model.NewPassword);
            }
            catch (Exception ex)
            {
                isSuccess = false;
                resultMessage = ex.Message;
            }

            return new ChangePasswordResponseModel() { IsSuccess = isSuccess, Message = resultMessage };
        }





        //Mail içerisindeki linke tıklandığında şifre yenileme ekranı açılacak
        //[System.Web.Mvc.HttpGet]
        //[System.Web.Mvc.Route("User/ForgotPasswordView")]
        //public ActionResult ForgotPasswordView(string email)
        //{
        //    var model = new ForgotPasswordViewModel();
        //    model.Email = email;
        //    model.Password = "";
        //    return View(model);
        //}

        ////şifre yenileme ekranındaki kaydet butonuna basılınca ilgili kullanıcının şifresi değiştirilecek
        //[System.Web.Mvc.HttpPost]
        //public ActionResult RefreshPassword(string password, string email)
        //{
        //    try
        //    {
        //        UserManager.UpdatePassword(email, password);
        //        return Json("Başarılı");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex);
        //    }
        //}
    }
}
