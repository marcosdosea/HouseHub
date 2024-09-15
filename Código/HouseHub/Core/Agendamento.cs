using System;
using System.Collections.Generic;

namespace Core;

public partial class Agendamento
{
    public uint Id { get; set; }

    public string? Observacoes { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataVisita { get; set; }

    public string Status { get; set; } = null!;

    public uint IdImovel { get; set; }

    public uint IdPessoa { get; set; }

    public virtual Imovel IdImovelNavigation { get; set; } = null!;

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;
}
