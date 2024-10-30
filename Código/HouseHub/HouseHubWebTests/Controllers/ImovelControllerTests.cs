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
    public class ImovelControllerTests
    {
        private static ImovelController? controller;
        private readonly IImovelService? @object;
        private readonly IMapper? mapper;

        [TestInitialize]
        public void Initialize()
        {
            // Arrange
            var mockService = new Mock<IImovelService>();

            IMapper mapper = new MapperConfiguration(cfg =>
               cfg.AddProfile(new ImovelProfile())).CreateMapper();

            mockService.Setup(service => service.GetAll())
               .Returns(GetTestSugestoes());
            mockService.Setup(service => service.Get(1))
               .Returns(GetTargetSugestao());
            mockService.Setup(service => service.Create(It.IsAny<Imovel>()))
               .Verifiable();
            controller = new ImovelController(mockService.Object, mapper);
        }

        [TestMethod()]
        public void IndexTest()
        {
            // Act
            var result = controller!.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(List<ImovelViewModel>));

            List<ImovelViewModel>? lista = (List<ImovelViewModel>)viewResult.ViewData.Model;
            Assert.AreEqual(2, lista.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            // Act
            var result = controller!.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ImovelViewModel));
            ImovelViewModel imovelViewModel = (ImovelViewModel)viewResult.ViewData.Model;

            Assert.AreEqual("Casa feia", imovelViewModel.Descricao);
            Assert.AreEqual(3, imovelViewModel.Quartos);
            Assert.AreEqual(2, imovelViewModel.Banheiros);
            Assert.AreEqual(200, imovelViewModel.ValorCondominio);
            Assert.AreEqual(800, imovelViewModel.PrecoAluguel);
            Assert.AreEqual(0, imovelViewModel.PrecoVenda);
            Assert.AreEqual(200, imovelViewModel.Iptu);
            Assert.AreEqual("Alugado", imovelViewModel.Status);
            Assert.AreEqual(false, imovelViewModel.PodeAnimal);
            Assert.AreEqual("Casa", imovelViewModel.Tipo);
            Assert.AreEqual((uint)2, imovelViewModel.IdPessoa);
            Assert.AreEqual("Centro", imovelViewModel.Bairro);
            Assert.AreEqual("Sergipe", imovelViewModel.Estado);
            Assert.AreEqual("Itabaiana", imovelViewModel.Cidade);
            Assert.AreEqual("49509-765", imovelViewModel.Cep);
            Assert.AreEqual("Rua do centro", imovelViewModel.Logradouro);
            Assert.AreEqual("123", imovelViewModel.Numero);
            Assert.AreEqual("Casa 1", imovelViewModel.Complemento);

        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var result = controller!.Create();

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
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void CreateTest_Invalid()
        {
            // Arrange
            controller!.ModelState.AddModelError("Descrição", "Descrição da casa é obrigatória.");

            // Act
            var result = controller.Create(GetNewSugestao());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        private ImovelViewModel GetNewSugestao()
        {
            return new ImovelViewModel
            {
                Id = 4,
                Descricao = "Casa feia",
                Quartos = 3,
                Banheiros = 2,
                ValorCondominio = 200,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                Status = "Alugado",
                PodeAnimal = true,
                Tipo = "Casa",
                IdPessoa = 2,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49509-765",
                Logradouro = "Rua do centro",
                Numero = "123",
                Complemento = "Casa 1"

            };
        }

        private Imovel GetTargetSugestao()
        {
            return new Imovel
            {
                Id = 4,
                Descricao = "Casa feia",
                Quartos = 3,
                Banheiros = 2,
                ValorCondominio = 200,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                Status = "Alugado",
                PrecoCondominio = 200,
                PodeAnimal = 0,
                Tipo = "Casa",
                IdPessoa = 2,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49509-765",
                Logradouro = "Rua do centro",
                Numero = "123",
                Complemento = "Casa 1"

            };
        }

        private Imovel GetTargetImovel()
        {
            return new Imovel
            {
                Id = 1,
                Descricao = "Casa bonita",
                Quartos = 3,
                Banheiros = 2,
                ValorCondominio = 200,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                PrecoCondominio = 200,
                PodeAnimal = 0,
                Tipo = "Casa",
                IdPessoa = 1,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49500-000",
                Logradouro = "Rua do centro",
                Numero = "123",
                Complemento = "Casa 1"

            };
        }

        private IEnumerable<Imovel> GetTestSugestoes()
        {
            return new List<Imovel>
            {
                new Imovel()
                {
                    Id = 2,
                    Descricao = "Casinha bonita",
                    Quartos = 2,
                    Banheiros = 1,
                    ValorCondominio = 100,
                    PrecoAluguel = 500,
                    PrecoVenda = 100000,
                    Iptu = 100,
                    Status = "Disponível",
                    PrecoCondominio = 100,
                    PodeAnimal = 1,
                    Tipo = "Casa",
                    IdPessoa = 1,
                    Bairro = "Centro",
                    Estado = "Sergipe",
                    Cidade = "Itabaiana",
                    Cep = "49500-000",
                    Logradouro = "Rua do centro",
                    Numero = "123",
                    Complemento = "Casa 1"
                },
                new Imovel()
                {
                    Id = 3,
                    Descricao = "Apartamento",
                    Quartos = 3,
                    Banheiros = 2,
                    ValorCondominio = 200,
                    PrecoAluguel = 800,
                    PrecoVenda = 200000,
                    Iptu = 200,
                    Status = "Disponível",
                    PrecoCondominio = 200,
                    PodeAnimal = 0,
                    Tipo = "Apartamento",
                    IdPessoa = 2,
                    Bairro = "Centro",
                    Estado = "Sergipe",
                    Cidade = "Itabaiana",
                    Cep = "49500-000",
                    Logradouro = "Rua do centro",
                    Numero = "123",
                    Complemento = "Casa 1"

                }
            };

        }

        [TestMethod()]
        public void EditTest()
        {
            // Act
            var result = controller!.Edit(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod()]
        public void EditTest_Valid()
        {
            //Act
            var result = controller!.Edit(1, GetNewSugestao());

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            RedirectToActionResult redirectToActionResult = (RedirectToActionResult)result;
            Assert.IsNull(redirectToActionResult.ControllerName);
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod()]
        public void EditTest_Invalid()
        {
            controller!.ModelState.AddModelError("Descrição", "Descrição da casa é obrigatória.");

            //Act
            var result = controller.Edit(1, GetNewSugestao());

            // Assert
            Assert.AreEqual(1, controller.ModelState.ErrorCount);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}

