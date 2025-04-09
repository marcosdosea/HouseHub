using AutoMapper;
using Core.Service;
using HouseHubWeb.Controllers;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseHubWeb.Mappers;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class BuscarImovelControllerTests
    {
        private BuscarImovelController _controller;
        private Mock<IImovelService> _mockImovelService;
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            _mockImovelService = new Mock<IImovelService>();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BuscaImovelProfile());
                cfg.AddProfile(new ImovelProfile());

            }).CreateMapper();

            _controller = new BuscarImovelController(_mockImovelService.Object, _mapper);
        }


        private static List<Core.Imovel> GetImoveis()
        {
            return new List<Core.Imovel>
            {
                new Core.Imovel { Id = 1, Cidade = "Cidade 1", Bairro = "Bairro 1", PrecoVenda = 150000, Quartos = 2, Modalidade = "Venda" },
                new Core.Imovel { Id = 2, Cidade = "Cidade 2", Bairro = "Bairro 2", PrecoVenda = 250000, Quartos = 3, Modalidade = "Locação" },
                new Core.Imovel { Id = 3, Cidade = "Cidade 3", Bairro = "Bairro 3", PrecoVenda = 350000, Quartos = 4, Modalidade = "Venda" }
            };
        }

        [TestMethod()]
        public void Index_Post_WithValidModel_ReturnsViewWithImoveis()
        {
            var buscarImovelViewModel = new BuscarImovelViewModel
            {
                Cidade = "Cidade 1",
                Bairro = "Bairro 1",
                ValorMaximo = 200000,
                Quartos = 2,
                Modalidade = "Venda"
            };

            _mockImovelService.Setup(service => service.GetAll(It.IsAny<Core.DTOs.BuscarImovelDto>()))
                .Returns(GetImoveis().Take(1));

            var result = _controller.Index(buscarImovelViewModel) as ViewResult;
            var imoveis = result?.Model as List<ImovelViewModel>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(imoveis);
            Assert.AreEqual(1, imoveis.Count);
            Assert.AreEqual("Cidade 1", imoveis[0].Cidade);
        }

        
    }
}
