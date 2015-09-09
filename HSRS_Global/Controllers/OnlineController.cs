using HSRS_Global.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace HSRS_Global.Controllers
{
    public class OnlineController : Controller
    {
        //
        // GET: /Online/

        public ActionResult Index()
        {
            coursedata();
            return View("~/Views/Home/Online.cshtml");
        }
        public void coursedata()
        {
            DataSet ds = null;
            OnlineCourseMaster course = new OnlineCourseMaster();
            course.Action = "Select";
            ds = course.OnlineCourseMasters(course);
            ViewData["AllcouData"] = ds.Tables[0];
        }
    }
}
