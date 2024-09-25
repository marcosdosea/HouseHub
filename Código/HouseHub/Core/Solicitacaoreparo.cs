using System;
using System.Collections.Generic;

namespace Core;

public partial class Solicitacaoreparo
{
    public uint Id { get; set; }

    public string Descricao { get; set; } = null!;

    public decimal Valor { get; set; }

    /// <summary>
    /// 0 = Não, 1 = Sim
    /// </summary>
    public sbyte EnviarAlguem { get; set; }

    public string RespostaProprietario { get; set; } = null!;

    public DateTime Data { get; set; }

    public string Status { get; set; } = null!;

    public uint IdLocacao { get; set; }

    public virtual Locacao IdLocacaoNavigation { get; set; } = null!;

    public virtual ICollection<Imagem> Imagems { get; set; } = new List<Imagem>();
}
