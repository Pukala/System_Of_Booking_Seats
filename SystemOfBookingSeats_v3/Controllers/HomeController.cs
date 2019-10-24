using DataLibary.BusinessLogic;
using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SystemOfBookingSeats_v3.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Start()
        {

            return View();
        }

        public ActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(PersonModel model)
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