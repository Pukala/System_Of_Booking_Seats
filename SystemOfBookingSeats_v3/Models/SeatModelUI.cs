using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SystemOfBookingSeats_v3.Models
{
    public class SeatModelUI
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Is Reserve")]
        public bool IsReserve { get; set; }

        [Display(Name = "Number Seat")]
        public int NumberSeat { get; set; }
    }
}