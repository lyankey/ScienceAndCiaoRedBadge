using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Models
{
    public class UserAgeRange : RangeAttribute
    {
        public UserAgeRange(string mininumValue) : base(typeof(DateTime), mininumValue, DateTime.Now.ToShortDateString())
        {

        }
    }
}
