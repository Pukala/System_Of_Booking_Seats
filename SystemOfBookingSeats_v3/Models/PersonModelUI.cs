using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemOfBookingSeats_v3.Models
{
    public class PersonModelUI
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to give us your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string LastName { get; set; }

        [Display(Name = "Email Adress")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string EmailAdress { get; set; }

        [Display(Name = "Email Adress")]
        [Required(ErrorMessage = "The Email and Confirm Email must match.")]
        public string ConfirmEmailAdress { get; set; }

        [Display(Name = "Seat Number")]
        [Required(ErrorMessage = "You need to give us seat number.")]
        public int SeatNumber { get; set; }
    }
}