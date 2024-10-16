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

        public LocarImovelController(IImovelService imovelService,  IMapper mapper, ILocacaoService locacaoService)
        {
            this.imovelService = imovelService;
            this.mapper = mapper;
            this.locacaoService = locacaoService;
        }

        [HttpGet]
        [Route("/LocarImovel/{idImovel}")]
        public IActionResult LocarImovel(uint idImovel)
        {
            var imovel = imovelService.GetImovelDto(idImovel);
            if(imovel == null || imovel.Status != "Disponível")
            {
                return NotFound();
            }
            var locacaoViewModel = mapper.Map<LocacaoViewModel>(imovel);
            locacaoViewModel.NomeUsuario = "Usuário Teste";
            return View(locacaoViewModel);
        }

        [HttpPost]
        [Route("/LocarImovel/{idImovel}")]
        [ValidateAntiForgeryToken]
        public IActionResult LocarImovel(uint idImovel, LocacaoViewModel locacaoViewModel)
        {
            Locacao locacao = new Locacao
            {
                DataInicio = DateTime.Now,
                DataContrato = DateTime.Now,
                DataFim = DateTime.Now.AddMonths(1),
                DataVencimento = DateTime.Now.AddDays(5),
                IdImovel = idImovel,
                IdPessoa = 1,
                Status = "Inativo",
                Valor = 99990000,
            };
            if(locacaoService.Create(locacao) > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
