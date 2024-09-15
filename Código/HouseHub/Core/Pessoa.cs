using System;
using System.Collections.Generic;

namespace Core;

public partial class Pessoa
{
    public uint Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public string Telefone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bairro { get; set; }

    public string? Estado { get; set; }

    public string? Cidade { get; set; }

    public string? Logradouro { get; set; }

    public string? Cep { get; set; }

    public string? Numero { get; set; }

    public string? Complemento { get; set; }

    public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();

    public virtual ICollection<Imovel> Imovels { get; set; } = new List<Imovel>();

    public virtual ICollection<Locacao> Locacaos { get; set; } = new List<Locacao>();
}
