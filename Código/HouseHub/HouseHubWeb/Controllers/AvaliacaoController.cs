using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace HouseHubWeb.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly IAvalicaoService avaliacaoService;
        private readonly IMapper mapper;

        public AvaliacaoController(IAvalicaoService avaliacaoService, IMapper mapper)
        {
            this.avaliacaoService = avaliacaoService;
            this.mapper = mapper;
        }

       
        // GET: ImovelController/Create
        public ActionResult Create()
        {
            var model = new AvaliacaoViewModel();

            return View(model);
        }

        // POST: ImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AvaliacaoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var avaliacao = mapper.Map<Core.Avaliacao>(model);                 
                    avaliacaoService.Create(avaliacao);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}