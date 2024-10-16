using Core.Service;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;


namespace Service.Tests
{
    [TestClass()]
    public class ImovelServiceTests
    {
        HouseHubContext houseHubContext;
        IImovelService imovelService;

        [TestInitialize]
        public void Initialize()
        {

            var options = new DbContextOptionsBuilder<HouseHubContext>()
            .UseInMemoryDatabase(databaseName: "MyTestDb")
            .Options;
            houseHubContext = new HouseHubContext(options);

            houseHubContext.Database.EnsureDeleted();
            houseHubContext.Database.EnsureCreated();

            var imoveis = GetTestSugestoes();

            houseHubContext.Imovels.AddRange(imoveis);
            houseHubContext.SaveChanges();

            imovelService = new ImovelService(houseHubContext);
        }


        private IEnumerable<Imovel> GetTestSugestoes()
        {
            return new List<Imovel>
            {
                new Imovel()
                {
                    Id = 1,
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
                    Id = 2,
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

        private Imovel GetNewSugestao()
        {
            return new Imovel
            {
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

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var imovel = GetNewSugestao();
            imovelService.Create(imovel);

            // Assert
            Assert.AreEqual(3, houseHubContext.Imovels.Count());
            var imovelCriado = houseHubContext.Imovels.Find((uint)4);
            Assert.IsNotNull(imovel);
            Assert.AreEqual("Casa feia", imovel.Descricao);
            Assert.AreEqual(3, imovel.Quartos);
            Assert.AreEqual("Alugado", imovel.Status);
            Assert.AreEqual(0, imovel.PodeAnimal);
            Assert.AreEqual((uint)2, imovel.IdPessoa);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            imovelService.Delete(1);
            // Assert
            Assert.AreEqual(1, imovelService.GetAll().Count());
            var imovel = imovelService.Get(1);
            Assert.AreEqual(null, imovel);
        }

        [TestMethod()]
        public void GetTest()
        {
            //Act
            var imovel = imovelService.Get(1);

            //Assert
            Assert.IsNotNull(imovel);
            Assert.AreEqual("Casinha bonita", imovel.Descricao);
            Assert.AreEqual(2, imovel.Quartos);
            Assert.AreEqual("Disponível", imovel.Status);
            Assert.AreEqual(1, imovel.PodeAnimal);
            Assert.AreEqual((uint)1, imovel.IdPessoa);
            
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var imoveis = imovelService.GetAll();

            // Assert
            Assert.IsNotNull(imoveis);
            Assert.AreEqual(2, imoveis.Count());

        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Act
            var imovel = imovelService.Get(1);  
            imovel!.Status = "Alugado";
            imovel!.PodeAnimal = 0;
            imovel!.IdPessoa = 2;
            imovelService.Update(imovel);

            //Assert
            var imovelAtualizado = imovelService.Get(1);
            Assert.IsNotNull(imovelAtualizado);
            Assert.AreEqual("Alugado", imovelAtualizado.Status);
            Assert.AreEqual(0, imovelAtualizado.PodeAnimal);
            Assert.AreEqual((uint)2, imovelAtualizado.IdPessoa);
        }
    }
}