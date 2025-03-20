using AutoMapper;
using Core.Service;
using HouseHubWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HouseHubWeb.Controllers
{
    [Authorize]
    public class ImovelController : Controller
    {

        private readonly IImovelService imovelService;
        private readonly IPessoaService pessoaService;
        private readonly IImagemService imagemService;
        private readonly IFileStorageService fileStorageService;
        private readonly IMapper mapper;

        public ImovelController(IImovelService imovelService, IPessoaService pessoaService, IImagemService imagemService, IFileStorageService fileStorageService, IMapper mapper)
        {
            this.imovelService = imovelService;
            this.pessoaService = pessoaService;
            this.imagemService = imagemService;
            this.fileStorageService = fileStorageService;
            this.mapper = mapper;
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
            ViewBag.Tipos = new List<SelectListItem>
            {
                new SelectListItem { Text = "Casa", Value = "Casa" },
                new SelectListItem { Text = "Apartamento", Value = "Apartamento" }
            };
            return View();
        }

        // POST: ImovelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImovelViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.PodeAnimal = (byte)(model.PodeAnimalBool ? 1 : 0);
                    var imovel = mapper.Map<Core.Imovel>(model);
                    string modalidade = model.ModalidadeVender ?
                        "Venda" : model.ModalidadeAluguel ? "Aluguel" : "Ambos";
                    imovel.Modalidade = modalidade;
                    imovel.Status = "Disponivel";

                    if (User.Identity != null && User.Identity.IsAuthenticated)
                    {
                        string name = User.Identity.GetUserName();
                        uint id = pessoaService.GetUserByEmail(name);
                        imovel.IdPessoa = id;
                    }

                    // Salvar o imóvel para obter um ID
                    uint imovelId = imovelService.Create(imovel);

                    // Processar imagens se existirem
                    if (model.ImageFiles != null && model.ImageFiles.Count > 0)
                    {
                        // Salvar os arquivos fisicamente
                        List<string> imageUrls = await fileStorageService.SaveImagesAsync(model.ImageFiles);

                        // Salvar as URLs no banco de dados e associar ao imóvel
                        foreach (var url in imageUrls)
                        {
                            var imagem = new Core.Imagem
                            {
                                Url = url
                            };

                            // Salvar a imagem no banco
                            uint imagemId = await imagemService.CreateAsync(imagem);

                            // Associar a imagem ao imóvel
                            await imagemService.AssociarImagemAoImovelAsync(imovelId, imagemId);
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Tipos = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Casa", Value = "Casa" },
                    new SelectListItem { Text = "Apartamento", Value = "Apartamento" }
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao tentar criar o imóvel: " + ex.Message);
                ViewBag.Tipos = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Casa", Value = "Casa" },
                    new SelectListItem { Text = "Apartamento", Value = "Apartamento" }
                };
                return View(model);
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
