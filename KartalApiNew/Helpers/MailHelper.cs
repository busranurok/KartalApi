using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace KartalApiNew.Helpers
{
    public class MailHelper
    {
        public static void SendSmtpMail(string subject, string body, string email)
        {

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("");
            //
            mail.To.Add(email);
            //
            mail.Subject = subject;
            //
            mail.Body = body;
            //
            SmtpClient smtp = new SmtpClient();
            //
            smtp.Credentials = new System.Net.NetworkCredential("", "");
            smtp.Port = 587;
            smtp.Host = "";
            smtp.EnableSsl = false;

            smtp.Send(mail);
        }
    }
}