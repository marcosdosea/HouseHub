using System;
using System.Collections.Generic;

namespace Core;

public partial class Pagamento
{
    public uint Id { get; set; }

    public DateTime DataPagamento { get; set; }

    public string FormaPagamento { get; set; } = null!;

    /// <summary>
    /// 0 = Não, 1 = Sim
    /// </summary>
    public sbyte PagamentoManual { get; set; }

    public uint IdLocacao { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DataVencimento { get; set; }

    public virtual Locacao IdLocacaoNavigation { get; set; } = null!;
}
