using DataLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibary.BusinessLogic
{
    public class SeatValidator
    {
        private List<SeatModel> Seats;

        public SeatValidator(List<SeatModel> seats)
        {
            Seats = seats;
        }

        public bool IsSeatFree(int numOfSeat)
        {
            if (numOfSeat <= Seats.Count())
            {
                return Seats[numOfSeat - 1].IsReserve;
            }
            return false;
        }
    }
}
