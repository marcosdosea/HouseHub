using System;
using System.Collections.Generic;

namespace Core;

public partial class Imovel
{
    public uint Id { get; set; }

    public string Descricao { get; set; } = null!;

    public byte Quartos { get; set; }

    public byte Banheiros { get; set; }

    public decimal? PrecoAluguel { get; set; }

    public decimal PrecoVenda { get; set; }

    public decimal Iptu { get; set; }

    public string Status { get; set; } = null!;

    public decimal? PrecoCondominio { get; set; }

    /// <summary>
    /// 0 = Não, 1 = Sim
    /// </summary>
    public sbyte PodeAnimal { get; set; }

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

    public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;

    public virtual ICollection<Locacao> Locacaos { get; set; } = new List<Locacao>();

    public virtual ICollection<Resultadoavaliacao> Resultadoavaliacaos { get; set; } = new List<Resultadoavaliacao>();

    public virtual ICollection<Imagem> Imagems { get; set; } = new List<Imagem>();
}
