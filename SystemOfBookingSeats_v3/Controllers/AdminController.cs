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
        private static int NumberMovie;
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
            NumberMovie = id;
            return View(SeatsData);
        }

        public ViewResult Edit(int id)
        {
            SeatModel data = SeatsData.Find(m => m.NumberSeat == id);

            SeatModelUI seatData = new SeatModelUI
            {
                NumberSeat = data.NumberSeat,
                IsReserve = data.PersonId != null
            };

            return View(seatData);
        }

        [HttpPost]
        public ActionResult Edit(SeatModelUI seatModelUI)
        {
            if (ModelState.IsValid)
            {
                if (seatModelUI.IsReserve)
                    DataProcessor.UpdateSeatData(seatModelUI.FirstName, seatModelUI.LastName, seatModelUI.IsReserve
                        , seatModelUI.NumberSeat);
                else
                {
                    DataProcessor.UpdateSeatData("", "", seatModelUI.IsReserve
                        , seatModelUI.NumberSeat);
                }
                return RedirectToAction("Manage_Seats");
            }
            else
            {
                return View(seatModelUI);
            }
        }

        public ViewResult Details(int id)
        {
            SeatModel seatdata = SeatsData.Find(m => m.NumberSeat == id);
            PersonModel personModel = new PersonModel();

            if (seatdata.PersonId != null)
            {
                personModel = DataProcessor.FindPerson(seatdata.PersonId);
            }
            personModel = DataProcessor.FindPerson(seatdata.PersonId);

            SeatModelUI seatData = new SeatModelUI
            {
                NumberSeat = seatdata.NumberSeat,
                IsReserve = seatdata.PersonId != null,
                FirstName = personModel.FirstName,
                LastName = personModel.LastName
            };

            return View(seatData);
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
                    NumberOfMovie = NumberMovie,
                    NumberSeat = seatModelUI.NumberSeat
                };
                DataProcessor.InsertSeatModelElement(seatModel.NumberSeat,
      default(int), NumberMovie);

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
                ImagePath = "~/Content/MoviesImages/" + file.FileName,
            };

            DataProcessor.InsertMovieData(data);

            return View();
        }

        public ViewResult Movies()
        {
            var movieData = DataProcessor.LoadMoviesData();
            return View(movieData);
        }
    }
}