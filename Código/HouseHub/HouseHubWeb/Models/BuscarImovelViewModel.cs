namespace HouseHubWeb.Models
{
    public class BuscarImovelViewModel
    {
        public string? Cidade { get; set; }
        public string? Bairro { get; set; }
        public decimal? ValorMaximo { get; set; }
        public byte? Quartos { get; set; }
        public string? Modalidade { get; set; }
    }
}
