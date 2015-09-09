using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/

        public ActionResult Index()
        {
            return View("~/Views/Home/Download.cshtml");
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            String Fname = collection["InputFName"].ToString();
            String Lname = collection["InputLname"].ToString();
            String mob = collection["mobileno"].ToString();
            String Email = collection["InputEmail"].ToString();
            String cmp = collection["Inputcmp"].ToString();
            String jtitle = collection["InputJtitle"].ToString();
            String country = collection["ddlcountry"];
            String intrest = collection["yesno"];
            String Queries = collection["InputMessage"];
            String chk1 = collection["chk1"];
            String chk2 = collection["chk2"];
            String chk3 = collection["chk3"];
            String chkval = chk1 + "," + chk2 + "," + chk3;
            //if (chk1 != null && chk2 != null && chk3 != null)
            //{ chkval = chk1 + "," + chk2 + "," + chk3; }
            //if (chk1 != null && chk2 != null && chk3 != null)
            //{ chkval = chk1 + "," + chk2 + "," + chk3; }
            String file = collection["filename"].ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append("<span>Following enquiries coming from HSRS Global</span><br><table style='border:1px solid black;border-collapse:collapse;'><tr><td style='border:1px solid black;'>First Name</td><td style='border:1px solid black;'>" + Fname + "</td></tr><tr><td style='border:1px solid black;'>Last Name</td><td style='border:1px solid black;'>" + Lname + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Mobile Number</td><td style='border:1px solid black;'>" + mob + "</td></tr><tr><td style='border:1px solid black;'>E-mail</td><td style='border:1px solid black;'>" + Email + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Company</td><td style='border:1px solid black;'>" + cmp + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Job Title</td><td style='border:1px solid black;'>" + jtitle + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Country</td><td style='border:1px solid black;'>" + country + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Interested in PRINCE2 </td><td style='border:1px solid black;'>" + intrest + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Queries</td><td style='border:1px solid black;'>" + Queries + "</td></tr>");
            sb.Append("<tr><td style='border:1px solid black;'>Needs</td><td style='border:1px solid black;'>" + chkval + "</td></tr></table>");
            sb.Append("<tr><td style='border:1px solid black;'>Download File Name</td><td style='border:1px solid black;'>" + file + "</td></tr></table>");
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Email);
                //mail.To.Add("globalhsrs@gmail.com");
                mail.To.Add("info@hsrsglobal.com");
                mail.From = new MailAddress("globalhsrs@gmail.com");
                mail.Subject = "HSRS Global Download Enquiry";
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
                TempData["file"] = file;
                string[] filenames = file.Split('/');
                TempData["filename"] = filenames[4].ToString();
                TempData["message"] = "Your message is send successfully";
            }
            catch (Exception ex)
            {
                TempData["message"] = "Sorry.Mail could not be sent.";
            }
            return RedirectToAction("Index", "downloadprince2");

        }
    }
}
