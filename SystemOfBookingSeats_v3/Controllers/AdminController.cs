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
        private static int MoId;
        private static List<SeatModel> SeatsData;
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
            SeatsData = DataProcessor.LoadSeatsData(id);
            //var dupa = DataProcessor.LoadSeatsData(id);
            MoId = id;
            return View(SeatsData);
        }

        public ViewResult Edit(int id)
        {
            SeatModel data = SeatsData.Find(m => m.NumberSeat == id);

            SeatModelUI seatData = new SeatModelUI
            {
                PersonId = data.PersonId ?? default(int),
                NumberSeat = data.NumberSeat,
                IsReserve = data.PersonId != 0
            };

            return View(seatData);
        }

        [HttpPost]
        public ActionResult Edit(SeatModelUI seatModelUI)
        {
            if (ModelState.IsValid)
            {
                if (seatModelUI.IsReserve)
                    DataProcessor.UpdateSeatData(seatModelUI.PersonId, seatModelUI.IsReserve
                        , seatModelUI.NumberSeat);
                else
                {
                    DataProcessor.DeletePersonData(seatModelUI.NumberSeat);
                    DataProcessor.UpdateSeatData(0, seatModelUI.IsReserve
                        , seatModelUI.NumberSeat);
                }
                return RedirectToAction("Manage_Seats");
            }
            else
            {
                return View(seatModelUI);
            }
        }

        public ActionResult Delete(int id)
        {
            DataProcessor.DeleteReservationData(id);
            return RedirectToAction("Manage_Seats");
        }

        public ViewResult CreateSeat()
        {
            SeatModelUI seatData = new SeatModelUI
            { NumberSeat = SeatsData.Count() + 1 };

            return View(seatData);
        }

        [HttpPost]
        public ActionResult CreateSeat(SeatModelUI seatModelUI)
        {
            if (ModelState.IsValid)
            {
                SeatModel seatModel = new SeatModel
                {
                    PersonId = null,
                    MovieId = MoId,
                    NumberSeat = seatModelUI.NumberSeat
                };
                DataProcessor.InsertSeatModelElement(seatModel.NumberSeat,
      default(int), seatModel.MovieId);

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