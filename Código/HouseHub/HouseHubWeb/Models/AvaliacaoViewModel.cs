using System.ComponentModel.DataAnnotations;

namespace HouseHubWeb.Models
{
    public class AvaliacaoViewModel
    {

        public decimal RendaMensal { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Rua { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IFormFile ComprovanteRenda { get; set; }
    }
}