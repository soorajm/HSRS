using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class FAQController : Controller
    {
        //
        // GET: /FAQ/

        public ActionResult Index()
        {
            return View("~/Views/Home/FAQ.cshtml");
        }

    }
}
