using System;
using System.Collections.Generic;

namespace Core;

public partial class Avaliacao
{
    public uint Id { get; set; }

    public decimal ValorAprovado { get; set; }

    public string Documento { get; set; } = null!;

    public decimal RendaMensal { get; set; }

    public ushort NumeroDependentes { get; set; }

    public ushort ScoreSerasa { get; set; }

    public string Status { get; set; } = null!;

    public uint IdPessoa { get; set; }

    public uint ResultadoAvaliacaoId { get; set; }

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;

    public virtual Resultadoavaliacao ResultadoAvaliacao { get; set; } = null!;
}
