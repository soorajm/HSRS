using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HsrsIndia.Models;

namespace HsrsIndia.Controllers
{
    public class TrainingmasterController : Controller
    {
        //
        // GET: /Trainingmaster/
        Trainingmaster master = new Trainingmaster();
        public ActionResult Index()
        {
            trainigdata();
            ViewBag.SubmitValue = "Submit";
            return View("~/Views/Home/Trainingmaster.cshtml");
        }
        public void trainigdata()
        {
            DataSet ds = null;
            Trainingmaster Trainig = new Trainingmaster();
            Trainig.Action = "Select";
            ds = Trainig.FTrainingmaster(Trainig);
            ViewData["AllcouData"] = ds.Tables[0];
        }
        [HttpPost]
        public ActionResult Index(Trainingmaster model, string submit)
        {
            if (submit == "Submit" || submit == null)
            {
                master.Action = "Insert";
                master.Name = model.Name;
                DataSet ds = master.FTrainingmaster(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                master.Action = "Update";
                master.T_Id = model.T_Id;
                master.Name = model.Name;
                DataSet ds = master.FTrainingmaster(master);
                TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            }
            return RedirectToAction("Index", "Trainingmaster");
        }

        public ActionResult Edit(String Id)
        {
            ViewBag.SubmitValue = "Update";
            master.Action = "SelectonID";
            TempData["id"] = Id.ToString();
            master.T_Id = Convert.ToInt32(Id);
            DataSet ds = master.FTrainingmaster(master);

            TempData["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
            TempData["ID"] = ds.Tables[0].Rows[0]["Id"].ToString();
            TempData["SubmitValue"] = "Update";
            if (master == null)
            {
                return HttpNotFound();
            }
            trainigdata();
            return View("~/Views/Home/Trainingmaster.cshtml");
        }
        public ActionResult Delete(String Id)
        {
            ViewBag.SubmitValue = "Submit";
            master.Action = "Delete";
            TempData["id"] = Id.ToString();
            master.T_Id = Convert.ToInt32(Id);
            DataSet ds = master.FTrainingmaster(master);
            TempData["message"] = ds.Tables[0].Rows[0][0].ToString();
            if (master == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
