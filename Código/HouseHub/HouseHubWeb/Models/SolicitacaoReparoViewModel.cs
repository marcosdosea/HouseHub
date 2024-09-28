using Core;

namespace HouseHubWeb.Models
{
    public class SolicitacaoReparoViewModel
    {
        public uint Id { get; set; }

        public string Descricao { get; set; } = null!;

        public decimal Valor { get; set; }

        public bool EnviarAlguem { get; set; }

        public DateTime Data { get; set; }

        public string Status { get; set; } = null!;

        public uint IdLocacao { get; set; }


    }
}
