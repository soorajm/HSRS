﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrsIndia.Controllers
{
    public class coursemethodologyController : Controller
    {
        //
        // GET: /coursemethodology/

        public ActionResult Index()
        {
            return View("~/Views/Home/coursemethodology.cshtml");
        }

    }
}
