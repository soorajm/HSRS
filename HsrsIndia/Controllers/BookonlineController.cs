using HsrsIndia.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class BookonlineController : Controller
    {
        //
        // GET: /Bookonline/

        public ActionResult Index(String seName, string location, String ObType, String price)
        {
            ViewBag.Type = ObType;
            if (ObType == "Course")
            {
                DataSet ds = null;
                Coursemaster course = new Coursemaster();
                course.Action = "Selectonslug";
                course.SlugName = seName;
                course.Location = location;
                ds = course.coursemaster(course);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.CName = ds.Tables[0].Rows[0]["Name"].ToString();
                    ViewBag.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    ViewBag.Date = ds.Tables[0].Rows[0]["Date"].ToString();
                    String pri = ds.Tables[0].Rows[0]["Price"].ToString();
                    ViewBag.Fee = pri;
                    double gst1 = (Convert.ToDouble(pri) * 10 / 100);
                    ViewBag.gst = gst1.ToString("0.00");
                    double total1 = Convert.ToDouble(pri) + gst1;
                    ViewBag.total = total1.ToString("0.00");
                }
            }
            else if (ObType == "Online")
            {
                DataSet ds = null;
                OnlineCourseMaster course = new OnlineCourseMaster();
                course.Action = "Selectonslug";
                course.Slug_Name = seName;
                ds = course.OnlineCourseMasters(course);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    ViewBag.CName = ds.Tables[0].Rows[0]["Name"].ToString();
                    String pri = ds.Tables[0].Rows[0]["price"].ToString();
                    ViewBag.Fee = pri;
                    double gst1 = (Convert.ToDouble(pri) * 10 / 100);
                    ViewBag.gst = gst1.ToString("0.00");
                    double total1 = Convert.ToDouble(pri) + gst1;
                    ViewBag.total = total1.ToString("0.00");
                }
            }
            else
            {
                ViewBag.CName = seName;
                ViewBag.Fee = price;
                double gst = (Convert.ToDouble(price) * 10 / 100);
                ViewBag.gst = gst;
                double total = Convert.ToDouble(price) + gst;
                ViewBag.total = total;
            }
            return View("~/Views/Home/Bookonline.cshtml");
        }
        [HttpPost]
        public ActionResult Index(String ID,FormCollection col)
        {
            String Ctype = Request["ctype"].ToString();
            String Cname = Request["cname"].ToString();
            String Title = col["title"].ToString();
            String Fname = Request["firstname"].ToString();
            String Lname = Request["lastname"].ToString();
            String cmpname = Request["cmpname"].ToString();
            String add1 = Request["address1"].ToString();
            String add2 = Request["address2"].ToString();
            String city = Request["city"].ToString();
            String pin = Request["postcode"].ToString();
            String country = col["country"].ToString();
            String tele = Request["tele"].ToString();
            String mobile = Request["mobile"].ToString();
            String email = Request["email"].ToString();
            String comments = Request["InputMessage"].ToString();
            String transactionamount = "12345678901";
            String totalamount = Request["totalfee"].ToString();
            Customerdata cus = new Customerdata();
            cus.Cus_id =ID;
            cus.Action = "Insert";
            cus.Title = Title;
            cus.Fname = Fname;
            cus.Lname = Lname;
            cus.cmpname = cmpname;
            cus.add1 = add1;
            cus.add2 = add2;
            cus.city = city;
            cus.postcode = pin;
            cus.country = country;
            cus.tele = tele;
            cus.mobile = mobile;
            cus.mail = email;
            cus.Transactionnumber = transactionamount;
            cus.TotalAmount = totalamount;
            cus.Tdate = DateTime.Now.ToShortDateString();
            DataSet ds = cus.custmaster(cus);
            TempData["Ctype"] = Ctype;
           TempData["Cname"] = Cname;
           Session["email"] = email;
           Session["comments"] = comments;
           Session["cdetails"] = Title + "," + Fname + "," + Lname + "," + add1 + "," + city + "," + pin + "," + country + "," + mobile;
            //TempData["message"] = "Transaction successfully";
           return RedirectToAction("Index", "payment");
        }
    }
}
