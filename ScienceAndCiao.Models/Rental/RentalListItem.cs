using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ScienceAndCiao.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceAndCiao.Models.Rental
{
    public class RentalListItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KitId { get; set; }


        [Required]
        public double RentalPrice { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
