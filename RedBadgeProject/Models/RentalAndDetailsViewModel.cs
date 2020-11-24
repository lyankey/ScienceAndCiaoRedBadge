using ScienceAndCiao.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedBadgeProject.Models
{
    public class RentalAndDetailsViewModel
    {
        //no requireds
        //user 
        public string UserId { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        [DisplayName("Date of Birth")]
        public DateTime BirthDate { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("First Name")]
        public string Name { get { return FirstName + " " + LastName; } }


        //kit that is getting rented

        public int KitId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Grade Grade { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public double Price { get; set; }

        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        public DateTime? DateAdded { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        public DateTime PublicationDate { get; set; }

        public int LengthInMinutes { get; set; }

        //rental info 

        [DisplayName("Your Price")]
        public double RentalPrice { get; set; }

        public double RentalPriceOneMonth { get; set; }
        public double RentalPriceSixMonth { get; set; }
        public int RentalId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        [DisplayName("Your rental began")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MMM dd yyy}")]
        [DisplayName("Your rental will end")]
        public DateTime? EndDate { get; set; }
        public string Duration { get; set; }
        public StatusEnum Status { get; set; }

    };
}