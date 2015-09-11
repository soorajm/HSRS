using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Data;
using HSRS_Global.Models;
namespace HSRS_Global.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            DataSet ds = null;
            Coursemaster course = new Coursemaster();
            course.Action = "Select";
            ds = course.coursemaster(course);
            ViewData["AllcouData"] = ds.Tables[0];
            ViewData["Agile"] = ds.Tables[1];
            ViewData["PMP"] = ds.Tables[2];            
            return View("~/Views/Home/Home.cshtml");
        }

        public ActionResult Contact()
        {
            Random rnd = new Random();
            int fd = rnd.Next(1, 13);
            int sd = rnd.Next(1, 7);
            ViewBag.first = fd;
            ViewBag.sec = sd;
            return View("~/Views/Home/Contact.cshtml");
        }
        [HttpPost]
        public ActionResult Contact(string name,FormCollection col)
        {
            String country = col["country"].ToString();
            String Fname = Request["InputName"].ToString();
            String Lname = Request["InputLName"].ToString();
            String mob = Request["Inputmob"].ToString();
            String Email = Request["InputEmail"].ToString();
            String Enquiry = Request["InputMessage"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<span>Following enquiries coming from HSRS Global</span><br><table style='border:1px solid black;border-collapse:collapse;'><tr><td style='border:1px solid black;'>First Name</td><td style='border:1px solid black;'>" + Fname + "</td></tr><tr><td style='border:1px solid black;'>Last Name</td><td style='border:1px solid black;'>" + Lname + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Mobile Number</td><td style='border:1px solid black;'>" + mob + "</td></tr><tr><td style='border:1px solid black;'>E-mail</td><td style='border:1px solid black;'>" + Email + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Country</td><td style='border:1px solid black;'>" + country + "</td></tr><tr><td style='border:1px solid black;'>Message</td><td style='border:1px solid black;'>" + Enquiry + "</td></tr></table>");
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
                //mail.To.Add("globalhsrs@gmail.com");
                mail.To.Add("info@hsrsglobal.com");
                mail.From = new MailAddress("globalhsrs@gmail.com", "info@hsrsglobal.com");
                mail.Subject = "HSRS Global Contact Enquiry";
                mail.Body = sb.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("globalhsrs@gmail.com", "Orbiosolutions");// Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                TempData["message"] = "Your message has been send and one of our staff members will contact  you shortly";
            }
            catch(Exception ex)
            {
                TempData["message"] = "Sorry.Mail could not be sent.";
            }
            return RedirectToAction("Contact", "contactus");
        }
        public ActionResult newsletter(String Id)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("info@hsrsglobal.com");
                //mail.To.Add("sankar@orbiosolutions.com");
                mail.From = new MailAddress("globalhsrs@gmail.com", "info@hsrsglobal.com");
                mail.Subject = "HSRS Global Contact Enquiry";
                mail.Body = Id;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("globalhsrs@gmail.com", "Orbiosolutions");// Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                TempData["message"] = "Your message has been sent and one of our staff members will contact  you shortly";
            }
            catch (Exception ex)
            {
                TempData["message"] = "Sorry.Mail could not be sent.";
            }
            return View("~/Views/View1.aspx");
        }
        public ActionResult Career()
        {
            return View("~/Views/Home/Career.cshtml");
        }
        public ActionResult Policy()
        {
            return View("~/Views/Home/Policy.cshtml");
        }
        public ActionResult Terms()
        {
            return View("~/Views/Home/Terms.cshtml");
        }
        public ActionResult Exam()
        {
            return View("~/Views/Home/Exam.cshtml");
        }
        public ActionResult Highlyinit()
        {
            return View("~/Views/Home/Highlyinit.cshtml");
        }
        public ActionResult Presentation()
        {
            return View("~/Views/Home/Presentation.cshtml");
        }
        public ActionResult Login()
        {
            return View("~/Views/Home/Login.cshtml");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return View("~/Views/Home/Login.cshtml");
        }
        public ActionResult Sitemap()
        {
            Session.Abandon();
            return View("~/Views/Home/sitemap.cshtml");
        }
        [HttpPost]
        public ActionResult Login(FormCollection Collection)
        {
            String Username = Request["login"].ToString();
            String Password = Request["password"].ToString();
            if (Username == "Hsrsglobaladmin" && Password == "Hsrsglobaladmin123")
            {
                Session["LogedUserID"] = "Hsrsglobaladmin";
                return RedirectToAction("Index", "Coursemaster");
            }
            else
            {
               TempData["Data"] = "Please check your username and password!!!";
                return View("~/Views/Home/Login.cshtml");
            }
        }
    }
}
