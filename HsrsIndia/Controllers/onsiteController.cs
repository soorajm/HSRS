using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class onsiteController : Controller
    {
        //
        // GET: /onsite/

        public ActionResult Index()
        {
            return View("~/Views/Home/Onsite.cshtml");
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            String Fname = collection["InputFName"].ToString();
            String Lname = collection["InputLName"].ToString();
            String mob = collection["Inputmob"].ToString();
            String Email = collection["InputEmail"].ToString();
            String cmp = collection["Inputcmp"].ToString();
            String cmpadd = collection["Inputcmpadd"].ToString();
            String course = collection["Inputcourse"].ToString();
            String nodelegates = collection["Inputnodelegates"].ToString();
            String Comment = collection["InputMessage"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<span>Following enquiries coming from HSRS Global</span><br><table style='border:1px solid black;border-collapse:collapse;'><tr><td style='border:1px solid black;'>First Name</td><td style='border:1px solid black;'>" + Fname + "</td></tr><tr><td style='border:1px solid black;'>Last Name</td><td style='border:1px solid black;'>" + Lname + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Mobile Number</td><td style='border:1px solid black;'>" + mob + "</td></tr><tr><td style='border:1px solid black;'>E-mail</td><td style='border:1px solid black;'>" + Email + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Company</td><td style='border:1px solid black;'>" + cmp + "</td></tr><tr><td style='border:1px solid black;'>Company Address</td><td style='border:1px solid black;'>" + cmpadd + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Course</td><td style='border:1px solid black;'>" + course + "</td></tr><tr><td style='border:1px solid black;'>No of Delegates</td><td style='border:1px solid black;'>" + nodelegates + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Comment</td><td style='border:1px solid black;'>" + Comment + "</td></tr></table>");
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
                //mail.To.Add("globalhsrs@gmail.com");
                mail.To.Add("info@hsrsglobal.com");
                mail.From = new MailAddress("globalhsrs@gmail.com");
                mail.Subject = "HSRS Global Onsite Enquiry";
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
            catch (Exception ex)
            {
                TempData["message"] = "Sorry.Mail could not be sent.";
            }
            return RedirectToAction("Index", "prince2onsite");
        }
    }
}
