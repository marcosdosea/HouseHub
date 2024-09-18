using System;
using System.Collections.Generic;

namespace Core;

public partial class Locacao
{
    public uint Id { get; set; }

    public DateTime DataContrato { get; set; }

    public DateTime DataVencimento { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime? DataFim { get; set; }

    public decimal Valor { get; set; }

    public string Status { get; set; } = null!;

    public uint IdImovel { get; set; }

    public uint IdPessoa { get; set; }

    public virtual Imovel IdImovelNavigation { get; set; } = null!;

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

    public virtual ICollection<Solicitacaoreparo> Solicitacaoreparos { get; set; } = new List<Solicitacaoreparo>();
}
