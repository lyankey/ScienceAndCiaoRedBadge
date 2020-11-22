using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RedBadgeProject.Models;
using ScienceAndCiao.Data;

namespace RedBadgeProject.Controllers
{
    public class KitController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Kit
        //doesn't display branch here
        public ActionResult Index()
        {
            var kits = db.Kits.Include(k => k.Branch);
            return View(kits.ToList());
        }

        // GET: Kit/Details/5
        //passing a viewmodel instead of viewbag
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kit kit = db.Kits.Find(id);
            if (kit == null)
            {
                return HttpNotFound();
            }
            var model = new KitViewModel
            {
                Kit = kit,
                Branch = db.Branches.ToList()
            };
            //passing my viewmodel in instead of the kit obj
            return View(model);
        }

        // GET: Kit/Create
        public ActionResult Create()
        {
            //removed default viewbag
            //ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName");
            var branch = db.Branches.ToList();
            var model = new KitViewModel
            //Branches is the list in kitviewmodel
            {
                Branch = branch
            };
            //passing in my viewmodel instead of default kit obj
            return View(model);
        }

        // POST: Kit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //don't like this because it's a magic string you'd have to remember to change. 
        //public ActionResult Create([Bind(Include = "KitId,Title,Description,Grade,ImageUrl,Price,DateAdded,BranchId,PublicationDate,LengthInMinutes")] Kit kit)
        //not getting a kit object so instead we are getting the kit view model, then turning that into an object - manually converting a viewmodel object into an object of a class
        public ActionResult Create(KitViewModel kvmodel)
        {
            var kit = new Kit
            {
                Title = kvmodel.Kit.Title,
                Description = kvmodel.Kit.Description,
                Grade = kvmodel.Kit.Grade,
                ImageUrl = kvmodel.Kit.ImageUrl,
                Price = kvmodel.Kit.Price,
                DateAdded = kvmodel.Kit.DateAdded,
                Branch = kvmodel.Kit.Branch,
                BranchId = kvmodel.Kit.BranchId,
                PublicationDate = kvmodel.Kit.PublicationDate,
                LengthInMinutes = kvmodel.Kit.LengthInMinutes

            };
            if (ModelState.IsValid)
            {
                db.Kits.Add(kit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //get it from the database and pass the kit viewmodel into the view
            kvmodel.Branch = db.Branches.ToList();
            return View(kvmodel);
            //don't like the viewbag as much as the viewmodel - you can associate the viewmodel strictly with the view
            //ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName", kit.BranchId);
            //passing in viewmodel into the view instead of default kit
           
        }

        // GET: Kit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kit kit = db.Kits.Find(id);
            if (kit == null)
            {
                return HttpNotFound();
            }
            //take out viewbag 
            //ViewBag.BranchId = new SelectList(db.Branches, "BranchId", "BranchName", kit.BranchId);
            var model = new KitViewModel
            {
                Kit = kit,
                Branch = db.Branches.ToList()
            };
           
            return View(model);
     
        }

        // POST: Kit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //removed "magic string" converting the kit view model into a kit object
        //have to add kitId now because it is not new anymore
        public ActionResult Edit(KitViewModel kvmodel)
        {
            var kit = new Kit
            {
                KitId = kvmodel.Kit.KitId,
                Title = kvmodel.Kit.Title,
                Description = kvmodel.Kit.Description,
                Grade = kvmodel.Kit.Grade,
                ImageUrl = kvmodel.Kit.ImageUrl,
                Price = kvmodel.Kit.Price,
                DateAdded = kvmodel.Kit.DateAdded,
                Branch = kvmodel.Kit.Branch,
                BranchId = kvmodel.Kit.BranchId,
                PublicationDate = kvmodel.Kit.PublicationDate,
                LengthInMinutes = kvmodel.Kit.LengthInMinutes

            };
            if (ModelState.IsValid)
            {
                db.Entry(kit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            kvmodel.Branch = db.Branches.ToList();
            return View(kvmodel);
        }

            // GET: Kit/Delete/5
            public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kit kit = db.Kits.Find(id);
            if (kit == null)
            {
                return HttpNotFound();
            }
            var model = new KitViewModel
            {
                Kit = kit,
                Branch = db.Branches.ToList()
            };
            return View(model);
        }

        // POST: Kit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kit kit = db.Kits.Find(id);
            db.Kits.Remove(kit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
