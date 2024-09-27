using System;
using System.Collections.Generic;

namespace Core;

public partial class Imagem
{
    public uint Id { get; set; }

    public string Url { get; set; } = null!;

    public virtual ICollection<Imovel> Imovels { get; set; } = new List<Imovel>();

    public virtual ICollection<Solicitacaoreparo> SolicitacaoReparos { get; set; } = new List<Solicitacaoreparo>();
}
