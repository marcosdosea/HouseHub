using Core.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace HouseHubWeb.Controllers
{
    [Authorize]
    public class MeusImoveisController : Controller
    {

        private readonly IImovelService imovelService;
        private readonly IPessoaService pessoaService;

        public MeusImoveisController(IImovelService imovelService, IPessoaService pessoaService)
        {
            this.imovelService = imovelService;
            this.pessoaService = pessoaService;
        }

        public IActionResult Index()
        {
            uint idPessoa = pessoaService.GetUserByEmail(User.Identity.GetUserName());
            var imoveis = imovelService.GetMeusImoveis(idPessoa).ToList();
            return View(imoveis);
        }
    }
}
