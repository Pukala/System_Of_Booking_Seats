using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SystemOfBookingSeats_v3.Models
{
    public class MovieModelUI
    {
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        [DisplayName("Name")]
        public string NameOfMovie { get; set; }

        [Required(ErrorMessage = "Upload image for movie")]
        [DisplayName("Upload File")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}