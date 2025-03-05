using Core;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HouseHubWeb.Models
{
    public class SolicitacaoReparoViewModel
    {
        [Key]
        [DisplayName("Código")]
        public uint Id { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        [MaxLength(100)]
        public string Descricao { get; set; } = null!;

        [MaxLength(100)]
        public string RespostaProprietario { get; set; } = null!;

        [Required(ErrorMessage = "O {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "É obrigatória a informação da necessidade de algum profissional")]
        public bool EnviarAlguem { get; set; }

        [Required(ErrorMessage = "A {0} é obrigatória")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O {0} é obrigatório")]
        public string Status { get; set; } = null!;

        public uint IdLocacao { get; set; }
    }
}
