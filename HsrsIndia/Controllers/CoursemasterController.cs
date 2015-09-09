using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using HsrsIndia.Models;

namespace HsrsIndia.Controllers
{
    public class CoursemasterController : Controller
    {
        //
        // GET: /Coursemaster/

        Coursemaster master = new Coursemaster();
        public ActionResult Index()
        {
            bindTraingtype();
            coursedata();
            ViewBag.SubmitValue = "Submit";
            return View("~/Views/Home/Coursemaster.cshtml");
        }
        public void coursedata()
        {
            DataSet ds = null;
            Coursemaster course = new Coursemaster();
            course.Action = "Select";
            ds = course.coursemaster(course);
            ViewData["AllcouData"] = ds.Tables[0];
        }
        [HttpPost]
        public ActionResult Index(Coursemaster model, string submit)
        {
            if (submit == "Submit" || submit == null)
            {
                master.Action = "Insert";
                master.Trainingid = model.Trainingid;
                master.Name = model.Name;
                master.SlugName = model.Name.Replace("®", "").Replace("&", "").Replace(" ", "").ToLower() + model.Duration + DateTime.Now.ToShortDateString().Replace("/", "");
                master.Location = model.Location;
                master.Duration = model.Duration;
                master.Date = model.Date;
                master.Price = model.Price;
                DataSet ds = master.coursemaster(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                master.Action = "Update";
                master.C_Id = model.C_Id;
                master.Trainingid = model.Trainingid;
                master.Name = model.Name;
                master.SlugName = model.Name.Replace("®", "").Replace("&", "").Replace(" ", "").ToLower() + model.Duration + DateTime.Now.ToShortDateString().Replace("/", "");
                master.Location = model.Location;
                master.Duration = model.Duration;
                master.Date = model.Date;
                master.Price = model.Price;
                DataSet ds = master.coursemaster(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            return RedirectToAction("Index", "Coursemaster");
        }
        public void bindTraingtype()
        {
            DataSet ds = null;
            Trainingmaster master = new Trainingmaster();
            master.Action = "Select";
            ds = master.FTrainingmaster(master);
            ViewBag.data = ds.Tables[0];
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select", Value = "0" });
            foreach (System.Data.DataRow dr in ViewBag.data.Rows)
            {
                items.Add(new SelectListItem { Text = @dr["Name"].ToString(), Value = @dr["Id"].ToString() });
            }
            ViewData["Type"] = items;
        }
        public ActionResult Edit(String Id)
        {
                ViewBag.SubmitValue = "Update";
                master.Action = "SelectonID";
                TempData["id"] = Id.ToString();
                master.C_Id = Convert.ToInt32(Id);
                DataSet ds = master.coursemaster(master);

                TempData["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                TempData["Location"] = ds.Tables[0].Rows[0]["Location"].ToString();
                TempData["Duration"] = ds.Tables[0].Rows[0]["Duration"].ToString();
                TempData["Price"] = ds.Tables[0].Rows[0]["Price"].ToString();
                TempData["Date"] = ds.Tables[0].Rows[0]["Date"].ToString();
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = ds.Tables[0].Rows[0]["Tname"].ToString(), Value = ds.Tables[0].Rows[0]["Tid"].ToString() });
                ViewData["Type"] = items;
                TempData["ID"] = ds.Tables[0].Rows[0]["ID"].ToString();
                TempData["SubmitValue"] = "Update";
                if (master == null)
                {
                    return HttpNotFound();
                }
                coursedata();
                return View("~/Views/Home/Coursemaster.cshtml");
        }
        public ActionResult Delete(String Id)
        {
            ViewBag.SubmitValue = "Submit";
            master.Action = "Delete";
            TempData["id"] = Id.ToString();
            master.C_Id = Convert.ToInt32(Id);
            DataSet ds = master.coursemaster(master);
            TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            if (master == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
