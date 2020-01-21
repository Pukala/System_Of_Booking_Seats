using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibary.DataAccess;
using DataLibary.Models;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data;

namespace DataLibary.BusinessLogic
{
    public static class DataProcessor
    {


        public static int FindPersonBySeatNumber(int seatNumber)
        {

            string sql = @"select * from dbo.SeatsTable where dbo.SeatsTable.NumberSeat = " + seatNumber + ";";

            return SqlDataAccess.LoadData<SeatModel>(sql).ElementAt(0).NumberSeat;
        }

        public static void UpdateReservation(
            string firstName, string lastName, string emailAddress, int seatNumber, int movieNumber)
        {
            InsertPersonModelElement(seatNumber, firstName, lastName, emailAddress);
            var lastPerson = GetLastPersonModel();

            string sql = @"UPDATE dbo.SeatsTable SET PersonId = " + lastPerson.Id +
            " WHERE NumberSeat = @seatNumber AND MovieId = " + movieNumber + "; ";
            SqlDataAccess.SaveData(sql, lastPerson);
        }


        public static List<SeatModel> LoadSeatsData(int movieNumber)
        {
            string sql = "select * from dbo.SeatsTable where dbo.SeatsTable.MovieId = " + movieNumber + ";";
            var data = SqlDataAccess.LoadData<SeatModel>(sql);

            if (data.Count() == 0)
            {
                return CreateAndFillDummySeatsData(movieNumber);
            }

            return data;
        }

        private static List<SeatModel> CreateAndFillDummySeatsData(int movieNumber)
        {
            List<SeatModel> dummyData = new List<SeatModel>();
            {
                SeatModel seatModel;
                for (int i = 0; i < 42; i++)
                {
                    seatModel = new SeatModel
                    {
                        MovieNumber = movieNumber,
                        NumberSeat = i + 1,
                        PersonId = null
                    };
                    dummyData.Add(seatModel);
                    InsertSeatModelElement(seatModel.NumberSeat,
                        seatModel.PersonId, seatModel.MovieNumber);
                }
            };
            return dummyData;
        }

        public static SeatModel LoadSeatData(int seatNumber)
        {
            string sql = @"select * from dbo.SeatsTable where dbo.SeatsTable.NumberSeat = " + seatNumber + ";";

            return SqlDataAccess.LoadData<SeatModel>(sql).ElementAt(0);
        }

        public static void UpdateSeatData(Nullable<int> personId, bool iReserve, int numberSeat)
        {
            SeatModel data = new SeatModel
            {
                PersonId = personId,
                NumberSeat = numberSeat
            };

            string sql = @"UPDATE dbo.SeatsTable SET PersonId = @PersonId, 
                                  NumberSeat = @NumberSeat WHERE NumberSeat = @NumberSeat;";

            SqlDataAccess.SaveData(sql, data);
        }

        public static PersonModel GetLastPersonModel()
        {
            string sql = "select top 1 * from dbo.Person order by Id desc;";
            return SqlDataAccess.LoadData<PersonModel>(sql).ToList().ElementAt(0);
        }

        public static void DeleteReservationData(int numberSeat)
        {
            string sql = "delete from dbo.SeatsTable where NumberSeat = " + numberSeat + ";";
            SqlDataAccess.SaveData(sql, new SeatModel());
            DeletePersonData(numberSeat);
        }

        public static void DeletePersonData(int numberSeat)
        {
            string sql = "delete from dbo.Person where SeatNumber = " + numberSeat + ";";
            SqlDataAccess.SaveData(sql, new PersonModel());
        }

        public static void InsertSeatModelElement(int numberSeat, Nullable<int> personId, int movieId)
        {
            SeatModel data = new SeatModel
            {
                PersonId = personId,
                NumberSeat = numberSeat,
                MovieNumber = movieId
            };
            string sql = @"insert into dbo.SeatsTable (PersonId, NumberSeat, MovieId) 
                           values(@PersonId, @NumberSeat, " + movieId + "); ";

            SqlDataAccess.SaveData(sql, data);
        }

        public static void InsertPersonModelElement(int seatNumber,
            string firstName = "none", string lastName = "none", string emailAdress = "None")
        {
            PersonModel data = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAdress = emailAdress,
                SeatNumber = seatNumber
            };
            string sql = @"insert into dbo.Person (FirstName, LastName, EmailAdress, SeatNumber) 
                           values(@FirstName, @LastName, @EmailAdress, @SeatNumber);";

            SqlDataAccess.SaveData(sql, data);
        }

        public static void InsertMovieData(MovieModel data)
        {
            string sql = @"insert into dbo.MovieDataTable (NameOfMovie, ImagePath, NumberOfMovie)
                        values(@NameOfMovie, @ImagePath, @NumberOfMovie);";

            SqlDataAccess.SaveData(sql, data);
        }

        public static List<MovieModel> LoadMoviesData()
        {
            string sql = "select * from dbo.MovieDataTable;";
            return SqlDataAccess.LoadData<MovieModel>(sql).ToList();
        }
    }
}
