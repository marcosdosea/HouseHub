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




        // GET: AgendarVisitaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            agendamentoImovel.Telefone = "99999999";
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

        // GET: AgendarVisitaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgendarVisitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendarVisitaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgendarVisitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
