using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Core.Service;
using Moq;
using HouseHubWeb.Mappers;
using Core;
using HouseHubWeb.Models;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class AgendarVisitaControllerTests
    {

        private static AgendarVisitaController? controller;
        private readonly IAgendamentoService? @object;
        private readonly IMapper? mapper;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IAgendamentoService>();

            IMapper mapper = new MapperConfiguration(cfg =>
               cfg.AddProfile(new AgendamentoProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
               .Returns(GetTestSugestoes());
            mockService.Setup(service => service.Get(1))
               .Returns(GetTargetSugestao());
            mockService.Setup(service => service.Create(It.IsAny<Agendamento>()))
               .Verifiable();
            controller = new AgendarVisitaController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var result = controller!.Create(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void CreateTestValid()
        {
            // Act
            var result = controller!.Create(GetNewSugestao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("HomeController", redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_Invalid()
        {
            // Arrange
            controller!.ModelState.AddModelError("Datavisita", "data da visita é obrigatória.");

            // Act
            var result = controller.Create(GetNewSugestao());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private AgendamentoViewModel GetNewSugestao()
        {
            return new AgendamentoViewModel
            {
                Id = 3,
                DataVisita = DateTime.Now,
                Observacoes = "teste",
                Telefone = "(79) 99999-9999",
                IdImovel = 1
            };
        }

        private Agendamento GetTargetSugestao()
        {
            return new Agendamento
            {
                Id = 2,
                DataVisita = DateTime.Now,
                Observacoes = "teste",
                Status = "Pendente",
                IdImovel = 1,
                IdPessoa = 1
            };
        }

        private Agendamento GetTargetImovel()
        {
            return new Agendamento
            {
                Id = 1,
                DataVisita = DateTime.Now,
                Observacoes = "teste",
                Status = "Agendado",
                IdImovel = 1,
                IdPessoa = 1
            };
        }

        private IEnumerable<Agendamento> GetTestSugestoes()
        {
            return new List<Agendamento>
            {
                new Agendamento()
                {
                    Id = 2,
                    DataVisita = DateTime.Now,
                    Observacoes = "teste",
                    Status = "Agendado",
                    IdImovel = 1,
                    IdPessoa = 1
                },
                new Agendamento()
                {
                    Id = 3,
                    DataVisita = DateTime.Now,
                    Observacoes = "teste",
                    Status = "Agendado",
                    IdImovel = 1,
                    IdPessoa = 1
                }
            };

        }
    }
}