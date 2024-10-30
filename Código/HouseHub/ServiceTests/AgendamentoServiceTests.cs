using Core.Service;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Service.Tests
{
    [TestClass()]
    public class AgendamentoServiceTests
    {
        HouseHubContext houseHubContext;
        IAgendamentoService agendamentoService;


        [TestInitialize]
        public void Initialize()
        {

            var options = new DbContextOptionsBuilder<HouseHubContext>()
            .UseInMemoryDatabase(databaseName: "MyTestDb").Options;
            houseHubContext = new HouseHubContext(options);

            houseHubContext.Database.EnsureDeleted();
            houseHubContext.Database.EnsureCreated();

            var agendamentos = GetTestSugestoes();

            houseHubContext.Agendamentos.AddRange(agendamentos);
            houseHubContext.SaveChanges();

            agendamentoService = new AgendamentoService(houseHubContext);
        }

        private List<Agendamento> GetTestSugestoes()
        {
            return new List<Agendamento> {
                new Agendamento
                {
                    Id = 1,
                    DataCriacao = DateTime.Now,
                    DataVisita = DateTime.Now.AddDays(10),
                    IdImovel = 1,
                    IdPessoa = 1,
                    Observacoes = "Observações necessárias",
                    Status = "Pendente"
                },
                new Agendamento
                {
                    Id = 2,
                    DataCriacao = DateTime.Now,
                    DataVisita = DateTime.Now.AddDays(10),
                    IdImovel = 2,
                    IdPessoa = 2,
                    Observacoes = "Observações",
                    Status = "Agendado"
                }
            };
        }

        private Agendamento GetTestAgendamento()
        {
            return new Agendamento
            {
                Id = 3,
                DataCriacao = DateTime.Now,
                DataVisita = DateTime.Now.AddDays(10),
                IdImovel = 1,
                IdPessoa = 1,
                Observacoes = "Observações",
                Status = "Agendado"
            };
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var agendamento = GetTestAgendamento();
            var result = agendamentoService.Create(agendamento);
            // Assert
            Assert.AreEqual(3, houseHubContext.Agendamentos.Count());
            var imovelCriado = houseHubContext.Imovels.Find((uint)3);
            Assert.IsNotNull(agendamento);
            Assert.AreEqual("Agendado", agendamento.Status);
            Assert.AreEqual((uint) 3, agendamento.Id);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            agendamentoService.Delete(1);
            // Assert
            Assert.AreEqual(1, agendamentoService.GetAll().Count());
            var agendamento = agendamentoService.Get(1);
            Assert.AreEqual(null, agendamento);
        }

        [TestMethod()]
        public void GetTest()
        {
            //Act
            var imovel = agendamentoService.Get(1);

            //Assert
            Assert.IsNotNull(imovel);
            Assert.AreEqual("Observações necessárias", imovel.Observacoes);
            Assert.AreEqual("Pendente", imovel.Status);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var agendamentos = agendamentoService.GetAll();

            // Assert
            Assert.IsNotNull(agendamentos);
            Assert.AreEqual(2, agendamentos.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Act
            var agendamento = agendamentoService.Get(2);
            agendamento!.Status = "Agendado";
            agendamentoService.Update(agendamento);


            // Assert
            Assert.AreEqual("Agendado", agendamento!.Status);

        }
    }
}