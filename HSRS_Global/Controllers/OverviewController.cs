﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSRS_Global.Controllers
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
