using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace HouseHubWeb.Controllers
{
    public class BuscarImovelController : Controller
    {
        private readonly IImovelService imovelService;

        public BuscarImovelController(IImovelService imovelService)
        {
            this.imovelService = imovelService;
        }
        public IActionResult BuscarImovel(IImovelService imovelService)
        {
            return View();
        }
    }
}
