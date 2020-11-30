using Microsoft.AspNet.Identity;
using RedBadgeProject.Models;
using ScienceAndCiao.Data;
using ScienceAndCiao.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace RedBadgeProject.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        private ApplicationDbContext db;

        public RentController()
        {
            db = ApplicationDbContext.Create();
        }

        //Get Method
        public ActionResult Create(string title = null)
        {
            if (title != null)
            {
                RentalAndDetailsViewModel model = new RentalAndDetailsViewModel
                {
                    Title = title,
               
                };
                return View(model);
            }
            return View(new RentalAndDetailsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RentalAndDetailsViewModel rental)
        {
            if (ModelState.IsValid)
            {
                var email = rental.Email;

                var userDetails = from u in db.Users
                                  where u.Email.Equals(email)
                                  select new { u.Id, u.FirstName, u.LastName, u.BirthDate };

                var title = rental.Title;

                Kit kitSelected = db.Kits.Where(b => b.Title == title).FirstOrDefault();

                var rentalDuration = rental.Duration;

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes
                                 on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 select new { m.MonthlyMembershipFee, m.SixMonthMemberShipFee };

                var monthlyMembershipFee = Convert.ToDouble(kitSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].MonthlyMembershipFee) / 100;
                var sixMonthMemberShipFee = Convert.ToDouble(kitSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].SixMonthMemberShipFee) / 100;

                double rentalPrice = 0;

                if (rental.Duration == StaticDetails.SixMonthCount)
                {
                    rentalPrice = sixMonthMemberShipFee;
                }
                else
                {
                    rentalPrice = monthlyMembershipFee;
                }
                var registeredUser = db.Users.SingleOrDefault(u => u.Email == email);  
                Rental modelToAddToDb = new Rental
                {
                    KitId = kitSelected.KitId,
                    RentalPrice = rentalPrice,
                    EndDate = rental.EndDate,
                    Duration = DateTime.Now.AddMonths(1),
                    Status = Rental.StatusEnum.Rented,
                    UserId = userDetails.ToList()[0].Id
                };

                db.Rentals.Add(modelToAddToDb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Rent
        //get the rental id, the kit id, the user id - join them, pass to a new rental view
        //joining all the tables, selecting a rental view object, return that object to the view (after filtering by user) 
        //the view has to be an IEnumerable of the model because we are converting the the list and returning that
        //the pageNumber is what we are passing to the Index view
        //To add being able to search the rentals - in the " string option = null, string search=null", back in the Index view of rent, the radiobuttons have an "option" to show what they selected, and the Html.Edtior has what is in "search" - basically whatever they write in the editor will show in the search. Then filter that using those options at the bottom of this.
        public ActionResult Index(int? pageNumber, string option = null, string search=null)
        {
            string userid = User.Identity.GetUserId();

            var model = from r in db.Rentals
                        join k in db.Kits on r.KitId equals k.KitId
                        join u in db.Users on r.UserId equals u.Id
                        select new RentalAndDetailsViewModel
                        {
                            RentalId = r.Id,
                            KitId = k.KitId,
                            RentalPrice = r.RentalPrice,
                            Price = k.Price,
                            LengthInMinutes = k.LengthInMinutes,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            BirthDate = u.BirthDate,
                            EndDate = r.EndDate,
                            Grade = k.Grade,
                            DateAdded = k.DateAdded,
                            Description = k.Description,
                            Email = u.Email,
                            StartDate = r.StartDate,
                            BranchId = k.BranchId,
                            Branch = db.Branches.Where(b => b.BranchId.Equals(b.BranchId)).FirstOrDefault(),
                            ImageUrl = k.ImageUrl,
                            PublicationDate = k.PublicationDate,
                            Title = k.Title,
                            UserId = u.Id

                        };

            //This is to filter the results passed in - only admin should be able to see all the rentals. users!admin should see only their rentals
            if (option == "email" && search.Length > 0)
            {
                model = model.Where(u => u.Email.Contains(search));
            }
            if (option == "name" && search.Length > 0)
            {
                model = model.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search));
            }
            if (!User.IsInRole(StaticDetails.AdminUserRole))
            {
                model = model.Where(u => u.UserId.Equals(userid));
            }
            //means for each 1 page, display 5 rows
            return View(model.ToList().ToPagedList(pageNumber?? 1,5));
        }







        [HttpPost]
        public ActionResult Rent(RentalAndDetailsViewModel kit)
        {
            var userid = User.Identity.GetUserId();
            Kit kitToRent = db.Kits.Find(kit.KitId);
            double rentalPrice = 0;

            if (userid != null)
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes
                                 on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userid)
                                 select new { m.MonthlyMembershipFee, m.SixMonthMemberShipFee };
                if (kit.Duration == StaticDetails.SixMonthCount)
                {
                    rentalPrice = Convert.ToDouble(kitToRent.Price) * Convert.ToDouble(chargeRate.ToList()[0].SixMonthMemberShipFee) / 100;
                }
                else
                {
                    rentalPrice = Convert.ToDouble(kitToRent.Price) * Convert.ToDouble(chargeRate.ToList()[0].MonthlyMembershipFee) / 100;
                }
                var userInDb = db.Users.SingleOrDefault(c => c.Id == userid);

                Rental rental = new Rental
                {
                    KitId = kitToRent.KitId,
                    UserId = userid,
                    RentalPrice = rentalPrice,
                    Status = Rental.StatusEnum.Rented,
                    StartDate = DateTime.Now,
                    Duration = DateTime.Now.AddMonths(1),

                };

                db.Rentals.Add(rental);
                var kitInDb = db.Kits.SingleOrDefault(c => c.KitId == kit.KitId);

                db.SaveChanges();
                return RedirectToAction("Index", "Rent");
            }
            return View();
        }

        //it is getting the rental Id
        //if the id is null, bad request, otherwise search the db for the rental id, then conver that into a rental view model. Need to join all the tables  in a separate function (named getVMFromRental).
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);

            var model = getVMFromRental(rental);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }



        //Return Get Method
        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rental rental = db.Rentals.Find(id);

            var model = getVMFromRental(rental);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Return",model);
        }


        //after it is returned I want to save it as closed in the list of rentals. I don't have a view for this. Not sure if I need one. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Closed (RentalAndDetailsViewModel model)
        {
            if (model.RentalId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                Rental rental = db.Rentals.Find(model.RentalId);
                rental.Status = Rental.StatusEnum.Closed;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(RentalAndDetailsViewModel model)
        {
            if (model.KitId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                Rental rental = db.Rentals.Find(model.RentalId);
                Kit kitInDb = db.Kits.Find(rental.KitId);

                var registeredUser = db.Users.Single(u => u.Id == rental.UserId);
 
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //Delete GET Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Rental rental = db.Rentals.Find(id);

            var model = getVMFromRental(rental);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                Rental rental = db.Rentals.Find(Id);
                db.Rentals.Remove(rental);

                var kitInDb = db.Kits.Where(b => b.KitId.Equals(rental.KitId)).FirstOrDefault();
                var registeredUser = db.Users.SingleOrDefault(c => c.Id == rental.UserId);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //get one record from the db based on Id. Then join user ingo to the rental info. Select new details I want from user model.
        //any details where I need to retrieve from db needs to have db.modelname.FirstOrDefault(lambda stuff)\
        //this converts a rental object into a rentalviewmodel object - go to details action method to consume that
        private RentalAndDetailsViewModel getVMFromRental(Rental rental)
        {
            Kit kitSelected = db.Kits.Where(b => b.KitId == rental.KitId).FirstOrDefault();
            var userDetails = from u in db.Users
                              where u.Id.Equals(rental.UserId)
                              select new { u.Id, u.FirstName, u.LastName, u.BirthDate, u.Email };

            RentalAndDetailsViewModel model = new RentalAndDetailsViewModel
            {
                RentalId = rental.Id,
                KitId = kitSelected.KitId,
                RentalPrice = rental.RentalPrice,
                Price = kitSelected.Price,
                Grade = kitSelected.Grade,
                LengthInMinutes = kitSelected.LengthInMinutes,
                FirstName = userDetails.ToList()[0].FirstName,
                LastName = userDetails.ToList()[0].LastName,
                BirthDate = userDetails.ToList()[0].BirthDate,
                EndDate = rental.EndDate,
                StartDate = rental.StartDate,
                DateAdded = kitSelected.DateAdded,
                Description = kitSelected.Description,
                Email = userDetails.ToList()[0].Email,
                BranchId = kitSelected.BranchId,
                Branch = db.Branches.FirstOrDefault(b => b.BranchId.Equals(kitSelected.BranchId)),
                ImageUrl = kitSelected.ImageUrl,
                PublicationDate = kitSelected.PublicationDate,
                //needs the rented kit object - need a string object in my RentalAndDetailsViewModel, not an Enum
                Status = rental.Status.ToString(),
                Title = kitSelected.Title,
                UserId = userDetails.ToList()[0].Id
            };

            return model;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}