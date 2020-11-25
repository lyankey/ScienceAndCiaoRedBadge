using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Data
{
   
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int KitId { get; set; }
        [ForeignKey(nameof(KitId))]
        public virtual Kit Kit { get; set; }
     
        public string UserId { get; set; }

        [Required]
        public double RentalPrice { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public string Duration { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
        public StatusEnum Status { get; set; }
        public enum StatusEnum
        {
            Rented,
            Closed
        }
    }
}

