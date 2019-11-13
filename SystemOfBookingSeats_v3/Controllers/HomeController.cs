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
        private SeatValidator seatValidator;

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

        public ActionResult SeatsDataMovie1(int id)
        {
            var seatsData = DataProcessor.LoadSeatsData(id);
            seatValidator = new SeatValidator(seatsData);

            return View(seatsData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeatsDataMovie1(PersonModelUI model)
        {
            if (ModelState.IsValid)
            {
                var seatsData = DataProcessor.LoadSeatsData(1); // to do
                seatValidator = new SeatValidator(seatsData);
                if (seatValidator.IsSeatValid(model.SeatNumber))
                {
                    int personId = DataProcessor.FindPersonIdBySeatNumber(model.SeatNumber);

                    DataProcessor.UpdateReservation(personId, model.FirstName, model.LastName, model.EmailAdress, model.SeatNumber);

                    return RedirectToAction("SeatsDataMovie1");
                }
            }
            return RedirectToAction("ErrorSeats");
        }
    }
}