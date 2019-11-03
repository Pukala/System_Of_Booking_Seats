using System;
using DataLibary.BusinessLogic;
using DataLibary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SystemOfBookingSeatsTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsFreeSeatTest()
        {
            List<SeatModel> Seats = new List<SeatModel>()
            {
                new SeatModel{ IsReserve=false},
                new SeatModel{ IsReserve=false},
                new SeatModel{ IsReserve=true},
                new SeatModel{ IsReserve=false},
                new SeatModel{ IsReserve=true}
            };

            SeatValidator validator = new SeatValidator(Seats);

            Assert.IsTrue(validator.IsSeatFree(1) == false);
            Assert.IsTrue(validator.IsSeatFree(2) == false);
            Assert.IsTrue(validator.IsSeatFree(3) == true);
            Assert.IsTrue(validator.IsSeatFree(4) == false);
            Assert.IsTrue(validator.IsSeatFree(5) == true);
        }
    }
}
