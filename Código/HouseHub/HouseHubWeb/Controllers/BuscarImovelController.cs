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
        public IActionResult Index()
        {
            return View("BuscarImovel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BuscarImovelViewModel buscarImovelViewModel)
        {
            var buscarImovelDto = mapper.Map<BuscarImovelDto>(buscarImovelViewModel);
            var imoveis = imovelService.GetAll(buscarImovelDto).Take(50).ToList();
            var imoveisViewModel = mapper.Map<List<ImovelViewModel>>(imoveis);
            return View(imoveisViewModel);
        }

    }
}
