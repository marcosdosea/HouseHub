using AutoMapper;
using Core.Service;
using Core;
using HouseHubWeb.Controllers;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using HouseHubWeb.Mappers;
using Core.DTOs;

[TestClass()]
public class PessoaControllerTests
{
    private static PessoaController pessoaController;
    private Mock<IPessoaService> mockService;
    private IMapper mapper;

    [TestInitialize]
    public void Initialize()
    {
        mockService = new Mock<IPessoaService>();

        mapper = new MapperConfiguration(cfg =>
            cfg.AddProfile(new PessoaProfile()))
            .CreateMapper();

        mockService.Setup(service => service.GetAll())
            .Returns(GetPessoas());
        mockService.Setup(service => service.Get(1))
            .Returns(GetPessoas().First());
        mockService.Setup(service => service.Create(It.IsAny<Pessoa>()))
            .Verifiable();
        mockService.Setup(service => service.Update(It.IsAny<Pessoa>()))
            .Verifiable();
        mockService.Setup(service => service.Delete(It.IsAny<uint>()))
            .Verifiable();

        pessoaController = new PessoaController(mockService.Object, mapper);
    }

    private List<Pessoa> GetPessoas()
    {
        return new List<Pessoa>
        {
            new Pessoa { Id = 1, Nome = "Pessoa 1", Cpf = "12345678901", Email = "pessoa1@email.com" },
            new Pessoa { Id = 2, Nome = "Pessoa 2", Cpf = "23456789012", Email = "pessoa2@email.com" },
            new Pessoa { Id = 3, Nome = "Pessoa 3", Cpf = "34567890123", Email = "pessoa3@email.com" }
        };
    }

    [TestMethod()]
    public void Create_ValidModel_ReturnsRedirectToAction()
    {
        var model = new PessoaViewModel
        {
            Nome = "Pessoa Teste",
            Cpf = "98765432100",
            Email = "pessoateste@email.com"
        };

        var result = pessoaController.Create(model) as RedirectToActionResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod()]
    public void Edit_ValidModel_ReturnsRedirectToAction()
    {
        var model = new PessoaViewModel
        {
            Id = 1,
            Nome = "Pessoa Atualizada",
            Cpf = "12345678901",
            Email = "atualizada@email.com"
        };

        var result = pessoaController.Edit(1, model) as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod()]
    public void Delete_Get_ReturnsCorrectViewModel()
    {
        var pessoa = GetPessoas().First();
        mockService.Setup(service => service.Get(1)).Returns(pessoa);

        var result = pessoaController.Delete(1) as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(PessoaViewModel));
    }

    [TestMethod()]
    public void Delete_Post_ReturnsRedirectToAction()
    {
        var model = new PessoaViewModel { Id = 1, Nome = "Pessoa 1", Cpf = "12345678901", Email = "pessoa1@email.com" };

        var result = pessoaController.Delete(1, model) as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
    }

    [TestMethod()]
    public void Details_ValidId_ReturnsCorrectViewModel()
    {
        var pessoa = GetPessoas().First();
        mockService.Setup(service => service.Get(1)).Returns(pessoa);

        var result = pessoaController.Details(1) as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Model, typeof(PessoaViewModel));
    }

    [TestMethod()]
    public void GetPessoasByCpf_ValidCpf_ReturnsJsonResult()
    {
        var cpf = "12345678901";
        var pessoa = GetPessoas().First(p => p.Cpf == cpf);
        var pessoaDto = mapper.Map<PessoaDto>(pessoa);
        mockService.Setup(service => service.GetByCpf(cpf)).Returns(pessoaDto);

        var result = pessoaController.GetPessoasByCpf(cpf) as JsonResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(pessoaDto, result.Value);
    }

    [TestMethod()]
    public void GetPessoaByEmail_ValidEmail_ReturnsJsonResult()
    {
        var email = "pessoa1@email.com";
        var pessoa = GetPessoas().First(p => p.Email == email);
        var pessoaDto = mapper.Map<PessoaDto>(pessoa);
        mockService.Setup(service => service.GetByEmail(email)).Returns(pessoaDto);

        var result = pessoaController.GetPessoaByEmail(email) as JsonResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(pessoaDto, result.Value);
    }

    [TestMethod()]
    public void Create_InvalidModel_ReturnsViewWithModel()
    {
        pessoaController.ModelState.AddModelError("Nome", "Required");
        var model = new PessoaViewModel { Nome = "", Cpf = "98765432100", Email = "pessoateste@email.com" };

        var result = pessoaController.Create(model) as ViewResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(model, result.Model);
    }
}
