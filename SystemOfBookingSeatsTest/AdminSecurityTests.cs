using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SystemOfBookingSeats_v3.Controllers;
using SystemOfBookingSeats_v3.Infrastructure;
using SystemOfBookingSeats_v3.Models;

namespace SystemOfBookingSeatsTest
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "password")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "password"
            };

            AccountController target = new AccountController(mock.Object);

            ActionResult result = target.Login(model, "/MyURL");

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }
    }
}
