using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Xml.Linq;

namespace HouseHubWeb.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly IAvalicaoService avaliacaoService;
        private readonly IPessoaService pessoaService;
        private readonly IMapper mapper;

        public AvaliacaoController(IAvalicaoService avaliacaoService, IPessoaService pessoaService, IMapper mapper)
        {
            this.avaliacaoService = avaliacaoService;
            this.mapper = mapper;
            this.pessoaService = pessoaService;
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

                    var userName = User.Identity.GetUserName();
                    var pessoa = pessoaService.GetByEmail(userName);
                    avaliacao.IdPessoa = pessoaService.GetUserByEmail(userName);

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