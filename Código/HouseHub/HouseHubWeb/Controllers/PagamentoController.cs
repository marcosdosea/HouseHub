using AutoMapper;
using Core;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class PagamentoController : Controller
    {
        private readonly IPagamentoService pagamentoService;
        private readonly IMapper mapper;

        public PagamentoController(IPagamentoService pagamentoService, IMapper mapper)
        {
            this.pagamentoService = pagamentoService;
            this.mapper = mapper;
        }

        public IActionResult Inserir()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inserir(PagamentoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pagamento = mapper.Map<Pagamento>(model);
                    pagamento.DataPagamento = DateTime.Now;
                    pagamento.Status = "Pago";
                    pagamentoService.Create(pagamento);
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}
