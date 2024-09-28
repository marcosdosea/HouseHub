using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: SolicitacaoReparoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SolicitacaoReparoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitacaoReparoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SolicitacaoReparoViewModel solicitacaoReparo)
        {
            try
            {
                var solicitacao = mapper.Map<Solicitacaoreparo>(solicitacaoReparo);
                solicitacaoreparoService.Create(solicitacao);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
