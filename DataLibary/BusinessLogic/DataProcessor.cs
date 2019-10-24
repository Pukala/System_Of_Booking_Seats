using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibary.DataAccess;
using DataLibary.Models;

namespace DataLibary.BusinessLogic
{
    public static class DataProcessor
    {
        public static int FindPersonIdBySeatNumber(int seatNumber)
        {

            string sql = @"select * from dbo.SeatsTable where dbo.SeatsTable.NumberSeat = " + seatNumber + ";";

            return SqlDataAccess.LoadOneElementById(sql).ElementAt(0).PersonId;
        }

        public static void UpdateReservation(int id,
            string firstName, string lastName, string emailAddress, int seatNumber)
        {
            PersonModel data = new PersonModel
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                EmailAdress = emailAddress,
                SeatNumber = seatNumber
            };

            string sql = @"UPDATE dbo.Person SET FirstName = @FirstName, LastName = @LastName, 
                                EmailAdress = '" + emailAddress + "', SeatNumber = @SeatNumber WHERE Id = @Id;";
            SqlDataAccess.SaveData(sql, data);


            sql = @"UPDATE dbo.SeatsTable SET IsReserve = " + 1 +
            " WHERE NumberSeat = @seatNumber;";
            SqlDataAccess.SaveData(sql, data);
        }

    }
}
