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
        public static void PrepareReservation(
            string firstName, string lastName, string emailAddress, int seatNumber, int movieNumber)
        {
            InsertPersonModelElement(firstName, lastName, emailAddress);
            var lastPerson = GetLastPersonModel();
            int movieId = FindMovie(movieNumber);

            string sql = @"UPDATE dbo.SeatsTable SET PersonId = " + lastPerson.Id +
            " WHERE NumberSeat = " + seatNumber + " AND IdMovie = " + movieId + "; ";
            SqlDataAccess.SaveData(sql, lastPerson);
        }


        public static List<SeatModel> LoadSeatsData(int movieNumber)
        {
            string sql = "select * from dbo.SeatsTable where dbo.SeatsTable.MovieNumber = " + movieNumber + ";";
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

        private static int FindMovie(int number)
        {
            string sql = "select * from dbo.MovieDataTable where dbo.MovieDataTable.NumberOfMovie = " + number + ";";
            return SqlDataAccess.LoadData<MovieModel>(sql)[0].Movieid;
        }

        public static void UpdateSeatData(string firstName, string lastName, bool iReserve, int numberSeat, int movieNumber)
        {
            var person = FindPerson(firstName, lastName);
            string sql;
            if (person.Count() == 0)
            {
                PrepareReservation(firstName, lastName, "none", numberSeat, movieNumber);
                var lastPerson = GetLastPersonModel();
                sql = "UPDATE dbo.SeatsTable SET PersonId = " + lastPerson.Id + " WHERE NumberSeat = " + numberSeat + "; ";
            }
            else
            {
                SeatModel data = new SeatModel
                {
                    PersonId = person[0].Id,
                    NumberSeat = numberSeat
                };

                sql = @"UPDATE dbo.SeatsTable SET PersonId = @PersonId WHERE NumberSeat = @NumberSeat;";

                SqlDataAccess.SaveData(sql, data);
            }


        }

        public static PersonModel GetLastPersonModel()
        {
            string sql = "select top 1 * from dbo.Person order by Id desc;";
            return SqlDataAccess.LoadData<PersonModel>(sql).ToList().ElementAt(0);
        }

        public static List<PersonModel> FindPerson(string firstName, string lastName)
        {
            string sql = @"select * from dbo.Person where FirstName = '" + firstName +
                "' AND LastName = '" + lastName + "';";

            return SqlDataAccess.LoadData<PersonModel>(sql).ToList();
        }

        public static PersonModel FindPerson(Nullable<int> id)
        {
            int personId = id ?? default(int);
            string sql = @"select * from dbo.Person where dbo.Person.Id = " + personId + ";";

            return SqlDataAccess.LoadData<PersonModel>(sql).ToList()[0];
        }

        public static void DeleteReservationData(SeatModel seatModel)
        {
            DeletePersonData(seatModel.PersonId);
            string sql = @"UPDATE dbo.SeatsTable SET PersonId = null WHERE PersonId = " + seatModel.PersonId + "; ";
            SqlDataAccess.SaveData(sql, new SeatModel());
        }
        public static void DeletePersonData(Nullable<int> id)
        {
            if (id != null)
            {
                string sql = "delete from dbo.Person where Id = " + id + ";";
                SqlDataAccess.SaveData(sql, new PersonModel());
            }
        }

        public static void InsertSeatModelElement(int numberSeat, Nullable<int> personId, int numberOfMovie)
        {
            int movieId = FindMovie(numberOfMovie);
            SeatModel data = new SeatModel
            {
                PersonId = personId,
                NumberSeat = numberSeat,
                MovieNumber = numberOfMovie
            };
            string sql = @"insert into dbo.SeatsTable (PersonId, NumberSeat, MovieNumber, IdMovie) 
                           values(@PersonId, @NumberSeat, " + numberOfMovie + "," + movieId + "); ";

            SqlDataAccess.SaveData(sql, data);
        }

        public static void InsertPersonModelElement(string firstName = "none", string lastName = "none", string emailAdress = "None")
        {
            PersonModel data = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAdress = emailAdress,
            };
            string sql = @"insert into dbo.Person (FirstName, LastName, EmailAdress) 
                           values(@FirstName, @LastName, @EmailAdress);";

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
