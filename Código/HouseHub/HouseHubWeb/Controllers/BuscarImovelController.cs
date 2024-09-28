using AutoMapper;
using Core.DTOs;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class BuscarImovelController : Controller
    {
        private readonly IImovelService imovelService;
        private readonly IMapper mapper;

        public BuscarImovelController(IImovelService imovelService, IMapper mapper)
        {
            this.imovelService = imovelService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("/BuscarImovel")]
        public IActionResult BuscarImovel()
        {
            return View();
        }

        [HttpPost]
        [Route("/BuscarImovel")]
        [ValidateAntiForgeryToken]
        public IActionResult BuscarImovel(BuscarImovelViewModel buscarImovelViewModel)
        {
            var buscarImovelDto = mapper.Map<BuscarImovelDto>(buscarImovelViewModel);
            return RedirectToAction("Index", "BuscarImovel", buscarImovelDto);
        }

        [Route("/BuscarImovel/Index")]
        public IActionResult Index(BuscarImovelDto buscarImovelDto)
        {
            var imoveis = imovelService.GetAll(buscarImovelDto);
            var imoveisViewModel = mapper.Map<IEnumerable<ImovelViewModel>>(imoveis);
            return View(imoveisViewModel);
        }


    }
}
