using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseHubWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseHubWeb.Models;

namespace HouseHubWeb.Controllers.Tests
{
    [TestClass()]
    public class AgendarVisitaControllerTests
    {
        [TestMethod()]
        public void AgendarVisitaControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest()
        {
            var visite = new AgendamentoViewModel
            {
                DataVisita = DateTime.Now,
                Observacoes = "teste",
                Telefone = "(79)99999-9999",
                IdImovel = 1
            };
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Fail();
        }
    }
}