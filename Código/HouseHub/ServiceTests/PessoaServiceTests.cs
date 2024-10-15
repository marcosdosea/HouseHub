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
    public class PessoaServiceTests
    {
        private HouseHubContext _context;
        private IPessoaService _pessoaService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<HouseHubContext>()
                .UseInMemoryDatabase(databaseName: "PessoaTestDb")
                .Options;

            _context = new HouseHubContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var pessoas = GetTestPessoas();
            _context.Pessoas.AddRange(pessoas);
            _context.SaveChanges();

            _pessoaService = new PessoaService(_context);
        }

        private IEnumerable<Pessoa> GetTestPessoas()
        {
            return new List<Pessoa>
        {
            new Pessoa
            {
                Id = 1,
                Nome = "João Silva",
                Cpf = "12345678901",
                DataNascimento = new DateTime(1990, 1, 1),
                Telefone = "11999999999",
                Email = "joao.silva@example.com",
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Aracaju",
                Logradouro = "Rua Principal",
                Cep = "49000-000",
                Numero = "123",
                Complemento = "Apto 101"
            },
            new Pessoa
            {
                Id = 2,
                Nome = "Maria Santos",
                Cpf = "98765432109",
                DataNascimento = new DateTime(1985, 6, 15),
                Telefone = "11988888888",
                Email = "maria.santos@example.com",
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Aracaju",
                Logradouro = "Rua Principal",
                Cep = "49000-000",
                Numero = "456",
                Complemento = "Apto 102"
            }
        };
        }

        [TestMethod()]
        public void CreateTest()
        {
            // Arrange
            var pessoa = GetNewPessoa();

            // Act
            _pessoaService.Create(pessoa);

            // Assert
            Assert.AreEqual(2, _context.Pessoas.Count());
            var pessoaCriada = _context.Pessoas.Find(pessoa.Id);
            Assert.IsNotNull(pessoaCriada);
            Assert.AreEqual(pessoa.Nome, pessoaCriada.Nome);
            Assert.AreEqual(pessoa.Cpf, pessoaCriada.Cpf);
            Assert.AreEqual(pessoa.DataNascimento, pessoaCriada.DataNascimento);
            Assert.AreEqual(pessoa.Telefone, pessoaCriada.Telefone);
            Assert.AreEqual(pessoa.Email, pessoaCriada.Email);
            Assert.AreEqual(pessoa.Bairro, pessoaCriada.Bairro);
            Assert.AreEqual(pessoa.Estado, pessoaCriada.Estado);
            Assert.AreEqual(pessoa.Cidade, pessoaCriada.Cidade);
            Assert.AreEqual(pessoa.Logradouro, pessoaCriada.Logradouro);
            Assert.AreEqual(pessoa.Cep, pessoaCriada.Cep);
            Assert.AreEqual(pessoa.Numero, pessoaCriada.Numero);
            Assert.AreEqual(pessoa.Complemento, pessoaCriada.Complemento);
        }

        private Pessoa GetNewPessoa()
        {
            return new Pessoa
            {
                Nome = "Pedro Oliveira",
                Cpf = "11122233344",
                DataNascimento = new DateTime(1995, 7, 25),
                Telefone = "11977777777",
                Email = "pedro.oliveira@example.com",
                Bairro = "Centro",
                Estado = "Sergipe",
                Cidade = "Aracaju",
                Logradouro = "Rua Principal",
                Cep = "49000-001",
                Numero = "789",
                Complemento = "Apto 103"
            };
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            var idToDelete = 1;

            // Act
            _pessoaService.Delete((uint)idToDelete);

            // Assert
            Assert.AreEqual(1, _context.Pessoas.Count());
            var pessoaDeleted = _context.Pessoas.Find(idToDelete);
            Assert.IsNull(pessoaDeleted);
        }

        [TestMethod()]
        public void GetTest()
        {
            // Act
            var pessoa = _pessoaService.Get(1);

            // Assert
            Assert.IsNotNull(pessoa);
            Assert.AreEqual("João Silva", pessoa.Nome);
            Assert.AreEqual("12345678901", pessoa.Cpf);
            Assert.AreEqual(new DateTime(1990, 1, 1), pessoa.DataNascimento);
            Assert.AreEqual("11999999999", pessoa.Telefone);
            Assert.AreEqual("joao.silva@example.com", pessoa.Email);
            Assert.AreEqual("Centro", pessoa.Bairro);
            Assert.AreEqual("Sergipe", pessoa.Estado);
            Assert.AreEqual("Aracaju", pessoa.Cidade);
            Assert.AreEqual("Rua Principal", pessoa.Logradouro);
            Assert.AreEqual("49000-000", pessoa.Cep);
            Assert.AreEqual("123", pessoa.Numero);
            Assert.AreEqual("Apto 101", pessoa.Complemento);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            // Act
            var pessoas = _pessoaService.GetAll();

            // Assert
            Assert.IsNotNull(pessoas);
            Assert.AreEqual(2, pessoas.Count());
        }
    }
}