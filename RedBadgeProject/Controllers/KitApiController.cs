using ScienceAndCiao.Data;
using ScienceAndCiao.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedBadgeProject.Controllers
{
    public class KitApiController : ApiController
    {
        private ApplicationDbContext db;
        public KitApiController()
        {
            db = ApplicationDbContext.Create();
        }

        //Type could be price or avail
        public IHttpActionResult Get(string query=null)
        {

            var kitQuery = db.Kits.Where(k => k.Title.ToLower().Contains(query.ToLower()));
            return Ok(kitQuery.ToList());
        }

        //price
        //find out parameters we want, then something unique about the product
        public IHttpActionResult Get(string type, string title = null, string rentalDuration = null, string email = null )
        {

            if (type.Equals("price"))
            {
                Kit KitQuery = db.Kits.Where(b => b.Title.Equals(title)).SingleOrDefault();
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 select new { m.MonthlyMembershipFee, m.SixMonthMemberShipFee };

                var price = Convert.ToDouble(KitQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].MonthlyMembershipFee) / 100;

                if (rentalDuration == StaticDetails.SixMonthCount)
                {
                    price = Convert.ToDouble(KitQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].SixMonthMemberShipFee) / 100;
                }
                return Ok(price);
            }
            else
            {
                Kit KitQuery = db.Kits.Where(b => b.Title.Equals(title)).SingleOrDefault();
                return Ok(title);
            }
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
