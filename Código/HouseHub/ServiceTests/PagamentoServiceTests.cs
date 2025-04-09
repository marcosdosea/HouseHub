using Core.Service;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestClass()]
    public class PagamentoServiceTests
    {
        private HouseHubContext _context;
        private IPagamentoService _pagamentoService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<HouseHubContext>()
                .UseInMemoryDatabase(databaseName: "PagamentoTestDb")
                .Options;

            _context = new HouseHubContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var locacoes = GetTestLocacoes();
            var pagamentos = GetTestPagamentos();

            _context.Locacaos.AddRange(locacoes);
            _context.Pagamentos.AddRange(pagamentos);
            _context.SaveChanges();

            _pagamentoService = new PagamentoService(_context);
        }

        private IEnumerable<Locacao> GetTestLocacoes()
        {
            return new List<Locacao>
            {
                new Locacao
                {
                    Id = 1,
                    IdPessoa = 1,
                    DataInicio = DateTime.Now.AddDays(-30),
                    DataFim = DateTime.Now.AddDays(335),
                    Status = "Ativo"
                },
                new Locacao
                {
                    Id = 2,
                    IdPessoa = 2,
                    DataInicio = DateTime.Now.AddDays(-60),
                    DataFim = DateTime.Now.AddDays(305),
                    Status = "Ativo"
                }
            };
        }

        private IEnumerable<Pagamento> GetTestPagamentos()
        {
            return new List<Pagamento>
            {
                new Pagamento
                {
                    Id = 1,
                    DataPagamento = DateTime.Now.AddDays(-15),
                    FormaPagamento = "Cartão",
                    PagamentoManual = 0,
                    IdLocacao = 1,
                    Status = "Pago",
                    DataVencimento = DateTime.Now.AddDays(-10)
                },
                new Pagamento
                {
                    Id = 2,
                    DataPagamento = DateTime.Now,
                    FormaPagamento = "PIX",
                    PagamentoManual = 0,
                    IdLocacao = 2,
                    Status = "Pago",
                    DataVencimento = DateTime.Now.AddDays(1)
                }
            };
        }

        private Pagamento GetNewPagamento()
        {
            return new Pagamento
            {
                DataPagamento = DateTime.Now,
                FormaPagamento = "Dinheiro",
                PagamentoManual = 1,
                IdLocacao = 1,
                Status = "Pago",
                DataVencimento = DateTime.Now.AddDays(30)
            };
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            var pagamento = GetNewPagamento();

            // Act
            uint id = _pagamentoService.Create(pagamento);

            // Assert
            Assert.AreEqual(2, _context.Pagamentos.Count());
            var pagamentoCriado = _context.Pagamentos.Find(id);
            Assert.IsNotNull(pagamentoCriado);
            Assert.AreEqual(pagamento.FormaPagamento, pagamentoCriado.FormaPagamento);
            Assert.AreEqual(pagamento.PagamentoManual, pagamentoCriado.PagamentoManual);
            Assert.AreEqual(pagamento.IdLocacao, pagamentoCriado.IdLocacao);
            Assert.AreEqual(pagamento.Status, pagamentoCriado.Status);
            Assert.AreEqual(pagamento.DataVencimento.Date, pagamentoCriado.DataVencimento.Date);
        }

        [TestMethod()]
        public void GetAllCobrancaTest()
        {
            // Arrange
            int idPessoa = 1;

            // Act
            var pagamentos = _pagamentoService.GetAllCobranca(idPessoa);

            // Assert
            Assert.IsNotNull(pagamentos);
            Assert.AreEqual(1, pagamentos.Count());
            var pagamento = pagamentos.First();
            Assert.AreEqual(1u, pagamento.IdLocacao);
            Assert.AreEqual("Cartão", pagamento.FormaPagamento);
        }

        [TestMethod()]
        public void GetAllCobranca_RetornaVazio_QuandoNaoExistemPagamentosPendentes()
        {
            // Arrange
            int idPessoa = 3; // ID de pessoa que não existe

            // Act
            var pagamentos = _pagamentoService.GetAllCobranca(idPessoa);

            // Assert
            Assert.IsNotNull(pagamentos);
            Assert.AreEqual(0, pagamentos.Count());
        }

        [TestMethod()]
        public void GetAllCobranca_RetornaPagamentosProximosDoVencimento()
        {
            // Arrange
            int idPessoa = 2;

            // Act
            var pagamentos = _pagamentoService.GetAllCobranca(idPessoa);

            // Assert
            Assert.IsNotNull(pagamentos);
            Assert.AreEqual(1, pagamentos.Count());
            var pagamento = pagamentos.First();
            Assert.IsTrue(pagamento.DataVencimento <= DateTime.Now.AddDays(2));
        }
    }
}