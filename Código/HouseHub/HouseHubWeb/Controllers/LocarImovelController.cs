using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class LocarImovelController : Controller
    {
        private readonly IImovelService imovelService;
        private readonly ILocacaoService locacaoService;
        private readonly IPessoaService pessoaService;
        private readonly IMapper mapper;

        public LocarImovelController(IImovelService imovelService, IMapper mapper, ILocacaoService locacaoService, IPessoaService pessoaService)
        {
            this.imovelService = imovelService;
            this.mapper = mapper;
            this.locacaoService = locacaoService;
            this.pessoaService = pessoaService;
        }

        [HttpGet]
        [Route("/LocarImovel/{id}")]
        public IActionResult LocarImovel(uint id)
        {
            var imovel = imovelService.GetImovelDto((uint)id);
            if (imovel == null || imovel.Status != "Disponivel")
            {
                return NotFound();
            }
            var locacaoViewModel = mapper.Map<LocacaoViewModel>(imovel);
            locacaoViewModel.IdImovel = (uint)id;
            return View(locacaoViewModel);
        }

        [HttpPost]
        [Route("/LocarImovel/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult LocarImovel(uint id, LocacaoViewModel locacaoViewModel)
        {
            locacaoViewModel.IdImovel = id;
            try
            {
                if (ModelState.IsValid)
                {
                    var locacao = mapper.Map<Locacao>(locacaoViewModel);
                    locacao.IdImovel = id;
                    locacao.IdPessoa = 1;///Get the user id;
                    locacaoService.Create(locacao);
                    return RedirectToAction("Index", "Home");
                }
                return View(locacaoViewModel);
            }
            catch (Exception e)
            {
                if (e.Message == "Imóvel já alugado")
                {
                    ViewData["Error"] = e.Message;
                }
                return View(locacaoViewModel);
            }
        }
    }
}
