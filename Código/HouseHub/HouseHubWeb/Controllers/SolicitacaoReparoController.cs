using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;

namespace HouseHubWeb.Controllers;

[Authorize]
public class SolicitacaoReparoController : Controller
{
    private readonly ISolicitacaoreparoService solicitacaoreparoService;
    private readonly IPessoaService pessoaService;
    private readonly ILocacaoService locacaoService;
    private readonly IMapper mapper;

    public SolicitacaoReparoController(ISolicitacaoreparoService solicitacaoreparoService, IPessoaService pessoaService, ILocacaoService locacaoService, IMapper mapper)
    {
        this.solicitacaoreparoService = solicitacaoreparoService;
        this.pessoaService = pessoaService;
        this.locacaoService = locacaoService;
        this.mapper = mapper;
    }

    // GET: SolicitacaoReparoController
    public async Task<ActionResult> Index()
    {
        var username = User.Identity.Name;
        var pessoaAutenticada = pessoaService.GetByEmail(username);

        if (pessoaAutenticada == null)
            return RedirectToAction("Login", "Account");

        // Buscar solicitações onde a pessoa é o proprietário
        var solicitacoesProprietario = await solicitacaoreparoService.ObterSolicitacoesPorProprietarioAsync(pessoaAutenticada.Id);
        var viewModelProprietario = mapper.Map<List<SolicitacaoReparoViewModel>>(solicitacoesProprietario);

        return View(viewModelProprietario);
    }

    // GET: SolicitacaoReparoController/MinhasSolicitacoes
    public async Task<ActionResult> MinhasSolicitacoes()
    {
        var username = User.Identity.Name;
        var pessoaAutenticada = pessoaService.GetByEmail(username);

        if (pessoaAutenticada == null)
            return RedirectToAction("Login", "Account");

        // Materializar a consulta de locações chamando ToList()
        var locacoes = locacaoService.GetAll()
            .Where(l => l.IdPessoa == pessoaAutenticada.Id)
            .ToList();  // <- Isso fecha o primeiro DataReader

        var solicitacoes = new List<Solicitacaoreparo>();

        foreach (var locacao in locacoes)
        {
            var solicitacoesLocacao = await solicitacaoreparoService.ObterSolicitacoesPorLocacaoAsync(locacao.Id);
            solicitacoes.AddRange(solicitacoesLocacao);
        }

        var viewModel = mapper.Map<List<SolicitacaoReparoViewModel>>(solicitacoes);
        return View(viewModel);
    }

    // GET: SolicitacaoReparoController/Detalhes/5
    [HttpGet]
    [Route("SolicitacaoReparo/Detalhes/{idSolicitacao}")]
    public async Task<ActionResult> Detalhes(int idSolicitacao)
    {
        var username = User.Identity.Name;
        var pessoaAutenticada = pessoaService.GetByEmail(username);

        if (pessoaAutenticada == null)
            return RedirectToAction("Login", "Account");

        var solicitacao = await solicitacaoreparoService.ObterSolicitacaoAsync((uint)idSolicitacao, pessoaAutenticada.Id);
        if (solicitacao == null) return NotFound();

        var viewModel = mapper.Map<SolicitacaoReparoViewModel>(solicitacao);
        return View(viewModel);
    }

    // GET: SolicitacaoReparoController/Create/5
    [HttpGet]
    [Route("SolicitacaoReparo/Create/{idImovel}")]
    public ActionResult Create(uint idImovel)
    {
        var viewModel = new SolicitacaoReparoViewModel
        {
            Status = "Pendente",
            Data = DateTime.Now
        };

        ViewBag.IdImovel = idImovel;
        return View(viewModel);
    }

    // POST: SolicitacaoReparoController/Create/5
    [HttpPost]
    [Route("SolicitacaoReparo/Create/{idImovel}")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(uint idImovel, SolicitacaoReparoViewModel solicitacaoReparo)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.IdImovel = idImovel;
            return View(solicitacaoReparo);
        }

        try
        {
            var username = User.Identity.Name;
            var pessoaAutenticada = pessoaService.GetByEmail(username);

            if (pessoaAutenticada == null)
                return RedirectToAction("Login", "Account");

            var locacao = locacaoService.GetIdLocacaoByIdImovelAndUser(idImovel, pessoaAutenticada.Id);

            if (locacao == null)
                return NotFound("Locação não encontrada para este imóvel e usuário.");

            var solicitacao = mapper.Map<Solicitacaoreparo>(solicitacaoReparo);
            solicitacao.IdLocacao = locacao.Id;
            solicitacao.Status = "Pendente";
            solicitacao.Data = DateTime.Now;
            solicitacao.RespostaProprietario = "";

            await solicitacaoreparoService.CriarSolicitacaoAsync(solicitacao);
            return RedirectToAction(nameof(MinhasSolicitacoes));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Erro ao criar solicitação: " + ex.Message);
            ViewBag.IdImovel = idImovel;
            return View(solicitacaoReparo);
        }
    }

    // GET: SolicitacaoReparoController/Responder/5
    [HttpGet]
    public async Task<ActionResult> Responder(int id)
    {
        var username = User.Identity.Name;
        var pessoaAutenticada = pessoaService.GetByEmail(username);

        if (pessoaAutenticada == null)
            return RedirectToAction("Login", "Account");

        var solicitacao = await solicitacaoreparoService.ObterSolicitacaoAsync((uint)id, pessoaAutenticada.Id);
        if (solicitacao == null) return NotFound();

        // Verificar se o usuário é o proprietário
        var ehProprietario = await VerificarSeEhProprietario(solicitacao.IdLocacao, pessoaAutenticada.Id);
        if (!ehProprietario)
            return Forbid();

        var viewModel = mapper.Map<SolicitacaoReparoViewModel>(solicitacao);
        return View(viewModel);
    }

    // POST: SolicitacaoReparoController/Responder/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Responder(int id, SolicitacaoReparoViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var username = User.Identity.Name;
            var pessoaAutenticada = pessoaService.GetByEmail(username);

            if (pessoaAutenticada == null)
                return RedirectToAction("Login", "Account");

            // Verificar se o usuário é o proprietário
            var ehProprietario = await VerificarSeEhProprietario(viewModel.IdLocacao, pessoaAutenticada.Id);
            if (!ehProprietario)
                return Forbid();

            var atualizado = await solicitacaoreparoService.AtualizarStatusAsync((uint)id, viewModel.Status, viewModel.RespostaProprietario);

            if (!atualizado) return NotFound();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Erro ao atualizar solicitação: " + ex.Message);
            return View(viewModel);
        }
    }

    // Método auxiliar para verificar se o usuário é proprietário do imóvel associado à locação
    private async Task<bool> VerificarSeEhProprietario(uint idLocacao, uint idPessoa)
    {
        return await solicitacaoreparoService.VerificaSeEhProprietario(idLocacao, idPessoa);
    }
}