using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseHubWeb.Controllers
{
    public class PessoaController : Controller
    {
        private readonly IPessoaService pessoaService;
        private readonly IMapper mapper;
        public PessoaController(IPessoaService pessoaService, IMapper _mapper)
        {
            this.pessoaService = pessoaService;
            mapper = _mapper;
        }

        // GET: PessoaController
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PessoaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pessoa = mapper.Map<Core.Pessoa>(model);
                    pessoaService.Create(pessoa);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: PessoaController/Update
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: PessoaController/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PessoaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pessoa = mapper.Map<Core.Pessoa>(model);
                    pessoaService.Update(pessoa);
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var pessoa = pessoaService.Get((uint)id);
            var model = mapper.Map<PessoaViewModel>(pessoa);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, PessoaViewModel model)
        {
            try
            {
                pessoaService.Delete((uint)id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            var pessoa = pessoaService.Get((uint)id);
            var model = mapper.Map<PessoaViewModel>(pessoa);
            return View(model);
        }
    }
}
