using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System.Collections.Generic;
using System.Linq;

namespace Service.Tests
{
    [TestClass()]
    public class SolicitacaoreparoServiceTests
    {
        private HouseHubContext _context;
        private SolicitacaoreparoService _service;

        [TestInitialize]
        public void Initialize()
        {
            // Configurar o contexto em memória para testes
            var options = new DbContextOptionsBuilder<HouseHubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new HouseHubContext(options);
            _service = new SolicitacaoreparoService(_context);

            // Limpar o banco de dados antes de cada teste
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            var solicitacao = new Solicitacaoreparo
            {
                Descricao = "Teste de reparo",
                Status = "Pendente",
                RespostaProprietario = "Resposta teste",
                // Adicione outros campos obrigatórios se houver
            };

            // Act
            uint id = _service.Create(solicitacao);

            // Assert
            Assert.AreNotEqual<uint>(0, id);
            var created = _context.Solicitacaoreparos.Find(id);
            Assert.IsNotNull(created);
            Assert.AreEqual("Teste de reparo", created.Descricao);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            var solicitacao = new Solicitacaoreparo
            {
                Descricao = "Teste para deletar",
                Status = "Pendente",
                RespostaProprietario = "Resposta teste"
            };
            _context.Solicitacaoreparos.Add(solicitacao);
            _context.SaveChanges();

            // Act
            bool result = _service.Delete(solicitacao.Id);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(_context.Solicitacaoreparos.Find(solicitacao.Id));
        }

        [TestMethod()]
        public void GetTest()
        {
            // Arrange
            var solicitacao = new Solicitacaoreparo
            {
                Descricao = "Teste para buscar",
                Status = "Pendente",
                RespostaProprietario = "Resposta teste"
            };
            _context.Solicitacaoreparos.Add(solicitacao);
            _context.SaveChanges();

            // Act
            var result = _service.Get(solicitacao.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(solicitacao.Descricao, result.Descricao);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Arrange
            var solicitacoes = new List<Solicitacaoreparo>
            {
                new Solicitacaoreparo { Descricao = "Teste 1", Status = "Pendente", RespostaProprietario = "Sim" },
                new Solicitacaoreparo { Descricao = "Teste 2", Status = "Em andamento", RespostaProprietario = "Sim" }
            };
            _context.Solicitacaoreparos.AddRange(solicitacoes);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(s => s.Descricao == "Teste 1"));
            Assert.IsTrue(result.Any(s => s.Descricao == "Teste 2"));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange
            var solicitacao = new Solicitacaoreparo
            {
                Descricao = "Teste original",
                Status = "Pendente",
                RespostaProprietario = "Resposta teste"
            };
            _context.Solicitacaoreparos.Add(solicitacao);
            _context.SaveChanges();

            // Act
            solicitacao.Descricao = "Teste atualizado";
            _service.Update(solicitacao);

            // Assert
            var updated = _context.Solicitacaoreparos.Find(solicitacao.Id);
            Assert.IsNotNull(updated);
            Assert.AreEqual("Teste atualizado", updated.Descricao);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}