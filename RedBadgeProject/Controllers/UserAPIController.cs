using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RedBadgeProject.Controllers
{
    public class UserAPIController : ApiController
    {
        private ApplicationDbContext db;
        public void UsersAPIController()
        {
            db = ApplicationDbContext.Create();
        }

        //Get Email or (Name and birthdate) to autopopulate
        public IHttpActionResult Get(string type, string query = null)
        {
            //get based on email match
            if (type.Equals("email") && query != null)
            {
                var formCustomerQuery = db.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));

                return Ok(formCustomerQuery.ToList());
            }
            //get based on name match and return first name, last name and birthdate
            if (type.Equals("name") && query != null)
            {
                var formCustomerQuery = from u in db.Users
                                    where u.Email.Contains(query)
                                    select new { u.FirstName, u.LastName, u.BirthDate };
                return Ok(formCustomerQuery.ToList()[0].FirstName + " " + formCustomerQuery.ToList()[0].LastName + ";" + formCustomerQuery.ToList()[0].BirthDate);
            }
            return Ok();
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