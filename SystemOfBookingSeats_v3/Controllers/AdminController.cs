using DataLibary.BusinessLogic;
using DataLibary.DataAccess;
using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemOfBookingSeats_v3.Models;

namespace SystemOfBookingSeats_v3.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Manage_Seats()
        {
            var seatsData = DataProcessor.LoadSeatsData();
            return View(seatsData);
        }

        public ViewResult Edit(int id)
        {
            SeatModel data = DataProcessor.LoadSeatData(id);

            SeatModelUI seatData = new SeatModelUI
            {
                SeatId = data.SeatId,
                PersonId = data.PersonId,
                IsReserve = data.IsReserve,
                NumberSeat = data.NumberSeat
            };

            return View(seatData);
        }

        public ActionResult Delete(int id)
        {
            DataProcessor.DeleteReservationData(id);
            return RedirectToAction("Manage_Seats");
        }

        [HttpPost]
        public ActionResult Edit(SeatModelUI seatModelUI)
        {
            if (ModelState.IsValid)
            {
                DataProcessor.UpdateSeatData(seatModelUI.SeatId, seatModelUI.PersonId, seatModelUI.IsReserve
                    , seatModelUI.NumberSeat);
                return RedirectToAction("Manage_Seats");
            }
            else
            {
                return View(seatModelUI);
            }
        }

        public ViewResult CreateSeat()
        {
            SeatModelUI seatData = new SeatModelUI
            { NumberSeat = DataProcessor.GetLastSeatModel().NumberSeat + 1 };

            return View(seatData);
        }

        [HttpPost]
        public ActionResult CreateSeat(SeatModelUI seatModelUI)
        {
            if (ModelState.IsValid)
            {

                DataProcessor.InsertPersonModelElement(seatModelUI.NumberSeat);
                DataProcessor.InsertSeatModelElement(
                    seatModelUI.NumberSeat, seatModelUI.IsReserve, DataProcessor.GetLastPersonModel().Id);


                return RedirectToAction("Manage_Seats");
            }
            else
            {
                return View(seatModelUI);
            }
        }
    }
}