using HSRS_Global.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HSRS_Global.Controllers
{
    public class paymentsuccessController : Controller
    {
        //
        // GET: /paymentsuccess/

        public ActionResult Index()
        {

            return View("~/Views/Home/Paymentsuccess.cshtml");
        }

        public void Update(string ID)
        {
            string referenceno = "";
            string amt = "";
           if(!string.IsNullOrEmpty(Session["ref_no"].ToString()))
           {
               referenceno = Session["ref_no"].ToString();
           }
           if (!string.IsNullOrEmpty(Session["amount"].ToString()))
           {
               amt = Session["amount"].ToString();
           }
          
            string amount = (Convert.ToDouble(amt) / 100).ToString();
            string transactiondate = DateTime.Now.ToShortDateString();
          

            Customerdata cus = new Customerdata();
            cus.C_Id =Convert.ToInt16(ID);
            cus.Action = "Update";
            cus.Transactionnumber = referenceno;
            cus.TotalAmount = amount;
            cus.paymentst = "Success";
            cus.Tdate = DateTime.Now.ToShortDateString();
            DataSet ds = cus.custmaster(cus);
        }

        public ActionResult sendmail(String ID, String ObType)
        {
            string referenceno = "";
            double book_cost = 0;
            try
            {
                String email = Session["email"].ToString();
                String C_ID = Session["C_ID"].ToString();
                String location = Session["location"].ToString();
                String comments = Session["comments"].ToString();
                if (!string.IsNullOrEmpty(Session["book_amount"].ToString()))
                {
                    String book_amount = Session["book_amount"].ToString();
                    book_cost = Convert.ToDouble(book_amount) /1.1;
                }

                String[] cdetails = new String[10];
               
                if (!string.IsNullOrEmpty(Session["ref_no"].ToString()))
                {
                    referenceno = Session["ref_no"].ToString();
                }
                cdetails = Session["cdetails"].ToString().Split(',');
                MailMessage mail = new MailMessage();
                //mail.To.Add("sankar@orbiosolutions.com");
                Update(C_ID);
                mail.To.Add("info@hsrsglobal.com");
                mail.To.Add(email);
                mail.From = new MailAddress("globalhsrs@gmail.com", "info@hsrsglobal.com");
                mail.Subject = "HSRS Global Payment Details";
                StringBuilder sb = new StringBuilder();
                sb.Append("<table style='border:1px solid black;border-collapse:collapse;'><tr><td style='border:1px solid black;'>Customer Name</td><td style='border:1px solid black;'>" + cdetails[0]+". "+cdetails[1]+" "+cdetails[2]+ "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Address</td><td style='border:1px solid black;'>" + cdetails[3] + "</td></tr><tr><td style='border:1px solid black;'>City</td><td style='border:1px solid black;'>" + cdetails[4] + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Postcode</td><td style='border:1px solid black;'>" + cdetails[5] + "</td></tr><tr><td style='border:1px solid black;'>Country</td><td style='border:1px solid black;'>" + cdetails[6] + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Mobile Number</td><td style='border:1px solid black;'>" + cdetails[7] + "</td></tr><tr><td style='border:1px solid black;'>Email</td><td style='border:1px solid black;'>" + email + "</td></tr><tr><td style='border:1px solid black;'>Comments</td><td style='border:1px solid black;'>" + comments + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Reference No</td><td style='border:1px solid black;'>" + referenceno + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Date</td><td style='border:1px solid black;'>" + DateTime.Now.ToShortDateString() + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Location</td><td style='border:1px solid black;'>" + location + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>Amount</td><td style='border:1px solid black;'>" + "$" + book_cost.ToString("0.00") + "</td></tr>");
                sb.Append("<tr><td style='border:1px solid black;'>GST</td><td style='border:1px solid black;'>" + "10%" + "</td></tr></table>");
                if (ObType == "Course")
                {
                    DataSet ds = null;
                    //Coursemaster course = new Coursemaster();
                    //course.Action = "SelectonID";
                    //course.C_Id = Convert.ToInt32(ID);
                    //ds = course.coursemaster(course);
                    //if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    mail.Body = "Your Course (" + ID + ") Payment is Successfully done. we'll contact you soon, <br/> " + sb.ToString();
                    //}
                }
                else if (ObType == "online")
                {
                    mail.Body = "Your Online Course (" + ID + ") Payment is Successfully done. we'll contact you soon,<br/> " + sb.ToString();
                }
                else
                {
                    mail.Body = "Your Book Purchace (" + ID + ") Payment is Successfully done. we'll contact you soon, <br/> " + sb.ToString();
                }
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("globalhsrs@gmail.com", "Orbiosolutions");// Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                TempData["message"] = "We will process the booking and send you an email with an invoice. The email and invoice will contain payment details. Payments may be made by electronic funds transfer, credit card or cheque. All payments must use the invoice number as a payment reference.";
            }
            catch (Exception ex)
            {
                TempData["message"] = "Sorry.Mail could not be sent.";
            }
            return View("~/Views/View1.aspx");
        }

        
    }
}
