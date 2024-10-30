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

    public uint AvaliacaoId { get; set; }

    public uint ImovelId { get; set; }

    public virtual Avaliacao Avaliacao { get; set; } = null!;

    public virtual Imovel Imovel { get; set; } = null!;
}
