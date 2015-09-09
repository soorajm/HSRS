using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class OverviewController : Controller
    {
        //
        // GET: /Overview/

        public ActionResult Index()
        {
            return View("~/Views/Home/Overview.cshtml");
        }

    }
}
