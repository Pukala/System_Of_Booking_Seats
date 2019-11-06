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
        [HiddenInput(DisplayValue = false)]
        public int SeatId { get; set; }

        [Display(Name = "Person Id")]
        public int PersonId { get; set; }

        [Display(Name = "Is Reserve")]
        public bool IsReserve { get; set; }

        [Display(Name = "Number Seat")]
        public int NumberSeat { get; set; }
    }
}