using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Data
{
    public class MembershipType
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public byte SignUpFee { get; set; }
        [Required]
        [DisplayName("Monthly Rate")]
        public byte MonthlyMembershipFee { get; set; }
        [Required]
        [DisplayName("Six Month Rate")]
        public byte SixMonthMemberShipFee { get; set; }
    }
}
