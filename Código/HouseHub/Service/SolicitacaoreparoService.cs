using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
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
            return await context.Solicitacaoreparos
                .AsNoTracking()
                .Include(s => s.IdLocacaoNavigation)
                .Include(s => s.IdLocacaoNavigation.IdImovelNavigation)
                .Where(solicitacao => solicitacao.IdLocacaoNavigation.IdImovelNavigation.IdPessoa == idProprietario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Solicitacaoreparo>> ObterSolicitacoesPorLocacaoAsync(uint idLocacao)
        {
            return await context.Solicitacaoreparos
                .AsNoTracking()
                .Where(solicitacao => solicitacao.IdLocacao == idLocacao)
                .ToListAsync();
        }

        public async Task<Solicitacaoreparo?> ObterSolicitacaoAsync(uint idSolicitacao, uint idPessoa)
        {
            var solicitacao = await context.Solicitacaoreparos
                .AsNoTracking()
                .Include(s => s.IdLocacaoNavigation)
                .Include(s => s.IdLocacaoNavigation.IdImovelNavigation)
                .Where(s => s.Id == idSolicitacao)
                .FirstOrDefaultAsync();

            if (solicitacao == null)
                return null;

            // Verificamos se a pessoa é o proprietário do imóvel ou o locatário
            var temPermissao = solicitacao.IdLocacaoNavigation.IdImovelNavigation.IdPessoa == idPessoa ||
                               solicitacao.IdLocacaoNavigation.IdPessoa == idPessoa;

            return temPermissao ? solicitacao : null;
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

        public async Task<bool> VerificaSeEhProprietario(uint idLocacao, uint idProprietario)
        {
            var solicitacao = await context.Solicitacaoreparos
                .AsNoTracking()
                .Include(s => s.IdLocacaoNavigation)
                .Include(s => s.IdLocacaoNavigation.IdImovelNavigation)
                .FirstOrDefaultAsync(s => s.IdLocacao == idLocacao);

            if (solicitacao == null)
                return false;

            return solicitacao.IdLocacaoNavigation.IdImovelNavigation.IdPessoa == idProprietario;
        }
    }
}