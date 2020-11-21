using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndCiao.Models.Kit
{
    public class KitCreate
    {
        [Required]
        public int KitId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Grade Grade { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        public DateTime? DateAdded { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        public DateTime PublicationDate { get; set; }
        [Required]
        public int LengthInMinutes { get; set; }
    }
}
