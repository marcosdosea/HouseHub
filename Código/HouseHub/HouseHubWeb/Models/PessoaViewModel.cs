using System.ComponentModel.DataAnnotations;

namespace HouseHubWeb.Models
{
    public class PessoaViewModel
    {
        [Key]
        public uint Id { get; set; }
        [Required (ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo CPF deve ter 11 caracteres")]
        public string Cpf { get; set; } = null!;

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Telefone é obrigatório")]
        [Phone]
        public string Telefone { get; set; } = null!;

        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo Bairro deve ter no máximo 100 caracteres")]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório")]
        public string? Numero { get; set; }

        [StringLength(100, ErrorMessage = "O campo Complemento deve ter no máximo 100 caracteres")]
        public string? Complemento { get; set; }
    }
}
