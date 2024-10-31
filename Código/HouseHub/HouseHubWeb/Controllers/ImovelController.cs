using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HouseHubWeb.Controllers
{
    [Authorize]
    public class ImovelController : Controller
    {

        private readonly IImovelService imovelService;
        private readonly IPessoaService pessoaService;
        private readonly IMapper mapper;

        public ImovelController(IImovelService imovelService, IMapper mapper, IPessoaService pessoaService)
        {
            this.mapper = mapper;
            this.imovelService = imovelService;
            this.pessoaService = pessoaService;
        }


        // GET: ImovelController
        public ActionResult Index()
        {
            var imoveis = imovelService.GetAll().ToList();
            var model = mapper.Map<List<ImovelViewModel>>(imoveis);
            return View(model);
        }

        // GET: ImovelController/Details/5
        public ActionResult Details(int id)
        {
            var imovel = imovelService.Get((uint)id);
            var model = mapper.Map<ImovelViewModel>(imovel);
            return View(model);
        }

        // GET: ImovelController/Create
        public ActionResult Create()
        {
            var model = new ImovelViewModel();

            return View(model);
        }

        // POST: ImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ImovelViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var imovel = mapper.Map<Core.Imovel>(model);
                    string modalidade = model.ModalidadeVender ?
                        "Venda" : model.ModalidadeAluguel ? "Aluguel" : "Ambos";
                    imovel.Modalidade = modalidade;

                    if (User.Identity != null && User.Identity.IsAuthenticated)
                    {
                        string name = User.Identity.GetUserName();
                        uint id = pessoaService.GetUserByEmail(name);
                        imovel.IdPessoa = id;
                    }
                    
                    imovelService.Create(imovel);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch(Exception)
            {
                
                return View();
            }
        }

        // GET: ImovelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImovelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ImovelViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var imovel = mapper.Map<Core.Imovel>(model);
                    imovelService.Update(imovel);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: ImovelController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var imovel = imovelService.Get((uint)id);
            var model = mapper.Map<ImovelViewModel>(imovel);
            return View(model);
        }

        // POST: ImovelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection imovel)
        {
            try
            {
                imovelService.Delete((uint)id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(imovel);
            }
        }
    }
}
