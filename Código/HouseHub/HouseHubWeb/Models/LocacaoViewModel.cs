using Core;

namespace HouseHubWeb.Models
{
    public class LocacaoViewModel
    {
        public uint Id { get; set; }
        public decimal Iptu { get; set; }
        public decimal PrecoAluguel { get; set; }
        public decimal PrecoCondominio { get; set; }
        public uint IdImovel { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;

    }
}