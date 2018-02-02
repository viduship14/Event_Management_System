using EventManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace EventManagementSystem.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (Session["email"] == null)
            {
                if (TempData["EmailError"] != null)
                    ViewBag.EmailError = TempData["EmailError"];
                if (TempData["error"] != null)
                    ViewBag.error = TempData["error"];
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Events");
            }

        }

        public ActionResult AuthenticateUser(string Email, string Password)
        {
            AdminBL admin = new AdminBL();
            bool authenticate = admin.AuthenticateUser(Email, Password);
            if (authenticate == true)
            {
                //session start and view .. 
                Session["email"] = Email;
                return RedirectToAction("Index", "Events"); //redirect to action where we have to move after login
            }
            else
            {

                TempData["error"] = "UserName or Password Incorrect";
                return RedirectToAction("Index", "Login"); //page when error occurs
            }

        }

        public ActionResult SendPassword(string Email)
        {
            AdminBL admin = new AdminBL();
            if (admin.CheckEmailId(Email))
            {
                string password = admin.GetPassword(Email);
                EmailModel model = new EmailModel();
                model.To = Email;
                model.Subject = "Password Recovery";
                model.Body = "The password for your account is : " + password;
                model.Password = "event.manager";
                model.Email = "manager.event2018@gmail.com";
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
                        try { smtp.Send(mm); }
                        catch (Exception e)
                        {
                            TempData["Message"] = "No Internet Connection";
                            return RedirectToAction("Index", "Login");
                        }
                        TempData["Message"] = "Email sent.";
                    }
                }
                return RedirectToAction("Index", "Login");            
        }
            else
            {
                TempData["EmailError"] = "Email Id not present";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}          
/*public ActionResult ForgotPassword(string email)
        {
                AdminBL admin = new AdminBL();
                String question = admin.GetPassword("Question", "Email", email);
                return View(question); //where we send the question and ask for answer
        }
        public ActionResult ValidateQuestion(string answer)
        {
            AdminBL admin = new AdminBL();
            String password = admin.GetPassword("Password", "Answer", answer); //because there is only single admin
            if(password == null)
            {
                ViewBag.error = "Incorrect Answer";
                return View(); // when the answer is incorrect.
            }
                return View(password); //where we get the ans and return the password.
        }*/
