using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service;
public interface ISolicitacaoreparoService
{
    Task<Solicitacaoreparo> CriarSolicitacaoAsync(Solicitacaoreparo solicitacao);
    Task<IEnumerable<Solicitacaoreparo>> ObterSolicitacoesPorProprietarioAsync(uint idProprietario);
    Task<IEnumerable<Solicitacaoreparo>> ObterSolicitacoesPorLocacaoAsync(uint idLocacao);
    Task<Solicitacaoreparo?> ObterSolicitacaoAsync(uint idSolicitacao, uint idPessoa);
    Task<bool> AtualizarStatusAsync(uint id, string novoStatus, string respostaProprietario);
    Task<bool> VerificaSeEhProprietario(uint idLocacao, uint idProprietario);
}
