using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibary.Models
{
    public class SeatModel
    {

        public int SeatId { get; set; }
        public int PersonId { get; set; }
        public bool IsReserve { get; set; }
        public int NumberSeat { get; set; }
    }
}
