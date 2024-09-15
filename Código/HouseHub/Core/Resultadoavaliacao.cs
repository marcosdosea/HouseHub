using System;
using System.Collections.Generic;

namespace Core;

public partial class Resultadoavaliacao
{
    public uint Id { get; set; }

    public string Descricao { get; set; } = null!;

    /// <summary>
    /// 0 - Não
    /// 1 - Sim
    /// </summary>
    public sbyte? Aprovado { get; set; }

    public DateTime? DataFinalizado { get; set; }

    public virtual ICollection<Avaliacao> Avaliacaos { get; set; } = new List<Avaliacao>();
}
