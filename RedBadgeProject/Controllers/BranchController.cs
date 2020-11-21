using RedBadgeProject.Data;
using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedBadgeProject.Controllers
{
    public class BranchController : Controller
    {
        private ApplicationDbContext db;
        public BranchController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(db.Branches.ToList());
        }
        //get
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}