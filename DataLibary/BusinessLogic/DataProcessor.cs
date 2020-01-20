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
            string firstName, string lastName, string emailAddress, int seatNumber)
        {
            PersonModel data = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAdress = emailAddress,
                SeatNumber = seatNumber
            };

            string sql = @"UPDATE dbo.Person SET FirstName = @FirstName, LastName = @LastName, 
                                EmailAdress = '" + emailAddress + "', SeatNumber = @SeatNumber WHERE @SeatNumber = " + seatNumber + ";";
            SqlDataAccess.SaveData(sql, data);


            sql = @"UPDATE dbo.SeatsTable SET IsReserve = " + 1 +
            " WHERE NumberSeat = @seatNumber;";
            SqlDataAccess.SaveData(sql, data);
        }


        public static List<SeatModel> LoadSeatsData(int movieNumber)
        {
            string sql = "select * from dbo.SeatsTable where dbo.SeatsTable.MovieId = " + movieNumber + ";";
            var data = SqlDataAccess.LoadData<SeatModel>(sql);

            if (data.Count() == 0)
            {
                return CreateDummySeatsData(movieNumber);
            }

            return data;
        }

        private static void FillSeatsTableWithDummyData(List<SeatModel> seatModels)
        {
            string sql;

            foreach (SeatModel model in seatModels)
            {
                sql = @"INSERT INTO dbo.SeatsTable (PersonId, IsReserve, NumberSeat, MovieId);";

                SqlDataAccess.SaveData(sql, model);
            }
        }

        private static List<SeatModel> CreateDummySeatsData(int movieNumber)
        {
            List<SeatModel> dummyData = new List<SeatModel>();
            {
                SeatModel seatModel;
                for (int i = 0; i < 42; i++)
                {
                    seatModel = new SeatModel
                    {
                        IsReserve = false,
                        MovieId = movieNumber,
                        NumberSeat = i + 1,
                        PersonId = null
                    };
                    dummyData.Add(seatModel);
                }
            };
            return dummyData;
        }

        public static SeatModel LoadSeatData(int seatNumber)
        {
            string sql = @"select * from dbo.SeatsTable where dbo.SeatsTable.NumberSeat = " + seatNumber + ";";

            return SqlDataAccess.LoadData<SeatModel>(sql).ElementAt(0);
        }

        public static void UpdateSeatData(int personId, bool iReserve, int numberSeat)
        {
            SeatModel data = new SeatModel
            {
                PersonId = personId,
                IsReserve = iReserve,
                NumberSeat = numberSeat
            };

            string sql = @"UPDATE dbo.SeatsTable SET PersonId = @PersonId, 
                                 IsReserve = @IsReserve, NumberSeat = @NumberSeat WHERE NumberSeat = @NumberSeat;";

            SqlDataAccess.SaveData(sql, data);
        }

        public static void InsertSeatAndPerson(SeatModel seatModel)
        {
            InsertPersonModelElement(seatModel.NumberSeat);
            InsertSeatModelElement(
                seatModel.NumberSeat, seatModel.IsReserve, GetLastPersonModel().Id, seatModel.MovieId);
        }

        public static SeatModel GetLastSeatModel()
        {
            string sql = "select top 1 * from dbo.SeatsTable order by NumberSeat desc;";
            return SqlDataAccess.LoadData<SeatModel>(sql).ToList().ElementAt(0);
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
            sql = "delete from dbo.Person where SeatNumber = " + numberSeat + ";";
            SqlDataAccess.SaveData(sql, new PersonModel());
        }

        public static void InsertSeatModelElement(int numberSeat, bool isReserve, int personId, int movieId)
        {
            SeatModel data = new SeatModel
            {
                PersonId = personId,
                IsReserve = isReserve,
                NumberSeat = numberSeat,
                MovieId = movieId
            };
            string sql = @"insert into dbo.SeatsTable (PersonId, IsReserve, NumberSeat, MovieId) 
                           values(@PersonId, @IsReserve, @NumberSeat, @MovieId);";

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
            string sql = @"insert into dbo.MovieDataTable (NameOfMovie, ImagePath)
                        values(@NameOfMovie, @ImagePath);";

            SqlDataAccess.SaveData(sql, data);
        }

        public static List<MovieModel> LoadMoviesData()
        {
            string sql = "select * from dbo.MovieDataTable;";
            return SqlDataAccess.LoadData<MovieModel>(sql).ToList();
        }
    }
}
