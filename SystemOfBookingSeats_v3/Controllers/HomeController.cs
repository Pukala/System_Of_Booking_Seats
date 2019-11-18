using DataLibary.BusinessLogic;
using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemOfBookingSeats_v3.Models;

namespace SystemOfBookingSeats_v3.Controllers
{
    public class HomeController : Controller
    {
        private static int MovieId { get; set; }
        private static SeatValidator seatValidator;

        public ViewResult Movies()
        {
            var movieData = DataProcessor.LoadMoviesData();
            return View(movieData);
        }

        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }

        public ActionResult ErrorSeats()
        {
            return View();
        }

        public ActionResult SeatsDataMovie(int id)
        {
            var seatsData = DataProcessor.LoadSeatsData(id);
            seatValidator = new SeatValidator(seatsData);
            MovieId = id;

            return View(seatsData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeatsDataMovie(PersonModelUI model)
        {
            if (ModelState.IsValid)
            {
                if (seatValidator.IsSeatValid(model.SeatNumber))
                {
                    DataProcessor.UpdateReservation(model.FirstName, model.LastName, model.EmailAdress, model.SeatNumber);

                    return RedirectToAction("SeatsDataMovie");
                }
            }
            return RedirectToAction("ErrorSeats");
        }

    }
}