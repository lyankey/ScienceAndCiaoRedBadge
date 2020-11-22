using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }
        //post action of edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                //better use the below if you have a lot of columns
                //var BranchInDb = db.Branches.FirstOrDefault(b => b.BranchId.Equals(branch.BranchId));
                //BranchInDb.BranchName = branch.BranchName;
                //expensive call if there are numerous numbers of columns, so don't use this if there are many columns, but using for this one
                db.Entry(branch).State = EntityState.Modified ;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(branch);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = db.Branches.Find(id);
            if(branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}