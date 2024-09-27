using Org.BouncyCastle.Bcpg.OpenPgp;

namespace HouseHubWeb.Models
{
    public class AgendamentoViewModel
    {
        public uint Id { get; set; }
        public string Observacoes { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataVisita { get; set; }
        public string Status { get; set; }
        public uint IdImovel { get; set; }
        public uint IdPessoa { get; set; }

        public string Telefone { get; set; } = string.Empty;

    }
}
