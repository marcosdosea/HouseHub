using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseHubWeb.Controllers;
using HouseHubWeb.Models;
using Core.Service;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.DTOs;
using Core;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class LocarImovelControllerTests
    {
        private Mock<IImovelService> _imovelServiceMock;
        private Mock<ILocacaoService> _locacaoServiceMock;
        private Mock<IPessoaService> _pessoaServiceMock;
        private Mock<IMapper> _mapperMock;
        private LocarImovelController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _imovelServiceMock = new Mock<IImovelService>();
            _locacaoServiceMock = new Mock<ILocacaoService>();
            _pessoaServiceMock = new Mock<IPessoaService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new LocarImovelController(_imovelServiceMock.Object, _mapperMock.Object, _locacaoServiceMock.Object, _pessoaServiceMock.Object);
        }

        [TestMethod()]
        public void LocarImovelControllerTest()
        {
            Assert.IsNotNull(_controller);
        }

        [TestMethod()]
        public void LocarImovel_Get_ReturnsNotFound_WhenImovelIsNull()
        {
            // Arrange
            uint imovelId = 1;
            _imovelServiceMock.Setup(service => service.GetImovelDto(imovelId)).Returns((ImovelDto)null);

            // Act
            var result = _controller.LocarImovel(imovelId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void LocarImovel_Get_ReturnsNotFound_WhenImovelIsNotAvailable()
        {
            // Arrange
            uint imovelId = 1;
            var imovelDto = new ImovelDto { Status = "Indisponivel" };
            _imovelServiceMock.Setup(service => service.GetImovelDto(imovelId)).Returns(imovelDto);

            // Act
            var result = _controller.LocarImovel(imovelId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void LocarImovel_Get_ReturnsViewResult_WhenImovelIsAvailable()
        {
            // Arrange
            uint imovelId = 1;
            var imovelDto = new ImovelDto { Status = "Disponivel" };
            var locacaoViewModel = new LocacaoViewModel();
            _imovelServiceMock.Setup(service => service.GetImovelDto(imovelId)).Returns(imovelDto);
            _mapperMock.Setup(mapper => mapper.Map<LocacaoViewModel>(imovelDto)).Returns(locacaoViewModel);

            // Act
            var result = _controller.LocarImovel(imovelId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(locacaoViewModel, viewResult.Model);
        }

        [TestMethod()]
        public void LocarImovel_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            uint imovelId = 1;
            var locacaoViewModel = new LocacaoViewModel { IdImovel = imovelId };
            var locacao = new Locacao();
            _mapperMock.Setup(mapper => mapper.Map<Locacao>(locacaoViewModel)).Returns(locacao);
            _locacaoServiceMock.Setup(service => service.Create(locacao)).Verifiable();

            // Act
            var result = _controller.LocarImovel(imovelId, locacaoViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }

        [TestMethod()]
        public void LocarImovel_Post_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            uint imovelId = 1;
            var locacaoViewModel = new LocacaoViewModel { IdImovel = imovelId };
            _controller.ModelState.AddModelError("Error", "ModelState is invalid");

            // Act
            var result = _controller.LocarImovel(imovelId, locacaoViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(locacaoViewModel, viewResult.Model);
        }

        [TestMethod()]
        public void LocarImovel_Post_ReturnsViewResult_WhenExceptionIsThrown()
        {
            // Arrange
            uint imovelId = 1;
            var locacaoViewModel = new LocacaoViewModel { IdImovel = imovelId };
            var locacao = new Locacao();
            _mapperMock.Setup(mapper => mapper.Map<Locacao>(locacaoViewModel)).Returns(locacao);
            _locacaoServiceMock.Setup(service => service.Create(locacao)).Throws(new Exception("Imóvel já alugado"));

            // Act
            var result = _controller.LocarImovel(imovelId, locacaoViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(locacaoViewModel, viewResult.Model);
            Assert.AreEqual("Imóvel já alugado", _controller.ViewData["Error"]);
        }
    }
}
