using HouseHubWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class AvaliacaoController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AvaliacaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}