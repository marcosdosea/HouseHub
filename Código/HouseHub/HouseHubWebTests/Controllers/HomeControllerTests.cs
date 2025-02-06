using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseHubWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using HouseHubWeb.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _loggerMock;
        private HomeController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_loggerMock.Object);
        }

        [TestMethod()]
        public void HomeControllerTest()
        {
            Assert.IsNotNull(_controller);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("", redirectResult.ActionName);
            Assert.AreEqual("BuscarImovel", redirectResult.ControllerName);
        }

        [TestMethod()]
        public void PrivacyTest()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void ErrorTest()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.TraceIdentifier = "trace-id";
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            // Act
            var result = _controller.Error();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            var model = viewResult.Model as ErrorViewModel;
            Assert.IsNotNull(model);
            Assert.AreEqual("trace-id", model.RequestId);
        }
    }
}
