using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseHubWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Core;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class PagamentoControllerTests
    {
        private static PagamentoController? controller;
        private Mock<IPagamentoService>? mockService;
        private IMapper? mapper;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            mockService = new Mock<IPagamentoService>();
            IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<PagamentoViewModel, Pagamento>()).CreateMapper();

            controller = new PagamentoController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void Inserir_Get_ReturnsView()
        {
            // Act
            var result = controller!.Inserir();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void Inserir_Post_ValidModel_ReturnsRedirect()
        {
            // Arrange
            var pagamentoViewModel = new PagamentoViewModel { FormaPagamento = "Cartão", IdLocacao = 1 };
            mockService!.Setup(service => service.Create(It.IsAny<Pagamento>())).Verifiable();

            // Act
            var result = controller!.Inserir(pagamentoViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            var redirectResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }

        [TestMethod()]
        public void Inserir_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            controller!.ModelState.AddModelError("FormaPagamento", "Campo obrigatório.");
            var pagamentoViewModel = new PagamentoViewModel();

            // Act
            var result = controller.Inserir(pagamentoViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
