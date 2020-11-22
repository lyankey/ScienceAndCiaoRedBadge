using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedBadgeProject.Models
{
    public class KitViewModel
    {
        public IEnumerable<Branch> Branches { get; set; }
        public Kit Kit { get; set; }
    }
}