using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Core;
using Core.Service;
using HouseHubWeb.Models;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class SolicitacaoReparoControllerTests
    {
        private Mock<ISolicitacaoreparoService> _serviceMock;
        private Mock<IMapper> _mapperMock;
        private SolicitacaoReparoController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _serviceMock = new Mock<ISolicitacaoreparoService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new SolicitacaoReparoController(_serviceMock.Object, _mapperMock.Object);
        }

        [TestMethod()]
        public void SolicitacaoReparoControllerTest()
        {
            var controller = new SolicitacaoReparoController(_serviceMock.Object, _mapperMock.Object);
            Assert.IsNotNull(controller);
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = _controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = _controller.Details(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreateGetTest()
        {
            var result = _controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreatePostTest()
        {
            var viewModel = new SolicitacaoReparoViewModel
            {
                Descricao = "Teste",
                Status = "Pendente",
                Data = DateTime.Now,
                Valor = 100.0m,
                EnviarAlguem = true,
                IdLocacao = 1
            };

            var entity = new Solicitacaoreparo();

            _mapperMock.Setup(m => m.Map<Solicitacaoreparo>(viewModel))
                .Returns(entity);

            _serviceMock.Setup(s => s.Create(entity))
                .Returns(1);

            var result = _controller.Create(viewModel) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            _serviceMock.Verify(s => s.Create(entity), Times.Once);
        }
    }
}