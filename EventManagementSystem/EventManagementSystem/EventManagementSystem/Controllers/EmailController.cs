using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        
        [HttpPost]
        public ActionResult Index(string EmailId)
        {
            AdminBL admin = new AdminBL();
            string password = admin.GetPassword(EmailId);
            EmailModel model = new EmailModel();
            model.To = EmailId;
            model.Subject = "Password Recovery";
            model.Body = password;
            model.Password = "";
            model.Email = "";
            using (MailMessage mm = new MailMessage(model.Email, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }
            return View();
        }
    }
}