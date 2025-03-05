using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class SolicitacaoreparoService : ISolicitacaoreparoService
{

    private readonly HouseHubContext context;

    public SolicitacaoreparoService(HouseHubContext houseHubContext)
    {
        this.context = houseHubContext;
    }

    public async Task<Solicitacaoreparo> CriarSolicitacaoAsync(Solicitacaoreparo solicitacao)
    {
        context.Solicitacaoreparos.Add(solicitacao);
        await context.SaveChangesAsync();

        return solicitacao;
    }

    public async Task<IEnumerable<Solicitacaoreparo>> ObterSolicitacoesPorProprietarioAsync(uint idProprietario)
    {
        return context.Solicitacaoreparos.AsNoTracking();
    }

    public async Task<Solicitacaoreparo?> ObterSolicitacaoAsync(uint id)
    {
        var solicitacao = await context.Solicitacaoreparos.FindAsync(id);
        if (solicitacao == null) return null;

        return solicitacao;
    }

    public async Task<bool> AtualizarStatusAsync(uint id, string novoStatus, string respostaProprietario)
    {
        var entidade = await context.Solicitacaoreparos.FindAsync(id);
        if (entidade == null) return false;

        entidade.Status = novoStatus;
        entidade.RespostaProprietario = respostaProprietario;
        await context.SaveChangesAsync();

        return true;
    }
}
