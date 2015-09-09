using HSRS_Global.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSRS_Global.Controllers
{
    public class OnlineCoursemasterController : Controller
    {
        //
        // GET: /OnlineCoursemaster/
        OnlineCourseMaster master = new OnlineCourseMaster();
        public ActionResult Index()
        {
            coursedata();
            ViewBag.SubmitValue = "Submit";
            return View("~/Views/Home/OnlineCoursemaster.cshtml");
        }
        public void coursedata()
        {
            DataSet ds = null;
            OnlineCourseMaster course = new OnlineCourseMaster();
            course.Action = "Select";
            ds = course.OnlineCourseMasters(course);
            ViewData["AllcouData"] = ds.Tables[0];
        }
        [HttpPost]
        public ActionResult Index(OnlineCourseMaster model, string submit)
        {
            if (submit == "Submit" || submit == null)
            {
                master.Action = "Insert";
                master.Name = model.Name;
                master.Slug_Name = model.Name.Replace("®", "").Replace("&", "").Replace(" ", "").ToLower() + model.C_Price + DateTime.Now.ToShortDateString().Replace("/", "");
                master.Details = model.Details;
                master.C_Price = model.C_Price;
                DataSet ds = master.OnlineCourseMasters(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                master.Action = "Update";
                master.Id = model.Id;
                master.Name = model.Name;
                master.Slug_Name = model.Name.Replace("®", "").Replace("&", "").Replace(" ", "").ToLower() + model.C_Price + DateTime.Now.ToShortDateString().Replace("/", "");
                master.Details = model.Details;
                master.C_Price = model.C_Price;
                DataSet ds = master.OnlineCourseMasters(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            return RedirectToAction("Index", "OnlineCoursemaster");
        }
        public ActionResult Edit(String Id)
        {
            ViewBag.SubmitValue = "Update";
            master.Action = "SelectonID";
            TempData["id"] = Id.ToString();
            master.Id = Convert.ToInt32(Id);
            DataSet ds = master.OnlineCourseMasters(master);

            TempData["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
            TempData["Det"] = ds.Tables[0].Rows[0]["Details"].ToString();
            TempData["price"] = ds.Tables[0].Rows[0]["price"].ToString();
            TempData["ID"] = ds.Tables[0].Rows[0]["ID"].ToString();
            TempData["SubmitValue"] = "Update";
            if (master == null)
            {
                return HttpNotFound();
            }
            coursedata();
            return View("~/Views/Home/OnlineCoursemaster.cshtml");
        }
        public ActionResult Delete(String Id)
        {
            ViewBag.SubmitValue = "Submit";
            master.Action = "Delete";
            TempData["id"] = Id.ToString();
            master.Id = Convert.ToInt32(Id);
            DataSet ds = master.OnlineCourseMasters(master);
            TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            if (master == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
