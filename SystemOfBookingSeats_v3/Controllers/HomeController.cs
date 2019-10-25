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

        public ViewResult Repertoire()
        {
            return View();
        }

        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }

        public ActionResult SeatsDataMovie1()
        {
            var seatsData = DataProcessor.LoadSeatsData();
            return View(seatsData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SeatsDataMovie1(PersonModelUI model)
        {
            if (ModelState.IsValid)
            {
                int personId = DataProcessor.FindPersonIdBySeatNumber(model.SeatNumber);

                DataProcessor.UpdateReservation(personId, model.FirstName, model.LastName, model.EmailAdress, model.SeatNumber);

                return RedirectToAction("Start");
            }

            return View();
        }
    }
}