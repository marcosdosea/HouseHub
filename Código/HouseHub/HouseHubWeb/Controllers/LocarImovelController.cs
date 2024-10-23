using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class LocarImovelController : Controller
    {
        private readonly IImovelService imovelService;
        private readonly ILocacaoService locacaoService;
        private readonly IMapper mapper;

        public LocarImovelController(IImovelService imovelService, IMapper mapper, ILocacaoService locacaoService)
        {
            this.imovelService = imovelService;
            this.mapper = mapper;
            this.locacaoService = locacaoService;
        }

        [HttpGet]
        [Route("/LocarImovel/{id}")]
        public IActionResult LocarImovel(int id)
        {
            var imovel = imovelService.GetImovelDto((uint)id);
            if (imovel == null || imovel.Status != "Disponível")
            {
                return NotFound();
            }
            var locacaoViewModel = mapper.Map<LocacaoViewModel>(imovel);
            locacaoViewModel.NomeUsuario = "Usuário Teste";
            return View(locacaoViewModel);
        }

        [HttpPost]
        [Route("/LocarImovel/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult LocarImovel(uint id, LocacaoViewModel locacaoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var locacao = mapper.Map<Locacao>(locacaoViewModel);
                    locacaoService.Create(locacao);
                    return RedirectToAction("Index", "Home");
                }
                return View(locacaoViewModel);
            }
            catch (Exception e)
            {
                if (e.Message == "Imóvel já alugado")
                {
                    ModelState.AddModelError("IdImovel", "Imóvel já alugado");
                }
                return View(locacaoViewModel);
            }

        }
    }
}
