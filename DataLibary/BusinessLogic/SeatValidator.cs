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

        public bool IsSeatValid(int numOfSeat)
        {
            return IsSeatFree(numOfSeat) && IsSeatNumberValid(numOfSeat);
        }

        private bool IsSeatFree(int numOfSeat)
        {
            if (numOfSeat <= Seats.Count())
            {
                return Seats[numOfSeat].PersonId == null;
            }
            return false;
        }

        private bool IsSeatNumberValid(int numOfSeat)
        {
            if (numOfSeat <= Seats.Count() && numOfSeat >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
