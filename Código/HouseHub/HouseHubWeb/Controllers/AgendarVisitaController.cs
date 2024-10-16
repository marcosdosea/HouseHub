using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class AgendarVisitaController : Controller
    {
        private readonly IAgendamentoService agendamentoService;
        private readonly IMapper mapper;
        
        public AgendarVisitaController(IAgendamentoService agendamentoService, IMapper mapper)
        {
            this.agendamentoService = agendamentoService;
            this.mapper = mapper;
        }

        // GET: AgendarVisitaController/Create
        /// <summary>
        /// redireciona para tela de criar visita
        /// </summary>
        /// <param name="id">id de imovel</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            var agendamentoImovel = new AgendamentoViewModel();
            agendamentoImovel.Telefone = "(79) 99999-9999";
            agendamentoImovel.IdImovel = (uint)id;
            return View(agendamentoImovel);
        }
        
        // POST: AgendarVisitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AgendamentoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var agendamento = mapper.Map<Core.Agendamento>(model);
                    agendamento.DataCriacao = DateTime.Now;
                    agendamento.IdPessoa = 1; //TODO: Implementar a autenticação
                    agendamentoService.Create(agendamento);
                    return RedirectToAction(nameof(Index),nameof(HomeController));
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
