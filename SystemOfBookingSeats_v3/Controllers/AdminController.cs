using DataLibary.BusinessLogic;
using DataLibary.DataAccess;
using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemOfBookingSeats_v3.Models;

namespace SystemOfBookingSeats_v3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Start()
        {
            return View();
        }

        public ActionResult Manage_Seats()
        {
            return View();
        }

        public ActionResult EditSeats(int id)
        {
            var seatsData = DataProcessor.LoadSeatsData(id);
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

        public ViewResult CreateMovieData()
        {
            MovieModelUI movie = new MovieModelUI();
            return View(movie);
        }

        [HttpPost]
        public ViewResult CreateMovieData(MovieModelUI movieModel, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = Path.GetFileName(movieModel.File.FileName);
                string imgPath = Path.Combine(Server.MapPath("~/Content/MoviesImages/"), fileName);
                file.SaveAs(imgPath);
            }

            MovieModel data = new MovieModel
            {
                NameOfMovie = movieModel.NameOfMovie,
                ImagePath = "~/Content/MoviesImages/" + file.FileName
            };

            DataProcessor.InsertMovieData(data);

            return View();
        }
    }
}