using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ScienceAndCiao.Data;
using Microsoft.AspNet.Identity;
using ScienceAndCiao.Models.User;
using RedBadgeProject.Models;

namespace RedBadgeProject.Controllers
{
    public class KitDetailController : Controller
    {
        private ApplicationDbContext db;

        public KitDetailController()
        {
            db = ApplicationDbContext.Create();
        }
        // GET: BookDetail
        public ActionResult Index(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(u => u.Id == userid);
            //joins kits and branches automatically based on the primary key, then get all details from the kit model

            var kitModel = db.Kits.Include(k => k.Branch).SingleOrDefault(k => k.KitId == id);

            var rentalPrice = 0.0;
            var oneMonthRental = 0.0;
            var sixMonthRental = 0.0;
            var rentalCount = 0;
            //get user details and then calculate the rental price   and also not an admin  -- if they are not an admin and have logged in, they will see....
            if (userid != null && !User.IsInRole(StaticDetails.AdminUserRole))
            {
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userid)
                                 select new { m.MonthlyMembershipFee, m.SixMonthMemberShipFee };
                //get price of kit, multiply by discount percentage, 0 record is for one month (first entry), 1 record is for 6 month

                oneMonthRental = Convert.ToDouble(kitModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].MonthlyMembershipFee) / 100;
                sixMonthRental = Convert.ToDouble(kitModel.Price) * Convert.ToDouble(chargeRate.ToList()[0].SixMonthMemberShipFee) / 100;
                //rentalCount = Convert.ToInt32(chargeRate.ToList()[0].RentalCount);
            }

            RentalAndDetailsViewModel model = new RentalAndDetailsViewModel
            {
                Title = kitModel.Title,
                KitId = kitModel.KitId,
                DateAdded = kitModel.DateAdded,
                Description = kitModel.Description,
                Grade = kitModel.Grade,
                Branch = db.Branches.FirstOrDefault(g => g.BranchId.Equals(kitModel.BranchId)),
                BranchId = kitModel.BranchId,
                ImageUrl = kitModel.ImageUrl,
                Price = kitModel.Price,
                LengthInMinutes = kitModel.LengthInMinutes,
                UserId = userid,
                RentalPrice = rentalPrice,
                RentalPriceOneMonth = oneMonthRental,
                RentalPriceSixMonth = sixMonthRental,
                //RentalCount = rentalCount

            };

            return View(model);
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