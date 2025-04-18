﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HouseHubWeb.Models
{
    public class ImovelViewModel
    {
        public uint Id { get; set; }

        [MaxLength(100)]
        public string Descricao { get; set; } = null!;

        public byte Quartos { get; set; }
        public byte Banheiros { get; set; }

        [DisplayName("Condomínio")]
        public decimal? PrecoCondominio { get; set; }

        [DisplayName("Aluguel")]
        public decimal? PrecoAluguel { get; set; }

        [DisplayName("Preço de Venda")]
        public decimal? PrecoVenda { get; set; }

        public decimal Iptu { get; set; }
        public string Status { get; set; } = string.Empty;

        [DisplayName("Pode Animal?")]
        public bool PodeAnimalBool { get; set; } = false;

        public byte PodeAnimal { get; set; } = 0;

        [Required(ErrorMessage = "Selecione uma modalidade de venda")]
        public bool IsModalidadeSelected =>
            ModalidadeAluguel || ModalidadeVender || ModalidadeAmbos;

        /// <summary>
        /// Casa, Apartamento
        /// </summary>
        public string Tipo { get; set; } = null!;

        public uint IdPessoa { get; set; }
        public string Bairro { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Cep { get; set; } = null!;
        public string Logradouro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string? Complemento { get; set; }

        public string? Modalidade { get; set; } = string.Empty;

        public bool ModalidadeAmbos { get; set; }
        public bool ModalidadeVender { get; set; }
        public bool ModalidadeAluguel { get; set; }

        [DisplayName("Imagens")]
        public List<IFormFile>? ImageFiles { get; set; }
        public string ImagemPrincipalUrl { get; set; } = string.Empty;
        // Opcional: para exibição de imagens existentes
        public List<string>? ExistingImageUrls { get; set; }
        public PessoaViewModel? IdPessoaNavigation { get; set; }


        public List<SelectListItem> Tipos { get; set; } = new List<SelectListItem>{
            new SelectListItem("Casa", "Casa"),
            new SelectListItem("Apartamento", "Apartamento")
        };
    }
}
