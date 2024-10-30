using Core.Service;
using Core;
using Microsoft.EntityFrameworkCore;


namespace Service.Tests
{
    [TestClass()]
    public class LocacaoServiceTests
    {
        HouseHubContext houseHubContext;
        ILocacaoService locacaoService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<HouseHubContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb").Options;
            houseHubContext = new HouseHubContext(options);

            houseHubContext.Database.EnsureDeleted();
            houseHubContext.Database.EnsureCreated();

            var locacoes = GetTestSugestoes();
            SetupDatabase();

            houseHubContext.Locacaos.AddRange(locacoes);
            houseHubContext.SaveChanges();

            locacaoService = new LocacaoService(houseHubContext);
        }

        private void SetupDatabase()
        {
            var pessoa = new Pessoa()
            {
                Id = 1,
                Nome = "João",
                Cpf = "12345678900",
                DataNascimento = DateTime.Now,
                Email = "walter@gmail.com",
                Telefone = "79999999999",
            };
            var imovel = new Imovel()
            {
                Id = 1,
                Descricao = "Casa bonita",
                Quartos = 3,
                Banheiros = 2,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                Status = "Disponível",
                PrecoCondominio = 200,
                PodeAnimal = 0,
                Tipo = "Casa",
                IdPessoa = 1,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49509-765",
                Logradouro = "Rua do centro",
                Numero = "123",
                Complemento = "Casa 1",
                Modalidade = "Aluguel"
            };

            var imovel2 = new Imovel()
            {
                Id = 2,
                Descricao = "Casa feia",
                Quartos = 3,
                Banheiros = 2,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                Status = "Disponível",
                PrecoCondominio = 200,
                PodeAnimal = 0,
                Tipo = "Casa",
                IdPessoa = 1,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49509-765",
                Logradouro = "Rua do centro",
                Numero = "123",
                Complemento = "Casa 1",
                Modalidade = "Aluguel"
            };

            var imovel3 = new Imovel()
            {
                Id = 3,
                Descricao = "Casa regular",
                Quartos = 3,
                Banheiros = 2,
                PrecoAluguel = 800,
                PrecoVenda = 0,
                Iptu = 200,
                Status = "Disponível",
                PrecoCondominio = 200,
                PodeAnimal = 0,
                Tipo = "Casa",
                IdPessoa = 1,
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Itabaiana",
                Cep = "49509-765",
                Logradouro = "Rua do centro",
                Numero = "1234",
                Complemento = "Casa 3",
                Modalidade = "Aluguel"
            };
            houseHubContext.Pessoas.Add(pessoa);
            houseHubContext.Imovels.Add(imovel);
            houseHubContext.Imovels.Add(imovel2);
            houseHubContext.Imovels.Add(imovel3);
        }

        private IEnumerable<Locacao> GetTestSugestoes()
        {
            return new List<Locacao>
            {
                new Locacao()
                {
                    Id = 1,
                    DataContrato = DateTime.Now,
                    DataVencimento = DateTime.Now.AddMonths(1),
                    DataInicio = DateTime.Now,
                    DataFim = null,
                    Valor = 500,
                    Status = "Ativo",
                    IdImovel = 1,
                    IdPessoa = 1
                },
                new Locacao()
                {
                    Id = 2,
                    DataContrato = DateTime.Now,
                    DataVencimento = DateTime.Now.AddMonths(1),
                    DataInicio = DateTime.Now.AddDays(2),
                    DataFim = DateTime.Now.AddDays(2),
                    Status = "Inativo",
                    Valor = 800,
                    IdImovel = 2,
                    IdPessoa = 1
                }
            };
        }

        private Locacao GetNewSugestao()
        {
            return new Locacao()
            {
                Id = 3,
                DataContrato = DateTime.Now,
                DataVencimento = DateTime.Now.AddMonths(1),
                DataInicio = DateTime.Now,
                DataFim = null,
                Valor = 235,
                Status = "Ativo",
                IdImovel = 3,
                IdPessoa = 1
            
            };
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Act
            var locacao = GetNewSugestao();
            locacaoService.Create(locacao);

            // Assert
            Assert.AreEqual(3, houseHubContext.Locacaos.Count());
            var locacaoCriado = houseHubContext.Locacaos.Find((uint)3);
            Assert.IsNotNull(locacaoCriado);
            Assert.AreEqual(235, locacaoCriado.Valor);
            Assert.AreEqual("Ativo", locacaoCriado.Status);
            Assert.AreEqual((uint)1, locacaoCriado.IdPessoa);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Act
            locacaoService.Delete(1);
            // Assert
            Assert.AreEqual(2, locacaoService.GetAll().Count());
            var locacao = locacaoService.Get(1);
            Assert.AreNotEqual(null, locacao);
            Assert.AreEqual("Inativo", locacao!.Status);
        }

        [TestMethod()]
        public void GetTest()
        {
            //Act
            var locacao = locacaoService.Get(1);

            //Assert
            Assert.IsNotNull(locacao);
            Assert.AreEqual(500, locacao.Valor);
            Assert.AreEqual("Ativo", locacao.Status);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var locacoes = locacaoService.GetAll();

            // Assert
            Assert.IsNotNull(locacoes);
            Assert.AreEqual(2, locacoes.Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Act
            var locacao = locacaoService.Get(1);
            locacao!.Valor = 0;
            locacaoService.Update(locacao);

            // Assert
            var locacaoAtualizado = locacaoService.Get(1);
            Assert.IsNotNull(locacaoAtualizado);
            Assert.AreEqual("Ativo", locacaoAtualizado.Status);
            Assert.AreEqual(0, locacaoAtualizado.Valor);
            Assert.AreEqual((uint)1, locacaoAtualizado.IdPessoa);
        }
    }
}