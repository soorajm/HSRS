using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class HsrsadminController : Controller
    {
        //
        // GET: /Hsrsadmin/

        public ActionResult Index()
        {
            return View("~/Views/Home/Login.cshtml");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return View("~/Views/Home/Login.cshtml");
        }
        [HttpPost]
        public ActionResult Index(FormCollection Collection)
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
