namespace HouseHubWeb.Models
{
    public class BuscarImovelViewModel
    {
        public string Cidade { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public decimal ValorMaximo { get; set; }
        public byte Quartos { get; set; }
    }
}
