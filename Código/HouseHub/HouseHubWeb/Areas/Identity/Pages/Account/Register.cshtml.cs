
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HouseHubWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Core.Service;
using Core;

namespace HouseHubWeb.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly IUserStore<UsuarioIdentity> _userStore;
        private readonly IUserEmailStore<UsuarioIdentity> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IPessoaService _pessoaService;


        public RegisterModel(
            UserManager<UsuarioIdentity> userManager,
            IUserStore<UsuarioIdentity> userStore,
            SignInManager<UsuarioIdentity> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IPessoaService pessoaService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _pessoaService = pessoaService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo Email é obrigatório")]
            [EmailAddress(ErrorMessage = "Email inválido")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "O campo Senha é obrigatório")]
            [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "A senha e a confirmação de senha não conferem.")]
            public string ConfirmPassword { get; set; }

            // Dados da entidade Pessoa
            [Required(ErrorMessage = "O campo Nome é obrigatório")]
            [StringLength(100, ErrorMessage = "O nome deve ter no máximo {1} caracteres")]
            [Display(Name = "Nome completo")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O campo CPF é obrigatório")]
            [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato: 000.000.000-00")]
            [Display(Name = "CPF")]
            public string Cpf { get; set; }

            [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
            [DataType(DataType.Date)]
            [Display(Name = "Data de Nascimento")]
            public DateTime DataNascimento { get; set; }

            [Required(ErrorMessage = "O campo Telefone é obrigatório")]
            [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "Telefone inválido. Use o formato: (00) 00000-0000")]
            [Display(Name = "Telefone")]
            public string Telefone { get; set; }

            // Endereço (campos opcionais)
            [Display(Name = "CEP")]
            [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido. Use o formato: 00000-000")]
            public string Cep { get; set; }

            [Display(Name = "Estado")]
            [StringLength(2, ErrorMessage = "Use a sigla do estado (2 letras)")]
            public string Estado { get; set; }

            [Display(Name = "Cidade")]
            [StringLength(100)]
            public string Cidade { get; set; }

            [Display(Name = "Bairro")]
            [StringLength(100)]
            public string Bairro { get; set; }

            [Display(Name = "Logradouro")]
            [StringLength(200)]
            public string Logradouro { get; set; }

            [Display(Name = "Número")]
            [StringLength(10)]
            public string Numero { get; set; }

            [Display(Name = "Complemento")]
            [StringLength(100)]
            public string Complemento { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    string cpfSemFormatacao = Input.Cpf?.Replace(".", "").Replace("-", "");
                    string cepSemFormatacao = Input.Cep?.Replace("-", "");


                    // Criar e salvar a entidade Pessoa
                    Pessoa pessoa = new Pessoa
                    {
                        Nome = Input.Nome,
                        Cpf = cpfSemFormatacao,
                        DataNascimento = Input.DataNascimento,
                        Telefone = Input.Telefone,
                        Email = Input.Email,
                        Cep =  cepSemFormatacao,
                        Estado = Input.Estado,
                        Cidade = Input.Cidade,
                        Bairro = Input.Bairro,
                        Logradouro = Input.Logradouro,
                        Numero = Input.Numero,
                        Complemento = Input.Complemento
                    };

                    // Salvar a entidade Pessoa
                    _pessoaService.Create(pessoa);

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirme seu email",
                    //    $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private UsuarioIdentity CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UsuarioIdentity>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UsuarioIdentity)}'. " +
                    $"Ensure that '{nameof(UsuarioIdentity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<UsuarioIdentity> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UsuarioIdentity>)_userStore;
        }
    }
}

