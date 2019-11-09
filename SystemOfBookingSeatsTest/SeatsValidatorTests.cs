using System;
using DataLibary.BusinessLogic;
using DataLibary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Moq;

namespace SystemOfBookingSeatsTest
{
    [TestClass]
    public class SeatsValidatorTests
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

            Assert.IsTrue(validator.IsSeatValid(1) == true);
            Assert.IsTrue(validator.IsSeatValid(2) == true);
            Assert.IsTrue(validator.IsSeatValid(3) == false);
            Assert.IsTrue(validator.IsSeatValid(4) == true);
            Assert.IsTrue(validator.IsSeatValid(5) == false);
            Assert.IsTrue(validator.IsSeatValid(22) == false);
        }
    }
}
