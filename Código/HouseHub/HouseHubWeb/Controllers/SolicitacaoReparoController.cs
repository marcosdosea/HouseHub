using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace HouseHubWeb.Controllers;

public class SolicitacaoReparoController : Controller
{
    private readonly ISolicitacaoreparoService solicitacaoreparoService;
    private readonly IMapper mapper;

    public SolicitacaoReparoController(ISolicitacaoreparoService solicitacaoreparoService, IMapper mapper)
    {
        this.solicitacaoreparoService = solicitacaoreparoService;
        this.mapper = mapper;
    }

    // GET: SolicitacaoReparoController
    public async Task<ActionResult> Index()
    {
        var listaSolicitacoes = await solicitacaoreparoService.ObterSolicitacoesPorProprietarioAsync(1); // Supondo que o ID do proprietário seja 1
        var viewModel = mapper.Map<List<SolicitacaoReparoViewModel>>(listaSolicitacoes);
        return View(viewModel);
    }

    // GET: SolicitacaoReparoController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var solicitacao = await solicitacaoreparoService.ObterSolicitacaoAsync((uint)id);
        if (solicitacao == null) return NotFound();

        var viewModel = mapper.Map<SolicitacaoReparoViewModel>(solicitacao);
        return View(viewModel);
    }

    // GET: SolicitacaoReparoController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: SolicitacaoReparoController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(SolicitacaoReparoViewModel solicitacaoReparo)
    {
        if (!ModelState.IsValid) return View(solicitacaoReparo);

        try
        {
            var solicitacao = mapper.Map<Solicitacaoreparo>(solicitacaoReparo);
            await solicitacaoreparoService.CriarSolicitacaoAsync(solicitacao);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(solicitacaoReparo);
        }
    }

    // GET: SolicitacaoReparoController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var solicitacao = await solicitacaoreparoService.ObterSolicitacaoAsync((uint)id);
        if (solicitacao == null) return NotFound();

        var viewModel = mapper.Map<SolicitacaoReparoViewModel>(solicitacao);
        return View(viewModel);
    }

    // POST: SolicitacaoReparoController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, SolicitacaoReparoViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        try
        {
            var solicitacaoAtualizada = mapper.Map<Solicitacaoreparo>(viewModel);
            var atualizado = await solicitacaoreparoService.AtualizarStatusAsync((uint)id, viewModel.Status, viewModel.RespostaProprietario);

            if (!atualizado) return NotFound();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(viewModel);
        }
    }
}
