
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
        public decimal ValorCondominio { get; set; }
        [DisplayName("Aluguel")]
        public decimal PrecoAluguel { get; set; }
        [DisplayName("Preço de Venda")]
        public decimal PrecoVenda { get; set; }

        public decimal Iptu { get; set; }

        public string Status { get; set; } = null!;


        /// <summary>
        /// 0 = Não, 1 = Sim
        /// </summary>
        [DisplayName("Pode Animal?")]
        public bool PodeAnimal { get; set; } = false;
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

        public string Modalidade { get; set; } = null!;
    }
}
